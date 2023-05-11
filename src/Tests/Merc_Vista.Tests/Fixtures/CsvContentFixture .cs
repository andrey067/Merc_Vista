using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text;

namespace Merc_Vista.Tests.Fixtures
{
    public static class CsvContentFixture
    {
        public static string CsvFile()
        {
            var csvData = CsvDtoFixture.ListCsvDto();

            using (var writer = new StringWriter())
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {                    
                    csv.WriteRecords(csvData);
                }

                return writer.ToString();
            }
        }

        public static List<T> ConvertToEntitie<T>(string file)
        {
            var list = new List<T>();

            using (var reader = new StringReader(file))
            {
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    MissingFieldFound = null,
                    Encoding = Encoding.UTF8,
                    PrepareHeaderForMatch = (arg) => arg.Header,
                    DetectDelimiter = true,
                }))
                {
                    csv.Read();
                    csv.ReadHeader();

                    var listaConvertida = csv.GetRecords<T>().ToList();
                    list.AddRange(listaConvertida);
                }
            }
            return list;
        }
    }
}
