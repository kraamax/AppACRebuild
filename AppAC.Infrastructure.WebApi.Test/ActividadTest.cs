using System;
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
     public class ActividadTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> 
            _factory;

        public ActividadTest(
            CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }
        [Fact]
        public async Task PuedeCrearActividadTestAsync()
        {
            var context = _factory.CreateContext();
            var jefeDptoToAdd = JefeDptoMother.CreateJefeDpto("123454a");
            var docenteToAdd = DocenteMother.CreateDocente("1254b");
            var tipoActividadToAdd = new TipoActividad("Investigacion");

            context.JefesDptos.Add(jefeDptoToAdd);
            context.Docentes.Add(docenteToAdd);
            context.TiposActividades.Add(tipoActividadToAdd);
            context.SaveChanges();
            
            var jefeDpto = context.JefesDptos.FirstOrDefault(t => t.Identificacion == "123454a");
            var docente = context.Docentes.FirstOrDefault(t => t.Identificacion == "1254b");
            var tipo = context.TiposActividades.FirstOrDefault(t => t.NombreActividad == "Investigacion");

            jefeDpto.Should().NotBeNull();
            docente.Should().NotBeNull();
            tipo.Should().NotBeNull();
            
            var request = new ActividadRequest(
                1,
                "123454a",
                "1254b",
                10
            );
            
            var jsonObject = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            var responseHttp = await _client.PostAsync("api/Actividad", content);
            responseHttp.StatusCode.Should().Be(HttpStatusCode.OK);
            var respuesta = await responseHttp.Content.ReadAsStringAsync();
            respuesta.Should().Contain("Se asignaron 10 horas de Investigacion al docente Sebastian");
            var actividad = context.Actividades.FirstOrDefault(t => t.Asignador.Identificacion == "123454a");
            actividad.Should().NotBeNull();
        }
    }
}