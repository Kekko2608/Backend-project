using Progetto_Albergo.Models;

namespace Progetto_Albergo.Services
{
    public interface IPrenotazioneService
    {
        PrenotazioneInfo GetPrenotazioneById(int idPrenotazione);
        List<Prenotazione> GetPrenotazioniByCodiceFiscale(string codiceFiscale);
        Prenotazione AddPrenotazione(Prenotazione prenotazione);
        List<Prenotazioni_Servizi> GetServiziAggiuntiviByPrenotazione(int idPrenotazione);
        decimal GetTotaleServiziAggiuntivi(int idPrenotazione);
        List<Prenotazione> GetAllPrenotazioni();
        int GetTotalePrenotazioniPensioneCompleta();
    }       
}
