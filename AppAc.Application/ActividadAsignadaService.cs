using AppAC.Domain.Contracts;
using AppAC.Domain;
using System;

namespace AppAc.Application
{
    public class ActividadAsignadaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IActividadAsignadaRepository _actividadAsignadaRepository;
        private readonly IMailServer _emailServer;

        public ActividadAsignadaService(
           IUnitOfWork unitOfWork,
           IActividadAsignadaRepository actividadAsignadaRepository,
           IMailServer emailServer
       )
        {
            _unitOfWork = unitOfWork;
            _actividadAsignadaRepository = actividadAsignadaRepository;
            _emailServer = emailServer;
        }
        public string AsignarActividad(int id, Docente docente, int horasAsignadas)
        {
            var actividad= _actividadAsignadaRepository.Find(id);
            var response = actividad.AsignarActividad(docente, horasAsignadas);
            _unitOfWork.Commit();
            _emailServer.Send($"Se efectúo la asignacion del a la actividad", docente.Email);
            return response;
        }


    }
}
