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
        public string NombreActividad { get; set; }
        
    }
}
