using System;
using AppAC.Domain;
using AppAC.Domain.Contracts;

namespace AppAC.Application
{
    public class DocenteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDepartamentoRepository _departamentoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMailServer _emailServer;
        public DocenteService(
            IUnitOfWork unitOfWork,
            IDepartamentoRepository departamentoRepository,
            IUsuarioRepository usuarioRepository,
            IMailServer emailServer
        )
        {
            _unitOfWork = unitOfWork;
            _departamentoRepository = departamentoRepository;
            _usuarioRepository = usuarioRepository;
            _emailServer = emailServer;
        }

        public DocenteResponse crearDocente(DocenteRequest request)
        {
            var departamento = _departamentoRepository.Find(request.departamentoId);
            var docente = new Docente(request.Identificacion, 
                                        request.Nombres,
                                        request.Apellidos,
                                        request.Email,
                                        request.Telefono,
                                        request.Sexo,
                                        departamento);
            string response = "";
            try
            {
                _usuarioRepository.Add(docente);
                response = "se guardo correctamente";
            }
            catch (Exception e)
            {
                response = "no se pudo guardar";
            }
            _unitOfWork.Commit();
            _emailServer.Send("Registro en AppAC","Se registro en el sistema",docente.Email);
            return new DocenteResponse(response);

        }
    }

    public record DocenteRequest(string Identificacion, string Nombres, string Apellidos, string Email, string Telefono, string Sexo, int departamentoId);

    public record DocenteResponse(string Mensaje);
}