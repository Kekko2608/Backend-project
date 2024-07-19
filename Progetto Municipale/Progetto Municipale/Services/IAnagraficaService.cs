using Progetto_Municipale.Models;

namespace Progetto_Municipale.Services
{
    public interface IAnagraficaService
    {
        Anagrafica Create(Anagrafica anagrafica);
        List<TrasgrByPuntiDecurtati> GetAllTrasgrByPuntiDec();
    }
}
