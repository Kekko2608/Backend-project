using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Progetto_Albergo.Models
{
    public class AggiungiServizioViewModel
    {
        public int IdPrenotazione { get; set; }
        public int ServizioId { get; set; }
        public List<SelectListItem> ServiziDisponibili { get; set; }
        public int Quantita { get; set; }
        public DateTime Data { get; set; }
        public decimal Prezzo { get; set; }
    }
}
