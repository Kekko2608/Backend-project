using Microsoft.AspNetCore.Mvc;
using S5.Models;
using S5.Nuova_cartella1;
using System.Diagnostics;

namespace S5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClienteService _clienteService;
        private readonly ISpedizioneService _spedizioneService;
        private readonly IAggiornamentoSpedizioneService _aggiornamentoSpedizioneService;

        public HomeController(
            ILogger<HomeController> logger,
            IClienteService clienteService,
            ISpedizioneService spedizioneService,
            IAggiornamentoSpedizioneService aggiornamentoSpedizioneService)
        {
            _logger = logger;
            _clienteService = clienteService;
            _spedizioneService = spedizioneService;
            _aggiornamentoSpedizioneService = aggiornamentoSpedizioneService;
        }

        public IActionResult Index()
        {
            return View(new ContattiViewModel());
        }

        [HttpPost]
        public IActionResult Contatti(ContattiViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Inviare email o salvare il messaggio nel database
                ViewBag.Messaggio = "Il tuo messaggio è stato inviato con successo!";
                ModelState.Clear();
                return View("Index", new ContattiViewModel()); // Passa un nuovo modello alla vista
            }
            return View("Index", model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Azioni per gestire i clienti
        public IActionResult Clienti()
        {
            var clienti = _clienteService.GetAllClienti();
            return View(clienti);
        }

        public IActionResult DettagliCliente(int id)
        {
            var cliente = _clienteService.GetClienteById(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        public IActionResult CreaCliente()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreaCliente(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _clienteService.CreateCliente(cliente);
                return RedirectToAction(nameof(Clienti));
            }
            return View(cliente);
        }

        public IActionResult ModificaCliente(int id)
        {
            var cliente = _clienteService.GetClienteById(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        public IActionResult ModificaCliente(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _clienteService.UpdateCliente(cliente);
                return RedirectToAction(nameof(Clienti));
            }
            return View(cliente);
        }

        public IActionResult EliminaCliente(int id)
        {
            var cliente = _clienteService.GetClienteById(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost, ActionName("EliminaCliente")]
        public IActionResult ConfermaEliminaCliente(int id)
        {
            _clienteService.DeleteCliente(id);
            return RedirectToAction(nameof(Clienti));
        }

        // Azioni per gestire le spedizioni
        public IActionResult Spedizioni()
        {
            var spedizioni = _spedizioneService.GetAllSpedizioni();
            return View(spedizioni);
        }

        public IActionResult DettagliSpedizione(int id)
        {
            var spedizione = _spedizioneService.GetSpedizioneById(id);
            if (spedizione == null)
            {
                return NotFound();
            }
            return View(spedizione);
        }

        public IActionResult CreaSpedizione()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreaSpedizione(Spedizione spedizione)
        {
            if (ModelState.IsValid)
            {
                _spedizioneService.CreateSpedizione(spedizione);
                return RedirectToAction(nameof(Spedizioni));
            }
            return View(spedizione);
        }

        public IActionResult ModificaSpedizione(int id)
        {
            var spedizione = _spedizioneService.GetSpedizioneById(id);
            if (spedizione == null)
            {
                return NotFound();
            }
            return View(spedizione);
        }

        [HttpPost]
        public IActionResult ModificaSpedizione(Spedizione spedizione)
        {
            if (ModelState.IsValid)
            {
                _spedizioneService.UpdateSpedizione(spedizione);
                return RedirectToAction(nameof(Spedizioni));
            }
            return View(spedizione);
        }

        public IActionResult EliminaSpedizione(int id)
        {
            var spedizione = _spedizioneService.GetSpedizioneById(id);
            if (spedizione == null)
            {
                return NotFound();
            }
            return View(spedizione);
        }

        [HttpPost, ActionName("EliminaSpedizione")]
        public IActionResult ConfermaEliminaSpedizione(int id)
        {
            _spedizioneService.DeleteSpedizione(id);
            return RedirectToAction(nameof(Spedizioni));
        }

        // Azioni per gestire gli aggiornamenti delle spedizioni
        public IActionResult AggiornamentiSpedizione(int spedizioneId)
        {
            var aggiornamenti = _aggiornamentoSpedizioneService.GetAllAggiornamentiBySpedizioneId(spedizioneId);
            ViewBag.SpedizioneId = spedizioneId;
            return View(aggiornamenti);
        }

        public IActionResult CreaAggiornamento(int spedizioneId)
        {
            ViewBag.SpedizioneId = spedizioneId;
            return View();
        }

        [HttpPost]
        public IActionResult CreaAggiornamento(AggiornamentoSpedizione aggiornamento)
        {
            if (ModelState.IsValid)
            {
                _aggiornamentoSpedizioneService.CreateAggiornamento(aggiornamento);
                return RedirectToAction(nameof(AggiornamentiSpedizione), new { spedizioneId = aggiornamento.SpedizioneID });
            }
            ViewBag.SpedizioneId = aggiornamento.SpedizioneID;
            return View(aggiornamento);
        }

        public IActionResult ModificaAggiornamento(int id)
        {
            var aggiornamento = _aggiornamentoSpedizioneService.GetAggiornamentoById(id);
            if (aggiornamento == null)
            {
                return NotFound();
            }
            return View(aggiornamento);
        }

        [HttpPost]
        public IActionResult ModificaAggiornamento(AggiornamentoSpedizione aggiornamento)
        {
            if (ModelState.IsValid)
            {
                _aggiornamentoSpedizioneService.UpdateAggiornamento(aggiornamento);
                return RedirectToAction(nameof(AggiornamentiSpedizione), new { spedizioneId = aggiornamento.SpedizioneID });
            }
            return View(aggiornamento);
        }

        public IActionResult EliminaAggiornamento(int id)
        {
            var aggiornamento = _aggiornamentoSpedizioneService.GetAggiornamentoById(id);
            if (aggiornamento == null)
            {
                return NotFound();
            }
            return View(aggiornamento);
        }

        [HttpPost, ActionName("EliminaAggiornamento")]
        public IActionResult ConfermaEliminaAggiornamento(int id)
        {
            var aggiornamento = _aggiornamentoSpedizioneService.GetAggiornamentoById(id);
            _aggiornamentoSpedizioneService.DeleteAggiornamento(id);
            return RedirectToAction(nameof(AggiornamentiSpedizione), new { spedizioneId = aggiornamento.SpedizioneID });
        }
    }
}
