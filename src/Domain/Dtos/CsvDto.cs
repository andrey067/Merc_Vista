namespace Domain.Dtos
{
    public record struct CsvDto(string Ativo,
                        DateTime? Data,
                        decimal Abertura,
                        decimal Maximo,
                        decimal Minimo,
                        decimal Fechamento,
                        decimal Volume,
                        decimal Quantidade);

}
