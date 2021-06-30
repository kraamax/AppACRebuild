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
            modelBuilder.Entity<Usuario>(c => { c.HasKey(c => c.Id); });
            modelBuilder.Entity<Docente>(c => { c.HasIndex(u => u.Email).IsUnique(); });
            modelBuilder.Entity<JefeDpto>(c => { c.HasIndex(u => u.Email).IsUnique(); });
            modelBuilder.Entity<Actividad>().HasKey(c => c.Id);
            modelBuilder.Entity<TipoActividad>(c =>
            {
                c.HasKey(c => c.Id);
                c.HasAlternateKey(c => c.NombreActividad);
            });
            modelBuilder.Entity<Departamento>().HasKey(c => c.Id);
            modelBuilder.Entity<PlanAccion>().HasKey(c => c.Id);
            modelBuilder.Entity<ItemPlan>().HasKey(c => c.Id);
            modelBuilder.Entity<ItemPlan>().OwnsOne(p => p.AccionPlaneada);
            modelBuilder.Entity<ItemPlan>().OwnsOne(p => p.AccionRealizada, a => a.OwnsOne(c => c.Evidencia));

            modelBuilder.Entity<Departamento>().HasData(
                new Departamento("ss232", "Prueba") { Id = 1 },
                new Departamento("ss432", "Ingeniería de Sistemas") {Id = 2},
                new Departamento("ss1233", "Ingeniería ambiental") {Id = 3},
                new Departamento("s165", "Licenciatura en Lenguas") {Id = 4},
                new Departamento("re342", "Ingeniería Electronica") {Id = 5},
                new Departamento("fs482", "Licenciatura en ciencias") { Id=6},
                new Departamento("kg213", "Matematicas y Fisica") { Id = 7 }

            );
            modelBuilder.Entity<TipoActividad>().HasData(
                new TipoActividad("Extensión") {Id = 1},
                new TipoActividad("Investigación") {Id = 2},
                new TipoActividad("Tutorias") {Id = 3},
                new TipoActividad("Asesorias") {Id = 4}
            );
            modelBuilder.Entity<Docente>().HasData(
                    new { Nombres = "UsuarioD Doc", 
                        Apellidos = "Prueba p", 
                        UserName = "prueba2@prueba", 
                        Password = "777",
                        Email = "prueba2@prueba",
                        Identificacion = "123prueba",
                        Sexo="Masculino",
                        Discriminator="Docente",
                        DepartamentoId = 1,
                        Id=1
                    }
                    );
            modelBuilder.Entity<JefeDpto>().HasData(
                    new
                    {
                        Nombres = "UsuarioJ Jefe",
                        Apellidos = "Prueba p",
                        UserName = "prueba1@prueba",
                        Password = "777",
                        Email = "prueba1@prueba",
                        Identificacion = "1233prueba",
                        Sexo = "Masculino",
                        Discriminator = "JefeDpto",
                        DepartamentoId = 1,
                        Id = 2
                    }
                    );
        }
    }
}

