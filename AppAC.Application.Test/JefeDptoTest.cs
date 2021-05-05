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
    public class JefeDptoTest
    {
        private AppACContext _dbContext;
        private CrearJefeDptoService _crearJefeDptoService;
        private DepartamentoRepository _departamentoRepository;
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
            _departamentoRepository = new DepartamentoRepository(_dbContext);

            _crearJefeDptoService = new CrearJefeDptoService(
                new UnitOfWork(_dbContext),
                _departamentoRepository,
                new JefeDptoRepository(_dbContext),
                new MailServerFake()
                );
        }

        [Test]
        public void PuedeGuardarJefeDptoTest()
        {
            var jefeDpto = JefeDptoMother.CreateJefeDpto("12141411");
            _departamentoRepository.Add(jefeDpto.Departamento);
            _dbContext.SaveChanges();
            var request = new JefeDptoRequest(
                jefeDpto.Identificacion,
                jefeDpto.Nombres,
                jefeDpto.Apellidos,
                jefeDpto.Email,
                jefeDpto.Sexo,
                jefeDpto.Departamento.Id
                );
            var response = _crearJefeDptoService.Handle(request).Mensaje;
            Assert.AreEqual("Se registró correctamente el Jefe de departamento Kelly", response);
        }
        [Test]
        public void NoPuedeGuardarJefeDptoSiElDptoNoExisteTest()
        {
            var jefeDpto = JefeDptoMother.CreateJefeDpto("12141411");
            _departamentoRepository.Add(jefeDpto.Departamento);
            _dbContext.SaveChanges();
            var request = new JefeDptoRequest(
                jefeDpto.Identificacion,
                jefeDpto.Nombres,
                jefeDpto.Apellidos,
                jefeDpto.Email,
                jefeDpto.Sexo,
                3
            );
            var response = _crearJefeDptoService.Handle(request).Mensaje;
            Assert.AreEqual("Se debe asignar un departamento al Jefe de departamento", response);
        }
    }
}