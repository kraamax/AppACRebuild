using AppAC.Domain.Contracts;
using System;
using AppAC.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAC.Application
{
    public class PlazoAperturaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPlazoAperturaRepository _plazoAperturaRepository;
        private readonly IMailServer _emailServer;
        public PlazoAperturaService(
            IUnitOfWork unitOfWork,
            IPlazoAperturaRepository plazoAperturaRepository,
            IMailServer emailServer
            )
        {
            _unitOfWork = unitOfWork;
            _plazoAperturaRepository = plazoAperturaRepository;
            _emailServer = emailServer;
        }
        public string CrearPlazoApertura(PlazoAperturaRequest request) {
            var plazoApertura = new PlazoApertura();
            var response = plazoApertura.EstablecerPlazo(request.FechaInicio, request.FechaFin);
            if (response.Equals("El plazo fue correctamente ingresado")) { 
                _plazoAperturaRepository.Add(plazoApertura);
                _unitOfWork.Commit();
            }
            return response;
        }
    }
    public class PlazoAperturaRequest{
        public PlazoAperturaRequest(DateTime fechaInicio, DateTime fechaFin)
        {
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
        }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

    }

}
