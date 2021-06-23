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
     public class DocenteTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        public DocenteTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        [Fact]
        public async Task PuedeCrearDocenteTestAsync()
        {
            var request = new DocenteRequest(
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
            var responseHttp = await httpClient.PostAsync("api/Docente", content);
            responseHttp.StatusCode.Should().Be(HttpStatusCode.OK);
            var respuesta = await responseHttp.Content.ReadAsStringAsync();
            respuesta.Should().Contain("Se registró correctamente el docente Sebastian");
            var context = _factory.CreateContext();
            var docente123adf = context.Docentes.FirstOrDefault(t => t.Identificacion == "123adf");
            docente123adf.Should().NotBeNull();
        }

       
    }
}