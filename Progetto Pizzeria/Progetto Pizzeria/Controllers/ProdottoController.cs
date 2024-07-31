using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Progetto_Pizzeria.Context;
using Progetto_Pizzeria.Models;

namespace Progetto_Pizzeria.Controllers
{
   
    public class ProdottoController : Controller
    {
        private readonly DataContext _ctx;

        public ProdottoController(DataContext dataContext)
        {
            _ctx = dataContext;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public async Task<IActionResult> CreaProdotto()
        {
            var viewModel = new ProdottoViewModel
            {
                Ingredienti = await _ctx.Ingredienti.ToListAsync()
            };

            return View(viewModel);
        }

        [Authorize(Policy = "AdminOnly")]
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

            model.Ingredienti = await _ctx.Ingredienti.ToListAsync();
            return View(model);
        }

        public async Task<IActionResult> GetAllProdotti()
        {
            var prodotti = await _ctx.Prodotti
                .Include(p => p.Ingredienti)
                .ToListAsync();
            return View(prodotti);
        }
    }
}
