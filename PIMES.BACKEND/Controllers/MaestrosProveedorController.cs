using Biblioteca.DTOsPIMES;
using Biblioteca.RepositoriesPIMES.Entities;
using Biblioteca.RepositoriesPIMES;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PIMES.BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaestrosProveedorController : ControllerBase
    {
        private readonly AppDbContextPIMES _context;
        private readonly IConfiguration _configuration;

        public MaestrosProveedorController(AppDbContextPIMES context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // POST: api/MaestrosCliente
        [HttpPost("{id}")]
        public async Task<IActionResult> CreateProveedor(Guid id, [FromBody] MaestrosProveedorDTO proveedorDto)
        {
            if (proveedorDto == null)
            {
                return BadRequest("Proveedor DTO no puede ser nulo.");
            }

            // Verifica si el usuario con el id proporcionado existe
            var usuario = await _context.SeguridadUsuarios.FindAsync(id);
            if (usuario == null)
            {
                return BadRequest("El usuario proporcionado no existe.");
            }

            // Validación de formato de correo electrónico para ambos campos
            var correoError = IsValidEmail(proveedorDto.Correo, "Correo");
            var emailError = IsValidEmail(proveedorDto.Email, "Email");

            if (!correoError.IsValid)
            {
                return BadRequest(correoError.ErrorMessage);
            }

            if (!emailError.IsValid)
            {
                return BadRequest(emailError.ErrorMessage);
            }
            var proveedor = new MaestrosProveedor
            {
                IdProveedor = Guid.NewGuid(),
                Nombres = proveedorDto.Nombres,
                Apellidos = proveedorDto.Apellidos,
                TipoDoc = proveedorDto.TipoDoc,
                NumDoc = proveedorDto.NumDoc,
                RazonSocial = proveedorDto.RazonSocial,
                Ubicacion = proveedorDto.Ubicacion,
                Correo = proveedorDto.Correo,
                Email = proveedorDto.Email,
                TipoProveedor = proveedorDto.TipoProveedor,
                Estado = 1,
                IdUsuarioRegistro = id, // Se establece como el usuario proporcionado
                IdUsuarioActual = id, // Se establece como el usuario proporcionado
                FechaRegistro = DateTime.UtcNow,
                FechaActual = DateTime.UtcNow
            };

            try
            {
                _context.MaestrosProveedor.Add(proveedor);
                await _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created, new { mensaje = "Proveedor creado correctamente." });
            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al guardar el proveedor." });
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
        public async Task<IActionResult> Put(Guid id, [FromBody] MaestrosProveedorDTO proveedorDto)
        {
            if (proveedorDto == null)
            {
                return BadRequest("Proveedor DTO no puede ser nulo.");
            }

            // Verificar si el id en la URL coincide con el id del clienteDto (si es necesario)
            // Omitir esta verificación si no es necesaria en tu caso

            // Buscar el cliente existente en la base de datos
            var proveedorExistente = await _context.MaestrosProveedor.FindAsync(id);
            if (proveedorExistente == null)
            {
                return NotFound("Cliente no encontrado.");
            }

            // Actualizar los datos del cliente existente
            proveedorExistente.Nombres = proveedorDto.Nombres;
            proveedorExistente.Apellidos = proveedorDto.Apellidos;
            proveedorExistente.TipoDoc = proveedorDto.TipoDoc;
            proveedorExistente.NumDoc = proveedorDto.NumDoc;
            proveedorExistente.RazonSocial = proveedorDto.RazonSocial;
            proveedorExistente.Ubicacion = proveedorDto.Ubicacion;
            proveedorExistente.Correo = proveedorDto.Correo;
            proveedorExistente.Email = proveedorDto.Email;
            proveedorExistente.TipoProveedor = proveedorDto.TipoProveedor;
            proveedorExistente.FechaActual = DateTime.UtcNow;

            try
            {
                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();
                return NoContent(); // O retorna Ok(clienteExistente) si deseas devolver el cliente actualizado
            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al actualizar el proveedor." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // Buscar producto utilizando LINQ
            var proveedorExistente = await _context.MaestrosProveedor
                .FirstOrDefaultAsync(p => p.IdProveedor == id);

            if (proveedorExistente == null)
            {
                return NotFound("Proveedor no encontrado.");
            }

            // Cambiar el estado a 0 (Eliminado)
            proveedorExistente.Estado = 0;

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
        //public async Task<IActionResult> GetProveedores()
        //{
        //    var proveedores = await _context.MaestrosProveedor.ToListAsync();

        //    var proveedorDTOs = proveedores.Select(c => new MaestrosProveedorDTO
        //    {
        //        IdProveedor = c.IdProveedor,
        //        Nombres = c.Nombres,
        //        Apellidos = c.Apellidos,
        //        TipoDoc = c.TipoDoc,
        //        NumDoc = c.NumDoc,
        //        RazonSocial = c.RazonSocial,
        //        Ubicacion = c.Ubicacion,
        //        Correo = c.Correo,
        //        Email = c.Email,
        //        TipoProveedor = c.TipoProveedor,
        //        IdUsuarioRegistro = c.IdUsuarioRegistro,
        //        IdUsuarioActual = c.IdUsuarioActual,
        //        FechaRegistro = c.FechaRegistro,
        //        FechaActual = c.FechaActual
        //    }).ToList();

        //    return Ok(proveedorDTOs);
        //}

        [HttpGet]
        public async Task<IActionResult> GetProveedores()
        {
            var proveedores = await _context.MaestrosProveedor
                .Where(c => c.Estado == 1)
                .ToListAsync();

            var proveedorDTOs = proveedores.Select(c => new MaestrosProveedorDTO
            {
                IdProveedor = c.IdProveedor,
                Nombres = c.Nombres,
                Apellidos = c.Apellidos,
                TipoDoc = c.TipoDoc,
                NumDoc = c.NumDoc,
                RazonSocial = c.RazonSocial,
                Ubicacion = c.Ubicacion,
                Correo = c.Correo,
                Email = c.Email,
                TipoProveedor = c.TipoProveedor,
                Estado = c.Estado,
                IdUsuarioRegistro = c.IdUsuarioRegistro,
                IdUsuarioActual = c.IdUsuarioActual,
                FechaRegistro = c.FechaRegistro,
                FechaActual = c.FechaActual
            }).ToList();

            return Ok(proveedorDTOs);
        }

        [HttpGet("{documento}")]
        public async Task<IActionResult> GetProveedorPorDocumento(string documento)
        {
            if (string.IsNullOrEmpty(documento))
            {
                return BadRequest("El documento no puede ser nulo o vacío.");
            }

            var proveedor = await _context.MaestrosProveedor
                .FirstOrDefaultAsync(c => c.NumDoc == documento && c.Estado == 1); 

            if (proveedor == null)
            {
                return NotFound("proveedor no encontrado.");
            }

            var proveedorDto = new MaestrosProveedorDTO
            {
                IdProveedor = proveedor.IdProveedor,
                Nombres = proveedor.Nombres,
                Apellidos = proveedor.Apellidos,
                TipoDoc = proveedor.TipoDoc,
                NumDoc = proveedor.NumDoc,
                RazonSocial = proveedor.RazonSocial,
                Ubicacion = proveedor.Ubicacion,
                Correo = proveedor.Correo,
                Email = proveedor.Email,
                TipoProveedor = proveedor.TipoProveedor,
                Estado = proveedor.Estado,
                IdUsuarioRegistro = proveedor.IdUsuarioRegistro,
                IdUsuarioActual = proveedor.IdUsuarioActual,
                FechaRegistro = proveedor.FechaRegistro,
                FechaActual = proveedor.FechaActual
            };

            return Ok(proveedorDto);
        }


    }
}
