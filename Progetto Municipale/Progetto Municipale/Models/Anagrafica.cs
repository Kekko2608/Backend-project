using System.ComponentModel.DataAnnotations;

namespace Progetto_Municipale.Models
{
    public class Anagrafica
    {
        public int IdAnagrafica { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Cognome { get; set; }

        [Required]
        public string Indirizzo { get; set; }

        [Required]
        public string Citta { get; set; }

        [Required]
        public string CAP { get; set; }

        [Required]
        public string Cod_Fisc { get; set; }
    }
}
