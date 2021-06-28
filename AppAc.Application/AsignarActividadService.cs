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
            if (tipoActividad == null)
                return new ActividadResponse("No existe ese tipo de actividad", null,"Error");
            var asignador = _usuarioRepository.FindJefeDpto(request.IdentificacionAsignador);
            if(asignador==null)
                return new ActividadResponse("No se encontró el Jefe de departamento", null,"Error");
            var responsable =_usuarioRepository.FindDocente(request.IdentificacionResponsable);
            if (responsable == null)
                return new ActividadResponse("No se encontró el docente", null,"Error");
            var actividad = new Actividad(tipoActividad,asignador);
            var response = actividad.Asignar(responsable, request.HorasAsignadas);
            _actividadRepository.Add(actividad);
            _unitOfWork.Commit();
            _emailServer.Send("Nueva actividad asignada",$"Se efectúo la asignacion de la actividad", responsable.Email);
            return new ActividadResponse(response, actividad,"Ok");
        }
    }
    public record ActividadRequest(int TipoActividadId,string IdentificacionAsignador, string IdentificacionResponsable, int HorasAsignadas);
    public record ActividadResponse(string Message, Actividad Actividad, string MessageType);
}
