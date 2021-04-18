using System;

namespace AppAC.Domain
{
    public class TipoActividad
    {
        public TipoActividad(string nombreActividad)
        {
            NombreActividad = nombreActividad;
        }
        public int TipoActividadId { get; set; }
        public string NombreActividad { get; set; }
        
    }
}
