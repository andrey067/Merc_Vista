using Application.Interfaces;
using Application.Query;
using Application.Query.Responses;
using Domain;
using Domain.Interfaces;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Handlers.Queries
{
    public class GetRelativeStrengthHandler : IQueryHandler<GetRelativeStrengthQuery, GetRelativeStrengthResponse>
    {
        private readonly IAcaoRepository _repository;

        public GetRelativeStrengthHandler(IAcaoRepository repository) => _repository = repository;

        public async Task<Result<GetRelativeStrengthResponse>> Handle(GetRelativeStrengthQuery request, CancellationToken cancellationToken)
        {
            GetRelativeStrengthResponse result = new();
            List<List<Acao>> listaAcoes = new List<List<Acao>>();
            var tasks = request.ativosSelecionados.Select(async ativo =>
            {
                Expression<Func<Acao, bool>> query = acao => acao.Ativo.ToLower() == ativo.ToLower() &&
                                                                 acao.Data >= request.DataInicial &&
                                                                 acao.Data <= request.DataFinal;

                var result = await _repository.GetQueryable(query)
                                                       .OrderByDescending(a => a.Data)
                                                       .Take(request.totalItensAmostrar)
                                                       .ToListAsync();
                return result;
            });
            listaAcoes.AddRange(await Task.WhenAll(tasks));

            if (listaAcoes.Count() == 0)
                return Result<GetRelativeStrengthResponse>.Failure(new Error("Erro", "Nenhum arquivo importado"));

            if (VerificarQuantidadeDeElementos(listaAcoes))
                return Result<GetRelativeStrengthResponse>.Failure(new Error("Erro", "Os titulos selecionados não contém o a mesma quantidade de items"));

            if (VerificarDatas(listaAcoes))
                return Result<GetRelativeStrengthResponse>.Failure(new Error("Erro", "Datas não compativeis"));

            ObterLabel(listaAcoes, result);

            listaAcoes.ForEach(a =>
            {
                var response = new ChartPropResponse();
                response.NomeAtivo = a.Select(s => s.Ativo).FirstOrDefault("");
                response.Valor = ObterValorDaAcao(a);
                result.ChartProp.Add(response);
            });

            return Result<GetRelativeStrengthResponse>.Success(result);
        }

        private bool VerificarDatas(List<List<Acao>> listaAcoes)
        {
            int tamanhoLista = listaAcoes.FirstOrDefault()?.Count ?? 0;

            for (int i = 0; i < tamanhoLista; i++)
            {
                DateTime? dataComparar = listaAcoes[0][i].Data;

                for (int j = 1; j < listaAcoes.Count; j++)
                {
                    if (listaAcoes[j][i].Data != dataComparar)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static double[] ObterValorDaAcao(List<Acao> titulo)
        {
            double[] valoresDaAcao = new double[titulo.Count()];

            titulo.Sort((a1, a2) => a2.Data.GetValueOrDefault().CompareTo(a1.Data.GetValueOrDefault()));

            foreach (var acao in titulo)
            {
                var dataDiaAnterior = ObterDiaAnterior(titulo, acao);
                var fechamentoAcaoAtual = acao.Fechamento;

                if (dataDiaAnterior is not null)
                {
                    var fechamentoDiaAnterior = titulo.Where(t => t.Data == dataDiaAnterior).Select(a => a.Fechamento).FirstOrDefault();
                    var valorPorcentagem = ((fechamentoAcaoAtual / fechamentoDiaAnterior) - 1) * 100;
                    valoresDaAcao[titulo.IndexOf(acao)] = (double)valorPorcentagem;
                }
            }

            return valoresDaAcao;
        }

        private static DateTime? ObterDiaAnterior(List<Acao> titulo2, Acao acao)
            => titulo2.IndexOf(acao) + 1 < titulo2.Count ? titulo2[titulo2.IndexOf(acao) + 1].Data : null;

        private static void ObterLabel(List<List<Acao>> listaAcoes, GetRelativeStrengthResponse result)
            => result.XAxisLabelsDatas = listaAcoes
                                        .SelectMany(acoes => acoes)
                                        .Where(acao => acao.Data != null)
                                        .Select(acao => acao.Data!.Value.ToString("dd/MM/yy"))
                                        .Distinct()
                                        .ToList();




        private static bool VerificarQuantidadeDeElementos(List<List<Acao>> listaAcoes)
            => listaAcoes.Select(x => x.Count).Distinct().Count() != 1;
    }
}
