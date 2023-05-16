using Application.Commands;
using Application.Interfaces;
using Domain;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Shared;
using Mapster;
using Microsoft.AspNetCore.Http;

namespace Application.Handlers.Commands
{
    public class UploadFileHandler: ICommandHandler<UploadFileCommand>
    {
        private readonly IAcaoRepository _acaoRepository;
        private readonly ICsvService _csvService;

        public UploadFileHandler(IAcaoRepository acaoRepository, ICsvService csvService)
        {
            _acaoRepository = acaoRepository;
            _csvService = csvService;
        }

        public async Task<Result> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            if (ValidateExistsFiles(request.File))
                return Result<IEnumerable<Acao>>.Failure(new Error(EnumErro.CsvError.ToString(), "Erro ao importar o arquivo"));

            var listCsvDto = await _csvService.ReadFormFile(request.File);

            if (listCsvDto.Count == 0)
                return Result<IEnumerable<Acao>>.Failure(new Error(EnumErro.CsvError.ToString(), "Erro ao importar o arquivo"));

            try
            {
                var acoes = listCsvDto.Adapt<List<Acao>>();
                await _acaoRepository.InsertRangeAsync(acoes);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(new Error(EnumErro.DomainError.ToString(), ex.Message));
            }
        }

        private bool ValidateExistsFiles(IFormFile file) => file is null;
    }
}
