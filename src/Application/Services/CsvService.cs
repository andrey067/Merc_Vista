using Application.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.Dtos;
using Infrastructure.Csv_Converters;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Text;
using Dasync.Collections;
using Infrastructure.CsvHelperConfiguration;

namespace Application.Services
{
    public class CsvService : ICsvService
    {
        public List<CsvDto> ReadFormFile(IFormFile file)
        {
            var list = new List<CsvDto>();
            using (var reader = new StreamReader(file.OpenReadStream(), Encoding.UTF8))
            {
                using (var csv = new CsvReader(CsvFileConfiguration.ReplaceWrongWords(reader), new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    MissingFieldFound = null,
                    Encoding = Encoding.UTF8,
                    PrepareHeaderForMatch = (arg) => arg.Header,
                    DetectDelimiter = true,
                }))
                {
                    csv.Context.RegisterClassMap<CsvMapConfiguration.CsvDtoMap>();
                    csv.Read();
                    csv.ReadHeader();

                    if (CsvFileConfiguration.ValidateHeader(csv))
                        return null;

                    var listaConvertida = csv.GetRecords<CsvDto>().ToList();
                    list.AddRange(listaConvertida);
                }
            }

            return list;
        }

        public async Task<List<CsvDto>> ReadFormFileAync(string[] files)
        {
            var list = new List<CsvDto>();

            await files.ToAsyncEnumerable().ParallelForEachAsync(async file =>
            {
                using (var reader = new StreamReader(file, Encoding.UTF8))
                {
                    var readerReplaced = CsvFileConfiguration.ReplaceWrongWords(reader);
                    using (var csv = new CsvReader(readerReplaced, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HasHeaderRecord = true,
                        MissingFieldFound = null,
                        Encoding = Encoding.UTF8,
                        PrepareHeaderForMatch = (arg) => arg.Header,
                        DetectDelimiter = true,
                    }))
                    {
                        csv.Context.RegisterClassMap<CsvMapConfiguration.CsvDtoMap>();
                        await csv.ReadAsync();
                        csv.ReadHeader();

                        if (CsvFileConfiguration.ValidateHeader(csv))
                            throw new Exception("Erro ao carregar o arquivo");

                        var listaConvertida = await csv.GetRecordsAsync<CsvDto>().ToListAsync();
                        list.AddRange(listaConvertida);
                    }
                }
            });

            return list;
        }        
    }
}
