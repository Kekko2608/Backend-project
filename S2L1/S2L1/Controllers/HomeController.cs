using Microsoft.AspNetCore.Mvc;
using S2L1.Models;
using System.Diagnostics;

namespace S2L1.Controllers
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
            var list = new List<cibiModel>
            {
                new cibiModel {Nome = "Coca Cola", Prezzo= 2.50m},
                new cibiModel {Nome = "Insalata di Pollo", Prezzo= 5.20m},
                new cibiModel {Nome = "Pizza Margherita", Prezzo= 10.00m},
                new cibiModel {Nome = "Pizza 4 formaggi", Prezzo= 12.50m},
                new cibiModel {Nome = "Pz patatine fritte", Prezzo= 3.50m},
                new cibiModel {Nome = "Insalata di riso", Prezzo= 8.00m},
                new cibiModel {Nome = "Frutta di stagione", Prezzo= 5.00m},
                new cibiModel {Nome = "Pizza fritta", Prezzo= 5.00m},
                new cibiModel {Nome = "Piadina vegetariana", Prezzo= 6.00m},
                new cibiModel {Nome = "Panino hamburger", Prezzo= 7.90m},
            };
            return View(list);
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
