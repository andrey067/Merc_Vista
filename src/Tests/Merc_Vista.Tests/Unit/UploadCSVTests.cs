using Application.Commands;
using Domain;
using Domain.Shared;
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
            mockSender.Setup(x => x.Send(It.IsAny<UploadFilesDiretoryCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResult);
            var controller = new UploadFileController(mockSender.Object);

            // Act
            var result = await controller.UploadCSVDiretory("some/path");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(commandResult.Data, okResult.Value);
        }

        [Fact]
        internal async Task UploadCSV_ReturnsBadRequest_WhenCommandFails()
        {
            // Arrange
            var mockSender = new Mock<ISender>();
            var commandResult = _resultFixture.FailureFixture(_errorFixture.NotFoundDiretory());
            mockSender.Setup(x => x.Send(It.IsAny<UploadFilesDiretoryCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResult);
            var controller = new UploadFileController(mockSender.Object);

            // Act
            var result = await controller.UploadCSVDiretory("some/path");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var okResult = (BadRequestObjectResult)result;
            Assert.Equal(400, okResult.StatusCode);
            Assert.Equal(commandResult.Errors, okResult.Value);
        }
    }
}
