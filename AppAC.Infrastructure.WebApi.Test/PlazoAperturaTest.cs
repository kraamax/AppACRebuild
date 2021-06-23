using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
     public class PlazoAperturaTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        public PlazoAperturaTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        [Fact]
        public async Task PuedeCrearPlazoAperturaTestAsync()
        {
            #region Registro jefeDpto
                var request = new JefeDptoRequest(
                    "123fff", 
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
                var jefeDpto = context.JefesDptos.FirstOrDefault(t => t.Identificacion == "123fff");
                jefeDpto.Should().NotBeNull();
            #endregion

            #region Registro Plazo apertura
            var fechaInicio = new DateTime(2021, 02, 20);
            var fechaFin = new DateTime(2021, 03, 20);
            var plazoRequest = new PlazoAperturaRequest(
                "123fff", 
                fechaInicio,
                fechaFin
            );
            jsonObject = JsonConvert.SerializeObject(plazoRequest);
            content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            responseHttp = await httpClient.PostAsync("api/PlazoApertura", content);
            responseHttp.StatusCode.Should().Be(HttpStatusCode.OK);
            respuesta = await responseHttp.Content.ReadAsStringAsync();
            respuesta.Should().Be("El plazo fue correctamente ingresado");
            var plazoApertura = context.PlazosApertura.FirstOrDefault(t => t.Creador.Identificacion == "123fff");
            plazoApertura.Should().NotBeNull();
            

            #endregion
            
        }

       
    }
}