using Application.Services;
using Domain;
using Merc_Vista.Tests.Fixtures;

namespace Merc_Vista.Tests.Unit.Services
{
    public class RelativeStrengthServiceTests
    {

        [Fact]
        public void ObterValorDaAcao_DeveRetornarValoresCorretos()
        {
            // Arrange
            var (acoes, valoresCalculados) = AcaoFixture.GetListFixed_RelativeStrenght();
            var service = new RelativeStrengthService();

            // Act            
            var valores = service.ObterValorDaAcao(acoes);
            acoes = acoes.OrderByDescending(acao => acao.Data).ToList();

            // Assert
            Assert.Equal(acoes.Count, valores.Length);
            Assert.Collection(valores,
                val => Assert.Equal(val, valoresCalculados[0]),
                val => Assert.Equal(val, valoresCalculados[1]),
                val => Assert.Equal(val, valoresCalculados[2]),
                val => Assert.Equal(val, valoresCalculados[3]),
                val => Assert.Equal(val, 0)
            );
        }

        [Fact]
        public void ObterLabel_DeveRetornarXAxisLabelsDatasCorretas()
        {
            //Arrange
            List<List<Acao>> listaAcaoes = new List<List<Acao>>();
            var random = new Random();
            var dataRandom = new DateTime(2023, random.Next(1, 13), random.Next(1, 29));

            var acoes1 = AcaoFixture.GetListRandom();
            acoes1.ForEach(a => a.Data = dataRandom);

            var acoes2 = AcaoFixture.GetListRandom();
            acoes2.ForEach(a => a.Data = dataRandom);

            listaAcaoes.Add(acoes1);
            listaAcaoes.Add(acoes2);

            //Act
            var service = new RelativeStrengthService();
            var label = service.ObterLabel(listaAcaoes);

            //Assert
            Assert.Equal(1, label.Count);
            Assert.Equal(new List<string> { dataRandom.ToString("dd/MM/yy") }, label);
        }

        [Fact]
        public void ValidarDatasIguais_DeveRetornarFalseParaDatasIguais()
        {
            //Arrange
            var random = new Random();
            var listaAcoes = Enumerable.Range(0, random.Next(2, 1000))
                                                    .Select(_ => AcaoFixture.GetListFixed())
                                                    .ToList();

            //Act
            var service = new RelativeStrengthService();
            var dataSaoIguais = service.VerificarDatas(listaAcoes);

            //Assert
            Assert.False(dataSaoIguais);
        }

        [Fact]
        public void VerificarQuantidadeDeElementos_DeveRetornarFalseParaListasComMesmaQuantidadeDeElementos()
        {
            //Arrange
            var random = new Random();
            var listaAcoes = Enumerable.Range(0, random.Next(2, 1000))
                                                    .Select(_ => AcaoFixture.GetListFixed())
                                                    .ToList();

            //Act
            var service = new RelativeStrengthService();
            var verificarQuantidadeDeElementos = service.VerificarQuantidadeDeElementos(listaAcoes);

            //Assert
            Assert.False(verificarQuantidadeDeElementos);
        }
    }
}
