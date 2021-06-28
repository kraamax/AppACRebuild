using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AppAc.Application;
using AppAC.Application;
using AppAC.Domain;
using AppAC.Infrastructure.Data.ObjectMother;
using AppAC.Infrastructure.WebApi.Test.Base;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace AppAC.Infrastructure.WebApi.Test
{
     public class PlanAccionTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> 
            _factory;

        public PlanAccionTest(
            CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }
        [Fact]
        public async Task PuedeCrearPlanAccionTestAsync()
        {
            // var httpClient = _factory.CreateClient();
            var context = _factory.CreateContext();
            var actividadToAdd = ActividadMother.CreateActividad();
            context.Actividades.Add(actividadToAdd);
            context.SaveChanges();
            
            var plazoToAdd = PlazoAperturaMother.CreatePlazoApertura("123313");
            var plazoRequest = new PlazoAperturaRequest("123313", plazoToAdd.FechaInicio, plazoToAdd.FechaFin);
            var jsonObject = JsonConvert.SerializeObject(plazoRequest);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            var responseHttp = await _client.PostAsync("api/PlazoApertura", content);
            responseHttp.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var item = new ItemPlanRequest(0,"Se describe aqui","Se describe lo que se hizo","loquesea/dir");
            var items = new List<ItemPlanRequest>();
            items.Add(item);
            var request = new PlanAccionRequest(
                1,
                items
            );
            
            jsonObject = JsonConvert.SerializeObject(request);
            content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            responseHttp = await _client.PostAsync("api/PlanAccion", content);
            responseHttp.StatusCode.Should().Be(HttpStatusCode.OK);
            var respuesta = await responseHttp.Content.ReadAsStringAsync();
            respuesta.Should().Contain("Plan de accion registrado correctamente");
            var plan = context.Planes.FirstOrDefault();
            plan.Should().NotBeNull();
        }
    }
}