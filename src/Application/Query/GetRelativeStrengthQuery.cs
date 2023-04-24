using Application.Interfaces;
using Application.Query.Responses;

namespace Application.Query
{
    public sealed record class GetRelativeStrengthQuery(List<string> ativosSelecionados,
                                                        DateTime DataInicial,
                                                        DateTime DataFinal,
                                                        int totalItensAmostrar = 10) : IQuery<GetRelativeStrengthResponse>;
}
