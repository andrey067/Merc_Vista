namespace Domain
{
    public class Ativo
    {
        public string AtivoNome { get; set; } = string.Empty;
        public DateTime? Data { get; set; }
        public decimal Abertura { get; set; }
        public decimal Maximo { get; set; }
        public decimal Minimo { get; set; }
        public decimal Fechamento { get; set; }
        public decimal Volume { get; set; }
        public decimal Quantidade { get; set; }
    }
}
