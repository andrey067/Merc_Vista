using Application.Interfaces;
using Domain;

namespace Application.Query
{
    public sealed record class GetRelativeStrengthQuery(): IQuery<IEnumerable<Acao>> { }
}
