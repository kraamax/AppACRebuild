using System;

namespace AppAC.Domain
{
    public class Actividad
    {
        public int IdActividad { get; set; }
        public string NombreActividad { get; set; }
        public int DocenteId { get; set; }
        public Docente Docente { get; set; }
        public int HorasAsignadas { get; set; }
        public string Estado { get; set; }
        public DateTime FechaAsignacion { get; set; }
    }
}
