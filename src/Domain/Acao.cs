namespace Domain
{
    public class Acao
    {
        public long Id { get; set; }
        public string CodidoAcao { get; set; } = string.Empty;
        public string EmpresaNome { get; set; } = string.Empty;
        public DateTime Data { get; set; }
        public decimal Abertura { get; set; }
        public decimal Maximo { get; set; }
        public decimal Minimo { get; set; }
        public decimal Fechamento { get; set; }
        public decimal Volume { get; set; }
        public decimal Quantidade { get; set; }
    }
}
