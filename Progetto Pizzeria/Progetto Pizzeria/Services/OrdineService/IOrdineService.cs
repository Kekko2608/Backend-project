using Progetto_Pizzeria.Models;

namespace Progetto_Pizzeria.Services.OrdineService
{
    public interface IOrdineService
    {
        Ordine CreaOrdine(Ordine ordine);
        public List<Ordine> GetAllOrdini();

    }
}
