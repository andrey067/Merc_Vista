using Domain.Interfaces;
using Moq;

namespace Merc_Vista.Tests.Unit.HandlersTests.QueryTests
{
    public class GetTitilesDatesHandlerTests
    {
        private readonly Mock<IAcaoRepository> _mockRepository;

        public GetTitilesDatesHandlerTests()
        {
            _mockRepository = new Mock<IAcaoRepository>();
        }

        // - TODO
        //[Fact]
        //public async Task Handle_ShouldReturnSuccessResult_WithGetTitlesDatesResponse()
        //{
        //    // Arrange
        //    var acoes = AcaoFixture.GetList();
        //    _mockRepository.Setup(repo => repo.GetQueryable(null)).Returns(acoes.Result.AsQueryable());
        //    var query = new GetTitilesQuery();
        //    var handler = new GetTitilesDatesHandler(_mockRepository.Object);

        //    // Act
        //    var result = await handler.Handle(query, CancellationToken.None);

        //    // Assert
        //    Assert.True(result.IsSuccess);
        //    Assert.Equal(acoes.Result.Count(), result.Data.Ativo.Count);
        //    Assert.Equal(acoes.Result.Min(a => a.Data), result.Data.DataInicial);
        //    Assert.Equal(acoes.Result.Max(a => a.Data), result.Data.DataInicial);
        //}
    }
}
