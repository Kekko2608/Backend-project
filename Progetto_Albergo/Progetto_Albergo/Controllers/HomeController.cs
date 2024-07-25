using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
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

        public HomeController(ILogger<HomeController> logger , IClienteService clienteService, ICameraService cameraService, IPrenotazioneService prenotazioneService, IServizioService servizioService)
        {
            _logger = logger;
            _cameraService = cameraService;
            _clienteService = clienteService;
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
        public IActionResult Addprenotazione(Prenotazione prenotazione)
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
                // Chiamata asincrona al servizio
                var prenotazioniByCodiceFiscale = await _prenotazioneService.GetPrenotazioniByCodiceFiscaleAsync(model.CodiceFiscale);

                // Ritorna la vista con le prenotazioni
                return View("GetPrenotazioniByCodiceFiscale", prenotazioniByCodiceFiscale);
            }

            // Ritorna la vista con errori se il modello non è valido
            return View(model);
        }


        public IActionResult PrenotazioneInfo(int id)
        {
            var prenotazione = _prenotazioneService.GetPrenotazioneById(id);
            if (prenotazione == null)
            {
                return NotFound(); // Gestisci il caso in cui la prenotazione non esiste
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
                // Chiamata asincrona al servizio
                var totalePrenotazioni = await _prenotazioneService.GetTotalePrenotazioniPensioneCompletaAsync();
                ViewBag.TotalePrenotazioni = totalePrenotazioni;
                return View();
            }
            catch (Exception ex)
            {
                // Gestione dell'eccezione e restituzione di una vista di errore o di un messaggio di errore
                ViewBag.ErrorMessage = "Errore nel recuperare il totale delle prenotazioni per pensione completa. Dettagli tecnici: " + ex.Message;
                return View("Error"); // Assumi che "Error" sia una vista che mostra il messaggio di errore
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
                    FK_Prenotazione = model.IdPrenotazione,
                    FK_Servizio = model.ServizioId,
                    Quantita = model.Quantita,
                    Data = model.Data,
                    Descrizione = _servizioService.GetServizioById(model.ServizioId).Descrizione
                };

                _prenotazioneService.AddServizio(servizio);

                return RedirectToAction("Dettaglio", new { id = model.IdPrenotazione });
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
