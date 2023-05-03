using Application.Interfaces;
using Application.Query.Responses;

namespace Application.Query
{
    public sealed record class GetRelativeStrengthQuery(List<string> AtivosSelecionados,
                                                        DateTime DataInicial,
                                                        DateTime DataFinal,
                                                        int TotalItensAmostrar = 10) : IQuery<GetRelativeStrengthResponse>;
}
