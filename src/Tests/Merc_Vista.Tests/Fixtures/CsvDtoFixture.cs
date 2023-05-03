using Domain.Dtos;

namespace Merc_Vista.Tests.Fixtures
{
    public static class CsvDtoFixture
    {
        public static List<CsvDto> ListCsvDto()
        {
            var csvData = new List<CsvDto>()
            {
                new CsvDto {  Ativo = "MGLU3", Data = DateTime.Now, Abertura = 0.405m, Maximo = 0.41m, Minimo = 0.40m, Fechamento = 0.41m, Volume = 100000, Quantidade = 1000 },
                new CsvDto {  Ativo = "PETR3", Data = DateTime.Now, Abertura = 5.024m, Maximo = 5.05m, Minimo = 5.00m, Fechamento = 5.05m, Volume = 200000, Quantidade = 2000 },
                new CsvDto {  Ativo = "VALE3", Data = DateTime.Now, Abertura = 15.512m, Maximo = 15.60m, Minimo = 15.50m, Fechamento = 15.60m, Volume = 300000, Quantidade = 3000 },
            };
            return csvData;
        }
    }
}
