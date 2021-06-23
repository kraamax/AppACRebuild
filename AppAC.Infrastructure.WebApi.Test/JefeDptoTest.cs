using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AppAC.Application;
using AppAC.Infrastructure.WebApi.Test.Base;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace AppAC.Infrastructure.WebApi.Test
{
     public class JefeDptoTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        public JefeDptoTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        [Fact]
        public async Task PuedeCrearJefeDptoTestAsync()
        {
            var request = new JefeDptoRequest(
                "123adf", 
                "Sebastian",
                "Oñate",
                "ssonate@unicesar.edu.co",
                "M",
                1
            );
            var jsonObject = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            var httpClient = _factory.CreateClient();
            var responseHttp = await httpClient.PostAsync("api/jefeDpto", content);
            responseHttp.StatusCode.Should().Be(HttpStatusCode.OK);
            var respuesta = await responseHttp.Content.ReadAsStringAsync();
            respuesta.Should().Contain("Se registró correctamente el Jefe de departamento Sebastian");
            var context = _factory.CreateContext();
            var jefeDpto = context.JefesDptos.FirstOrDefault(t => t.Identificacion == "123adf");
            jefeDpto.Should().NotBeNull();
        }

       
    }
}