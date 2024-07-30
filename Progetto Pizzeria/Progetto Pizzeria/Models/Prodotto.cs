using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Progetto_Pizzeria.Models
{
    public class Prodotto
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public required string Nome { get; set; }
        [Range(0, 100)]
        public decimal Prezzo { get; set; }
        [Required, StringLength(128)]
        public required string Immagine { get; set; }
        [Range(0, 60)]
        public int TempoDiConsegna { get; set; }
        public List<Ingrediente> Ingredienti { get; set; } = = new List<Ingrediente>();
    }
}
