using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarifsController : ControllerBase
    {
        private readonly ProiectContoareContext _context;

        public TarifsController(ProiectContoareContext context)
        {
            _context = context;
        }

        // GET: api/Tarifs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarif>>> GetTarifs()
        {
            return await _context.Tarif.ToListAsync();
        }

        // GET: api/Tarifs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarif>> GetTarif(int id)
        {
            var tarif = await _context.Tarif.FindAsync(id);

            if (tarif == null)
            {
                return NotFound();
            }

            return tarif;
        }

        // POST: api/Tarifs
        [HttpPost]
        public async Task<ActionResult<Tarif>> PostTarif(Tarif tarif)
        {
            _context.Tarif.Add(tarif);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTarif), new { id = tarif.TarifId }, tarif);
        }

        // DELETE: api/Tarifs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarif(int id)
        {
            var tarif = await _context.Tarif.FindAsync(id);
            if (tarif == null)
            {
                return NotFound();
            }

            _context.Tarif.Remove(tarif);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
