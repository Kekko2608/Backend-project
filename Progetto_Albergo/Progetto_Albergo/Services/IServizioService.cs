using Progetto_Albergo.Models;

namespace Progetto_Albergo.Services
{
    public interface IServizioService
    {
        List<Servizio_Aggiuntivo> GetAllServizi();
        Servizio_Aggiuntivo GetServizioById(int id);

    }
}
