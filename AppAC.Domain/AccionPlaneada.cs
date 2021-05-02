using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppAC.Domain
{
    public class AccionPlaneada: Accion
    {
        public IReadOnlyList<string> CanDeliver(string descripcion) {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(descripcion))
                errors.Add("Debe ingresar una descripción");

            return errors;
        }
        public void Deliver(string descripcion)
        {
            if (CanDeliver(descripcion).Any())
                throw new InvalidOperationException();
            Descripcion = descripcion;
        }
    }
}
