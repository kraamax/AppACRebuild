using AppAC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAC.Infrastructure.Data.ObjectMother
{
    //Se puede mejorar con fluent api (desarrollada por nosotros)
    public static class DocenteMother
    {

        public static Docente CreateDocente(string identificacion) 
        {
            var dpto = DepartamentoMother.CreateDepartamento("ss001");
             return new Docente(identificacion, 
                                "Sebastian", 
                                "Onate", 
                                "sebastianonatetest@gmail.com",
                                "M",dpto);
        }
    }
    public static class DepartamentoMother
    {

        public static Departamento CreateDepartamento(string codigo)
        {
            return new Departamento(codigo,"Ingenieria de sistemas"); ;
        }
    }
}
