using Application.Commands;
using Application.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using Dasync.Collections;
using Domain;
using Domain.Shared;
using Infrastructure;
using System.Globalization;

namespace Application.Handlers
{
    public sealed class UploadFileHandler : ICommandHandler<UploadFilesCommand, List<Ativo>>
    {
        public async Task<Result<List<Ativo>>> Handle(UploadFilesCommand request, CancellationToken cancellationToken)
        {
            var ativos = new List<Ativo>();

            var files = Directory.GetFiles(request.path, "*.csv");

            await files.ToAsyncEnumerable().ParallelForEachAsync(async file =>
            {
                using (var reader = new StreamReader(file))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ";",
                    HasHeaderRecord = true,
                    MissingFieldFound = null,
                }))
                {
                    csv.Context.RegisterClassMap<CsvMapConfiguration.AtivoMap>();

                    var records = csv.GetRecords<Ativo>();
                    ativos = records.ToList();
                    await foreach (var record in records)
                    {
                        Console.WriteLine($"Ativo: {record.AtivoNome}, Data: {record.Data}, Abertura: {record.Abertura}, Máximo: {record.Maximo}, Mínimo: {record.Minimo}, Fechamento: {record.Fechamento}, Volume: {record.Volume}, Quantidade: {record.Quantidade}");
                    }
                }
            });
            return Result<List<Ativo>>.Success(ativos);
        }
    }
}
