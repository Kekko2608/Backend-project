using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Progetto_Pizzeria.Context;
using Progetto_Pizzeria.Models;


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
            var ordine = await _context.Ordini.SingleOrDefaultAsync(o => o.Id == ordineId);

            if (ordine == null)
            {
                return NotFound("Ordine non trovato.");
            }

            ordine.Indirizzo = indirizzo;
            ordine.Noteaggiuntive = noteAggiuntive;
            ordine.Evaso = false; // Imposta lo stato su "In Attesa" quando si conferma l'ordine

            await _context.SaveChangesAsync();

            return RedirectToAction("Dettagli");
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> EvadiOrdine(int ordineId)
        {
            var ordine = await _context.Ordini.SingleOrDefaultAsync(o => o.Id == ordineId);

            if (ordine == null)
            {
                return NotFound("Ordine non trovato.");
            }

            ordine.Evaso = true; // Imposta lo stato su "Evaso"

            await _context.SaveChangesAsync();

            return RedirectToAction("ListaOrdini");
        }

        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> ListaOrdini()
        {
            var ordini = await _context.Ordini
                .Include(o => o.ProdottiOrdinati)
                .ThenInclude(po => po.Prodotto) // Assicurati di includere i dettagli del prodotto
                .ToListAsync();

            return View(ordini);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public async Task<IActionResult> DettagliOrdineCompleto(int id)
        {
            var ordine = await _context.Ordini
                .Include(o => o.ProdottiOrdinati)
                    .ThenInclude(po => po.Prodotto) // Includi i dettagli del prodotto
                .SingleOrDefaultAsync(o => o.Id == id);

            if (ordine == null)
            {
                return NotFound("Ordine non trovato.");
            }

            return View(ordine);
        }


    }
}


