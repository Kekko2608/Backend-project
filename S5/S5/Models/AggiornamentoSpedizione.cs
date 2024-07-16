using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace S5.Models
{
    public class AggiornamentoSpedizione
    {
        public int AggiornamentoID { get; set; }

        public int SpedizioneID { get; set; }

        [Required]
        public string Stato { get; set; }

        [Required]
        public string Luogo { get; set; }

        public string Descrizione { get; set; }

        [Required]
        public DateTime DataOra { get; set; }
    }
}
