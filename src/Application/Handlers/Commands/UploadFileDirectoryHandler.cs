using Application.Commands;
using Application.Interfaces;
using Domain;
using Domain.Interfaces;
using Domain.Shared;
using Mapster;

namespace Application.Handlers.Commands
{
    public sealed class UploadFileDirectoryHandler: ICommandHandler<UploadFilesDiretoryCommand>
    {
        private readonly IAcaoRepository _acaoRepository;
        private readonly ICsvService _csvService;

        public UploadFileDirectoryHandler(IAcaoRepository acaoRepository, ICsvService csvService)
        {
            _acaoRepository = acaoRepository;
            _csvService = csvService;
        }

        public async Task<Result> Handle(UploadFilesDiretoryCommand request, CancellationToken cancellationToken)
        {
            if (ValidatePathExists(request.path))
                return Result<Task>.Failure(new Error("Caminho não encontrato", "Fornecessa um diretorio valido"));

            var files = Directory.GetFiles(request.path, "*.csv");

            if (ValidateExistsFiles(files))
                return Result<Task>.Failure(new Error("Não existem arquivos com a extensão .csv", "Fornecessa um diretorio valido"));

            var listCsvDto = await _csvService.ReadFormFileAync(files);

            var acoes = listCsvDto.Adapt<List<Acao>>();
            await _acaoRepository.InsertRangeAsync(acoes);


            return Result.Success();
        }

        private static bool ValidatePathExists(string path)
         => !Directory.Exists(path);

        private static bool ValidateExistsFiles(string[] files)
         => !(files != null && files.Any());
    }
}
