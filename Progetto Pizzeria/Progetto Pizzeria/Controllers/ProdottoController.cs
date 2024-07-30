using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Progetto_Pizzeria.Context;
using Progetto_Pizzeria.Models;
using Progetto_Pizzeria.Services.ProdottoService;

namespace Progetto_Pizzeria.Controllers
{
    public class ProdottoController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _ctx;
      

        public ProdottoController(DataContext dataContext, ILogger<HomeController> logger)
        {
            _ctx = dataContext;
            _logger = logger;
           
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult CreaProdotto()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreaProdotto(Prodotto model)
        {
            if (ModelState.IsValid)
            {
                var prodotto = new Prodotto
                {
                    Nome = model.Nome!,
                    Prezzo = model.Prezzo!,
                    Immagine = model.Immagine!,
                    TempoDiConsegna = model.TempoDiConsegna!,
                    Ingredienti = model.Ingredienti!,
                };
                _ctx.Prodotti.Add(prodotto);
                await _ctx.SaveChangesAsync();
                return RedirectToAction(nameof(GetAllProdotti)); 
            }
            return View("Index", model);
        }


        public async Task<IActionResult> GetAllProdotti()
        {
            var prodotti = await _ctx.Prodotti.ToListAsync();
            return View(prodotti); 
        }





    }



}




    

