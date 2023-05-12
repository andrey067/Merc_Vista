using Application.Interfaces;
using Domain;

namespace Application.Services
{
    public class RelativeStrengthService : IRelativeStrengthService
    {
        public List<string> ObterLabel(List<List<Acao>> listaAcoes)
            => listaAcoes.SelectMany(acoes => acoes)
                         .OrderBy(a => a.Data)
                         .Select(acao => acao.Data!.ToString("dd/MM/yy"))
                         .Distinct()
                         .ToList();

        public double[] ObterValorDaAcao(List<Acao> titulo)
        {
            double[] valoresDaAcao = new double[titulo.Count()];

            titulo.Sort((a1, a2) => a2.Data.CompareTo(a1.Data));

            foreach (var acao in titulo)
            {
                var dataDiaAnterior = ObterDiaAnterior(titulo, acao);
                var fechamentoAcaoAtual = acao.Fechamento;

                if (dataDiaAnterior is not null)
                {
                    var fechamentoDiaAnterior = titulo.Where(t => t.Data == dataDiaAnterior).Select(a => a.Fechamento).FirstOrDefault();
                    var valorPorcentagem = Math.Round(((fechamentoAcaoAtual / fechamentoDiaAnterior) - 1) * 100, 4);
                    valoresDaAcao[titulo.IndexOf(acao)] = (double)valorPorcentagem;
                }
            }

            return valoresDaAcao;
        }

        public bool VerificarDatas(List<List<Acao>> listaAcoes)
        {
            int tamanhoLista = listaAcoes.FirstOrDefault()?.Count ?? 0;

            listaAcoes.ForEach(acoes => acoes.Sort((a1, a2) => a1.Data.CompareTo(a2.Data)));         

            return false;
        }

        public bool VerificarQuantidadeDeElementos(List<List<Acao>> listaAcoes)
            => listaAcoes.Select(x => x.Count).Distinct().Count() != 1;

        private DateTime? ObterDiaAnterior(List<Acao> titulo2, Acao acao)
              => titulo2.IndexOf(acao) + 1 < titulo2.Count ? titulo2[titulo2.IndexOf(acao) + 1].Data : null;
    }
}
