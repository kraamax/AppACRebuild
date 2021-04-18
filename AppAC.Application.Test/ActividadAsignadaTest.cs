using AppAc.Application;
using AppAC.Application.Test.Dobles;
using NUnit.Framework;
using AppAC.Domain;

namespace AppAC.Application.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            //Arrange
            var service = new ActividadAsignadaService(
                new UnitOfWorkFake(),
                new ActividadAsignadaRepositoryFake(),
                new MailServerFake());
            var dpto = new Departamento("AB21", "Departamento de Ingenieria de Sistemas");
            var docente = new Docente("103423424", "Juan", "Perez", "chevichantoti@gmail.com", "65434343", "Masculino", dpto);
            //Act
            var response = service.AsignarActividad(1, docente, 0);
            //Assert
            Assert.AreEqual("Las horas asignadas a la actividad tienen que ser mayor a 0", response);
        }
    }
}