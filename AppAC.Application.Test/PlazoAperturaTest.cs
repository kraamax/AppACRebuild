using AppAc.Application;
using AppAC.Application.Test.Dobles;
using NUnit.Framework;
using AppAC.Domain;
using AppAC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using AppAC.Infrastructure.Systems;
using System;
using AppAC.Infrastructure.Data.ObjectMother;

namespace AppAC.Application.Test
{
    public class PlazoAperturaTest
    {
        private AppACContext _dbContext;
        private PlazoAperturaService _plazoAperturaService;
        private JefeDptoRepository _jefeDptoRepository;
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
            _jefeDptoRepository = new JefeDptoRepository(_dbContext);
            _plazoAperturaService = new PlazoAperturaService(
                new UnitOfWork(_dbContext),
                new PlazoAperturaRepository(_dbContext),
                new MailServerFake(),
                _jefeDptoRepository
                );
        }

        [Test]
        public void LaFechaDeInicioNoPuedeSerMayorALaDeFin()
        {
            var fechaInicio = new DateTime(2021, 03, 20);
            var fechaFin = new DateTime(2021, 02, 20);
            var jefeDpto = JefeDptoMother.CreateJefeDpto("1234");
            _jefeDptoRepository.Add(jefeDpto);
            _dbContext.SaveChanges();
            var request = new PlazoAperturaRequest("1234",fechaInicio,fechaFin);
            var response = _plazoAperturaService.CrearPlazoApertura(request);
            Assert.AreEqual("La fecha de inicio no puede ser mayor o igual a la fecha de fin", response);
        }
        [Test]
        public void PuedoGuardarPlazoApertura()
        {
            var fechaInicio = new DateTime(2021, 02, 20);
            var fechaFin = new DateTime(2021, 03, 20);
            var jefeDpto = JefeDptoMother.CreateJefeDpto("1234");
            _jefeDptoRepository.Add(jefeDpto);
            _dbContext.SaveChanges();
            var request = new PlazoAperturaRequest("1234",fechaInicio,fechaFin);
            var response = _plazoAperturaService.CrearPlazoApertura(request);
            Assert.AreEqual("El plazo fue correctamente ingresado", response);
        }
        [Test]
        public void NoPuedoGuardarPlazoAperturaSiNoExisteUnJefeDptoCreador()
        {
            var fechaInicio = new DateTime(2021, 02, 20);
            var fechaFin = new DateTime(2021, 03, 20);
            var request = new PlazoAperturaRequest("1234",fechaInicio,fechaFin);
            var response = _plazoAperturaService.CrearPlazoApertura(request);
            Assert.AreEqual("No existe el Jefe de departamento", response);
        }
    }
}