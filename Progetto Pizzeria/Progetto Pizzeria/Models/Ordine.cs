using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Progetto_Pizzeria.Models
{
    public class Ordine
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public bool Evaso { get; set; }

        [Required]
        public string Indirizzo { get; set; }

        public string Noteaggiuntive { get; set; }

        public DateTime DataOrdine { get; set; }

        [Required]
        public List<ProdottoOrdinato> ProdottiOrdinati { get; set; } = new();
    }
}
