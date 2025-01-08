using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;


namespace WebAPI.Controllers
{
    [ApiController] 
    [Route("api/[controller]")] 
    public class FacturiController : ControllerBase
    {
        private readonly ProiectContoareContext _context;
        //private readonly ILogger<FacturiController> _logger; 

        public FacturiController(ProiectContoareContext context, ILogger<FacturiController> logger)
        {
            _context = context;
           // _logger = logger;
        }

        // GET: api/facturi
        [HttpGet]
        public async Task<ActionResult<List<Factura>>> GetFactura()
        {
           // _logger.LogInformation("Cerere GET pentru toate facturile a fost inițiată.");
            try
            {
                var facturi = await _context.Factura
                    .Include(f => f.Contor)
                    .Include(f => f.Plata)
                    .ToListAsync();

                if (facturi == null || !facturi.Any())
                {
                  //  _logger.LogWarning("Nu s-au găsit facturi în baza de date.");
                    return NotFound("Nu există facturi.");
                }

                //_logger.LogInformation($"S-au găsit {facturi.Count} facturi în baza de date.");
                return Ok(facturi);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "A apărut o eroare la preluarea facturilor.");
                return StatusCode(500, "A apărut o eroare internă. Vă rugăm să încercați din nou.");
            }
        }

        // GET: api/facturi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Factura>> GetFactura(int id)
        {
            //_logger.LogInformation($"Cerere GET pentru factura cu ID-ul {id} a fost inițiată.");
            try
            {
                var factura = await _context.Factura
                    .Include(f => f.Contor)
                    .Include(f => f.Plata)
                    .FirstOrDefaultAsync(f => f.FacturaId == id);

                if (factura == null)
                {
                    //_logger.LogWarning($"Factura cu ID-ul {id} nu a fost găsită.");
                    return NotFound($"Factura cu ID-ul {id} nu există.");
                }

               // _logger.LogInformation($"Factura cu ID-ul {id} a fost găsită.");
                return factura;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"A apărut o eroare la preluarea facturii cu ID-ul {id}.");
                return StatusCode(500, "A apărut o eroare internă. Vă rugăm să încercați din nou.");
            }
        }
    }
}
