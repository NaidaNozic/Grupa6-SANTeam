using Implementacija.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementacija.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Osoba> Osoba { get; set; } 
        public DbSet<Korisnik> Korisnik { get; set; }
        public DbSet<Administrator> Administrator { get; set; }
        public DbSet<Zaposlenik> Zaposlenik { get; set; }
        public DbSet<Film> Film { get; set; }
        public DbSet<Kolekcija> Kolekcija { get; set; }
        public DbSet<Obavijest> Obavijest { get; set; }
        public DbSet<Komentar> Komentar { get; set; }
        public DbSet<Glumci> Glumci { get; set; }
        public DbSet<Ocjena> Ocjena { get; set; }

        public DbSet<KolekcijaVeza> KolekcijaVeza { get; set; }
        public DbSet<FilmVeza> FilmVeza { get; set; }
        public DbSet<GlumciVeza> GlumciVeza { get; set; }
        public DbSet<KomentarVeza> KomentarVeza { get; set; }
        public DbSet<Odgovori> Odgovori { get; set; }
        public DbSet<OcjenaVeza> OcjenaVeza { get; set; }
        public DbSet<ObavijestVeza> ObavijestVeza { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Osoba>().ToTable("Osoba");
            modelBuilder.Entity<Korisnik>().ToTable("Korisnik");
            modelBuilder.Entity<Administrator>().ToTable("Administrator");
            modelBuilder.Entity<Zaposlenik>().ToTable("Zaposlenik");
            modelBuilder.Entity<Film>().ToTable("Film");
            modelBuilder.Entity<Kolekcija>().ToTable("Kolekcija");
            modelBuilder.Entity<Obavijest>().ToTable("Obavijest");
            modelBuilder.Entity<ObavijestVeza>().ToTable("ObavijestVeza");
            modelBuilder.Entity<Komentar>().ToTable("Komentar");
            modelBuilder.Entity<Glumci>().ToTable("Glumci");
            modelBuilder.Entity<Ocjena>().ToTable("Ocjena");
            modelBuilder.Entity<KolekcijaVeza>().ToTable("KolekcijaVeza");
            modelBuilder.Entity<FilmVeza>().ToTable("FilmVeza");
            modelBuilder.Entity<GlumciVeza>().ToTable("GlumciVeza");
            modelBuilder.Entity<KomentarVeza>().ToTable("KomentarVeza");
            modelBuilder.Entity<Odgovori>().ToTable("Odgovori");
            modelBuilder.Entity<OcjenaVeza>().ToTable("OcjenaVeza");
            base.OnModelCreating(modelBuilder);
        }

    }
}
