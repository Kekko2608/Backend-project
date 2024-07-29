using Microsoft.AspNetCore.Mvc;

namespace Progetto_Pizzeria.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
