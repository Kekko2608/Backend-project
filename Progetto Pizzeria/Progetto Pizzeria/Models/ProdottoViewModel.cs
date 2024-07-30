namespace Progetto_Pizzeria.Models
{
    public class ProdottoViewModel
    {
        public Prodotto Prodotto { get; set; }
        public List<Ingrediente> Ingredienti { get; set; } = new List<Ingrediente>();
        public List<int> SelectedIngredienti { get; set; } = new List<int>();
    }
}
