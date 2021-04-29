using AppAC.Domain;
using AppAC.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAC.Application
{
    public class UsuarioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDepartamentoRepository _departamentoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMailServer _emailServer;
        public UsuarioService(
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
        public UsuarioResponse CrearUsuario(UsuarioRequest request) {
            var departamento = _departamentoRepository.Find(request.departamentoId);
            return new UsuarioResponse("");
        }
    }
    //No implementado...Buscando opciones
    public record UsuarioRequest(string Tipo, string Identificacion, string Nombres, string Apellidos, string Email, string Telefono, string Sexo, int departamentoId);
    public record UsuarioResponse(string Mensaje);
    static class TipoUsuario {
        public static Usuario CrearUsuario(string tipo, 
                                           string identificacion, 
                                           string nombres, 
                                           string apellidos, 
                                           string email, 
                                           string telefono, 
                                           string sexo,
                                           Departamento departamento) {
           if (tipo == "Docente") return new Docente(identificacion,
                                                    nombres,
                                                    apellidos,
                                                    email,
                                                    telefono,
                                                    sexo,
                                                    departamento);
           if(tipo=="JefeDpto") return new JefeDpto(identificacion,
                                                     nombres,
                                                     apellidos,
                                                     email,
                                                     telefono,
                                                     sexo,
                                                     departamento);
            return new Administrador(identificacion,
                                        nombres,
                                        apellidos,
                                        email,
                                        telefono,
                                        sexo);
        }
    }
}
