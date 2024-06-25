using Microsoft.AspNetCore.Mvc;
using S2L2.Models;
using System.Diagnostics;
using S2L2Console;


namespace S2L2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            CV mioCV = new CV
            {
                InformazioniPersonali = new InformazioniPersonali
                {
                    Nome = "Mario",
                    Cognome = "Rossi",
                    Telefono = "3489765456",
                    Email = "mariorossi@gmail.com"
                },

                StudiEffettuati = new List<Studi>
                {
                    new Studi
                    {
                        Qualifica = "Programmatore",
                        Istituto = "EPICODE",
                        Tipo = "Full-stack",
                        Dal = new DateTime(2024, 11, 1),
                        Al = new DateTime(2024, 5, 1)
                    }
                },

                Impieghi = new List<Impiego>
                {
                    new Impiego
                    {
                        Esperienza = new Esperienza
                        {
                            Azienda = "Tech Solutions",
                            JobTitle = "Sviluppatore Web",
                            Dal = new DateTime(2019, 1, 1),
                            Al = new DateTime(2022, 1, 1),
                            Descrizione = "Sviluppo di applicazioni web",
                            Compiti = new List<string>
                            {
                                "Progettazione di architetture software",
                                "Scrittura di codice pulito e manutenibile",
                                "Collaborazione con il team di sviluppo"
                            }
                        }

                    }
                }
            };
            return View(mioCV);
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
    }
}
