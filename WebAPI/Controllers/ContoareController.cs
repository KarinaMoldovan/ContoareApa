using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Data;

[Route("api/contoare")]
[ApiController]
public class ContoareController : ControllerBase
{
    private readonly ProiectContoareContext _context;

    public ContoareController(ProiectContoareContext context)
    {
        _context = context;
    }

    // GET: api/contoare
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Contor>>> GetContoare()
    {
        return await _context.Contor
            .Select(c => new Contor
            {
                ContorId = c.ContorId,
                NumarSerie = c.NumarSerie,
                ValoareActuala = c.ValoareActuala,
                ConsumatorId = c.ConsumatorId
            }).ToListAsync();
    }

    // GET: api/contoare/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Contor>> GetContor(int id)
    {
        var contor = await _context.Contor.FindAsync(id);

        if (contor == null)
        {
            return NotFound();
        }

        return contor;
    }

    // POST: api/contoare
    [HttpPost]
    public async Task<ActionResult<Contor>> CreateContor(Contor contor)
    {
        _context.Contor.Add(contor);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetContor), new { id = contor.ContorId }, contor);
    }

    // PUT: api/contoare/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateContor(int id, Contor contor)
    {
        if (id != contor.ContorId)
        {
            return BadRequest();
        }

        var existingContor = await _context.Contor.FindAsync(id);
        if (existingContor == null)
        {
            return NotFound();
        }

        existingContor.NumarSerie = contor.NumarSerie;
        existingContor.ValoareActuala = contor.ValoareActuala;
        existingContor.ConsumatorId = contor.ConsumatorId;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ContorExists(id))
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

    // DELETE: api/contoare/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContor(int id)
    {
        var contor = await _context.Contor.FindAsync(id);

        if (contor == null)
        {
            return NotFound();
        }

        _context.Contor.Remove(contor);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ContorExists(int id)
    {
        return _context.Contor.Any(e => e.ContorId == id);
    }
}
