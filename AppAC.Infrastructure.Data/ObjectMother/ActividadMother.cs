using AppAC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAC.Infrastructure.Data.ObjectMother
{
    //Se puede mejorar con fluent api (desarrollada por nosotros)
    public static class ActividadMother
    {

        public static Actividad CreateActividad() 
        {
            var docente = DocenteMother.CreateDocente("12321313");
            var jefeDpto = JefeDptoMother.CreateJefeDpto("123313");
            var tipoActividad = new TipoActividad("Tutorias");
            var actividad = new Actividad(tipoActividad,jefeDpto);
            actividad.Asignar(docente, 10);
            return actividad;
        }
    }
}
