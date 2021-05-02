using AppAC.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppAC.Domain
{
    public class Departamento: Entity<int>, IAggregateRoot
    {
        public Departamento(string codigoDpto, string nombreDpto)
        {
            CodigoDpto = codigoDpto;
            NombreDpto = nombreDpto;
        }

        public Departamento()
        {
            
        }
        public string CodigoDpto { get; set; }
        public string NombreDpto { get; set; }
        public IReadOnlyList<string> CanDeliver(string codigoDpto, string nombreDpto) {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(codigoDpto))
                errors.Add("Debe ingresar un codigo");

            if (string.IsNullOrWhiteSpace(nombreDpto))
                errors.Add("Debe ingresar un nombre");
            return errors;
        }
        public void Deliver(string codigoDpto, string nombreDpto)
        {
            if (CanDeliver(codigoDpto, nombreDpto).Any())
                throw new InvalidOperationException();
            CodigoDpto = codigoDpto;
            NombreDpto = nombreDpto;
        }
    }
}
