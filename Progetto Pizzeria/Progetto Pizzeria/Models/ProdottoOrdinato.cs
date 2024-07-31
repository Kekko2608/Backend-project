using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Progetto_Pizzeria.Models
{
    public class ProdottoOrdinato
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int Quantita { get; set; }

        // Chiave esterna per l'ordine
        [Required]
        public int OrdineId { get; set; }
        public Ordine Ordine { get; set; }

        // Chiave esterna per il prodotto
        [Required]
        public int ProdottoId { get; set; }
        public Prodotto Prodotto { get; set; }
    }
}
