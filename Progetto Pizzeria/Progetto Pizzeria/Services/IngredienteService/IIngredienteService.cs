using Progetto_Pizzeria.Models;

namespace Progetto_Pizzeria.Services.IngredienteService
{
    public interface IIngredienteService
    {
      public  List<Ingrediente> GetAllIngredienti(Prodotto Id);
    }
}
