namespace Application.Query.Responses
{
    public record class GetTitilesDatesResponse(List<string> Ativo, DateTime DataInicial, DateTime DataFinal);
}
