using Microsoft.AspNetCore.Mvc;
using Progetto_Municipale.Models;
using Progetto_Municipale.Services;
using System.Diagnostics;

namespace Progetto_Municipale.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAnagraficaService _anagraficaService;
        private readonly IViolazioneService _violazioneService;
        private readonly IVerbaleService _verbaleService;

        public HomeController(ILogger<HomeController> logger, IAnagraficaService anagraficaService, IViolazioneService violazioneService, IVerbaleService verbaleService)
        {
            _logger = logger;
            _anagraficaService = anagraficaService;
            _violazioneService = violazioneService;
            _verbaleService = verbaleService;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Link()
        {
            return View(Link);
        }




        ///ANAGRAFICA



        public IActionResult CreaAnagrafica()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreaAnagrafica(Anagrafica anagrafica)
        {
            if (ModelState.IsValid)
            {
                _anagraficaService.Create(anagrafica);
                return RedirectToAction("Index");
            }
            return View(anagrafica);
        }



        ///VIOLAZIONI
        
        
        
        public IActionResult CreaViolazione()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreaViolazione(Violazione violazione)
        {
            if (ModelState.IsValid)
            {
                _violazioneService.Create(violazione);
                return RedirectToAction("Index");
            }
            return View(violazione);
        }

        public IActionResult GetAllViolazioni()
        {
            var violazioni = _violazioneService.GetAllViolazioni();
            return View(violazioni);
        }

        public IActionResult ViolazioniOver10Punti()
        {
            var violazioni = _violazioneService.GetViolOver10Punti();
            return View(violazioni);
        }



        public IActionResult ViolazioniOver400Importo()
        {
            var violazioni = _violazioneService.GetViolOver400Importo();
            return View(violazioni);
        }



        ///VERBALI
        



        public IActionResult CreaVerbale()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreaVerbale(Verbale verbale)
        {
            if (ModelState.IsValid)
            {
                _verbaleService.Create(verbale);
                return RedirectToAction("Index");
            }
            return View(verbale);
        }


        public IActionResult VerbaliPerTrasgressore()
        {
            var verbaliPerTrasgressore = _verbaleService.GetAllVerbaliByTrasgr();
            return View(verbaliPerTrasgressore);
        }


        public IActionResult TrasgressorePuntiDecurtati()
        {
            var trasgrByPuntiDec = _anagraficaService.GetAllTrasgrByPuntiDec();
            return View(trasgrByPuntiDec);
        }



        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
