using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace S5.Models
{
    public class Spedizione
    {
        public int SpedizioneID { get; set; }

        public int ClienteID { get; set; }

        [Required]
        public string NumeroIdentificativo { get; set; }

        [Required]
        public DateTime DataSpedizione { get; set; }

        [Required]
        public decimal Peso { get; set; }

        [Required]
        public string CittaDestinataria { get; set; }

        [Required]
        public string IndirizzoDestinatario { get; set; }

        [Required]
        public string NominativoDestinatario { get; set; }

        [Required]
        public decimal Costo { get; set; }

        [Required]
        public DateTime DataConsegnaPrevista { get; set; }

        public ICollection<AggiornamentoSpedizione> AggiornamentiSpedizione { get; set; }
    }
}
