using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppAC.Domain
{
    public class Evidencia
    {
        public Evidencia()
        {
        }
        public string Ruta { get; private set; }
        public DateTime FechaCarga { get; private set; }
        public IReadOnlyList<string> CanDeliver(string ruta) {
            var errors = new List<string>();
            if (ruta==null)
                errors.Add("Debe ingresar una ruta");

            return errors;
        }
        public void Deliver(string ruta)
        {
            if (CanDeliver(ruta).Any())
                throw new InvalidOperationException();
            Ruta = ruta;
            FechaCarga=DateTime.Now;
        }
    }
}
