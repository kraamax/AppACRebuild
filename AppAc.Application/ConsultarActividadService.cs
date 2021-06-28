using AppAC.Domain;
using AppAC.Domain.Contracts;
using System;
using System.Collections.Generic;

namespace AppAc.Application
{
    public class ConsultarActividadService
    {
        private readonly IActividadRepository _actividadRepository;

        public ConsultarActividadService(
           IActividadRepository actividadRepository
       )
        {
            _actividadRepository = actividadRepository;
        }
        public IEnumerable<Actividad> GetAll()
        {
            return _actividadRepository.GetAll();
        }
        public IEnumerable<Actividad> GetByDocente(string identificacion)
        {
            return _actividadRepository.FindByDocente(identificacion);
        }
        public Actividad Get(int id)
        {
            return _actividadRepository.Find(id);
        }
    }
 
}
