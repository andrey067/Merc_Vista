namespace Merc_Vista_Blazor.ViewModel
{
    public record AcaoViewModel(long Id, 
                                string Ativo, 
                                DateTime? Data, 
                                decimal Abertura, 
                                decimal Maximo, 
                                decimal Minimo, 
                                decimal Fechamento, 
                                decimal Volume, 
                                decimal Quantidade);

}
