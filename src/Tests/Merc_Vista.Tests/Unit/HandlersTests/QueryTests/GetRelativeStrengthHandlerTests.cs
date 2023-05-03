using Application.Handlers.Queries;
using Application.Interfaces;
using Application.Query;
using Domain;
using Domain.Interfaces;
using Merc_Vista.Tests.Fixtures;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq.Expressions;

namespace Merc_Vista.Tests.Unit.HandlersTests.QueryTests
{
    public class GetRelativeStrengthHandlerTests
    {
        //TODO
        //[Fact]
        //public async Task Handle_Should_Return_Success_When_GetRelativeStrength()
        //{
        //    //Arrange 
        //    var query = new GetRelativeStrengthQuery(
        //                                    new List<string>() { "PETR4", "VALE3", "ITUB4" },
        //                                         new DateTime(2021, 1, 1),
        //                                          new DateTime(2021, 12, 31),
        //                                   10);


        //    var mockRepository = new Mock<IAcaoRepository>();

        //    mockRepository.Setup(repo => repo.GetQueryable(It.IsAny<Expression<Func<Acao, bool>>>()))
        //        .Returns(await Task.FromResult(AcaoFixture.GetList().AsQueryable()));

        //    var mockRelativeStrengthService = new Mock<IRelativeStrengthService>();
        //    mockRelativeStrengthService.Setup(service => service.VerificarQuantidadeDeElementos(It.IsAny<List<List<Acao>>>())).Returns(false);
        //    mockRelativeStrengthService.Setup(service => service.VerificarDatas(It.IsAny<List<List<Acao>>>())).Returns(false);
        //    mockRelativeStrengthService.Setup(service => service.ObterValorDaAcao(It.IsAny<List<Acao>>())).Returns(new double[] { 10, 20, 30 });
        //    var handler = new GetRelativeStrengthHandler(mockRepository.Object, mockRelativeStrengthService.Object);

        //    //Act
        //    var result = await handler.Handle(query, default);

        //    // Assert
        //    Assert.True(result.IsSuccess);
        //    Assert.NotNull(result.Data);
        //    Assert.Equal(3, result.Data.ChartProp.Count);
        //    Assert.Equal("PETR4", result.Data.ChartProp[0].NomeAtivo);
        //    //Assert.Equal(10, result.Data.ChartProp[0].Valor);
        //    Assert.Equal("VALE3", result.Data.ChartProp[1].NomeAtivo);
        //    //Assert.Equal(10, result.Data.ChartProp[1].Valor);
        //    Assert.Equal("ITUB4", result.Data.ChartProp[2].NomeAtivo);
        //    //Assert.Equal(10, result.Data.ChartProp[2].Valor);
        //}
    }
}
