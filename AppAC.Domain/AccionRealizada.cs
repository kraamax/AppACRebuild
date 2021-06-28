using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppAC.Domain
{
    public class AccionRealizada:Accion
    {
        public Evidencia Evidencia { get; private set; }
        public IReadOnlyList<string> CanDeliver(string descripcion, Evidencia evidencia) {
            var errors = new List<string>();
            if (descripcion==null)
                errors.Add("Debe ingresar una descripción");

            if (evidencia==null)
                errors.Add("Debe tener una evidencia");
            
            return errors;
        }
        public void Deliver(string descripcion, Evidencia evidencia)
        {
            if (CanDeliver(descripcion, evidencia).Any())
                throw new InvalidOperationException();
            Descripcion = descripcion;
            Evidencia = evidencia;
        }
    }
}
