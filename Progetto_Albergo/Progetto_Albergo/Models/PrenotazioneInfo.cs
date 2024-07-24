namespace Progetto_Albergo.Models
{
    public class PrenotazioneInfo
    {
        public int IdPrenotazione { get; set; }
        public string NumeroStanza { get; set; }
        public DateTime DataInizio { get; set; }
        public DateTime DataFine { get; set; }
        public decimal Tariffa { get; set; }
        public decimal Caparra { get; set; }
    }
}
