using AppAC.Domain.Contracts;
using System;
using AppAC.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAC.Application
{
    public class AuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public AuthService(
            IUsuarioRepository usuarioRepository
            )
        {
            _usuarioRepository = usuarioRepository;
        }
       
        public LoginResponse Login(LoginRequest request)
        {
            var user=_usuarioRepository.FindFirstOrDefault(x =>
                x.UserName == request.Username && x.Password == request.Password);
            if (user == null)
                return new LoginResponse(false, null, null);
            
            return new LoginResponse(user != null, user.Identificacion, user.GetType().Name);
        }
    }
    public record LoginRequest(string Username, string Password);

    public record LoginResponse(bool LoggedIn, string Token, string Tipo);

}
