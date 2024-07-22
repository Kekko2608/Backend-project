using System.ComponentModel.DataAnnotations;

namespace Progetto_Albergo.Models
{
    public class Camera
    {
        public int IdCamera { get; set; }

        [Required]
        public string NumeroCamera { get; set; }

        [Required]
        public string Descrizione { get; set; }

        [Required]
        public string Tipologia { get; set; }
    }
}
