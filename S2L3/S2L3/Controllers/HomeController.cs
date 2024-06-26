using Microsoft.AspNetCore.Mvc;
using S2L3.Models;
using System.Diagnostics;

namespace S2L3.Controllers
{
    public class HomeController : Controller
    {
        private static List<User> users = new List<User>();
        private static List<Sala> sale = new List<Sala>
    {
        new Sala { Nome = "SALA NORD" },
        new Sala { Nome = "SALA EST" },
        new Sala { Nome = "SALA SUD" }
    };


        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new HomeViewModel
            {
                Sale = sale,
                Users = users
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Prenota(string nome, string cognome, string sala, bool isRidotto)
        {
            var salaScelta = sale.FirstOrDefault(s => s.Nome == sala);
            if (salaScelta != null && salaScelta.PostiOccupati < salaScelta.PostiTotali)
            {
                users.Add(new User { Nome = nome, Cognome = cognome, Sala = sala, IsRidotto = isRidotto });
                salaScelta.PostiOccupati++;
                if (isRidotto)
                {
                    salaScelta.PostiRidotti++;
                }
            }

            return RedirectToAction("Index");
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
