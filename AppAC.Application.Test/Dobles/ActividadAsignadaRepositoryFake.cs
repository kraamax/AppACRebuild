using System;
using System.Collections.Generic;
using System.Text;
using AppAC.Domain;
using AppAC.Domain.Contracts;

namespace AppAC.Application.Test.Dobles
{
    class ActividadAsignadaRepositoryFake : IActividadAsignadaRepository
    {
        public Actividad Find(int id)
        {
            var actividad = new TipoActividad("Asesoria");
            return new Actividad(actividad);
        }
    }
}
