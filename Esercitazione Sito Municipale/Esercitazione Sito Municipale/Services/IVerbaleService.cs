using Esercitazione_Sito_Municipale.Models;

namespace Esercitazione_Sito_Municipale.Services
{
    public interface IVerbaleService
    {
        void CreaVerbale(Verbale verbale);

        IEnumerable<Verbale> GetAllVerbali();
    }
}
