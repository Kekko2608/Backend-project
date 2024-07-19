using System.ComponentModel.DataAnnotations;

namespace Progetto_Municipale.Models
{
    public class Violazione
    {
        public int IdViolazione { get; set; }

        [Required]
        public string Descrizione { get; set; }
    }
}
