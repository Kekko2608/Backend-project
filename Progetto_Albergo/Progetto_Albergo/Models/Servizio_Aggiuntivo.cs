using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Progetto_Albergo.Models
{
    public class Servizio_Aggiuntivo
    {
        public int IdServizio { get; set; }

        [Required]
        public string Descrizione { get; set; }


    }
}
