using AppAC.Domain.Base;
using System;

namespace AppAC.Domain
{
    public class TipoActividad: Entity<int>, IAggregateRoot
    {
        public TipoActividad(string nombreActividad)
        {
            NombreActividad = nombreActividad;
        }
        public int TipoActividadId { get; set; }
        public string NombreActividad { get; set; }
        
    }
}
