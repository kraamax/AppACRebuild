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
        public DbSet<TipoActividad> TiposActividades { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Docente> Docentes { get; set; }
        public DbSet<JefeDpto> JefesDptos { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Actividad> Actividades { get; set; }
        public DbSet<PlanAccion> Planes { get; set; }
        public DbSet<ItemPlan> ItemsPlanes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlazoApertura>().HasKey(c => c.Id);
            modelBuilder.Entity<Usuario>(c=> {
                c.HasKey(c => c.Id);
            });
            modelBuilder.Entity<Actividad>().HasKey(c => c.Id);
            modelBuilder.Entity<TipoActividad>(c=> {
                c.HasKey(c => c.Id);
                c.HasAlternateKey(c => c.NombreActividad);
            });
            modelBuilder.Entity<Departamento>().HasKey(c => c.Id);
            modelBuilder.Entity<PlanAccion>().HasKey(c => c.Id);
            modelBuilder.Entity<ItemPlan>().HasKey(c => c.Id);
            modelBuilder.Entity<ItemPlan>().OwnsOne(p => p.AccionPlaneada);
            modelBuilder.Entity<ItemPlan>().OwnsOne(p => p.AccionRealizada, a=>a.OwnsOne(c=>c.Evidencia));

            modelBuilder.Entity<Departamento>().HasData(
                new Departamento("ss232", "Matematicas y Fisica") { Id=1}
            );
        }
    }
}

