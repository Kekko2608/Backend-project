using System.ComponentModel.DataAnnotations;

namespace S5.Models
{
    public class Cliente
    {
        public int ClienteID { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Cognome { get; set; }

        public string CodiceFiscale { get; set; }

        public string PartitaIVA { get; set; }

        [Required]
        public string Indirizzo { get; set; }

        [Required]
        public string Telefono { get; set; }

        [Required]
        public string Email { get; set; }

        public ICollection<Spedizione> Spedizioni { get; set; }
    }
}
