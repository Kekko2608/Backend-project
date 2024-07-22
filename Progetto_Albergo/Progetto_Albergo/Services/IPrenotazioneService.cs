using Progetto_Albergo.Models;

namespace Progetto_Albergo.Services
{
    public interface IPrenotazioneService
    {
        Prenotazione GetPrenotazioneById(int id);
        IEnumerable<Prenotazione> GetPrenotazioniByCodiceFiscale(string codiceFiscale);
        void AddPrenotazione(Prenotazione prenotazione);
    }
}
