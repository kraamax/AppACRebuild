using AppAC.Domain;
using AppAC.Domain.Contracts;
using System;

namespace AppAc.Application
{
    public class AsignarActividadService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IActividadRepository _actividadRepository;
        private readonly IMailServer _emailServer;

        public AsignarActividadService(
           IUnitOfWork unitOfWork,
           IActividadRepository actividadRepository,
           IMailServer emailServer
       )
        {
            _unitOfWork = unitOfWork;
            _actividadRepository = actividadRepository;
            _emailServer = emailServer;
        }
        public string AsignarActividad(int id, Docente docente, int horasAsignadas)
        {
            var actividad= _actividadRepository.Find(id);
            var response = actividad.Asignar(docente, horasAsignadas);
            _unitOfWork.Commit();
            _emailServer.Send($"Se efectúo la asignacion del a la actividad", docente.Email);
            return response;
        }
    }
}
