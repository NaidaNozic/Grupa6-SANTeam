using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Implementacija.Data;
using Implementacija.Models;

namespace Implementacija.Controllers
{
    public class OdgovoriController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OdgovoriController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Odgovori
        public async Task<IActionResult> Index()
        {
            return View(await _context.Odgovori.ToListAsync());
        }
    

        // GET: Odgovori/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var odgovori = await _context.Odgovori
                .FirstOrDefaultAsync(m => m.Id == id);
            if (odgovori == null)
            {
                return NotFound();
            }

            return View(odgovori);
        }

        // GET: Odgovori/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Odgovori/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,KomentarId,Autor,Tekst")] Odgovori odgovori)
        {
            if (ModelState.IsValid)
            {
                _context.Add(odgovori);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(odgovori);
        }

        // GET: Odgovori/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var odgovori = await _context.Odgovori.FindAsync(id);
            if (odgovori == null)
            {
                return NotFound();
            }
            return View(odgovori);
        }

        // POST: Odgovori/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,KomentarId,Autor,Tekst")] Odgovori odgovori)
        {
            if (id != odgovori.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(odgovori);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OdgovoriExists(odgovori.Id))
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
            return View(odgovori);
        }

        // GET: Odgovori/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var odgovori = await _context.Odgovori
                .FirstOrDefaultAsync(m => m.Id == id);
            if (odgovori == null)
            {
                return NotFound();
            }

            return View(odgovori);
        }

        // POST: Odgovori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var odgovori = await _context.Odgovori.FindAsync(id);
            _context.Odgovori.Remove(odgovori);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OdgovoriExists(int id)
        {
            return _context.Odgovori.Any(e => e.Id == id);
        }
    }
}
