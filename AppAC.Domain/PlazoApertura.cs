using AppAC.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppAC.Domain
{
    public class PlazoApertura: Entity<int>, IAggregateRoot
    {
        public PlazoApertura()
        {
            Activo = true;
        }
        public DateTime FechaInicio { get; private set; }
        public DateTime FechaFin { get; private set; }
        public bool Activo { get; private set; }
        public String EstablecerPlazo(DateTime fechaInicio, DateTime fechaFin){
            if (fechaInicio >= fechaFin)
            {
                return "La fecha de inicio no puede ser mayor o igual a la fecha de fin";
            }
            else {
                FechaInicio = fechaInicio;
                FechaFin = fechaFin;
                return "El plazo fue correctamente ingresado";
            }
        }

    }
}
