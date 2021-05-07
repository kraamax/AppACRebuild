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
    public class ItemPlanTest
    {
        private AppACContext _dbContext;
        private ItemPlanService _itemPlanService;
        private PlanAccionRepository _planAccionRepository;
        private ItemPlanRepository _itemPlanRepository;
        private PlazoAperturaRepository _plazoAperturaRepository;

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
            _planAccionRepository = new PlanAccionRepository(_dbContext);
            _itemPlanRepository = new ItemPlanRepository(_dbContext);
            _plazoAperturaRepository = new PlazoAperturaRepository(_dbContext);
            _itemPlanService = new ItemPlanService(
                new UnitOfWork(_dbContext),
                _planAccionRepository,
                _itemPlanRepository,
                _plazoAperturaRepository
                );
        }

        [Test]
        public void PuedoCrearItemPlanDeAccion()
        {

            var plan=PlanAccionMother.CreatePlanAccion();
            var plazo = PlazoAperturaMother.CreatePlazoApertura("123313");
            _plazoAperturaRepository.Add(plazo);
            _planAccionRepository.Add(plan);
            _dbContext.SaveChanges();
            var request = new ItemPlanRequest(1,"Se describe aqui","Se describe lo que se hizo","loquesea/dir");
            var response = _itemPlanService.RegistrarItem(request);
            response.Message.Should().Be("Item registrado correctamente");
        }
        [Test]
        public void NoPuedoCrearItemPlanSiNoExistePlanAccion()
        {

            var plazo = PlazoAperturaMother.CreatePlazoApertura("123313");
            _plazoAperturaRepository.Add(plazo);
            _dbContext.SaveChanges();
            var request = new ItemPlanRequest(1,"Se describe aqui","Se describe lo que se hizo","loquesea/dir");
            var response = _itemPlanService.RegistrarItem(request);
            response.Message.Should().Be("No se encontró el plan de acción");
        }
        [Test]
        public void NoPuedoGuardarItemPlanSiNoEstaDentroDelPlazoDeAperturaEstablecido()
        {

            var plan=PlanAccionMother.CreatePlanAccion();
            var plazo = PlazoAperturaMother.CreatePlazoAperturaVencido("123313");
            _plazoAperturaRepository.Add(plazo);
            _planAccionRepository.Add(plan);
            _dbContext.SaveChanges();
            var request = new ItemPlanRequest(1,"Se describe aqui","Se describe lo que se hizo","loquesea/dir");
            var response = _itemPlanService.RegistrarItem(request);
            response.Message.Should().Be("Error: La fecha no esta dentro del plazo establecido por el jefe de departamento");
        }
        [Test]
        public void PuedoEliminarItemPlan()
        {
            var plan=PlanAccionMother.CreatePlanAccion();
            _planAccionRepository.Add(plan);
            var plazo = PlazoAperturaMother.CreatePlazoApertura("123313");
            _plazoAperturaRepository.Add(plazo);
            _dbContext.SaveChanges();
            var request = new ItemPlanRequest(1,"Se describe aqui","Se describe lo que se hizo","loquesea/dir");
            _itemPlanService.RegistrarItem(request);
            var response = _itemPlanService.EliminarItem(1);
            response.Message.Should().Be("Se elimino el item");
        }
        [Test]
        public void NoPuedoEliminarItemPlanSiNoExiste()
        {
            var plan=PlanAccionMother.CreatePlanAccion();
            _planAccionRepository.Add(plan);
            _dbContext.SaveChanges();
            var request = new ItemPlanRequest(1,"Se describe aqui","Se describe lo que se hizo","loquesea/dir");
            _itemPlanService.RegistrarItem(request);
            var response = _itemPlanService.EliminarItem(100);
            response.Message.Should().Be("No se encontró el item");
        }

        [Test]
        public void PuedoModificarItem()
        {
            var plan = PlanAccionMother.CreatePlanAccion();
            var plazo = PlazoAperturaMother.CreatePlazoApertura("123313");
            _plazoAperturaRepository.Add(plazo);
            _planAccionRepository.Add(plan);
            _dbContext.SaveChanges();
            var request = new ItemPlanUpdateRequest(1, "Se describe aqui dsadasdad", "Se describe lo que se hizo",
                "loquesea/dir");
            var response = _itemPlanService.ModificarItem(request);
            response.Message.Should().Be("Se actualizó el item correctamente");
        }

        [Test]
        public void NoPuedoModificarItemSiNoEstaDentroDelPlazoDeAperturaEstablecido()
        {
            var plan=PlanAccionMother.CreatePlanAccion();
            var plazo = PlazoAperturaMother.CreatePlazoAperturaVencido("123313");
            _plazoAperturaRepository.Add(plazo);
            _planAccionRepository.Add(plan);
            _dbContext.SaveChanges();
            var request = new ItemPlanUpdateRequest(1,"Se describe aqui dsadasdad","Se describe lo que se hizo","loquesea/dir");
            var response = _itemPlanService.ModificarItem(request);
            response.Message.Should().Be("Error: La fecha no esta dentro del plazo establecido por el jefe de departamento");
        }
        [Test]
        public void NoPuedoEliminarItemSiNoEstaDentroDelPlazoDeAperturaEstablecido()
        {
            var plan=PlanAccionMother.CreatePlanAccion();
            _planAccionRepository.Add(plan);
            var plazo = PlazoAperturaMother.CreatePlazoAperturaVencido("123313");
            _plazoAperturaRepository.Add(plazo);
            _dbContext.SaveChanges();
            var request = new ItemPlanRequest(1,"Se describe aqui","Se describe lo que se hizo","loquesea/dir");
            _itemPlanService.RegistrarItem(request);
            var response = _itemPlanService.EliminarItem(1);
            response.Message.Should().Be("Error: La fecha no esta dentro del plazo establecido por el jefe de departamento");
        }
    }
}