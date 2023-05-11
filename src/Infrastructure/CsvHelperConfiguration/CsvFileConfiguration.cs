using CsvHelper;
using Domain.Dtos;

namespace Infrastructure.Csv_Converters
{
    public static class CsvFileConfiguration
    {
        public static StringReader ReplaceWrongWords(StreamReader reader)
        {
            var csvText = reader.ReadToEnd();
            csvText = csvText.Replace("M�ximo", "Maximo").Replace("M�nimo", "Minimo");
            return new StringReader(csvText);
        }

        public static StringReader ReplaceWrongWords(StringReader reader)
        {
            var csvText = reader.ReadToEnd();
            csvText = csvText.Replace("M�ximo", "Maximo").Replace("M�nimo", "Minimo");            
            return new StringReader(csvText);
        }

        public static bool ValidateHeader(CsvReader csv)
        {
            var actualHeaders = csv.HeaderRecord!.ToList();
            var expectedHeaders = typeof(CsvDto).GetProperties().Where(a => a.Name != "Linha").Select(p => p.Name).ToList();
            return !expectedHeaders.SequenceEqual(actualHeaders);
        }
    }
}
