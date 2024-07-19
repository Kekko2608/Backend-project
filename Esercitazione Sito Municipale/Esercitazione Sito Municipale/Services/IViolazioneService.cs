using Esercitazione_Sito_Municipale.Models;

namespace Esercitazione_Sito_Municipale.Services
{
    public interface IViolazioneService
    {
        void CreaViolazione(Violazione violazione);

        IEnumerable<Violazione> GetAllViolazioni();
    }
}
