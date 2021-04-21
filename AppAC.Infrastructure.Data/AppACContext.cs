using AppAC.Domain;
using AppAC.Infrastructure.Data.Base;
using Microsoft.EntityFrameworkCore;
using System;

namespace AppAC.Infrastructure.Data
{
    public class AppACContext : DbContextBase
    {
        public AppACContext(DbContextOptions options) : base(options)
        {

        }
       // public DbSet<Actividad> Actividades { get; set; }
        public DbSet<PlazoApertura> PlazosApertura { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlazoApertura>().HasKey(c => c.Id);
           // modelBuilder.Entity<TipoActividad>().HasKey(c => c.Id);
        }

    }
}

