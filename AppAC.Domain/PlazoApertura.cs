using System;
using System.Collections.Generic;
using System.Text;

namespace AppAC.Domain
{
    public class PlazoApertura
    {
        public PlazoApertura(int id, DateTime fechaInicio, DateTime fechaFin)
        {
            IdApertura = id;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            Activo = true;
        }
        public int IdApertura { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Activo { get; set; }
        public String ValidarFechas(){
            
            if (FechaInicio >= FechaFin)
            {
                return "La fecha de inicio no puede ser mayor o igual a la fecha de fin";
            }
            else {
                return "El plazo fue correctamente ingresado";
            }
            throw new NotImplementedException();
        }

    }
}
