using System;
using System.Linq;
using AppAC.Domain;
using AppAC.Domain.Contracts;

namespace AppAC.Application
{
    public class CrearDocenteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDepartamentoRepository _departamentoRepository;
        private readonly IDocenteRepository _docenteRepository;
        private readonly IMailServer _emailServer;
        public CrearDocenteService(
            IUnitOfWork unitOfWork,
            IDepartamentoRepository departamentoRepository,
            IDocenteRepository docenteRepository,
            IMailServer emailServer
        )
        {
            _unitOfWork = unitOfWork;
            _departamentoRepository = departamentoRepository;
            _docenteRepository = docenteRepository;
            _emailServer = emailServer;
        }

        public DocenteResponse Handle(DocenteRequest request)
        {
            if (_docenteRepository.FindDocente(request.Identificacion) != null)
                return new DocenteResponse("Ya existe el docente con esa identificacion","Error");
            var departamento = _departamentoRepository.Find(request.departamentoId);
            if (departamento == null)
                return new DocenteResponse("Se debe asignar un departamento al docente","Error");
            var docente = new Docente();
            var errors = docente.CanDeliver(request.Identificacion,
                request.Nombres,
                request.Apellidos,
                request.Email,
                request.Sexo,
                departamento);
            if (errors.Any())
            {
                var result = String.Join(", ", errors.ToArray());
                return new DocenteResponse(result,"Error");
            }
            docente.Deliver(request.Identificacion,
                request.Nombres,
                request.Apellidos,
                request.Email,
                request.Sexo,
                departamento);
            string response = "";
            try
            {
                _docenteRepository.Add(docente);
                response = $"Se registró correctamente el docente {docente.Nombres}";
            }
            catch (Exception e)
            {
                response = "No se pudo registrar";
            }
            _unitOfWork.Commit();
            _emailServer.Send("Registro en AppAC","Se registro en el sistema",docente.Email);
            return new DocenteResponse(response,"Ok");
        }
    }

    public record DocenteRequest(string Identificacion, string Nombres, string Apellidos, string Email, string Sexo, int departamentoId);

    public record DocenteResponse(string Mensaje, String MessageType);
}