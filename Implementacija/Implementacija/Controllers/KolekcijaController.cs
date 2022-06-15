using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Implementacija.Data;
using Implementacija.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;

namespace Implementacija.Controllers
{
    [Authorize]
    public class KolekcijaController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public KolekcijaController(ApplicationDbContext context, IConfiguration config, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _config = config;
            _userManager = userManager;
        }

        // GET: Kolekcija
        public IActionResult Index()
        {
            var osoba = _context.Osoba.ToList().Find(o => o.UserId == _userManager.GetUserAsync(User).Result?.Id);
            var korisnik = _context.Korisnik.ToList().Find(k => k.osobaId == osoba.Id);
            var kolekcije = _context.Kolekcija.ToList().FindAll(k => k.KorisnikId == korisnik.Id);
            return View(kolekcije);
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
                //first create the collection
                //then create KolekcijaVeza to connect the current user with the collection
                var osoba= _context.Osoba.ToList().Find(o => o.UserId== _userManager.GetUserAsync(User).Result?.Id);
                var korisnik = _context.Korisnik.ToList().Find(k => k.osobaId == osoba.Id);

                kolekcija.KorisnikId = korisnik.Id;

                _context.Add(kolekcija);
                await _context.SaveChangesAsync();

                /*  if (korisnik != null)
                  { 
                      KolekcijaVeza kolekcijaVeza = new KolekcijaVeza
                      {
                          Korisnik = korisnik,
                          Kolekcija=kolekcija
                      };
                      _context.Add(kolekcijaVeza);
                      await _context.SaveChangesAsync();
                  }
                  else return NotFound();*/
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
                    var osoba = _context.Osoba.ToList().Find(o => o.UserId == _userManager.GetUserAsync(User).Result?.Id);
                    var korisnik = _context.Korisnik.ToList().Find(k => k.osobaId == osoba.Id);
                    kolekcija.KorisnikId = korisnik.Id;
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

        [HttpGet("/save/{movieId}/{title}")]
        public async Task<IActionResult> SaveMovieToCollection(int movieId, string title)
        {
            var movieApiKey = _config["TMDBApiKey"];
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://api.themoviedb.org/3/movie/{movieId}?api_key={movieApiKey}&language=en-US"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    Film film = new Film();
                    ApiMovie ThisMovie = JsonConvert.DeserializeObject<ApiMovie>(apiResponse);

                    film.Naziv = ThisMovie.title;
                    film.Sinopsis = ThisMovie.overview;
                    film.Trajanje = (ThisMovie.runtime / 60).ToString() + ":" + ((int)ThisMovie.runtime % 60).ToString();
                    film.OcjenaIMDb = ThisMovie.vote_average;
                    film.GodinaObjave = int.Parse(ThisMovie.release_date.Split('-')[0]);
                    film.Slika = ThisMovie.poster_path;
                    film.tmbd_id = ThisMovie.id;
                    if (ThisMovie.genres != null)
                    {
                        foreach (var zanr in ThisMovie.genres)
                        {
                            if (film.Zanr == null) film.Zanr = zanr.name;
                            else film.Zanr = film.Zanr +"-"+ zanr.name;
                        }
                    }
                    if((_context.Film!=null && _context.Film.ToList() != null && _context.Film.ToList().Find(f =>f.Naziv==film.Naziv && f.Slika==film.Slika)==null) ||
                        _context.Film==null)
                    if (ModelState.IsValid)
                    {
                        _context.Add(film);
                        await _context.SaveChangesAsync();
                            //moram film ubaciti u kolekciju
                    }
                    ViewBag.filmId = _context.Film.ToList().Find(f => f.Naziv == film.Naziv && f.Slika == film.Slika).Id;
                }
            }
            var osoba = _context.Osoba.ToList().Find(o => o.UserId == _userManager.GetUserAsync(User).Result?.Id);
            var korisnik = _context.Korisnik.ToList().Find(k => k.osobaId == osoba.Id);
            var kolekcije = _context.Kolekcija.ToList().FindAll(k => k.KorisnikId == korisnik.Id);
            return View(kolekcije);
        }
        [HttpGet("/saveMovie/{movieId}/{collectionId}")]
        public async Task<IActionResult> SaveMovie(int movieId, int collectionId)
        {
            //moram kreirati objekat tipa medjutabele FilmVeza
            FilmVeza filmVeza = new FilmVeza
            {
                Kolekcija = _context.Kolekcija.ToList().Find(k => k.Id==collectionId),
                Film = _context.Film.ToList().Find(k => k.Id == movieId)
            };
            if (ModelState.IsValid)
            {
                if(_context.FilmVeza.ToList().Find(v => v.FilmId==movieId && v.KolekcijaId==collectionId)==null)
                _context.Add(filmVeza);
                await _context.SaveChangesAsync();
            }
            return View();
        }
    }
}
