namespace back_end_s5_l05.Models
{
    public class StatisticheVerbale
    {
        public int IdAnagrafica { get; set; }
        public string NomeAnagrafica { get; set; }
        public string CognomeAnagrafica { get; set; }
        public DateTime DataViolazione { get; set; }
        public int TotaleVerbali { get; set; }
        public decimal Importo { get; set; }
        public int TotalePuntiDecurtati { get; set; }
    }
}

