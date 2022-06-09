using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Implementacija.Data;
using Implementacija.Models;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Implementacija.Controllers
{
  
    public class FilmController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;

        public FilmController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config=config;
        }

        // GET: Film
        public async Task<IActionResult> Index()
        {
            return View(await _context.Film.ToListAsync());
        }
        public async Task<IActionResult> PreporuceniFilmovi()
        {
            var movieApiKey = _config["TMDBApiKey"];
            List<Film> filmovi = new List<Film>();
            MoviesResponse movies = new MoviesResponse();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://api.themoviedb.org/3/movie/popular?api_key={movieApiKey}&language=en-US&page=1"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    movies = JsonConvert.DeserializeObject<MoviesResponse>(apiResponse);
                }
                for (int i = 0; i < movies.results.Count; i++)
                {
                    Film film = new Film();
                    film.Slika = "https://image.tmdb.org/t/p/w500/" + movies.results[i].poster_path;
                    filmovi.Add(film);
                }
            }
            return View(filmovi);
            //return View(await _context.Film.ToListAsync());
        }

        // GET: Film/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film
                .FirstOrDefaultAsync(m => m.Id == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // GET: Film/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Film/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv,GodinaObjave,Zanr,Trajanje,Sinopsis,Direktor,OcjenaIMDb,Slika")] Film film)
        {
            if (ModelState.IsValid)
            {
                _context.Add(film);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(film);
        }

        // GET: Film/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            return View(film);
        }

        // POST: Film/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv,GodinaObjave,Zanr,Trajanje,Sinopsis,Direktor,OcjenaIMDb,Slika")] Film film)
        {
            if (id != film.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(film);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmExists(film.Id))
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
            return View(film);
        }

        // GET: Film/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film
                .FirstOrDefaultAsync(m => m.Id == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // POST: Film/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var film = await _context.Film.FindAsync(id);
            _context.Film.Remove(film);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmExists(int id)
        {
            return _context.Film.Any(e => e.Id == id);
        }
    }
}
