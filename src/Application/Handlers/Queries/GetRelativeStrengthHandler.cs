using Application.Interfaces;
using Application.Query;
using Application.Query.Responses;
using Domain;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Shared;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Queries
{
    public class GetRelativeStrengthHandler : IQueryHandler<GetRelativeStrengthQuery, GetRelativeStrengthResponse>
    {
        private readonly IAcaoRepository _repository;
        private readonly IRelativeStrengthService _relativeStrengthService;

        public GetRelativeStrengthHandler(IAcaoRepository repository, IRelativeStrengthService relativeStrengthService)
        {
            _repository = repository;
            _relativeStrengthService = relativeStrengthService;
        }

        public async Task<Result<GetRelativeStrengthResponse>> Handle(GetRelativeStrengthQuery request, CancellationToken cancellationToken)
        {
            GetRelativeStrengthResponse getRelativeStrengthResponse = new();
            List<List<Acao>> listaAcoes = new List<List<Acao>>();
            var tasks = request.AtivosSelecionados.Select(async ativo =>
            {
                var query = PredicateBuilder.New<Acao>().And(a => a.CodidoAcao.ToLower() == ativo.ToLower())
                                                        .And(a => a.Data >= request.DataInicial)
                                                        .And(a => a.Data <= request.DataFinal);

                var searchResult = await _repository.GetQueryable(query)
                                                       .OrderBy(a => a.Data)
                                                       .Take(request.TotalItensAmostrar)
                                                       .ToListAsync();
                return searchResult;
            });

            listaAcoes.AddRange(await Task.WhenAll(tasks));

            if (listaAcoes.Count() == 0)
                return Result<GetRelativeStrengthResponse>.Failure(new Error(EnumErro.ApplicationError.ToString(), "Nenhum arquivo importado"));

            getRelativeStrengthResponse.XAxisLabelsDatas = _relativeStrengthService.ObterLabel(listaAcoes);

            listaAcoes.ForEach(a =>
            {
                var charPropResponse = new ChartPropResponse()
                {
                    NomeAtivo = a.Select(s => string.IsNullOrEmpty(s.EmpresaNome) ? s.CodidoAcao : s.EmpresaNome).FirstOrDefault(""),
                    Valor = _relativeStrengthService.ObterValorDaAcao(a)
                };
                getRelativeStrengthResponse.ChartProp.Add(charPropResponse);
            });

            return Result<GetRelativeStrengthResponse>.Success(getRelativeStrengthResponse);
        }
    }
}
