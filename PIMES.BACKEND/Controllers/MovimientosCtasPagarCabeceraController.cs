using Biblioteca.DTOsPIMES;
using Biblioteca.RepositoriesPIMES;
using Biblioteca.RepositoriesPIMES.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PIMES.BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosCtasPagarCabeceraController : ControllerBase
    {
        private readonly AppDbContextPIMES _context;
        private readonly IConfiguration _configuration;

        public MovimientosCtasPagarCabeceraController(AppDbContextPIMES context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> CreateCompra(Guid id, [FromBody] MovimientosCtasPagarCabeceraDTO pagarCabeceraDto)
        {
            if (pagarCabeceraDto == null)
            {
                return BadRequest("Item DTO no puede ser nulo.");
            }

            // Verifica si el usuario con el id proporcionado existe
            var usuario = await _context.SeguridadUsuarios.FindAsync(id);
            if (usuario == null)
            {
                return BadRequest("El usuario proporcionado no existe.");
            }

            // Verifica si el proveedor con el DNI (NumDoc) proporcionado existe
            var proveedor = await _context.MaestrosProveedor
                .FirstOrDefaultAsync(p => p.NumDoc == pagarCabeceraDto.NumDoc); // Busca por NumDoc (DNI)

            if (proveedor == null)
            {
                return BadRequest("El proveedor con el DNI proporcionado no existe.");
            }

            // Crea la compra
            var compra = new MovimientosCtasPagarCabecera
            {
                IdCtasPagarCabecera = Guid.NewGuid(),
                Fecha = DateTime.UtcNow,
                IdProveedor = proveedor.IdProveedor, // Usa el ID del proveedor encontrado
                NumDoc = pagarCabeceraDto.NumDoc, // Si necesitas conservar el DNI en NumDoc
                Total = pagarCabeceraDto.Total,
                IdUsuarioRegistro = id, // Se establece como el usuario proporcionado
                IdUsuarioActual = id, // Se establece como el usuario proporcionado
                FechaRegistro = DateTime.UtcNow,
                FechaActual = DateTime.UtcNow
            };

            try
            {
                _context.MovimientosCtasPagarCabecera.Add(compra);
                await _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created, new { mensaje = "Item creado correctamente." });
            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al guardar el item." });
            }
        }






        // PUT: api/MaestrosCliente/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] MovimientosCtasPagarCabeceraDTO pagarCabeceraDto)
        {
            if (pagarCabeceraDto == null)
            {
                return BadRequest("Proveedor DTO no puede ser nulo.");
            }


            var compraExistente = await _context.MovimientosCtasPagarCabecera.FindAsync(id);
            if (compraExistente == null)
            {
                return NotFound("Cliente no encontrado.");
            }

            // Actualizar los datos del cliente existente
            compraExistente.Fecha = pagarCabeceraDto.Fecha;
            compraExistente.Total = pagarCabeceraDto.Total;
            compraExistente.NumDoc = pagarCabeceraDto.NumDoc;
            compraExistente.FechaRegistro = pagarCabeceraDto.FechaRegistro;
            compraExistente.FechaActual = pagarCabeceraDto.FechaActual;

            try
            {
                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();
                return NoContent(); // O retorna Ok(clienteExistente) si deseas devolver el cliente actualizado
            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al actualizar el item." });
            }
        }




        // GET: api/MaestrosCliente
        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var compras = await _context.MovimientosCtasPagarCabecera
                .Include(c => c.Proveedor) // Asegúrate de tener la relación definida
                .ToListAsync();

            var comprasDTOs = compras.Select(c => new MovimientosCtasPagarCabeceraResponseDTO
            {
                IdCtasPagarCabecera = c.IdCtasPagarCabecera,
                Fecha = c.Fecha,
                IdProveedor = c.IdProveedor,
                NumDoc = c.NumDoc,
                Total = c.Total,
                NombresProveedor = c.Proveedor.Nombres,
                ApellidosProveedor = c.Proveedor.Apellidos,
                IdUsuarioRegistro = c.IdUsuarioRegistro,
                IdUsuarioActual = c.IdUsuarioActual,
                FechaRegistro = c.FechaRegistro,
                FechaActual = c.FechaActual
            }).ToList();

            return Ok(comprasDTOs);
        }
    }
}
