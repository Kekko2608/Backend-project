using Progetto_Pizzeria.Models;

namespace Progetto_Pizzeria.Services.ProdottoService
{
    public interface IProdottoService
    {
        Prodotto CreaProdotto(Prodotto prodotto);
        Task<List<Prodotto>> GetAllProdotti();
    }
}
