namespace back_end_s5_l05.Models
{
    public class Violazione
    {
        public int IdVerbale { get; set; }
        public DateTime DataViolazione { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public decimal Importo { get; set; }
        public int DecurtamentoPunti { get; set; }
    }
}
