using System.ComponentModel.DataAnnotations;

namespace Progetto_Albergo.Models
{
    public class CodiceFiscaleViewModel
    {
        [Required(ErrorMessage = "Il codice fiscale è obbligatorio.")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Il codice fiscale deve essere lungo 16 caratteri.")]
        public string CodiceFiscale { get; set; }
    }
}
