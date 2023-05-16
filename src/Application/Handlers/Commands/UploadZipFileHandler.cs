using Application.Commands;
using Application.Interfaces;
using Domain;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Shared;
using Infrastructure.Extensions;
using Mapster;

namespace Application.Handlers.Commands
{
    public class UploadZipFileHandler: ICommandHandler<UploadZipFileCommand>
    {
        private readonly IAcaoRepository _acaoRepository;
        private readonly ICsvService _csvService;

        public UploadZipFileHandler(IAcaoRepository acaoRepository, ICsvService csvService)
        {
            _acaoRepository = acaoRepository;
            _csvService = csvService;
        }
        public async Task<Result> Handle(UploadZipFileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.RequestStream.Files.All(f => f.Length < 0))
                    return Result.Failure(new Error(EnumErro.ApplicationError.ToString(), "Erro ao carregar os arquivos"));

                foreach (var file in request.RequestStream.Files)
                {
                    var arquivoEmTextoDescompactado = await CompressGzipExtension.Decompress(file);
                    var listCsvDto = await _csvService.ReadFileAsync(arquivoEmTextoDescompactado);
                    var acoes = listCsvDto.Adapt<List<Acao>>();
                    await _acaoRepository.InsertRangeAsync(acoes);
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(new Error(EnumErro.CsvError.ToString(), ex.Message));
            }
        }
    }
}
