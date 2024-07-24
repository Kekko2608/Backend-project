namespace Progetto_Albergo.Models
{
    public class DettaglioPrenotazioneViewModel
    {
        public PrenotazioneInfo PrenotazioneInfo { get; set; }
        public List<Prenotazioni_Servizi> ServiziAggiuntivi { get; set; }
        public decimal ImportoDaSaldare { get; set; }
    }

}
