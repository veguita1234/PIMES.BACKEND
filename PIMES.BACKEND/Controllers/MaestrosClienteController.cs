using Biblioteca.DTOsPIMES;
using Biblioteca.RepositoriesPIMES;
using Biblioteca.RepositoriesPIMES.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PIMES.BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaestrosClienteController : ControllerBase
    {
        private readonly AppDbContextPIMES _context;
        private readonly IConfiguration _configuration;

        public MaestrosClienteController(AppDbContextPIMES context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // POST: api/MaestrosCliente
        [HttpPost("{id}")]
        public async Task<IActionResult> CreateCliente(Guid id, [FromBody] MaestrosClienteDTO clienteDto)
        {
            if (clienteDto == null)
            {
                return BadRequest("Cliente DTO no puede ser nulo.");
            }

            // Verifica si el usuario con el id proporcionado existe
            var usuario = await _context.SeguridadUsuarios.FindAsync(id);
            if (usuario == null)
            {
                return BadRequest("El usuario proporcionado no existe.");
            }

            // Validación de formato de correo electrónico para ambos campos
            var correoError = IsValidEmail(clienteDto.Correo, "Correo");
            var emailError = IsValidEmail(clienteDto.Email, "Email");

            if (!correoError.IsValid)
            {
                return BadRequest(correoError.ErrorMessage);
            }

            if (!emailError.IsValid)
            {
                return BadRequest(emailError.ErrorMessage);
            }

            var cliente = new MaestrosCliente
            {
                IdCliente = Guid.NewGuid(),
                Nombres = clienteDto.Nombres,
                Apellidos = clienteDto.Apellidos,
                TipoDoc = clienteDto.TipoDoc,
                NumDoc = clienteDto.NumDoc,
                RazonSocial = clienteDto.RazonSocial,
                Ubicacion = clienteDto.Ubicacion,
                Correo = clienteDto.Correo,
                Email = clienteDto.Email,
                TipoCliente = clienteDto.TipoCliente,
                Estado = 1,
                IdUsuarioRegistro = id, // Se establece como el usuario proporcionado
                IdUsuarioActual = id, // Se establece como el usuario proporcionado
                FechaRegistro = DateTime.UtcNow,
                FechaActual = DateTime.UtcNow
            };

            try
            {
                _context.MaestrosCliente.Add(cliente);
                await _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created, new { mensaje = "Cliente creado correctamente." });
            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al guardar el cliente." });
            }
        }

        // Método para validar si un correo electrónico es válido
        private (bool IsValid, string ErrorMessage) IsValidEmail(string email, string fieldName)
        {
            if (string.IsNullOrEmpty(email))
            {
                return (false, $"{fieldName} no puede ser nulo o vacío.");
            }
            if (!email.Contains('@'))
            {
                return (false, $"{fieldName} debe contener un símbolo '@'.");
            }
            return (true, string.Empty);
        }





        // PUT: api/MaestrosCliente/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] MaestrosClienteDTO clienteDto)
        {
            if (clienteDto == null)
            {
                return BadRequest("Cliente DTO no puede ser nulo.");
            }

            // Verificar si el id en la URL coincide con el id del clienteDto (si es necesario)
            // Omitir esta verificación si no es necesaria en tu caso

            // Buscar el cliente existente en la base de datos
            var clienteExistente = await _context.MaestrosCliente.FindAsync(id);
            if (clienteExistente == null)
            {
                return NotFound("Cliente no encontrado.");
            }

            // Actualizar los datos del cliente existente
            clienteExistente.Nombres = clienteDto.Nombres;
            clienteExistente.Apellidos = clienteDto.Apellidos;
            clienteExistente.TipoDoc = clienteDto.TipoDoc;
            clienteExistente.NumDoc = clienteDto.NumDoc;
            clienteExistente.RazonSocial = clienteDto.RazonSocial;
            clienteExistente.Ubicacion = clienteDto.Ubicacion;
            clienteExistente.Correo = clienteDto.Correo;
            clienteExistente.Email = clienteDto.Email;
            clienteExistente.TipoCliente = clienteDto.TipoCliente;
            clienteExistente.FechaActual = DateTime.UtcNow;

            try
            {
                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();
                return NoContent(); // O retorna Ok(clienteExistente) si deseas devolver el cliente actualizado
            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al actualizar el cliente." });
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // Buscar producto utilizando LINQ
            var clienteExistente = await _context.MaestrosCliente
                .FirstOrDefaultAsync(p => p.IdCliente == id);

            if (clienteExistente == null)
            {
                return NotFound("Cliente no encontrado.");
            }

            // Cambiar el estado a 0 (Eliminado)
            clienteExistente.Estado = 0;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al eliminar el cliente.", detalle = ex.Message });
            }
        }

        // GET: api/MaestrosCliente
        //[HttpGet]
        //public async Task<IActionResult> GetClientes()
        //{
        //    var clientes = await _context.MaestrosCliente.ToListAsync();

        //    var clienteDtos = clientes.Select(c => new MaestrosClienteDTO
        //    {
        //        IdCliente = c.IdCliente,
        //        Nombres = c.Nombres,
        //        Apellidos = c.Apellidos,
        //        TipoDoc = c.TipoDoc,
        //        NumDoc = c.NumDoc,
        //        RazonSocial = c.RazonSocial,
        //        Ubicacion = c.Ubicacion,
        //        Correo = c.Correo,
        //        Email = c.Email,
        //        TipoCliente = c.TipoCliente,
        //        IdUsuarioRegistro = c.IdUsuarioRegistro,
        //        IdUsuarioActual = c.IdUsuarioActual,
        //        FechaRegistro = c.FechaRegistro,
        //        FechaActual = c.FechaActual
        //    }).ToList();

        //    return Ok(clienteDtos);
        //}


        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            var clientes = await _context.MaestrosCliente
                .Where(c => c.Estado == 1)
                .ToListAsync();

            var clienteDtos = clientes.Select(c => new MaestrosClienteDTO
            {
                IdCliente = c.IdCliente,
                Nombres = c.Nombres,
                Apellidos = c.Apellidos,
                TipoDoc = c.TipoDoc,
                NumDoc = c.NumDoc,
                RazonSocial = c.RazonSocial,
                Ubicacion = c.Ubicacion,
                Correo = c.Correo,
                Email = c.Email,
                TipoCliente = c.TipoCliente,
                Estado = c.Estado,
                IdUsuarioRegistro = c.IdUsuarioRegistro,
                IdUsuarioActual = c.IdUsuarioActual,
                FechaRegistro = c.FechaRegistro,
                FechaActual = c.FechaActual
            }).ToList();

            return Ok(clienteDtos);
        }

        // GET: api/MaestrosCliente/{dni}
        [HttpGet("{dni}")]
        public async Task<IActionResult> GetClientePorDNI(string dni)
        {
            if (string.IsNullOrEmpty(dni))
            {
                return BadRequest("El DNI no puede ser nulo o vacío.");
            }

            // Buscar el cliente en la base de datos por DNI
            var cliente = await _context.MaestrosCliente
                .FirstOrDefaultAsync(c => c.NumDoc == dni && c.Estado == 1); // Filtrar por NumDoc

            if (cliente == null)
            {
                return NotFound("Cliente no encontrado.");
            }

            var clienteDto = new MaestrosClienteDTO
            {
                IdCliente = cliente.IdCliente,
                Nombres = cliente.Nombres,
                Apellidos = cliente.Apellidos,
                TipoDoc = cliente.TipoDoc,
                NumDoc = cliente.NumDoc,
                RazonSocial = cliente.RazonSocial,
                Ubicacion = cliente.Ubicacion,
                Correo = cliente.Correo,
                Email = cliente.Email,
                TipoCliente = cliente.TipoCliente,
                Estado = cliente.Estado,
                IdUsuarioRegistro = cliente.IdUsuarioRegistro,
                IdUsuarioActual = cliente.IdUsuarioActual,
                FechaRegistro = cliente.FechaRegistro,
                FechaActual = cliente.FechaActual
            };

            return Ok(clienteDto);
        }



    }
}
