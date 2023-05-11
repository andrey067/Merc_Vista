using Application.Services;
using Domain.Dtos;
using Merc_Vista.Tests.Fixtures;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Text;

namespace Merc_Vista.Tests.Unit.Services
{
    public class CsvServiceTests
    {
        private readonly CsvService _csvService;

        public CsvServiceTests() => _csvService = new CsvService();

        [Fact]
        public async Task ReadFormFile_Should_Return_Null_When_Invalid_Header()
        {
            // Arrange
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(x => x.OpenReadStream()).Returns(new MemoryStream(Encoding.UTF8.GetBytes("Header1,Header2,Header3\nValue1,Value2,Value3")));

            // Act
            var result = await _csvService.ReadFormFile(fileMock.Object);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ReadFormFile_Should_Return_List_Of_CsvDto_When_Valid_Header()
        {
            // Arrange
            var fileMock = new Mock<IFormFile>();
            var csvContent = CsvContentFixture.CsvFile();
            var csvConverted = CsvContentFixture.ConvertToEntitie<CsvDto>(csvContent);
            fileMock.Setup(x => x.OpenReadStream()).Returns(new MemoryStream(Encoding.UTF8.GetBytes(csvContent)));

            // Act
            var result = await _csvService.ReadFormFile(fileMock.Object);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(csvConverted, result);
        }
    }
}
