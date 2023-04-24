using Application.Interfaces;
using Application.Query.Responses;

namespace Application.Query
{
    public sealed record class GetTitilesQuery() : IQuery<GetTitilesDatesResponse> { }
}
