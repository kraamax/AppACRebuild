using NUnit.Framework;

namespace AppAC.Domain.Test
{
    public class ActividadTests
    {
        [SetUp]
        public void Setup()
        {
        }
        /*
            Como Jefe de Departamento 
            Quiero asignar actividades complementarias a los docentes 
            Para que este pueda consultar las actividades que debe realizar durante el período académico.
            Criterios de Aceptación
            1.	Las horas asignadas a la actividad tienen que ser mayor a 0
            Dada una actividad complementaria "Tutorias", 
            cuando se va asignar a asignar al docente 103423424 Juan Perez, 0 horas
            el sistema mostrará el mensaje "Las horas asignadas a la actividad tienen que ser mayor a 0"
             
             */
        [Test]
        public void LasHorasAsignadaNoPuedenSerIgualACero()
        {
            var dpto = new Departamento("AB21", "Departamento de Ingenieria de Sistemas");
            var docente = new Docente("103423424","Juan", "Perez","chevichantoti@gmail.com","65434343","Masculino",dpto);
            var actividad = new TipoActividad("Asesoria");
            var actividadAsignada = new Actividad(actividad);
            var resultado = actividadAsignada.AsignarActividad(docente,0);
            var esperado = "Las horas asignadas a la actividad tienen que ser mayor a 0";
            Assert.AreEqual(esperado,resultado);
        }
        /*
            Como Jefe de Departamento 
            Quiero asignar actividades complementarias a los docentes 
            Para que este pueda consultar las actividades que debe realizar durante el período académico.
            Criterios de Aceptación
            1.	Las horas asignadas a la actividad tienen que ser mayor a 0
            Dada una actividad complementaria "Tutorias", 
            cuando se va asignar a asignar al docente 103423424 Juan Perez, -1 horas
            el sistema mostrará el mensaje "Las horas asignadas a la actividad tienen que ser mayor a 0"
             
             */
        [Test]
        public void LasHorasAsignadaNoPuedenSerMenorACero()
        {
            var dpto = new Departamento("AB21", "Departamento de Ingenieria de Sistemas");
            var docente = new Docente("103423424", "Juan", "Perez", "chevichantoti@gmail.com", "65434343", "Masculino", dpto);
            var actividad = new TipoActividad("Asesoria");
            var actividadAsignada = new Actividad(actividad);
            var resultado = actividadAsignada.AsignarActividad(docente,-1);
            var esperado = "Las horas asignadas a la actividad tienen que ser mayor a 0";
            Assert.AreEqual(esperado, resultado);
        }
        /*
           Como Jefe de Departamento 
           Quiero asignar actividades complementarias a los docentes 
           Para que este pueda consultar las actividades que debe realizar durante el período académico.
           Criterios de Aceptación
           1.	Las horas asignadas a la actividad tienen que ser mayor a 0
           Dada una actividad complementaria "Tutorias", 
           cuando se va asignar a asignar al docente 103423424 Juan Perez, 5 horas
           el sistema mostrará el mensaje "Se asignaron 5 al docente Juan";

            */
        [Test]
        public void LasHorasAsignadaDebenSerMayorACero()
        {
            var dpto = new Departamento("AB21", "Departamento de Ingenieria de Sistemas");
            var docente = new Docente("103423424", "Juan", "Perez", "chevichantoti@gmail.com", "65434343", "Masculino", dpto);
            var actividad = new TipoActividad("Asesoria");
            var actividadAsignada = new Actividad(actividad);
            var resultado = actividadAsignada.AsignarActividad(docente,5);
            var esperado = "Se asignaron 5 al docente Juan";
            Assert.AreEqual(esperado, resultado);
        }
        /*
           Como Jefe de Departamento 
           Quiero asignar actividades complementarias a los docentes 
           Para que este pueda consultar las actividades que debe realizar durante el período académico.
           Criterios de Aceptación
           1.	Las horas asignadas a la actividad tienen que ser no pueden ser mayor a 20
           Dada una actividad complementaria "Tutorias", 
           cuando se va asignar a asignar al docente 103423424 Juan Perez, 25 horas
           el sistema mostrará el mensaje "Se asignaron 5 al docente Juan";

            */
        [Test]
        public void LasHorasAsignadasNoPuedenSerMayorAVeinte()
        {
            var dpto = new Departamento("AB21", "Departamento de Ingenieria de Sistemas");
            var docente = new Docente("103423424", "Juan", "Perez", "chevichantoti@gmail.com", "65434343", "Masculino", dpto);
            var actividad = new TipoActividad("Asesoria");
            var actividadAsignada = new Actividad(actividad);
            var resultado = actividadAsignada.AsignarActividad(docente,25);
            var esperado = "Las horas asignadas no pueden ser mayor a veinte";
            Assert.AreEqual(esperado, resultado);
        }
    }
}