using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AppAC.Application;
using AppAC.Infrastructure.Data.ObjectMother;
using AppAC.Infrastructure.WebApi.Test.Base;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace AppAC.Infrastructure.WebApi.Test
{
     public class TipoActividadTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        public TipoActividadTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        [Fact]
        public async Task PuedeCrearJefeDptoTestAsync()
        {
            var request = new TipoActividadRequest(
                "Ejemplo" 
            );
            var jsonObject = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            var httpClient = _factory.CreateClient();
            var responseHttp = await httpClient.PostAsync("api/TipoActividad", content);
            responseHttp.StatusCode.Should().Be(HttpStatusCode.OK);
            var respuesta = await responseHttp.Content.ReadAsStringAsync();
            respuesta.Should().Contain("Actividad Ejemplo guardada");
            var context = _factory.CreateContext();
            var tipo = context.TiposActividades.FirstOrDefault(t => t.NombreActividad == "Ejemplo");
            tipo.Should().NotBeNull();
        }

       
    }
}