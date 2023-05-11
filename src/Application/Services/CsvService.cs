using Application.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using Dasync.Collections;
using Domain.Dtos;
using Infrastructure.Csv_Converters;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Text;
using static Infrastructure.CsvHelperConfiguration.CsvMapConfiguration;

namespace Application.Services
{
    public class CsvService: ICsvService
    {
        public async Task<List<CsvDto>> ReadFormFile(IFormFile file)
        {
            var list = new List<CsvDto>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var readerReplaced = CsvFileConfiguration.ReplaceWrongWords(reader);
                using (var csv = new CsvReader(readerReplaced, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    MissingFieldFound = null,
                    Encoding = Encoding.UTF8,
                    PrepareHeaderForMatch = (arg) => arg.Header,
                    DetectDelimiter = true
                }))
                {
                    await csv.ReadAsync();
                    csv.ReadHeader();
                    csv.Context.RegisterClassMap<CsvDtoMap>();

                    if (CsvFileConfiguration.ValidateHeader(csv))
                        throw new Exception("Erro ao carregar o arquivo");


                    var listaConvertida = await csv.GetRecordsAsync<CsvDto>().ToListAsync();
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
                using (var reader = new StringReader(file))
                {
                    var readerReplaced = CsvFileConfiguration.ReplaceWrongWords(reader);
                    using (var csv = new CsvReader(readerReplaced, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HasHeaderRecord = true,
                        MissingFieldFound = null,
                        Encoding = Encoding.UTF8,
                        PrepareHeaderForMatch = (arg) => arg.Header,
                        DetectDelimiter = true
                    }))
                    {
                        await csv.ReadAsync();
                        csv.ReadHeader();
                        csv.Context.RegisterClassMap<CsvDtoMap>();

                        if (CsvFileConfiguration.ValidateHeader(csv))
                            throw new Exception("Erro ao carregar o arquivo");

                        var listaConvertida = await csv.GetRecordsAsync<CsvDto>().ToListAsync();
                        list.AddRange(listaConvertida);
                    }
                }
            });
            return list;
        }

        public async Task<List<CsvDto>> ReadFileAsync(string fileContent)
        {
            var list = new List<CsvDto>();
            using (var reader = new StringReader(fileContent))
            {
                var readerReplaced = CsvFileConfiguration.ReplaceWrongWords(reader);
                using (var csv = new CsvReader(readerReplaced, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    MissingFieldFound = null,
                    Encoding = Encoding.UTF8,
                    PrepareHeaderForMatch = (arg) => arg.Header,
                    DetectDelimiter = true
                }))
                {
                    await csv.ReadAsync();
                    csv.ReadHeader();
                    csv.Context.RegisterClassMap<CsvDtoMap>();

                    if (CsvFileConfiguration.ValidateHeader(csv))
                        throw new Exception("Erro ao carregar o arquivo");


                    var listaConvertida = await csv.GetRecordsAsync<CsvDto>().ToListAsync();
                    list.AddRange(listaConvertida);
                }
            }
            return list;
        }
    }
}
