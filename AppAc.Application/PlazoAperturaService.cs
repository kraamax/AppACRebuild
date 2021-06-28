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
        private readonly IJefeDptoRepository _jefeDptoRepository;
        public PlazoAperturaService(
            IUnitOfWork unitOfWork,
            IPlazoAperturaRepository plazoAperturaRepository,
            IMailServer emailServer,
            IJefeDptoRepository jefeDptoRepository
            )
        {
            _unitOfWork = unitOfWork;
            _plazoAperturaRepository = plazoAperturaRepository;
            _emailServer = emailServer;
            _jefeDptoRepository = jefeDptoRepository;
        }
        public PlazoAperturaResponse CrearPlazoApertura(PlazoAperturaRequest request)
        {
            var jefeDpto =_jefeDptoRepository.FindFirstOrDefault(c => c.Identificacion == request.IdentificacionCreador);
            if (jefeDpto == null)
                return new PlazoAperturaResponse("No existe el Jefe de departamento","error");
            var plazoApertura = new PlazoApertura(jefeDpto);
            var response = plazoApertura.EstablecerPlazo(request.FechaInicio, request.FechaFin);
            if (response.Equals("El plazo fue correctamente ingresado"))
            {
                var currentPlazo=_plazoAperturaRepository.GetCurrentPlazoByCreador(jefeDpto.Identificacion);
                if (currentPlazo != null)
                {
                    currentPlazo.Deshabilitar();
                    _plazoAperturaRepository.Update(currentPlazo);

                }
                _plazoAperturaRepository.Add(plazoApertura);
                _unitOfWork.Commit();
                return new PlazoAperturaResponse(response,"Ok");

            }
            return new PlazoAperturaResponse(response,"Info");
        }
        public IEnumerable<PlazoApertura> GetAll()
        {
            return _plazoAperturaRepository.GetAll();
        }
        public IEnumerable<PlazoApertura> GetByJefeDpto(string identificacion)
        {
            return _plazoAperturaRepository.FindBy(c=>c.Creador.Identificacion==identificacion);
        }

        public PlazoApertura GetCurrentByJefeDpto(string identificacion)
        {
            return _plazoAperturaRepository.FindFirstOrDefault(c=>c.Creador.Identificacion==identificacion && c.Activo == true);
        }
    }
    

    public record PlazoAperturaRequest(string IdentificacionCreador, DateTime FechaInicio, DateTime FechaFin);

    public record PlazoAperturaResponse(string Message, string MessageType);
}
