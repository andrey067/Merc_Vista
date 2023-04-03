using CsvHelper.Configuration;
using Domain;
using Infrastructure.Csv_Converters;

namespace Infrastructure
{
    public static class CsvMapConfiguration
    {
        public sealed class AtivoMap: ClassMap<Acao>
        {
            public AtivoMap()
            {
                Map(m => m.Ativo);
                Map(m => m.Data).TypeConverter<ConvertStringToDateTime>();
                Map(m => m.Abertura).TypeConverter<ConvertStringToDecimal>();
                Map(m => m.Maximo).TypeConverter<ConvertStringToDecimal>();
                Map(m => m.Minimo).TypeConverter<ConvertStringToDecimal>();
                Map(m => m.Fechamento).TypeConverter<ConvertStringToDecimal>();
                Map(m => m.Volume).TypeConverter<ConvertStringToDecimal>();
                Map(m => m.Quantidade).TypeConverter<ConvertStringToDecimal>();
            }
        }
    }
}
