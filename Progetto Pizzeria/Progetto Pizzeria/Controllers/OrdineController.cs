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
                    OrdineId = ordine.Id,  
                    ProdottoId = model.ProdottoId  
                };

                ordine.ProdottiOrdinati.Add(prodottoOrdinato);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Prodotto aggiunto all'ordine." });
            }
            catch (Exception ex)
            {
               
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
                    .ThenInclude(po => po.Prodotto)  
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
                .ThenInclude(po => po.Prodotto) 
                .ToListAsync();

            return View(ordini);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public async Task<IActionResult> DettagliOrdineCompleto(int id)
        {
            var ordine = await _context.Ordini
                .Include(o => o.ProdottiOrdinati)
                    .ThenInclude(po => po.Prodotto) 
                .SingleOrDefaultAsync(o => o.Id == id);

            if (ordine == null)
            {
                return NotFound("Ordine non trovato.");
            }

            return View(ordine);
        }


        [HttpPost]
        public async Task<IActionResult> RimuoviProdotto(int prodottoOrdinatoId)
        {
            try
            {
                // Trova il prodotto ordinato da eliminare
                var prodottoOrdinato = await _context.Prodottiordinati
                    .Include(po => po.Ordine) // Include per controllare l'ordine associato
                    .SingleOrDefaultAsync(po => po.Id == prodottoOrdinatoId);

                if (prodottoOrdinato == null)
                {
                    return NotFound("Prodotto ordinato non trovato.");
                }

                // Verifica che l'ordine non sia già evaso
                if (prodottoOrdinato.Ordine.Evaso)
                {
                    return BadRequest("Non è possibile modificare un ordine già evaso.");
                }

                // Rimuovo il prodotto ordinato dall'ordine
                _context.Prodottiordinati.Remove(prodottoOrdinato);
                await _context.SaveChangesAsync();

                return RedirectToAction("Dettagli", new { id = prodottoOrdinato.OrdineId });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
                return StatusCode(500, new { message = "Si è verificato un problema durante la rimozione del prodotto dall'ordine." });
            }
        }



    }
}


