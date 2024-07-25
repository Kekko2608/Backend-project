using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Progetto_Albergo.Models;
using Progetto_Albergo.Services;
using System.Diagnostics;


namespace Progetto_Albergo.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClienteService _clienteService;
        private readonly ICameraService _cameraService;
        private readonly IPrenotazioneService _prenotazioneService;
        private readonly IServizioService _servizioService;

        public HomeController(ILogger<HomeController> logger, IClienteService clienteService, ICameraService cameraService, IPrenotazioneService prenotazioneService, IServizioService servizioService)
        {
            _logger = logger;
            _clienteService = clienteService;
            _cameraService = cameraService;
            _prenotazioneService = prenotazioneService;
            _servizioService = servizioService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // CLIENTE

        public IActionResult AddCliente()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCliente(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _clienteService.AddCliente(cliente);
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // PRENOTAZIONE 

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
                return RedirectToAction("Index");
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

        // SERVIZI

        public IActionResult AggiungiServizio(int id)
        {
            var serviziDisponibili = _servizioService.GetAllServizi();
            var viewModel = new AggiungiServizioViewModel
            {
                IdPrenotazione = id,
                ServiziDisponibili = serviziDisponibili.Select(s => new SelectListItem
                {
                    Value = s.IdServizio.ToString(),
                    Text = s.Descrizione
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AggiungiServizio(AggiungiServizioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var servizio = new Prenotazioni_Servizi
                {
                    Data = model.Data,
                    Quantita = model.Quantita,
                    Prezzo = model.Prezzo,
                    FK_Prenotazione = model.IdPrenotazione,
                    FK_Servizio = model.ServizioId
                };

                _prenotazioneService.AddServizio(servizio);
                return RedirectToAction("PrenotazioneInfo", new { id = model.IdPrenotazione });
            }

            // Ricarica i servizi disponibili se la validazione fallisce
            model.ServiziDisponibili = _servizioService.GetAllServizi().Select(s => new SelectListItem
            {
                Value = s.IdServizio.ToString(),
                Text = s.Descrizione
            }).ToList();

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
