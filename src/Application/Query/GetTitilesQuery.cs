using Application.Interfaces;
using Application.Query.Responses;

namespace Application.Query
{
    public sealed record class GetTitilesQuery(int RangeDate): IQuery<GetTitilesDatesResponse> { }
}
