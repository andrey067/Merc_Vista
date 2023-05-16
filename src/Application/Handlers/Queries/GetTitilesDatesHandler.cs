using Application.Interfaces;
using Application.Query;
using Application.Query.Responses;
using Dasync.Collections;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Queries
{
    public class GetTitilesDatesHandler: IQueryHandler<GetTitilesQuery, GetTitilesDatesResponse>
    {
        private readonly IAcaoRepository _repository;

        public GetTitilesDatesHandler(IAcaoRepository repository) => _repository = repository;

        public async Task<Result<GetTitilesDatesResponse>> Handle(GetTitilesQuery request, CancellationToken cancellationToken)
        {
            var queryable = _repository.GetQueryable().OrderByDescending(a => a.Data).AsQueryable();
            var result = await queryable.ToListAsync();

            if (result.Count > 0)
            {
                var dataInicial = await queryable.Take(request.RangeDate).MinAsync(a => a.Data);
                var dataFinal = await queryable.Take(request.RangeDate).MaxAsync(a => a.Data);

                var response = new GetTitilesDatesResponse(result.DistinctBy(c => c.CodidoAcao).Select(r => r.CodidoAcao).ToList(), dataInicial, dataFinal);
                return Result<GetTitilesDatesResponse>.Success(response);
            }

            return Result<GetTitilesDatesResponse>.Failure(new Error(EnumErro.ApplicationError.ToString(), "Não foram carregados Titulos"));
        }
    }
}
