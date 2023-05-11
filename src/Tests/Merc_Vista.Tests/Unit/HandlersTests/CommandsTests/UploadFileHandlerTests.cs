using Application.Commands;
using Application.Handlers.Commands;
using Application.Interfaces;
using Domain;
using Domain.Dtos;
using Domain.Interfaces;
using Domain.Shared;
using Merc_Vista.Tests.Fixtures;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Merc_Vista.Tests.Unit.HandlersTests.CommandsTests
{
    public class UploadFileHandlerTests
    {
        private readonly Mock<ICsvService> _csvServiceMock;
        private readonly Mock<IAcaoRepository> _acaoRepositoryMock;
        private readonly UploadFileHandler _handler;

        public UploadFileHandlerTests()
        {
            _csvServiceMock = new();
            _acaoRepositoryMock = new();
            _handler = new UploadFileHandler(_acaoRepositoryMock.Object, _csvServiceMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Insert_All_CsvDto_In_Database()
        {
            // Arrange
            var file = new Mock<IFormFile>();
            var csvDtos = CsvDtoFixture.ListCsvDto();
            var command = new UploadFileCommand(file.Object);
            var expectedResult = Result.Success();

            _csvServiceMock.Setup(x => x.ReadFormFile(It.IsAny<IFormFile>())).ReturnsAsync(csvDtos);
            _acaoRepositoryMock.Setup(x => x.InsertRangeAsync(It.IsAny<List<Acao>>())).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedResult.IsSuccess, result.IsSuccess);
            Assert.Equal(expectedResult.Errors, result.Errors);
            _acaoRepositoryMock.Verify(x => x.InsertRangeAsync(It.IsAny<List<Acao>>()), Times.Once);
            _csvServiceMock.Verify(x => x.ReadFormFile(It.IsAny<IFormFile>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_Failure_When_File_Is_Null()
        {
            // Arrange
            var command = new UploadFileCommand(null);
            var expectedError = new Error("Erro ao importar o arquivo", "Erro");
            var expectedResult = Result<IEnumerable<Acao>>.Failure(expectedError);
            _csvServiceMock.Setup(x => x.ReadFormFile(It.IsAny<IFormFile>())).ReturnsAsync(new List<CsvDto>());
            _acaoRepositoryMock.Setup(x => x.InsertRangeAsync(It.IsAny<List<Acao>>())).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedResult.IsSuccess, result.IsSuccess);
            Assert.Equal(expectedResult.Errors.First().error, result.Errors.First().error);

            _csvServiceMock.Verify(x => x.ReadFormFileAync(It.IsAny<string[]>()), Times.Never);
            _acaoRepositoryMock.Verify(x => x.InsertRangeAsync(It.IsAny<List<Acao>>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Should_Return_Failure_When_ReadFormFile_Returns_Empty_List()
        {
            // Arrange
            var file = new Mock<IFormFile>();
            var csvDtos = new List<CsvDto>();
            var command = new UploadFileCommand(file.Object);
            var expectedError = new Error("Erro ao importar o arquivo", "Erro");
            var expectedResult = Result<IEnumerable<Acao>>.Failure(expectedError);

            _csvServiceMock.Setup(x => x.ReadFormFile(It.IsAny<IFormFile>())).ReturnsAsync(csvDtos);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedResult.IsSuccess, result.IsSuccess);
            _csvServiceMock.Verify(x => x.ReadFormFileAync(It.IsAny<string[]>()), Times.Never);
            _acaoRepositoryMock.Verify(x => x.InsertRangeAsync(It.IsAny<List<Acao>>()), Times.Never);
        }
    }
}
