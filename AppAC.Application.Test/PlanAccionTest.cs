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
        private PlanAccionRepository _planAccionRepository;
        private PlazoAperturaRepository _plazoAperturaRepository;
        private ActividadRepository _actividadRepository;
        private PlazoAperturaService _plazoAperturaService;

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
            _plazoAperturaRepository = new PlazoAperturaRepository(_dbContext);
            _planAccionRepository = new PlanAccionRepository(_dbContext);
            _actividadRepository = new ActividadRepository(_dbContext);
            _crearPlanAccionService = new CrearPlanAccionService(
                new UnitOfWork(_dbContext),
                _actividadRepository,
                _planAccionRepository,
                _plazoAperturaRepository,
                new MailServerFake()
                );
            _plazoAperturaService = new PlazoAperturaService(
                new UnitOfWork(_dbContext),
                _plazoAperturaRepository,
                new MailServerFake(),
                new JefeDptoRepository(_dbContext)
            );
        }

        [Test]
        public void PuedoCrearPlanDeAccion()
        {
            var actividad = ActividadMother.CreateActividad();
            _actividadRepository.Add(actividad);
            _dbContext.SaveChanges();
            var plazo = PlazoAperturaMother.CreatePlazoApertura("123313");
            var plazoRequest = new PlazoAperturaRequest("123313",plazo.FechaInicio,plazo.FechaFin);
            _plazoAperturaService.CrearPlazoApertura(plazoRequest).Message.Should().Be("El plazo fue correctamente ingresado"); 
            var item = new ItemPlanRequest(0,"Se describe aqui","Se describe lo que se hizo","loquesea/dir");
            var items = new List<ItemPlanRequest>();
            items.Add(item);
            var request = new PlanAccionRequest(1,items);
            var response = _crearPlanAccionService.Handle(request);
            response.Message.Should().Be("Plan de accion registrado correctamente");
        }
        [Test]
        public void NoPuedoCrearPlanDeAccionNoEstaDentroDelPlazoDeApertura()
        {
            var actividad = ActividadMother.CreateActividad();
            _actividadRepository.Add(actividad);
            _dbContext.SaveChanges();

            var plazo = PlazoAperturaMother.CreatePlazoAperturaVencido("123313");
            var plazoRequest = new PlazoAperturaRequest("123313",plazo.FechaInicio,plazo.FechaFin);
            _plazoAperturaService.CrearPlazoApertura(plazoRequest).Message.Should().Be("El plazo fue correctamente ingresado"); 
            var item = new ItemPlanRequest(0,"Se describe aqui","Se describe lo que se hizo","loquesea/dir");
            var items = new List<ItemPlanRequest>();
            items.Add(item);
            var request = new PlanAccionRequest(1,items);
            var response = _crearPlanAccionService.Handle(request);
            response.Message.Should().Be("Error: La fecha no esta dentro del plazo establecido por el jefe de departamento");
        }
        [Test]
        public void NoPuedoCrearPlanDeAccionSiNoExisteElPlazoDeApertura()
        {
            var actividad = ActividadMother.CreateActividad();
            _actividadRepository.Add(actividad);
            _dbContext.SaveChanges();
            var item = new ItemPlanRequest(0,"Se describe aqui","Se describe lo que se hizo","loquesea/dir");
            var items = new List<ItemPlanRequest>();
            items.Add(item);
            var request = new PlanAccionRequest(1,items);
            var response = _crearPlanAccionService.Handle(request);
            response.Message.Should().Be("Error: No se encontro ningun plazo de apertura");
        }

        
        [Test]
        public void NoPuedoCrearPlanDeAccionSiNoExisteUnaActividad()
        {
            
            var item = new ItemPlanRequest(0,"Se describe aqui","Se describe lo que se hizo","loquesea/dir");
            var items = new List<ItemPlanRequest>();
            items.Add(item);
            var request = new PlanAccionRequest(2,items);
            var response = _crearPlanAccionService.Handle(request);
            response.Message.Should().Be("Debe tener una actividad asignada para crear una plan de acciones");
        }
        [Test]
        public void NoPuedoCrearMasDeUnPlanPorActividad()
        {
            var actividad = ActividadMother.CreateActividad();
            _actividadRepository.Add(actividad);
            _dbContext.SaveChanges();

            var plazo = PlazoAperturaMother.CreatePlazoApertura("123313");
            var plazoRequest = new PlazoAperturaRequest("123313",plazo.FechaInicio,plazo.FechaFin);
            _plazoAperturaService.CrearPlazoApertura(plazoRequest).Message.Should().Be("El plazo fue correctamente ingresado"); 
            var item = new ItemPlanRequest(0,"Se describe aqui","Se describe lo que se hizo","loquesea/dir");
            var items = new List<ItemPlanRequest>();
            items.Add(item);
            var request = new PlanAccionRequest(1,items);
            _crearPlanAccionService.Handle(request);
            var response = _crearPlanAccionService.Handle(request);
            response.Message.Should().Be("Ya existe un plan de acci??n para esta actividad");
        }
    }
}