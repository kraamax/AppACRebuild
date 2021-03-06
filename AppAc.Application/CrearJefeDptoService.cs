using System;
using System.Linq;
using AppAC.Domain;
using AppAC.Domain.Contracts;

namespace AppAC.Application
{
    public class CrearJefeDptoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDepartamentoRepository _departamentoRepository;
        private readonly IJefeDptoRepository _jefeDptoRepository;
        private readonly IMailServer _emailServer;
        public CrearJefeDptoService(
            IUnitOfWork unitOfWork,
            IDepartamentoRepository departamentoRepository,
            IJefeDptoRepository jefeDptoRepository,
            IMailServer emailServer
        )
        {
            _unitOfWork = unitOfWork;
            _departamentoRepository = departamentoRepository;
            _jefeDptoRepository = jefeDptoRepository;
            _emailServer = emailServer;
        }

        public JefeDptoResponse Handle(JefeDptoRequest request)
        {
            if (_jefeDptoRepository.FindByIdentificacion(request.Identificacion) != null)
                return new JefeDptoResponse("Ya existe un JefeDpto con esa identificacion","Error");
            var departamento = _departamentoRepository.Find(request.departamentoId);
            if (departamento == null)
                return new JefeDptoResponse("Se debe asignar un departamento al Jefe de departamento","Error");

            var jefeDpto = new JefeDpto();
            var errors = jefeDpto.CanDeliver(request.Identificacion,
                request.Nombres,
                request.Apellidos,
                request.Email,
                request.Sexo,
                departamento);
            if (errors.Any())
            {
                var result = String.Join(", ", errors.ToArray());
                return new JefeDptoResponse(result,"Error");
            }
            jefeDpto.Deliver(request.Identificacion,
                request.Nombres,
                request.Apellidos,
                request.Email,
                request.Sexo,
                departamento);
            string response = "";
            try
            {
                _jefeDptoRepository.Add(jefeDpto);
                response = $"Se registró correctamente el Jefe de departamento {jefeDpto.Nombres}";
            }
            catch (Exception e)
            {
                response = "No se pudo registrar";
            }
            _unitOfWork.Commit();
            _emailServer.Send("Registro en AppAC","Se registro en el sistema",jefeDpto.Email);
            return new JefeDptoResponse(response,"Ok");
        }
    }

    public record JefeDptoRequest(string Identificacion, string Nombres, string Apellidos, string Email, string Sexo, int departamentoId);

    public record JefeDptoResponse(string Mensaje, string MessageType);
}