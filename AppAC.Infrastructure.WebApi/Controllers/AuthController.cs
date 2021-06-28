using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAC.Application;
using AppAC.Domain;
using AppAC.Domain.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAC.Infrastructure.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public AuthController(
            IUsuarioRepository usuarioRepository
        )

        {
            _usuarioRepository = usuarioRepository;
        }
        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var service = new AuthService(_usuarioRepository);
            var response = service.Login(request);
            return Ok(response);
        }
    }

  
}