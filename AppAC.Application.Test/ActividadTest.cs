using AppAc.Application;
using AppAC.Application.Test.Dobles;
using NUnit.Framework;
using AppAC.Domain;
using Microsoft.EntityFrameworkCore;
using AppAC.Infrastructure.Data.Base;
using AppAC.Infrastructure.Data;
using AppAC.Infrastructure.Systems;
using AppAC.Infrastructure.Data.ObjectMother;
using FluentAssertions;

namespace AppAC.Application.Test
{
    public class Tests
    {
        private AppACContext _dbContext;
        private AsignarActividadService _asignarActividadService;
        private UsuarioRepository _usuarioRepository;
        private TipoActividadRepository _tipoActividadRepository;

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
            _usuarioRepository = new UsuarioRepository(_dbContext);
            _tipoActividadRepository = new TipoActividadRepository(_dbContext);
            _asignarActividadService = new AsignarActividadService(
                new UnitOfWork(_dbContext),
                new ActividadRepository(_dbContext),
                _usuarioRepository,
                _tipoActividadRepository,
                new MailServerFake()
                );
        }

        [Test]
        public void PuedoAsignarActividadAUnDocente()
        {

            var docente = DocenteMother.CreateDocente("103523423");
            var jefeDpto = JefeDptoMother.CreateJefeDpto("11223334");
            _usuarioRepository.Add(jefeDpto);
            _usuarioRepository.Add(docente);
            var tipo = new TipoActividad("Investigaci�n");
            _tipoActividadRepository.Add(tipo);
            _dbContext.SaveChanges();
            var request = new ActividadRequest(1 ,"11223334","103523423", 10);
            var response = _asignarActividadService.Handle(request);
            response.Message.Should().Be("Se asignaron 10 al docente Sebastian");
        }
    }
}