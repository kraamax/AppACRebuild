using NUnit.Framework;
using System;

namespace AppAC.Domain.Test
{
    public class AperturaTest
    {
        /*
            Como Jefe dpto  
            Quiero Establecer una fechar de apertura y cierre 
            Para controlar los plazos de registro de las acciones realizadas por los docentes y sus evidencias.
            Criterios de Aceptaci�n
            2.1	 La fecha de inicio debe ser menor a la fecha de cierre

            Dada una plazo de apertarura comprendido entre 29/03/2021 hasta el 29/02/2021
            Cuando se van a validar las fechas de inicio y fin 
            el sistema mostrar� el mensaje "La fecha de inicio no puede ser mayor o igual a la fecha de fin";

             */
        [Test]
        public void LaFechaDeInicioNoPuedeSerMayorAlaFechaFinal()
        {
            var dpto = new Departamento("AB21", "Departamento de Ingenieria de Sistemas");
            var creador = new JefeDpto("1223425","Lucas", "Ortiz","example@gmail.com","Masculino",dpto);
            var fechaInicio = new DateTime(2021, 03, 20);
            var fechaFin = new DateTime(2021, 02, 20);
            var plazoApertura = new PlazoApertura(creador);
            var resultado=plazoApertura.EstablecerPlazo(fechaInicio, fechaFin);
            var esperado = "La fecha de inicio no puede ser mayor o igual a la fecha de fin";
            Assert.AreEqual(esperado,resultado);
        }
        /*
          Como Jefe dpto 
          Quiero Establecer una fechar de apertura y cierre 
          Para controlar los plazos de registro de las acciones realizadas por los docentes y sus evidencias.
          Criterios de Aceptaci�n
          2.1	 La fecha de inicio debe ser menor a la fecha de cierre

          Dada una plazo de apertarura comprendido entre 29/03/2021 hasta el 29/04/2021
          Cuando se van a validar las fechas de inicio y fin 
          el sistema mostrar� el mensaje "El plazo fue correctamente ingresado";

       */

        [Test]
        public void LaFechaDeFinDebeSerMayorAlaFechaDeInicio()
        {
            var dpto = new Departamento("AB21", "Departamento de Ingenieria de Sistemas");
            var creador = new JefeDpto("1223425","Lucas", "Ortiz","example@gmail.com","Masculino",dpto);
            var fechaInicio = new DateTime(2021, 02, 20);
            var fechaFin = new DateTime(2021, 03, 20);
            var plazoApertura = new PlazoApertura(creador);
            var resultado = plazoApertura.EstablecerPlazo(fechaInicio, fechaFin);
            var esperado = "El plazo fue correctamente ingresado";
            Assert.AreEqual(esperado, resultado);
        }
        /*
          Como Jefe dpto 
          Quiero Establecer una fechar de apertura y cierre 
          Para controlar los plazos de registro de las acciones realizadas por los docentes y sus evidencias.
          Criterios de Aceptaci�n
          2.1	 La fecha de inicio debe ser menor a la fecha de cierre

          Dada una plazo de apertarura comprendido entre 29/03/2021 hasta el 29/03/2021
          Cuando se van a validar las fechas de inicio y fin 
          el sistema mostrar� el mensaje "La fecha de inicio no puede ser mayor o igual a la fecha de fin";

       */
        [Test]
        public void LaFechaDeInicioNoPuedeSerIgualALaFechaDeFin()
        {
            var dpto = new Departamento("AB21", "Departamento de Ingenieria de Sistemas");
            var creador = new JefeDpto("1223425","Lucas", "Ortiz","example@gmail.com","Masculino",dpto);
            var fechaInicio = new DateTime(2021, 02, 20);
            var fechaFin = new DateTime(2021, 02, 20);
            var plazoApertura = new PlazoApertura(creador);
            var resultado = plazoApertura.EstablecerPlazo(fechaInicio, fechaFin);
            var esperado = "La fecha de inicio no puede ser mayor o igual a la fecha de fin";
            Assert.AreEqual(esperado, resultado);
        }
    }
}