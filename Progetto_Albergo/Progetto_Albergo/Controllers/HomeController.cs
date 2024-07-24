using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Progetto_Albergo.Models;
using Progetto_Albergo.Services;
using System.Diagnostics;



namespace Progetto_Albergo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClienteService _clienteService;
        private readonly ICameraService _cameraService;
        private readonly IPrenotazioneService _prenotazioneService;

        public HomeController(ILogger<HomeController> logger , IClienteService clienteService, ICameraService cameraService, IPrenotazioneService prenotazioneService)
        {
            _logger = logger;
            _cameraService = cameraService;
            _clienteService = clienteService;
            _prenotazioneService = prenotazioneService;
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

        public IActionResult GetPrenotazioniByCodiceFiscale(CodiceFiscaleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var prenotazioniByCodiceFiscale = _prenotazioneService.GetPrenotazioniByCodiceFiscale(model.CodiceFiscale);
                return View("GetPrenotazioniByCodiceFiscale", prenotazioniByCodiceFiscale); // Assumi che PrenotazioniList sia la view che mostra le prenotazioni
            }

            return View(model); // Ritorna la vista con errori se il modello non è valido
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


        public IActionResult PrenotazioniPensioneCompleta()
        {
            var totalePrenotazioni = _prenotazioneService.GetTotalePrenotazioniPensioneCompleta();
            ViewBag.TotalePrenotazioni = totalePrenotazioni;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
