using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Data;


[Route("api/consumatori")]
[ApiController]
public class ConsumatorsController : ControllerBase
{
    private readonly ProiectContoareContext _context;

    public ConsumatorsController(ProiectContoareContext context)
    {
        _context = context;
    }

    // GET: api/consumatori
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Consumator>>> GetConsumatori()
    {
        return await _context.Consumator
            .Select(c => new Consumator
            {
                ConsumatorId = c.ConsumatorId,
                Nume = c.Nume,
                Prenume = c.Prenume,
                Email = c.Email
            }).ToListAsync();
    }

    // GET: api/consumatori/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Consumator>> GetConsumator(int id)
    {
        var consumator = await _context.Consumator.FindAsync(id);

        if (consumator == null)
        {
            return NotFound();
        }

        return consumator;
    }

    // POST: api/consumatori
    [HttpPost]
    public async Task<ActionResult<Consumator>> CreateConsumator(Consumator consumator)
    {
        _context.Consumator.Add(consumator);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetConsumator), new { id = consumator.ConsumatorId }, consumator);
    }

    // PUT: api/consumatori/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateConsumator(int id, Consumator consumator)
    {
        if (id != consumator.ConsumatorId)
        {
            return BadRequest();
        }

        var existingConsumator = await _context.Consumator.FindAsync(id);
        if (existingConsumator == null)
        {
            return NotFound();
        }

        existingConsumator.Nume = consumator.Nume;
        existingConsumator.Prenume = consumator.Prenume;
        existingConsumator.Email = consumator.Email;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ConsumatorExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/consumatori/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteConsumator(int id)
    {
        var consumator = await _context.Consumator.FindAsync(id);

        if (consumator == null)
        {
            return NotFound();
        }

        _context.Consumator.Remove(consumator);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ConsumatorExists(int id)
    {
        return _context.Consumator.Any(e => e.ConsumatorId == id);
    }
}
