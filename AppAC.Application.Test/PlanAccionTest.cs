using System;
using System.Collections.Generic;
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
    public class PlanAccionTest
    {
        private AppACContext _dbContext;
        private CrearPlanAccionService _crearPlanAccionService;
        private UsuarioRepository _usuarioRepository;
        private PlanAccionRepository _planAccionRepository;
        private ActividadRepository _actividadRepository;

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
            _planAccionRepository = new PlanAccionRepository(_dbContext);
            _actividadRepository = new ActividadRepository(_dbContext);
            _crearPlanAccionService = new CrearPlanAccionService(
                new UnitOfWork(_dbContext),
                _actividadRepository,
                _planAccionRepository,
                new MailServerFake()
                );
        }

        [Test]
        public void PuedoCrearPlanDeAccion()
        {
            var actividad = ActividadMother.CreateActividad();
            _actividadRepository.Add(actividad);
            _dbContext.SaveChanges();
            var planItem = new ItemPlan();
            var evidencia = new Evidencia();
            evidencia.Deliver("loquesea/dir");
            var accionPlaneada = new AccionPlaneada();
            accionPlaneada.Deliver("Se describe aqui");
            var accionRealizada = new AccionRealizada();
            accionRealizada.Deliver("Se describe lo que se hizo",evidencia);
            planItem.Deliver(accionPlaneada, accionRealizada);
            var items = new List<ItemPlan>();
            items.Add(planItem);
            var request = new PlanAccionRequest(1,items);
            var response = _crearPlanAccionService.Handle(request);
            response.Message.Should().Be("Plan de accion registrado correctamente");
        }
        
        [Test]
        public void NoPuedoCrearPlanDeAccionSiNoExisteUnaActividad()
        {
            
            var planItem = new ItemPlan();
            var evidencia = new Evidencia();
            evidencia.Deliver("loquesea/dir");
            var accionPlaneada = new AccionPlaneada();
            accionPlaneada.Deliver("Se describe aqui");
            var accionRealizada = new AccionRealizada();
            accionRealizada.Deliver("Se describe lo que se hizo",evidencia);
            planItem.CanDeliver(accionPlaneada, accionRealizada);
            var items = new List<ItemPlan>();
            items.Add(planItem);
            var request = new PlanAccionRequest(2,items);
            var response = _crearPlanAccionService.Handle(request);
            response.Message.Should().Be("Debe tener una actividad asignada para crear una plan de acciones");
        }
    }
}