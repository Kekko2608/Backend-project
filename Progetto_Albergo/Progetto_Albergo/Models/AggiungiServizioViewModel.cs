using Microsoft.AspNetCore.Mvc.Rendering;

namespace Progetto_Albergo.Models
{
    public class AggiungiServizioViewModel
    {
        public int IdPrenotazione { get; set; }
        public List<SelectListItem> ServiziDisponibili { get; set; }
        public int ServizioId { get; set; }
        public int Quantita { get; set; }
        public DateTime Data { get; set; }
    }
}
