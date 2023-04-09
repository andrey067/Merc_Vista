using Application.Commands;
using Application.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using Dasync.Collections;
using Domain;
using Domain.Shared;
using Infrastructure;
using System.Globalization;
using System.Text;

namespace Application.Handlers
{
    public sealed class UploadFileDirectoryHandler: ICommandHandler<UploadFilesDiretoryCommand, List<Acao>>
    {
        public async Task<Result<List<Acao>>> Handle(UploadFilesDiretoryCommand request, CancellationToken cancellationToken)
        {
            if(ValidatePathExists(request.path))
                return Result<List<Acao>>.Failure(new Error("Caminho não encontrato", "Fornecessa um diretorio valido"));

            var files = Directory.GetFiles(request.path, "*.csv");

            if(ValidateExistsFiles(files))
                return Result<List<Acao>>.Failure(new Error("Não existem arquivos com a extensão .csv", "Fornecessa um diretorio valido"));

            var acao = new List<Acao>();

            files.ToList().ForEach(file =>
                {
                    using(var reader = new StreamReader(file, Encoding.UTF8))
                    {
                        var readerReplaced = ReplaceWrongWords(reader);
                        using(var csv = new CsvReader(readerReplaced, new CsvConfiguration(CultureInfo.InvariantCulture)
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

                            if(ValidateHeader(csv))
                                throw new Exception("Erro ao carregar o arquivo");

                            acao = csv.GetRecords<Acao>().ToList();                            
                        }
                    }
                });

            foreach(var record in acao)
                Console.WriteLine($"Ativo: {record.Ativo}, Data: {record.Data}, Abertura: {record.Abertura}, Máximo: {record.Maximo}, Mínimo: {record.Minimo}, Fechamento: {record.Fechamento}, Volume: {record.Volume}, Quantidade: {record.Quantidade}");

            return Result<List<Acao>>.Success(acao);
        }

        private static bool ValidateHeader(CsvReader csv)
        {
            var actualHeaders = csv.HeaderRecord!.ToList();
            var expectedHeaders = typeof(Acao).GetProperties().Select(p => p.Name).ToList();
            return !expectedHeaders.SequenceEqual(actualHeaders);
        }

        private static bool ValidatePathExists(string path)
         => !Directory.Exists(path);

        private static bool ValidateExistsFiles(string[] files)
         => !(files != null && files.Any());

        private static StringReader ReplaceWrongWords(StreamReader reader)
        {
            var csvText = reader.ReadToEnd();
            csvText = csvText.Replace("M�ximo", "Maximo").Replace("M�nimo", "Minimo");
            var readerReplaced = new StringReader(csvText);

            return readerReplaced;
        }
    }
}
