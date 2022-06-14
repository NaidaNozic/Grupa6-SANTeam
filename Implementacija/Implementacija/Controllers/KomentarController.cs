using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Implementacija.Data;
using Implementacija.Models;
using Microsoft.AspNetCore.Identity;

namespace Implementacija.Controllers
{
    public class KomentarController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private static int _commentId;
        private static string _userWhoIsGettingAReply;

        public KomentarController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Komentar
        public async Task<IActionResult> Index()
        {
            return View(await _context.Komentar.ToListAsync());
        }

        public async Task<IActionResult> OverviewOfComments()
        {
            return View("OverviewOfComments", await _context.Komentar.ToListAsync());
        }


        // GET: Komentar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var komentar = await _context.Komentar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (komentar == null)
            {
                return NotFound();
            }

            return View(komentar);
        }
        //GET Odgovore
        public IActionResult Odgovori(int commentId,string originalAuthor)
        {
            _commentId = commentId;
            _userWhoIsGettingAReply = originalAuthor;
            var prevousReply = _context.Odgovori.ToList().FindAll(o => o.KomentarId == commentId);
            return View(prevousReply);
        }
        [HttpPost]
        public async Task<IActionResult> CreateReply(string Tekst)
        {
            Odgovori odgovor = new Odgovori();
            odgovor.KomentarId = _commentId;
            odgovor.Autor = _userManager.GetUserAsync(User).Result?.KorisnickoIme;
            odgovor.Tekst = Tekst;
            if (ModelState.IsValid)
            {
                _context.Add(odgovor);
                await _context.SaveChangesAsync();
                //moram poslati obavijest originalnom korisniku da je dobio reply
                var osoba1 = _context.Osoba.ToList().Find(o => o.KorisnickoIme == _context.Osoba.ToList().Find(q => q.KorisnickoIme==_userWhoIsGettingAReply).KorisnickoIme);
                var korisnik1=_context.Korisnik.ToList().Find(k => k.osobaId==osoba1.Id);
                Obavijest obavijest = new Obavijest
                {
                    Tekst = "You have a reply from " + odgovor.Autor + " : " + odgovor.Tekst,
                    Vrsta = VrstaObavijesti.KomentarObavijest
                };
                ObavijestVeza obavijestVeza = new ObavijestVeza
                {
                    Korisnik=korisnik1,
                    Obavijest=obavijest
                };
                _context.Add(obavijestVeza);
                await _context.SaveChangesAsync();
                return RedirectToAction("Odgovori", "Komentar" ,new {commentId = _commentId });
            }
            return View(await _context.Odgovori.ToListAsync());
        }

        // GET: Komentar/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Komentar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tekst,Autor")] Komentar komentar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(komentar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(komentar);
        }

        // GET: Komentar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var komentar = await _context.Komentar.FindAsync(id);
            if (komentar == null)
            {
                return NotFound();
            }
            return View(komentar);
        }

        // POST: Komentar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tekst,Autor")] Komentar komentar)
        {
            if (id != komentar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(komentar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KomentarExists(komentar.Id))
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
            return View(komentar);
        }

        // GET: Komentar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var komentar = await _context.Komentar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (komentar == null)
            {
                return NotFound();
            }

            return View(komentar);
        }

        // POST: Komentar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var komentar = await _context.Komentar.FindAsync(id);
            _context.Komentar.Remove(komentar);
            //obrisati sve odgovore od tog komentara
            var odgovori =  _context.Odgovori.ToList().FindAll(o => o.KomentarId==komentar.Id);
            foreach(var o in odgovori)
            {
                var odg = await _context.Odgovori.FindAsync(o.Id);
                _context.Odgovori.Remove(odg);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("OverviewOfComments", "Admin");
        }

        private bool KomentarExists(int id)
        {
            return _context.Komentar.Any(e => e.Id == id);
        }
    }
}
