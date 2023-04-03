using CsvHelper.Configuration;
using Domain;
using Infrastructure.Csv_Converters;
using System.Globalization;

namespace Infrastructure
{
    public static class CsvMapConfiguration
    {
        public sealed class AtivoMap : ClassMap<Ativo>
        {
            public AtivoMap()
            {
                Map(m => m.AtivoNome).Name("Ativo");
                Map(m => m.Data).Name("Data").TypeConverter<ConvertStringToDateTime>();
                Map(m => m.Abertura).Name("Abertura").TypeConverter<ConvertStringToDecimal>();
                Map(m => m.Maximo).Name("M�ximo").TypeConverter<ConvertStringToDecimal>();
                Map(m => m.Minimo).Name("M�nimo").TypeConverter<ConvertStringToDecimal>();
                Map(m => m.Fechamento).Name("Fechamento").TypeConverter<ConvertStringToDecimal>();
                Map(m => m.Volume).Name("Volume").TypeConverter<ConvertStringToDecimal>();
                Map(m => m.Quantidade).Name("Quantidade").TypeConverter<ConvertStringToDecimal>();
            }
        }
    }
}
