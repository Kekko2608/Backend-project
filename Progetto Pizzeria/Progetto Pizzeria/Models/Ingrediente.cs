using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Progetto_Pizzeria.Models
{
    public class Ingrediente
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string Nome { get; set; }
        public List<Prodotto> Prodotti { get; set; } = new List<Prodotto>();
    }
}
