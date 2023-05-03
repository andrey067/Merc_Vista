using Application.Query.Responses;
using Domain;

namespace Application.Interfaces
{
    public interface IRelativeStrengthService
    {
        bool VerificarDatas(List<List<Acao>> listaAcoes);
        double[] ObterValorDaAcao(List<Acao> titulo);
        List<string> ObterLabel(List<List<Acao>> listaAcoes);
        bool VerificarQuantidadeDeElementos(List<List<Acao>> listaAcoes);
    }
}
