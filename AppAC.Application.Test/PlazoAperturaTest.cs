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
    public class PlazoAperturaTest
    {
        private AppACContext _dbContext;
        private PlazoAperturaService _plazoAperturaService;
        [SetUp]
        public void Setup()
        {
            //Arrange
            var optionsSqlite = new DbContextOptionsBuilder<AppACContext>()
           .UseSqlite(@"Data Source=C:\sqlite\AppACDataBaseTest.db")
           .Options;

            _dbContext = new AppACContext(optionsSqlite);
            if (!_dbContext.Database.EnsureCreated())
            {
                _dbContext.Database.EnsureCreated();
            }
            _plazoAperturaService = new PlazoAperturaService(
                new UnitOfWork(_dbContext),
                new PlazoAperturaRepository(_dbContext),
                new MailServer()
                );
        }

        [Test]
        public void Test1()
        {
            var fechaInicio = new DateTime(2021, 03, 20);
            var fechaFin = new DateTime(2021, 02, 20);
            var request = new PlazoAperturaRequest(fechaInicio,fechaFin);
            var response = _plazoAperturaService.CrearPlazoApertura(request);
            Assert.AreEqual("La fecha de inicio no puede ser mayor o igual a la fecha de fin", response);
        }
    }
}