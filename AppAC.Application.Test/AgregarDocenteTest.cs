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
    public class AgregarDocenteTest
    {
        private AppACContext _dbContext;
        private CrearDocenteService _crearDocenteService;
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

            _crearDocenteService = new CrearDocenteService(
                new UnitOfWork(_dbContext),
                _departamentoRepository,
                new UsuarioRepository(_dbContext),
                new MailServerFake()
                );
        }

        [Test]
        public void PuedeGuardarDocenteTest()
        {
            var docente = DocenteMother.CreateDocente("12141411");
            _departamentoRepository.Add(docente.Departamento);
            _dbContext.SaveChanges();
            var request = new DocenteRequest(
                docente.Identificacion,
                docente.Nombres,
                docente.Apellidos,
                docente.Email,
                docente.Sexo,
                docente.Departamento.Id
                );
            var response = _crearDocenteService.Handle(request).Mensaje;
            Assert.AreEqual("Se registró correctamente el docente Sebastian", response);
        }
        [Test]
        public void NoPuedeGuardarDocenteSiElDptoNoExisteTest()
        {
            var docente = DocenteMother.CreateDocente("342324214");
            _departamentoRepository.Add(docente.Departamento);
            _dbContext.SaveChanges();
            var request = new DocenteRequest(
                docente.Identificacion,
                docente.Nombres,
                docente.Apellidos,
                docente.Email,
                docente.Sexo,
                3
            );
            var response = _crearDocenteService.Handle(request).Mensaje;
            Assert.AreEqual("Se debe asignar un departamento al docente", response);
        }
    }
}