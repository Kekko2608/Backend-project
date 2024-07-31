using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Progetto_Pizzeria.Context;
using Progetto_Pizzeria.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Progetto_Pizzeria.Controllers
{
    [Authorize]
    public class OrdineController : Controller
    {
        private readonly DataContext _context;

        public OrdineController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AggiungiProdotto([FromBody] AggiungiProdottoModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Modello non valido." });
            }

            try
            {
                var userEmail = User.Identity.Name;
                var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == userEmail);

                if (user == null)
                {
                    return BadRequest(new { message = "Utente non trovato." });
                }

                var ordine = await _context.Ordini.SingleOrDefaultAsync(o => o.UserId == user.Id && !o.Evaso);

                if (ordine == null)
                {
                    ordine = new Ordine
                    {
                        UserId = user.Id,
                        Indirizzo = "Indirizzo da specificare",
                        Noteaggiuntive = "",
                        Evaso = false,
                    };
                    _context.Ordini.Add(ordine);
                    await _context.SaveChangesAsync();
                }

                var prodotto = await _context.Prodotti.FindAsync(model.ProdottoId);
                if (prodotto == null)
                {
                    return NotFound(new { message = "Prodotto non trovato." });
                }

                var prodottoOrdinato = new ProdottoOrdinato
                {
                    Quantita = model.Quantita,
                    OrdineId = ordine.Id,  // Associa l'ordine
                    ProdottoId = model.ProdottoId  // Associa il prodotto
                };

                ordine.ProdottiOrdinati.Add(prodottoOrdinato);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Prodotto aggiunto all'ordine." });
            }
            catch (Exception ex)
            {
                // Log dell'errore
                Console.Error.WriteLine(ex.ToString());
                return StatusCode(500, new { message = "Si è verificato un problema durante l'aggiunta del prodotto all'ordine." });
            }
        }




        public async Task<IActionResult> Dettagli()
        {
            var userEmail = User.Identity.Name;
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == userEmail);

            if (user == null)
            {
                return NotFound("Utente non trovato.");
            }

            var ordine = await _context.Ordini
                .Include(o => o.ProdottiOrdinati)
                    .ThenInclude(po => po.Prodotto)  // Assicurati di includere 'Prodotto'
                .SingleOrDefaultAsync(o => o.UserId == user.Id && !o.Evaso);

            if (ordine == null)
            {
                return NotFound("Ordine non trovato.");
            }

            return View(ordine);
        }



        [HttpPost]
        public async Task<IActionResult> ConfermaOrdine(int ordineId, string indirizzo, string noteAggiuntive)
        {
            if (string.IsNullOrWhiteSpace(indirizzo))
            {
                return BadRequest("L'indirizzo è obbligatorio.");
            }

            var userEmail = User.Identity.Name;
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == userEmail);

            if (user == null)
            {
                return BadRequest("Utente non trovato.");
            }

            var ordine = await _context.Ordini.SingleOrDefaultAsync(o => o.Id == ordineId && o.UserId == user.Id && !o.Evaso);

            if (ordine == null)
            {
                return NotFound("Ordine non trovato.");
            }

            ordine.Indirizzo = indirizzo;
            ordine.Noteaggiuntive = noteAggiuntive;
            ordine.Evaso = true; // Imposta lo stato dell'ordine come "evaso"

            _context.Ordini.Update(ordine);
            await _context.SaveChangesAsync();

            return RedirectToAction("Dettagli", new { id = ordineId });
        }

    }
}


