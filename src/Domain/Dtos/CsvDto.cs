namespace Domain.Dtos
{
    public record struct CsvDto(int Linha,
                        string Ativo,
                        DateTime? Data,
                        decimal Abertura,
                        decimal Maximo,
                        decimal Minimo,
                        decimal Fechamento,
                        decimal Volume,
                        decimal Quantidade);

}
