using Domain;
using Domain.Enums;
using Infrastructure.Extensions;

namespace Merc_Vista.Tests.Fixtures
{
    public static class AcaoFixture
    {
        public static List<Acao> GetListRandom()
        {
            var acoes = new List<Acao>();
            var random = new Random();
            var tamanhoLista = random.Next(2, 1000);
            var empresas = Enum.GetValues(typeof(B3Companies)).Cast<B3Companies>().ToList();
            var empresa = empresas[random.Next(empresas.Count)];

            for (int i = 0; i < tamanhoLista; i++)
            {
                var data = new DateTime(2023, random.Next(1, 13), random.Next(1, 29));
                var abertura = random.Next(1, 100);
                var maximo = abertura + random.Next(1, 10);
                var minimo = abertura - random.Next(1, 10);
                var fechamento = minimo + random.Next(1, 20);
                var volume = random.Next(100000, 10000000);
                var quantidade = random.Next(10000, 1000000);

                var acao = new Acao
                {
                    CodidoAcao = empresa.ToString(),
                    EmpresaNome = empresa.GetDescription(),
                    Data = data,
                    Abertura = abertura,
                    Maximo = maximo,
                    Minimo = minimo,
                    Fechamento = fechamento,
                    Volume = volume,
                    Quantidade = quantidade
                };

                acoes.Add(acao);
            }

            return acoes;
        }


        public static (List<Acao>, double[]) GetListFixed_RelativeStrenght()
        {
            var acoes = new List<Acao>
                {
                    new Acao
                    {
                        CodidoAcao = "ITSA4",
                        EmpresaNome = "ITAUSA PN N1",
                        Data = new DateTime(2023, 3, 29),
                        Abertura = 11.85M,
                        Maximo = 11.91M,
                        Minimo = 11.54M,
                        Fechamento = 101792.52M,
                        Volume = 16006300,
                        Quantidade = 1365700
                    },
                    new Acao
                    {
                        CodidoAcao = "BBAS3",
                        EmpresaNome = "BRASIL ON NM",
                        Data = new DateTime(2023, 3, 28),
                        Abertura = 34.91M,
                        Maximo = 35.00M,
                        Minimo = 34.39M,
                        Fechamento = 101185.09M,
                        Volume = 12167900,
                        Quantidade = 3470400
                    },
                    new Acao
                    {
                        CodidoAcao = "BBDC3",
                        EmpresaNome = "BRADESCO ON EJ N1",
                        Data = new DateTime(2023, 3, 27),
                        Abertura = 25.15M,
                        Maximo = 25.20M,
                        Minimo = 24.87M,
                        Fechamento = 99670.47M,
                        Volume = 10489600,
                        Quantidade = 4193300
                    },
                    new Acao
                    {
                        CodidoAcao = "VALE3",
                        EmpresaNome = "VALE ON NM",
                        Data = new DateTime(2023, 3, 24),
                        Abertura = 106.22M,
                        Maximo = 107.45M,
                        Minimo = 105.90M,
                        Fechamento = 98829.27M,
                        Volume = 16429600,
                        Quantidade = 1548400
                    },
                    new Acao
                    {
                        CodidoAcao = "PETR4",
                        EmpresaNome = "PETROBRAS PN N2",
                        Data = new DateTime(2023, 3, 23),
                        Abertura = 29.80M,
                        Maximo = 30.25M,
                        Minimo = 29.60M,
                        Fechamento = 97926.34M,
                        Volume = 32303100,
                        Quantidade = 10739800
                    }
                };

            double[] forcarelativa = new double[]
            {
             0.6003,
             1.5196,
             0.8512,
             0.9221,
             0
            };

            return (acoes, forcarelativa);
        }

        public static List<Acao> GetListFixed()
        {
            var random = new Random();
            var empresas = Enum.GetValues(typeof(B3Companies)).Cast<B3Companies>().ToList();
            var empresa = empresas[random.Next(empresas.Count)];

            var acoes = new List<Acao>
                {
                    new Acao
                    {
                        CodidoAcao = empresa.ToString(),
                        EmpresaNome = empresa.GetDescription(),
                        Data = new DateTime(2023, 3, 29),
                        Abertura = 11.85M,
                        Maximo = 11.91M,
                        Minimo = 11.54M,
                        Fechamento = 101792.52M,
                        Volume = 16006300,
                        Quantidade = 1365700
                    },
                    new Acao
                    {
                        CodidoAcao = empresa.ToString(),
                        EmpresaNome = empresa.GetDescription(),
                        Data = new DateTime(2023, 3, 28),
                        Abertura = 34.91M,
                        Maximo = 35.00M,
                        Minimo = 34.39M,
                        Fechamento = 101185.09M,
                        Volume = 12167900,
                        Quantidade = 3470400
                    },
                    new Acao
                    {
                        CodidoAcao = empresa.ToString(),
                        EmpresaNome = empresa.GetDescription(),
                        Data = new DateTime(2023, 3, 27),
                        Abertura = 25.15M,
                        Maximo = 25.20M,
                        Minimo = 24.87M,
                        Fechamento = 99670.47M,
                        Volume = 10489600,
                        Quantidade = 4193300
                    },
                    new Acao
                    {
                        CodidoAcao = empresa.ToString(),
                        EmpresaNome = empresa.GetDescription(),
                        Data = new DateTime(2023, 3, 24),
                        Abertura = 106.22M,
                        Maximo = 107.45M,
                        Minimo = 105.90M,
                        Fechamento = 98829.27M,
                        Volume = 16429600,
                        Quantidade = 1548400
                    },
                    new Acao
                    {
                        CodidoAcao = empresa.ToString(),
                        EmpresaNome = empresa.GetDescription(),
                        Data = new DateTime(2023, 3, 23),
                        Abertura = 29.80M,
                        Maximo = 30.25M,
                        Minimo = 29.60M,
                        Fechamento = 97926.34M,
                        Volume = 32303100,
                        Quantidade = 10739800
                    }
                };

            return acoes;
        }
    }
}
