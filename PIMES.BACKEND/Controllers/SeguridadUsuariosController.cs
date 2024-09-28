using Biblioteca.DTOsPIMES.Request;
using Biblioteca.DTOsPIMES.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Biblioteca.RepositoriesPIMES;
using Biblioteca.DTOsPIMES;
using Microsoft.EntityFrameworkCore;
using Biblioteca.RepositoriesPIMES.Entities;

namespace Biblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeguridadUsuariosController : ControllerBase
    {
        private readonly AppDbContextPIMES _context;
        private readonly IConfiguration _configuration;

        public SeguridadUsuariosController(AppDbContextPIMES context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private string Encripter(string texto)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] textoEnBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(texto));
                return string.Concat(textoEnBytes.Select(b => b.ToString("x2")));
            }
        }

        private LoginResponseDTO GenerateToken(SeguridadUsuariosDTO seguridadUser)
        {
            var expires = DateTime.UtcNow.AddHours(16);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, seguridadUser.UserName),
                new Claim("Nombres", seguridadUser.Nombres ?? string.Empty),
                new Claim("Apellidos", seguridadUser.Apellidos ?? string.Empty),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var securityToken = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );


            return new LoginResponseDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Nombres = seguridadUser.Nombres,
                Apellidos = seguridadUser.Apellidos,
                UserName = seguridadUser.UserName
            };
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDTO loginDto)
        {
            var user = await _context.SeguridadUsuarios.SingleOrDefaultAsync(u => u.UserName == loginDto.UserName);

            if (user == null)
            {
                return Unauthorized(new { Success = false, Message = "Usuario o tipo incorrecto." });
            }

            if (user.Password != Encripter(loginDto.Password))
            {
                return Unauthorized(new { Success = false, Message = "Contraseña incorrecta." });
            }

            var seguridaduserDto = new SeguridadUsuariosDTO
            {
                IdUsuario = user.IdUsuario,
                Nombres = user.Nombres,
                Apellidos = user.Apellidos,
                Estado = user.Estado,
                Email = user.Email,
                UserName = user.UserName,
                Password = user.Password
            };

            var tokenResponse = GenerateToken(seguridaduserDto);

            return Ok(new
            {
                Success = true,
                Message = "Login exitoso.",
                Token = tokenResponse.Token,
                Nombres = tokenResponse.Nombres,
                IdUsuario = seguridaduserDto.IdUsuario

            });
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(SeguridadUsuariosDTO seguridaduserDto)
        {
            if (await _context.SeguridadUsuarios.AnyAsync(u => u.Nombres == seguridaduserDto.Nombres))
                return BadRequest(new { Success = false, Field = "nombre", Message = "El nombre ya está en uso." });

            if (await _context.SeguridadUsuarios.AnyAsync(u => u.Email == seguridaduserDto.Email))
                return BadRequest(new { Success = false, Field = "email", Message = "El email ya está en uso." });

            if (await _context.SeguridadUsuarios.AnyAsync(u => u.UserName == seguridaduserDto.UserName))
                return BadRequest(new { Success = false, Field = "userName", Message = "El nombre de usuario ya está en uso." });

            var seguridadUserEntity = new SeguridadUsuarios
            {
                IdUsuario = Guid.NewGuid(),
                Nombres = seguridaduserDto.Nombres,
                Apellidos = seguridaduserDto.Apellidos,
                Estado = seguridaduserDto.Estado,
                Email = seguridaduserDto.Email,
                UserName = seguridaduserDto.UserName,
                Password = Encripter(seguridaduserDto.Password)
            };


            _context.SeguridadUsuarios.Add(seguridadUserEntity);
            await _context.SaveChangesAsync();

            return Ok(new { Success = true, Message = "Registro exitoso.", IdUsuario = seguridadUserEntity.IdUsuario });
        }

    }
}
