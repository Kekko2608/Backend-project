using System.ComponentModel.DataAnnotations;

namespace Esercitazione_Sito_Municipale.Models
{
    public class Violazione
    {
        public int IdViolazione { get; set; }

        [Required]
        public string Descrizione { get; set; }
    }
}
