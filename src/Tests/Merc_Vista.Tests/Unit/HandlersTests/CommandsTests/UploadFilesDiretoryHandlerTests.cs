using Application.Commands;
using Application.Handlers.Commands;
using Application.Interfaces;
using Domain;
using Domain.Dtos;
using Domain.Interfaces;
using Domain.Shared;
using MediatR;
using Merc_Vista.Tests.Fixtures;
using Moq;

namespace Merc_Vista.Tests.Unit.HandlersTests.CommandsTests
{
    public class UploadFilesDiretoryHandlerTests
    {
        private readonly Mock<ICsvService> _csvServiceMock;
        private readonly Mock<IAcaoRepository> _acaoRepositoryMock;
        private readonly IRequestHandler<UploadFilesDiretoryCommand, Result> _handler;
        private readonly PathFixture _pathFixture;

        public UploadFilesDiretoryHandlerTests()
        {
            _csvServiceMock = new Mock<ICsvService>();
            _acaoRepositoryMock = new Mock<IAcaoRepository>();
            _handler = new UploadFileDirectoryHandler(_acaoRepositoryMock.Object, _csvServiceMock.Object);
            _pathFixture = new PathFixture();
        }

        [Fact]
        public async Task Handle_Should_Return_Error_When_Invalid_Path()
        {
            // Arrange
            var invalidPath = "invalidPath";
            var command = new UploadFilesDiretoryCommand(invalidPath);
            var expectedResult = Result<Task>.Failure(new Error("Caminho não encontrato", "Fornecessa um diretorio valido"));

            _csvServiceMock.Setup(x => x.ReadFormFileAync(It.IsAny<string[]>())).ReturnsAsync(new List<CsvDto>());

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedResult.IsSuccess, result.IsSuccess);
            Assert.Equal(expectedResult.Errors.First().error, result.Errors.First().error);
            Assert.Equal(expectedResult.Errors.First().errorName, result.Errors.First().errorName);

            _acaoRepositoryMock.Verify(x => x.InsertRangeAsync(It.IsAny<List<Acao>>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Should_Return_Error_When_No_Files_With_Extension_CSV()
        {
            // Arrange
            var path = _pathFixture.PathEmpity();
            var command = new UploadFilesDiretoryCommand(path);
            var expectedResult = Result<Task>.Failure(new Error("Não existem arquivos com a extensão .csv", "Fornecessa um diretorio valido"));

            _csvServiceMock.Setup(x => x.ReadFormFileAync(It.IsAny<string[]>())).ReturnsAsync(new List<CsvDto>());
            _acaoRepositoryMock.Setup(x => x.InsertRangeAsync(It.IsAny<List<Acao>>())).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            _pathFixture.Dispose();
            // Assert
            Assert.Equal(expectedResult.IsSuccess, result.IsSuccess);
            Assert.Equal(expectedResult.Errors.First().error, result.Errors.First().error);
            Assert.Equal(expectedResult.Errors.First().errorName, result.Errors.First().errorName);

            _csvServiceMock.Verify(x => x.ReadFormFileAync(It.IsAny<string[]>()), Times.Never);
            _acaoRepositoryMock.Verify(x => x.InsertRangeAsync(It.IsAny<List<Acao>>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Insert_All_CsvDto_In_Database()
        {
            // Arrange
            var (path, files) = _pathFixture.PathWithFile();
            var command = new UploadFilesDiretoryCommand(path);
            _csvServiceMock.Setup(x => x.ReadFormFileAync(files)).ReturnsAsync(new List<CsvDto>());
            _acaoRepositoryMock.Setup(x => x.InsertRangeAsync(It.IsAny<List<Acao>>())).Returns(Task.CompletedTask);
            var handler = new UploadFileDirectoryHandler(_acaoRepositoryMock.Object, _csvServiceMock.Object);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            _csvServiceMock.Verify(x => x.ReadFormFileAync(It.IsAny<string[]>()), Times.Once);
            _acaoRepositoryMock.Verify(x => x.InsertRangeAsync(It.IsAny<List<Acao>>()), Times.Once);
        }
    }
}
