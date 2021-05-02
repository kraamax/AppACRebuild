using AppAC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAC.Infrastructure.Data.ObjectMother
{
    //Se puede mejorar con fluent api (desarrollada por nosotros)
    public static class JefeDptoMother
    {

        public static JefeDpto CreateJefeDpto(string identificacion) 
        {
            var dpto = DepartamentoMother.CreateDepartamento("ss001");
             return new JefeDpto(identificacion, 
                                "Kelly", 
                                "Pimienta Clavijo", 
                                "kpimienta@gmail.com",
                                "M",dpto);
        }
    }
   
}
