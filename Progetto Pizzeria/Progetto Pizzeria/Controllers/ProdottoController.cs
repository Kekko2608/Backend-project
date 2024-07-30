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
        public async Task<IActionResult> CreaProdotto()
        {
            var viewModel = new ProdottoViewModel
            {
                Ingredienti = await _ctx.Ingredienti.ToListAsync()
            };

            return View(viewModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreaProdotto(ProdottoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var prodotto = new Prodotto
                {
                    Nome = model.Prodotto.Nome,
                    Prezzo = model.Prodotto.Prezzo,
                    Immagine = model.Prodotto.Immagine,
                    TempoDiConsegna = model.Prodotto.TempoDiConsegna,
                    Ingredienti = await _ctx.Ingredienti
                        .Where(i => model.SelectedIngredienti.Contains(i.Id))
                        .ToListAsync()
                };

                _ctx.Prodotti.Add(prodotto);
                await _ctx.SaveChangesAsync();

                return RedirectToAction(nameof(GetAllProdotti));
            }

            // Se c'è un errore, ricarica la lista degli ingredienti
            model.Ingredienti = await _ctx.Ingredienti.ToListAsync();
            return View(model);
        }


        public async Task<IActionResult> GetAllProdotti()
        {
            var prodotti = await _ctx.Prodotti
                .Include(p => p.Ingredienti) // Include gli ingredienti associati
                .ToListAsync();
            return View(prodotti);
        }

    }

}




    

