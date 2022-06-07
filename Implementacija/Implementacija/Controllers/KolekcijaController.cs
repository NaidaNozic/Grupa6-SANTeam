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
    public class KolekcijaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KolekcijaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Kolekcija
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kolekcija.ToListAsync());
        }

        // GET: Kolekcija/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kolekcija = await _context.Kolekcija
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kolekcija == null)
            {
                return NotFound();
            }

            return View(kolekcija);
        }

        // GET: Kolekcija/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kolekcija/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv")] Kolekcija kolekcija)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kolekcija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kolekcija);
        }

        // GET: Kolekcija/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kolekcija = await _context.Kolekcija.FindAsync(id);
            if (kolekcija == null)
            {
                return NotFound();
            }
            return View(kolekcija);
        }

        // POST: Kolekcija/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv")] Kolekcija kolekcija)
        {
            if (id != kolekcija.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kolekcija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KolekcijaExists(kolekcija.Id))
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
            return View(kolekcija);
        }

        // GET: Kolekcija/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kolekcija = await _context.Kolekcija
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kolekcija == null)
            {
                return NotFound();
            }

            return View(kolekcija);
        }

        // POST: Kolekcija/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kolekcija = await _context.Kolekcija.FindAsync(id);
            _context.Kolekcija.Remove(kolekcija);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KolekcijaExists(int id)
        {
            return _context.Kolekcija.Any(e => e.Id == id);
        }
    }
}
