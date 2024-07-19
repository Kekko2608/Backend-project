using Esercitazione_Sito_Municipale.Models;
using Esercitazione_Sito_Municipale.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Esercitazione_Sito_Municipale.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITrasgressoreService _trasgressoreService;
        private readonly IVerbaleService _verbaleService;
        private readonly IViolazioneService _violazioneService;

        public HomeController(ILogger<HomeController> logger, IVerbaleService verbaleService, IViolazioneService violazioneService, ITrasgressoreService trasgressoreService)
        {
            _logger = logger;
            _verbaleService = verbaleService;
            _violazioneService = violazioneService;
            _trasgressoreService= trasgressoreService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        /// TRASGRESSORE

        public IActionResult CreaTrasgressore()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreaTrasgressore(Anagrafica trasgressore)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _trasgressoreService.CreaTrasgressore(trasgressore);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Errore durante la creazione del trasgressore: {ex.Message}");
                }
            }
            return View(trasgressore);
        }

        /// VIOLAZIONE

        public IActionResult ElencoViolazioni()
        {
            var violazioni = _violazioneService.GetAllViolazioni().ToList();
            return View(violazioni);
        }

        public IActionResult CreaViolazione()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreaViolazione(Violazione violazione)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _violazioneService.CreaViolazione(violazione);
                    return RedirectToAction(nameof(ElencoViolazioni));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Errore durante la creazione della violazione: {ex.Message}");
                }
            }
            return View(violazione);
        }
    


    /// VERBALE

    public IActionResult ElencoVerbali()
    {
        var verbali = _verbaleService.GetAllVerbali().ToList();
        return View(verbali);
    }

    public IActionResult CreaVerbale()
    {
       
        return View();
    }

        [HttpPost]
        public IActionResult CreaVerbale(Verbale verbale)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _verbaleService.CreaVerbale(verbale);
                    return RedirectToAction(nameof(ElencoVerbali));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Errore durante la creazione del verbale: {ex.Message}");
                }
            }

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
