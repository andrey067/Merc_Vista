using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Infrastructure.Csv_Converters
{
    public class ConvertStringToDecimal : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (!string.IsNullOrEmpty(text) && decimal.TryParse(text.Replace(".", "").Replace(",", "."), out decimal result))
                return result;
            else
                return 0;
        }
    }
}
