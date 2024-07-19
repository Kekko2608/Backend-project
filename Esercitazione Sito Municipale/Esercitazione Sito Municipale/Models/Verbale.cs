using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Esercitazione_Sito_Municipale.Models
{
    public class Verbale
    {
        public int IdVerbale { get; set; }

        [Required]
        public DateTime DataViolazione { get; set; }

        [Required]
        public string IndirizzoViolazione { get; set; }

        [Required]
        public string NominativoAgente { get; set; }

        public DateTime? DataTrascrizioneVerbale { get; set; }

        [Required]
        public decimal Importo { get; set; }

        public int? DecurtamentoPunti { get; set; }

        [Required]
        public int FK_Violazione { get; set; }

        [Required]
        public int FK_Anagrafica { get; set; }

    }
}
