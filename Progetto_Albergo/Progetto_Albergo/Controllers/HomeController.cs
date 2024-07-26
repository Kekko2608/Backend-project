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

        public HomeController(ILogger<HomeController> logger, IClienteService clienteService, ICameraService cameraService, IServizioService servizioService)
        {
            _logger = logger;
            _clienteService = clienteService;
            _cameraService = cameraService;
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
