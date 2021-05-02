using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppAC.Domain
{
    public class Docente : Usuario
    {
        public Docente(string identificacion, string nombres, string apellidos, string email, string sexo) : base(identificacion, nombres, apellidos, email, sexo)
        {
        }

        public Docente(string identificacion, string nombres, string apellidos, string email, string sexo, Departamento departamento) : base(identificacion, nombres, apellidos, email, sexo)
        {
            Departamento = departamento;
        }

        public Departamento Departamento { get; set; }
        
        public IReadOnlyList<string> CanDeliver(string identificacion, string nombres, string apellidos, string email, string sexo, Departamento departamento) {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(identificacion))
                errors.Add("Debe ingresar un codigo");

            if (string.IsNullOrWhiteSpace(nombres))
                errors.Add("Debe ingresar los nombres");
            
            if (string.IsNullOrWhiteSpace(apellidos))
                errors.Add("Debe ingresar los apellidos");
            
            if (string.IsNullOrWhiteSpace(email))
                errors.Add("Debe ingresar un email");
            
            if (string.IsNullOrWhiteSpace(sexo))
                errors.Add("Debe ingresar un sexo");

            if (departamento==null)
                errors.Add("Debe asignar un departamento");
            
            return errors;
        }
        public void Deliver(string identificacion, string nombres, string apellidos, string email, string sexo, Departamento departamento)
        {
            if (CanDeliver(identificacion, nombres, apellidos, email, sexo, departamento).Any())
                throw new InvalidOperationException();
            Identificacion = identificacion;
            Nombres = nombres;
            Apellidos = apellidos;
            Email = email;
            Sexo = sexo;
            Departamento = departamento;
        }

    }
}
