using System;
using System.Collections.Generic;
using System.Linq;
using AppAC.Domain;
using AppAC.Domain.Contracts;

namespace AppAC.Application
{
    public class ConsultarJefeDptoService
    {
        private readonly IJefeDptoRepository _jefeDptoRepository;
        
        public ConsultarJefeDptoService(
            IJefeDptoRepository jefeDptoRepository
        )
        {
            _jefeDptoRepository = jefeDptoRepository;
        }

        public IEnumerable<JefeDpto> GetAll( )
        {
            return _jefeDptoRepository.GetAll();
        }
        public JefeDpto GetByIdentificacion( string identificion )
        {
            return _jefeDptoRepository.FindFirstOrDefault(j=>j.Identificacion==identificion);
        }
    }


}