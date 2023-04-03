using Domain;
using System.Collections;

namespace Merc_Vista.Tests.Fixtures
{
    public class AcaoFixture: List<Acao>
    {
        private readonly List<Acao> _acoes = new List<Acao>
        {
            new Acao
            {
            Ativo = "AALR3",
            Data = new DateTime(2023, 03, 30),
            Abertura = 22.65M,
            Maximo = 22.7M,
            Minimo = 22.51M,
            Fechamento = 22.56M,
            Volume = 2641889M,
            Quantidade = 117000M
            }
        };

        public List<Acao> GetList() => _acoes;
    }
}
