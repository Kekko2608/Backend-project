using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Progetto_Pizzeria.Context;

namespace Progetto_Pizzeria.Controllers
{
    public class IngredienteController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _ctx;


        public IngredienteController(DataContext dataContext, ILogger<HomeController> logger)
        {
            _ctx = dataContext;
            _logger = logger;

        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllIngredienti()
        {
            var ingredienti = await _ctx.Ingredienti.ToListAsync();
            return View(ingredienti);
        }

    }
}
