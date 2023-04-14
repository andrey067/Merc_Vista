using Application.Interfaces;
using Application.Query;
using Domain;
using Domain.Interfaces;
using Domain.Shared;

namespace Application.Handlers.Queries
{
    public class GetRelativeStrengthHandler: IQueryHandler<GetRelativeStrengthQuery, IEnumerable<Acao>>
    {
        private readonly IAcaoRepository _repository;

        public GetRelativeStrengthHandler(IAcaoRepository repository) => _repository = repository;

        public async Task<Result<IEnumerable<Acao>>> Handle(GetRelativeStrengthQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAll();

            if (result.Count() == 0)
                return Result<IEnumerable<Acao>>.Failure(new Error("Erro", "Nenhum arquivo importado"));

            return Result<IEnumerable<Acao>>.Success(result);
        }
    }
}
