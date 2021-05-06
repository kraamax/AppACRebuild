using AppAC.Domain.Contracts;
using System;
using AppAC.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAC.Application
{
    public class VerificarPlazoAperturaService
    {
        private readonly IPlazoAperturaRepository _plazoAperturaRepository;
        public VerificarPlazoAperturaService(
            IPlazoAperturaRepository plazoAperturaRepository
            )
        {
            _plazoAperturaRepository = plazoAperturaRepository;
        }
       
        public string Handle(string identificacionCreador)
        {
            var plazo= _plazoAperturaRepository.GetCurrentPlazoByCreador(identificacionCreador);
            if (plazo == null)
                return "Error: No se encontro ningun plazo de apertura";
            var estaEntreElPlazoEstablecido =plazo.EstaEntreElPlazoEstablecido(DateTime.Now);
            if (!estaEntreElPlazoEstablecido)
                return "Error: La fecha no esta dentro del plazo establecido por el jefe de departamento";
            return "La fecha es válida";
        }
    }


}
