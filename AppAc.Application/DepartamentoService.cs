using AppAC.Domain;
using AppAC.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAC.Application
{
    public class DepartamentoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDepartamentoRepository _departamentoRepository;
        private readonly IMailServer _emailServer;
        public DepartamentoService(
            IUnitOfWork unitOfWork,
            IDepartamentoRepository departamentoRepository,
            IMailServer emailServer
            )
        {
            _unitOfWork = unitOfWork;
            _departamentoRepository = departamentoRepository;
            _emailServer = emailServer;
        }
        public DepartamentoResponse CrearDepartamento(DepartamentoRequest request) {
            var departamento = new Departamento();
            var errors=departamento.CanDeliver(request.Codigo, request.Nombre);
            if (errors.Any())
            {
                var result = String.Join(",", errors);
                return new DepartamentoResponse(result);
            }
            departamento.Deliver(request.Codigo, request.Nombre);
            try
            {
                _departamentoRepository.Add(departamento);
            }
            catch (Exception)
            {
                return new DepartamentoResponse($"No se pudo guardar el departamento {departamento.NombreDpto}");
            }
            _unitOfWork.Commit();
            return new DepartamentoResponse($"Departamento {departamento.NombreDpto} guardado");
        }
        public IEnumerable<Departamento> GetAll()
        {
            return _departamentoRepository.GetAll();
        }
    }
    public record DepartamentoRequest(string Codigo, string Nombre);
    public record DepartamentoResponse(string Mensaje);
}
