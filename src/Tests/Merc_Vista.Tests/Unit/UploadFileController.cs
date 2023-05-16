using Application.Commands;
using Domain;
using MediatR;
using Merc_Vista.Tests.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Text;

namespace Merc_Vista.Tests.Unit
{
    public class UploadFileController
    {
        private readonly ErrorFixture _errorFixture = new();
        private readonly ResultFixture<List<Acao>> _resultFixture = new();

        [Fact]
        internal async Task UploadCSVUploadFilesDiretory_ReturnsOk_WhenCommandSucceeds()
        {
            // Arrange
            var mockSender = new Mock<ISender>();
            var commandResult = _resultFixture.SuccessFixture();
            mockSender.Setup(x => x.Send(It.IsAny<UploadFilesDiretoryCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResult);
            var controller = new Presentation.Controllers.UploadFileController(mockSender.Object);

            // Act
            var result = await controller.UploadCSVDiretory("some/path");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            //var okResult = (OkObjectResult)result;
            //Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        internal async Task UploadCSVUploadFilesDiretory_ReturnsBadRequest_WhenCommandFails()
        {
            // Arrange
            var mockSender = new Mock<ISender>();
            var commandResult = _resultFixture.FailureFixture(_errorFixture.NotFoundDiretory());
            mockSender.Setup(x => x.Send(It.IsAny<UploadFilesDiretoryCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResult);
            var controller = new Presentation.Controllers.UploadFileController(mockSender.Object);

            // Act
            var result = await controller.UploadCSVDiretory("some/path");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var okResult = (BadRequestObjectResult)result;
            Assert.Equal(400, okResult.StatusCode);
            Assert.Equal(commandResult.Errors, okResult.Value);
        }

        [Fact]
        internal async Task UploadCSVUploadFileCommand_ReturnsOk_WhenCommandSucceeds()
        {
            // Arrange
            var mockSender = new Mock<ISender>();
            var formFile = new FormFileFixture("teste.csv", "text/csv", Encoding.UTF8.GetBytes(CsvContentFixture.CsvFile()));
            var commandResult = _resultFixture.SuccessFixture();
            mockSender.Setup(x => x.Send(It.IsAny<UploadFileCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResult);
            var controller = new Presentation.Controllers.UploadFileController(mockSender.Object);

            // Act
            var result = await controller.UploadCSVFileStream(formFile);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        internal async Task UploadCSVUploadFileCommand_ReturnsBadRequest_WhenCommandFails()
        {
            // Arrange
            var mockSender = new Mock<ISender>();
            var formFile = new FormFileFixture("teste.csv", "text/csv", Encoding.UTF8.GetBytes(CsvContentFixture.CsvFile()));
            var commandResult = _resultFixture.FailureFixture(_errorFixture.NotFoundDiretory());
            mockSender.Setup(x => x.Send(It.IsAny<UploadFileCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResult);
            var controller = new Presentation.Controllers.UploadFileController(mockSender.Object);

            // Act
            var result = await controller.UploadCSVFileStream(formFile);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var okResult = (BadRequestObjectResult)result;
            Assert.Equal(400, okResult.StatusCode);
            Assert.Equal(commandResult.Errors, okResult.Value);
        }
    }
}
