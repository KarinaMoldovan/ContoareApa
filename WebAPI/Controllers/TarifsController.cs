using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class TarifsController : Controller
    {
        private readonly ProiectContoareContext _context;

        public TarifsController(ProiectContoareContext context)
        {
            _context = context;
        }

        // GET: Tarifs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tarif.ToListAsync());
        }

        // GET: Tarifs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarif = await _context.Tarif
                .FirstOrDefaultAsync(m => m.TarifId == id);
            if (tarif == null)
            {
                return NotFound();
            }

            return View(tarif);
        }

        // GET: Tarifs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tarifs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TarifId,PretPeMetruCub,DataInceput,DataSfarsit")] Tarif tarif)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tarif);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tarif);
        }

        // GET: Tarifs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarif = await _context.Tarif.FindAsync(id);
            if (tarif == null)
            {
                return NotFound();
            }
            return View(tarif);
        }

        // POST: Tarifs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TarifId,PretPeMetruCub,DataInceput,DataSfarsit")] Tarif tarif)
        {
            if (id != tarif.TarifId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarif);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TarifExists(tarif.TarifId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tarif);
        }

        // GET: Tarifs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarif = await _context.Tarif
                .FirstOrDefaultAsync(m => m.TarifId == id);
            if (tarif == null)
            {
                return NotFound();
            }

            return View(tarif);
        }

        // POST: Tarifs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarif = await _context.Tarif.FindAsync(id);
            if (tarif != null)
            {
                _context.Tarif.Remove(tarif);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TarifExists(int id)
        {
            return _context.Tarif.Any(e => e.TarifId == id);
        }
    }
}
