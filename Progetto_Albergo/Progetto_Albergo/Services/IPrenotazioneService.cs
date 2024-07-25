using Progetto_Albergo.Models;

namespace Progetto_Albergo.Services
{
    public interface IPrenotazioneService
    {
        PrenotazioneInfo GetPrenotazioneById(int idPrenotazione);
        Task<List<Prenotazione>> GetPrenotazioniByCodiceFiscaleAsync(string codiceFiscale);
        Prenotazione AddPrenotazione(Prenotazione prenotazione);
        List<Prenotazioni_Servizi> GetServiziAggiuntiviByPrenotazione(int idPrenotazione);
        decimal GetTotaleServiziAggiuntivi(int idPrenotazione);
        List<Prenotazione> GetAllPrenotazioni();
        Task<int> GetTotalePrenotazioniPensioneCompletaAsync();
    }       
}
