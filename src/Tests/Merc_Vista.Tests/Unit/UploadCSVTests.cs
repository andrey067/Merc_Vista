using Application.Commands;
using Domain;
using MediatR;
using Merc_Vista.Tests.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Controllers;

namespace Merc_Vista.Tests.Unit
{
    public class UploadCSVTests
    {
        private readonly AcaoFixture _fixture = new();
        private readonly ErrorFixture _errorFixture = new();
        private readonly ResultFixture<List<Acao>> _resultFixture = new();

        [Fact]
        internal async Task UploadCSV_ReturnsOk_WhenCommandSucceeds()
        {
            // Arrange
            var mockSender = new Mock<ISender>();
            var commandResult = _resultFixture.SuccessFixture(_fixture.GetList());
            mockSender.Setup(x => x.Send(It.IsAny<UploadFilesCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResult);
            var controller = new UploadFileController(mockSender.Object);

            // Act
            var result = await controller.UploadCSV("some/path") as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(commandResult.Data, result.Value);
        }

        [Fact]
        internal async Task UploadCSV_ReturnsBadRequest_WhenCommandFails()
        {
            // Arrange
            var mockSender = new Mock<ISender>();
            var commandResult = _resultFixture.FailureFixture(_errorFixture.NotFoundDiretory());
            mockSender.Setup(x => x.Send(It.IsAny<UploadFilesCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResult);
            var controller = new UploadFileController(mockSender.Object);

            // Act
            var result = await controller.UploadCSV("some/path") as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal(commandResult.Errors, result.Value);
        }
    }
}
