namespace Progetto_Albergo.Models
{
    public class Prenotazioni_Servizi
    {
        public int IdPrenotServizi { get; set; }
        public DateTime Data { get; set; }
        public int Quantita { get; set; }
        public decimal Prezzo { get; set; }
        public int FK_Prenotazione { get; set; }
        public int FK_Servizio { get; set; }
        public string Descrizione { get; set; }
    }
}
