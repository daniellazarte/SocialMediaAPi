using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Core.Entities;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration;

        }


        [HttpPost]
        public IActionResult Authentication(UserLogin login)
        {
            //Validar el Usuario
            if (IsValidUser(login))
            {
                var token = GenerateToken();
                return Ok(new { token });
            }
            return NotFound();

        }

        private bool IsValidUser(UserLogin login)
        {
            return true;
        }

        private string GenerateToken()
        {
            //Generar el Token Si un Usuario es Valido
            //Generando el Header
            var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var signingcredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingcredentials);


            //Creando los CLaims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "Daniel Lazarte"),
                new Claim(ClaimTypes.Email, "daniel.lazarte@gmail.com"),
                new Claim(ClaimTypes.Role, "Administrador")
            };


            //Creando el PAyload
            var payload = new JwtPayload
            (
                 _configuration["Authentication:Issuer"],
                 _configuration["Authentication:Audience"],
                 claims,
                 DateTime.Now, //tiempo minimo de un token
                 DateTime.UtcNow.AddMinutes(2)  // tiempo en que vencera el token

            );
            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token); //Serializando el token para enviarlo 
        }
    }
}
