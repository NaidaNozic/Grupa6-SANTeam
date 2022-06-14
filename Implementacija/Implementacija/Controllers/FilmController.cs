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
using Microsoft.AspNetCore.Identity;

namespace Implementacija.Controllers
{

    public class FilmController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private static int idMovie;

        public FilmController(ApplicationDbContext context, IConfiguration config, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _config = config;
            _userManager = userManager;
        }
        //GET filmove kolekcije
        [HttpGet("/filmovi/{collectionId}")]
        public IActionResult CollectionMovies(int collectionId)
        {
            //i need all FilmVeza items
            List<FilmVeza> filmVeze = _context.FilmVeza.ToList().FindAll(f => f.KolekcijaId == collectionId);
            //gather all the movies from the list above
            List<Film> collectionMovies = new List<Film>();
            foreach (var f in filmVeze)
            {
                //_context.Film.ToList().Find(f1 => f1.Id==f.FilmId);
                collectionMovies.Add(_context.Film.ToList().Find(f1 => f1.Id == f.FilmId));
            }
            return View(collectionMovies);
        }

        // GET: Film
        public async Task<IActionResult> Index()
        {
            return View(await _context.Film.ToListAsync());
        }
        [HttpGet("/movie/popular")]
        public async Task<IActionResult> Recommended()
        {
            var movieApiKey = _config["TMDBApiKey"];


            MoviesResponse movieList = new MoviesResponse();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://api.themoviedb.org/3/movie/popular?language=en-US&api_key={movieApiKey}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    movieList = JsonConvert.DeserializeObject<MoviesResponse>(apiResponse);
                }
            }
            ViewBag.recommended = true;
            ViewBag.recommendedPage = 1;
            return View("Recommended", movieList.results);
        }

        [HttpGet("/movie/popular/{pageNum}")]
        public async Task<IActionResult> RecommendedPages(int pageNum)
        {
            var movieApiKey = _config["TMDBApiKey"];
            MoviesResponse movieList = new MoviesResponse();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://api.themoviedb.org/3/movie/popular?language=en-US&api_key={movieApiKey}&page={pageNum}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    movieList = JsonConvert.DeserializeObject<MoviesResponse>(apiResponse);
                }
            }
            ViewBag.recommended = true;
            ViewBag.recommendedPage = pageNum;

            return View("Recommended", movieList.results);
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
            return RedirectToAction("MoviesFromDatabase","Admin");
        }

        private bool FilmExists(int id)
        {
            return _context.Film.Any(e => e.Id == id);
        }

        [HttpPost("search/{searchTitle}")]
        public async Task<IActionResult> SearchMovie(string searchTitle)
        {
            var movieApiKey = _config["TMDBApiKey"];

            MoviesResponse movieList = new MoviesResponse();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://api.themoviedb.org/3/search/movie?api_key={movieApiKey}&query={searchTitle}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    movieList = JsonConvert.DeserializeObject<MoviesResponse>(apiResponse);
                    var searchResult = movieList.results;
                    ViewBag.result = searchResult.All(movie => movie.title == searchTitle);

                    ViewBag.title = searchTitle;
                    // Console.WriteLine(apiResponse);   
                    ViewBag.searchPage = true;

                }
            }
            return View("SearchResult", movieList.results);
        }
        [HttpPost]
        public async Task<IActionResult> CreateComment(string tekst)
        {
            Komentar komentar = new Komentar();
            komentar.Tekst = tekst;
            komentar.tmbd_id = idMovie;
            komentar.Autor = _userManager.GetUserAsync(User).Result?.KorisnickoIme;
            if (ModelState.IsValid)
            {
                _context.Add(komentar);
                await _context.SaveChangesAsync();
                return RedirectToAction("DetailPage", "Film", new { movieId = idMovie});
            }
            return View(await _context.Komentar.ToListAsync());
        }

        [HttpGet("/movie/{movieId}")]
        public async Task<IActionResult> DetailPage(int movieId)
        {

            var movieApiKey = _config["TMDBApiKey"];
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://api.themoviedb.org/3/movie/{movieId}?api_key={movieApiKey}&language=en-US"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    idMovie = movieId;

                    ViewBag.ThisMovie = JsonConvert.DeserializeObject(apiResponse);
                    ViewBag.Id = movieId;
                    ViewBag.title = ViewBag.ThisMovie.title;
                    ViewBag.overview = ViewBag.ThisMovie.overview;
                    ViewBag.runtimeHour = ViewBag.ThisMovie.runtime / 60;
                    ViewBag.runtimeMinute = (int)ViewBag.ThisMovie.runtime % 60;
                    ViewBag.vote_average = ViewBag.ThisMovie.vote_average;
                    ViewBag.release_date = ViewBag.ThisMovie.release_date;
                    ViewBag.poster_path = ViewBag.ThisMovie.poster_path;
                    ViewBag.backdrop_path = ViewBag.ThisMovie.backdrop_path;
                    if (ViewBag.ThisMovie.genres != null)
                    {
                        ViewBag.genre = ViewBag.ThisMovie.genres;
                    }

                }
            
            using (var r = await httpClient.GetAsync($"https://api.themoviedb.org/3/movie/{movieId}/credits?api_key={movieApiKey}&language=en-US"))
            {
                string apiR = await r.Content.ReadAsStringAsync();

                ViewBag.Filmic = JsonConvert.DeserializeObject(apiR);
                ViewBag.id = movieId;


                if (ViewBag.Filmic.cast != null)
                {

                    ViewBag.cast = ViewBag.Filmic.cast;

                }




            }
        }
            //return comments from that specific movie
            List<Komentar> komentari = _context.Komentar.ToList().FindAll(k => k.tmbd_id== movieId);
            //return View(await _context.Komentar.ToListAsync());
            return View(komentari);
        }
        public ActionResult SetRating(int movieId, int rank)
        {
            Ocjena ocjena = new Ocjena();
            ocjena.movieId = idMovie;
            ocjena.OcjenaKorisnika = rank;
            ocjena.userId = _userManager.GetUserId(HttpContext.User);

            _context.Ocjena.Add(ocjena);
            _context.SaveChanges();
            return RedirectToAction("DetailPage", "Film", new { movieId = idMovie });

        }

    }
}
