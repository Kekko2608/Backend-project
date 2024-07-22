using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Progetto_Albergo.Models
{
    public class Prenotazione
    {
        public int IdPrenotazione { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        public int Numero { get; set; }

        [Required]
        public int Anno { get; set; }

        [Required]
        public DateTime Dal { get; set; }

        [Required]
        public DateTime Al { get; set; }

        public decimal? Caparra { get; set; }

        public decimal? Tariffa { get; set; }

        [Required]
        public string Descrizione { get; set; }

        [Required]
        public int FK_Cliente { get; set; }

        [Required]
        public int FK_Camera { get; set; }
    }
}
