using Microsoft.AspNetCore.Mvc;
using Progetto_Albergo.Services;
using Progetto_Albergo.Models;


namespace Progetto_Albergo.Controllers.API
{
    [Route("API/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly IPrenotazioneService _prenotazioneService;

        public ApiController(IPrenotazioneService prenotazioneService)
        {
            _prenotazioneService = prenotazioneService;
        }

        [HttpGet("ByCf")]
        public async Task<ActionResult<IEnumerable<Prenotazione>>> GetByCfAsync(string cf)
        {
            if (string.IsNullOrWhiteSpace(cf))
            {
                return BadRequest("Il codice fiscale è richiesto.");
            }

            var prenotazioni = await _prenotazioneService.GetPrenotazioniByCodiceFiscaleAsync(cf);
            if (prenotazioni == null || !prenotazioni.Any())
            {
                return NotFound("Nessuna prenotazione trovata per il codice fiscale fornito.");
            }

            return Ok(prenotazioni);
        }

        [HttpGet("TotalePensioneCompleta")]
        public async Task<ActionResult<int>> GetTotalePensioneCompleta()
        {
            try
            {
                int totalePrenotazioni = await _prenotazioneService.GetTotalePrenotazioniPensioneCompletaAsync();
                return Ok(totalePrenotazioni);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Errore nel recuperare il totale delle prenotazioni per pensione completa: " + ex.Message);
            }
        }
    }
}
