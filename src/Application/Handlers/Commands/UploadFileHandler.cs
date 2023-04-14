using Application.Commands;
using Application.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using Domain;
using Domain.Interfaces;
using Domain.Shared;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Text;

namespace Application.Handlers.Commands
{
    public class UploadFileHandler : ICommandHandler<UploadFileCommand, List<Acao>>
    {
        private readonly IAcaoRepository _acaoRepository;

        public UploadFileHandler(IAcaoRepository acaoRepository)
         => _acaoRepository = acaoRepository;

        public async Task<Result<List<Acao>>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            if (ValidateExistsFiles(request.File))
                return Result<List<Acao>>.Failure(new Error("Erro ao importar o arquivo", "Erro"));

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
                        return Result<List<Acao>>.Failure(new Error("Erro ao importar o arquivo", "Erro no cabeçalho do arquivo"));

                    var listaConvertida = csv.GetRecords<Acao>().ToList();
                    acao.AddRange(await _acaoRepository.InsertRangeAsync(listaConvertida));
                }
            }

            return Result<List<Acao>>.Success(acao);
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
            var expectedHeaders = typeof(Acao).GetProperties().Where(p => p.Name != "Id").Select(p => p.Name).ToList();
            return !expectedHeaders.SequenceEqual(actualHeaders);
        }
    }
}
