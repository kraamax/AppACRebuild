using AppAC.Domain;
using AppAC.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAC.Application
{
    public class TipoActividadService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITipoActividadRepository _tipoActividadRepository;
        private readonly IMailServer _emailServer;
        public TipoActividadService(
            IUnitOfWork unitOfWork,
            ITipoActividadRepository tipoActividadRepository,
            IMailServer emailServer
            )
        {
            _unitOfWork = unitOfWork;
            _tipoActividadRepository = tipoActividadRepository;
            _emailServer = emailServer;
        }
        public TipoActividadResponse CrearTipoActividad(TipoActividadRequest request)
        {
            var tipoActividad = _tipoActividadRepository.FindFirstOrDefault(a => a.NombreActividad == request.Nombre);
            if (tipoActividad != null)
                return new TipoActividadResponse("Ya existe la una actividad llamada así.");
            tipoActividad = new TipoActividad(request.Nombre);
            try
            {
                _tipoActividadRepository.Add(tipoActividad);
            }
            catch (Exception)
            {
                return new TipoActividadResponse($"No se pudo guardar la actividad {tipoActividad.NombreActividad}");
            }
            _unitOfWork.Commit();
            return new TipoActividadResponse($"Actividad {tipoActividad.NombreActividad} guardada");
        }
    }
    public record TipoActividadRequest(string Nombre);
    public record TipoActividadResponse(string Mensaje);
}
