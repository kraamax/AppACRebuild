using AppAC.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppAC.Domain
{
    public class Actividad: Entity<string>, IAggregateRoot
    {
        public Actividad(TipoActividad tipoActividad)
        {
            TipoActividad = tipoActividad;
            Estado = "";
            FechaAsignacion = DateTime.Now;
        }
        public TipoActividad TipoActividad { get; set; }
        public Docente Docente { get; set; }
        public int HorasAsignadas { get; set; }
        public string Estado { get; set; }
        public DateTime FechaAsignacion { get; set; }
   
        public string Asignar(Docente docente,int horasAsignadas)
        {
            if (horasAsignadas <= 0)
            {
                return "Las horas asignadas a la actividad tienen que ser mayor a 0";
            }
            if (horasAsignadas > 20)
            {
                return "Las horas asignadas no pueden ser mayor a veinte";
            }
            Docente = docente;
            HorasAsignadas = horasAsignadas;
            return $"Se asignaron {HorasAsignadas} al docente {docente.Nombres}";
        }
    }
}
