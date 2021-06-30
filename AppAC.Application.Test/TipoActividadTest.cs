using AppAc.Application;
using AppAC.Application.Test.Dobles;
using NUnit.Framework;
using AppAC.Domain;
using AppAC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using AppAC.Infrastructure.Systems;
using System;

namespace AppAC.Application.Test
{
    public class TipoActividadTest
    {
        private AppACContext _dbContext;
        private TipoActividadService _tipoActividadService;
        [SetUp]
        public void Setup()
        {
            //Arrange
            var optionsSqlite = new DbContextOptionsBuilder<AppACContext>()
           .UseSqlite(@"Data Source=C:\sqlite\AppACDataBaseTest.db")
           .Options;

            _dbContext = new AppACContext(optionsSqlite);
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
            _tipoActividadService = new TipoActividadService(
                new UnitOfWork(_dbContext),
                new TipoActividadRepository(_dbContext),
                new MailServerFake()
                );
        }

        [Test]
        public void PuedoGuardarActividadTest()
        {
            var request = new TipoActividadRequest("Prueba");
            var response = _tipoActividadService.CrearTipoActividad(request).Mensaje;
            Assert.AreEqual("Actividad Prueba guardada", response);
        }

        [Test] public void NoPuedoGuardarActividadConElMismoNombreTest()
        {
            var request = new TipoActividadRequest("Investigación");
            _tipoActividadService.CrearTipoActividad(request);
            var response = _tipoActividadService.CrearTipoActividad(request).Mensaje;
            Assert.AreEqual("Ya existe la una actividad llamada así.", response);
        }
    }
}