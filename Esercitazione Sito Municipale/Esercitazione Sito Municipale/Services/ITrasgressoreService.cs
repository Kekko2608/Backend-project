using Esercitazione_Sito_Municipale.Models;

namespace Esercitazione_Sito_Municipale.Services
{
    public interface ITrasgressoreService
    {
        void CreaTrasgressore(Anagrafica trasgressore);

        IEnumerable<Anagrafica> GetAllTrasgressori();
    }
}
