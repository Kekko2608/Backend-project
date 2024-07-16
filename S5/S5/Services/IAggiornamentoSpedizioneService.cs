using S5.Models;

namespace S5.Nuova_cartella1
{
    public interface IAggiornamentoSpedizioneService
    {
        IEnumerable<AggiornamentoSpedizione> GetAllAggiornamentiBySpedizioneId(int spedizioneId);
        AggiornamentoSpedizione GetAggiornamentoById(int aggiornamentoId);
        AggiornamentoSpedizione CreateAggiornamento(AggiornamentoSpedizione aggiornamento);
        void UpdateAggiornamento(AggiornamentoSpedizione aggiornamento);
        void DeleteAggiornamento(int aggiornamentoId);
    }
}
