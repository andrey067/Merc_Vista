using Application.Commands;
using Application.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using Domain;
using Domain.Shared;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Text;

namespace Application.Handlers
{
    public class UploadFileHandler : ICommandHandler<UploadFileCommand, List<Acao>>
    {
        public Task<Result<List<Acao>>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            if (ValidateExistsFiles(request.File))
                return Task.FromResult(Result<List<Acao>>.Failure(new Error("Erro ao emportar o arquivo", "Erro")));

            var acao = new List<Acao>();
            using (var reader = new StreamReader(request.File.OpenReadStream(), Encoding.UTF8))
            {
                using (var csv = new CsvReader(ReplaceWrongWords(reader), new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    MissingFieldFound = null,
                    Encoding = Encoding.UTF8,
                    PrepareHeaderForMatch = (arg) => arg.Header,
                    DetectDelimiter = true,
                }))
                {
                    csv.Context.RegisterClassMap<CsvMapConfiguration.AtivoMap>();
                    csv.Read();
                    csv.ReadHeader();

                    if (ValidateHeader(csv))
                        throw new Exception("Erro ao carregar o arquivo");

                    acao = csv.GetRecords<Acao>().ToList();
                }
            }

            foreach (var record in acao)
                Console.WriteLine($"Ativo: {record.Ativo}, Data: {record.Data}, Abertura: {record.Abertura}, Máximo: {record.Maximo}, Mínimo: {record.Minimo}, Fechamento: {record.Fechamento}, Volume: {record.Volume}, Quantidade: {record.Quantidade}");

            return Task.FromResult(Result<List<Acao>>.Success(acao));
        }

        private static bool ValidateExistsFiles(IFormFile file)
         => file is null;

        private static StringReader ReplaceWrongWords(StreamReader reader)
        {
            var csvText = reader.ReadToEnd();
            csvText = csvText.Replace("M�ximo", "Maximo").Replace("M�nimo", "Minimo");
            var readerReplaced = new StringReader(csvText);

            return readerReplaced;
        }

        private static bool ValidateHeader(CsvReader csv)
        {
            var actualHeaders = csv.HeaderRecord!.ToList();
            var expectedHeaders = typeof(Acao).GetProperties().Select(p => p.Name).ToList();
            return !expectedHeaders.SequenceEqual(actualHeaders);
        }
    }
}
