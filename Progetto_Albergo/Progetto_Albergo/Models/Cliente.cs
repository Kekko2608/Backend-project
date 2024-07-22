using System.ComponentModel.DataAnnotations;

namespace Progetto_Albergo.Models
{
    public class Cliente
    {
        public int IdCliente { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Cognome { get; set; }

        [Required]
        public string Cod_Fisc { get; set; }

        public string Citta { get; set; }

        public string Provincia { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Telefono { get; set; }

        public string Cellulare { get; set; }
    }
}
