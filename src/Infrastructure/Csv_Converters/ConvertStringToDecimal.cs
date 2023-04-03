using CsvHelper.Configuration;
using CsvHelper;
using CsvHelper.TypeConversion;

namespace Infrastructure.Csv_Converters
{
    public class ConvertStringToDecimal : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (decimal.TryParse(text.Replace(".", "").Replace(",", "."), out decimal result))
                return result;
            else
                return 0;
        }
    }
}
