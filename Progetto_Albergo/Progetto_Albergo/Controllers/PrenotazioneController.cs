using Microsoft.AspNetCore.Mvc;
using Progetto_Albergo.Models;
using Progetto_Albergo.Services;

namespace Progetto_Albergo.Controllers
{
    public class PrenotazioneController : Controller
    {

        private readonly IPrenotazioneService _prenotazioneService;

        public PrenotazioneController (IPrenotazioneService prenotazioneService)
        {
            _prenotazioneService = prenotazioneService;
        }

       

        public IActionResult AddPrenotazione()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPrenotazione(Prenotazione prenotazione)
        {
            if (ModelState.IsValid)
            {
                _prenotazioneService.AddPrenotazione(prenotazione);
                return RedirectToAction("Prenotazioni");
            }
            return View(prenotazione);
        }

        public IActionResult InserisciCodiceFiscale()
        {
            return View(new CodiceFiscaleViewModel());
        }

        public async Task<IActionResult> GetPrenotazioniByCodiceFiscale(CodiceFiscaleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var prenotazioniByCodiceFiscale = await _prenotazioneService.GetPrenotazioniByCodiceFiscaleAsync(model.CodiceFiscale);
                return View("GetPrenotazioniByCodiceFiscale", prenotazioniByCodiceFiscale);
            }
            return View(model);
        }

        public IActionResult PrenotazioneInfo(int id)
        {
            var prenotazione = _prenotazioneService.GetPrenotazioneById(id);
            if (prenotazione == null)
            {
                return NotFound();
            }

            var serviziAggiuntivi = _prenotazioneService.GetServiziAggiuntiviByPrenotazione(id);
            var totaleServizi = _prenotazioneService.GetTotaleServiziAggiuntivi(id);
            var importoDaSaldare = prenotazione.Tariffa - prenotazione.Caparra + totaleServizi;

            var viewModel = new DettaglioPrenotazioneViewModel
            {
                PrenotazioneInfo = prenotazione,
                ServiziAggiuntivi = serviziAggiuntivi,
                ImportoDaSaldare = importoDaSaldare
            };

            return View(viewModel);
        }

        public IActionResult Prenotazioni()
        {
            var prenotazioni = _prenotazioneService.GetAllPrenotazioni();
            return View(prenotazioni);
        }

        public async Task<IActionResult> PrenotazioniPensioneCompleta()
        {
            try
            {
                var totalePrenotazioni = await _prenotazioneService.GetTotalePrenotazioniPensioneCompletaAsync();
                ViewBag.TotalePrenotazioni = totalePrenotazioni;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Errore nel recuperare il totale delle prenotazioni per pensione completa. Dettagli tecnici: " + ex.Message;
                return View("Error");
            }
        }



        public IActionResult Index()
        {
            return View();
        }
    }
}
