using AppAC.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace AppAC.Infrastructure.Data
{
    public class AppACContext:DbContext
    {
        public DbSet<Actividad> Actividades { get; set; }//equivale a Repositorios

        protected override void OnConfiguring(DbContextOptionsBuilder options)
         => options.UseSqlite(@"Data Source=C:\sqlite\blogging.db");
    }
}

