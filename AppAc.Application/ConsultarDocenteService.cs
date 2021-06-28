using System;
using System.Collections.Generic;
using System.Linq;
using AppAC.Domain;
using AppAC.Domain.Contracts;

namespace AppAC.Application
{
    public class ConsultarDocenteService
    {
        private readonly IDocenteRepository _docenteRepository;
        public ConsultarDocenteService(
            IDocenteRepository docenteRepository
        )
        {
            _docenteRepository = docenteRepository;
        }

        public IEnumerable<Docente> GetAll( )
        {
            return _docenteRepository.GetAll();
        }
        public Docente GetByIdentificacion( string identificion )
        {
            return _docenteRepository.FindDocente(identificion);
        }
        public IEnumerable<Docente> GetByDepartamento( int dptoId )
        {
            return _docenteRepository.FindBy(x=>x.Departamento.Id==dptoId);
        }
    }


}