using AppAC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAC.Infrastructure.Data.ObjectMother
{
    //Se puede mejorar con fluent api (desarrollada por nosotros)
    public static class PlazoAperturaMother
    {

        public static PlazoApertura CreatePlazoApertura(string identificacion)
        {
            var jefe = JefeDptoMother.CreateJefeDpto(identificacion);
            var plazo =new PlazoApertura(jefe);
            plazo.EstablecerPlazo(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(3));
            return plazo;
        }
    }
   
}
