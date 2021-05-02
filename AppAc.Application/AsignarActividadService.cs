using AppAC.Domain;
using AppAC.Domain.Contracts;
using System;

namespace AppAc.Application
{
    public class AsignarActividadService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IActividadRepository _actividadRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITipoActividadRepository _tipoActividadRepository;
        private readonly IMailServer _emailServer;

        public AsignarActividadService(
           IUnitOfWork unitOfWork,
           IActividadRepository actividadRepository,
           IUsuarioRepository usuarioRepository,
           ITipoActividadRepository tipoActividadRepository,
           IMailServer emailServer
       )
        {
            _unitOfWork = unitOfWork;
            _actividadRepository = actividadRepository;
            _usuarioRepository = usuarioRepository;
            _tipoActividadRepository = tipoActividadRepository;
            _emailServer = emailServer;
        }
        public ActividadResponse Handle(ActividadRequest request)
        {
            var tipoActividad = _tipoActividadRepository.Find(request.TipoActividadId);
            var docente =(Docente)_usuarioRepository.FindFirstOrDefault(d=>d.Identificacion.Equals(request.IdentificaciónDocente));
            var actividad = new Actividad(tipoActividad);
            var response = actividad.Asignar(docente, request.horasAsignadas);
            _actividadRepository.Add(actividad);
            _unitOfWork.Commit();
            _emailServer.Send("Nueva actividad asignada",$"Se efectúo la asignacion de la actividad", docente.Email);
            return new ActividadResponse(response);
        }
    }
    public record ActividadRequest(int TipoActividadId,string IdentificaciónDocente, int horasAsignadas);
    public record ActividadResponse(string Message);
}
