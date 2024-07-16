using S5.Models;

namespace S5.Nuova_cartella1
{
    public interface ISpedizioneService
    {
        IEnumerable<Spedizione> GetAllSpedizioni();
        Spedizione GetSpedizioneById(int spedizioneId);
        Spedizione CreateSpedizione(Spedizione spedizione);
        void UpdateSpedizione(Spedizione spedizione);
        void DeleteSpedizione(int spedizioneId);
    }
}
