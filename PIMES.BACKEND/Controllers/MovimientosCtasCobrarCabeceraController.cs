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
    public class MovimientosCtasCobrarCabeceraController : ControllerBase
    {
        private readonly AppDbContextPIMES _context;
        private readonly IConfiguration _configuration;

        public MovimientosCtasCobrarCabeceraController(AppDbContextPIMES context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> CreateComprobante(Guid id, [FromBody] MovimientosCtasCobrarCabeceraDTO cobrarCabeceraDto)
        {
            if (cobrarCabeceraDto == null)
            {
                return BadRequest("Comprobante DTO no puede ser nulo.");
            }

            // Verifica si el usuario con el id proporcionado existe
            var usuario = await _context.SeguridadUsuarios.FindAsync(id);
            if (usuario == null)
            {
                return BadRequest("El usuario proporcionado no existe.");
            }

            // Verifica si el proveedor con el DNI (NumDoc) proporcionado existe
            var cliente = await _context.MaestrosCliente
                .FirstOrDefaultAsync(p => p.NumDoc == cobrarCabeceraDto.NumDoc); // Busca por NumDoc (DNI)

            if (cliente == null)
            {
                return BadRequest("El proveedor con el DNI proporcionado no existe.");
            }

            // Crea la compra
            var comprobante = new MovimientosCtasCobrarCabecera
            {
                IdCtasCobrarCabecera = Guid.NewGuid(),
                Fecha = DateTime.UtcNow,
                IdCliente = cliente.IdCliente, // Usa el ID del proveedor encontrado
                NumDoc = cobrarCabeceraDto.NumDoc, // Si necesitas conservar el DNI en NumDoc
                Total = cobrarCabeceraDto.Total,
                IdUsuarioRegistro = id, // Se establece como el usuario proporcionado
                IdUsuarioActual = id, // Se establece como el usuario proporcionado
                FechaRegistro = DateTime.UtcNow,
                FechaActual = DateTime.UtcNow,
                Tipo = cobrarCabeceraDto.Tipo
            };

            try
            {
                _context.MovimientosCtasCobrarCabecera.Add(comprobante);
                await _context.SaveChangesAsync();

                // Devuelve la respuesta con el ID de la cabecera y el mensaje
                return StatusCode(StatusCodes.Status201Created, new
                {
                    mensaje = "Comprobante creado correctamente.",
                    idCtasCobrarCabecera = comprobante.IdCtasCobrarCabecera // Incluye el ID en la respuesta
                });
            }
            catch (DbUpdateException ex)
            {
                // Devuelve un mensaje de error más detallado
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    mensaje = "Error al guardar el comprobante.",
                    detalle = ex.Message // Incluye detalles del error
                });
            }
        }






        // PUT: api/MaestrosCliente/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] MovimientosCtasCobrarCabeceraDTO cobrarCabeceraDto)
        {
            if (cobrarCabeceraDto == null)
            {
                return BadRequest("Proveedor DTO no puede ser nulo.");
            }


            var comprobanteExistente = await _context.MovimientosCtasCobrarCabecera.FindAsync(id);
            if (comprobanteExistente == null)
            {
                return NotFound("Cliente no encontrado.");
            }

            // Actualizar los datos del cliente existente
            comprobanteExistente.Fecha = cobrarCabeceraDto.Fecha;
            comprobanteExistente.Total = cobrarCabeceraDto.Total;
            comprobanteExistente.NumDoc = cobrarCabeceraDto.NumDoc;
            comprobanteExistente.FechaRegistro = cobrarCabeceraDto.FechaRegistro;
            comprobanteExistente.FechaActual = cobrarCabeceraDto.FechaActual;
            comprobanteExistente.Tipo = cobrarCabeceraDto.Tipo;

            try
            {
                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();
                return NoContent(); // O retorna Ok(clienteExistente) si deseas devolver el cliente actualizado
            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al actualizar el comprobante." });
            }
        }




        // GET: api/MaestrosCliente
        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var compras = await _context.MovimientosCtasCobrarCabecera
                .Include(c => c.Cliente) // Asegúrate de tener la relación definida
                .ToListAsync();

            var comprobantesDTOs = compras.Select(c => new MovimientosCtasCobrarCabecera
            {
                IdCtasCobrarCabecera = c.IdCtasCobrarCabecera,
                Fecha = c.Fecha,
                IdCliente = c.IdCliente,
                NumDoc = c.NumDoc,
                Total = c.Total,
                IdUsuarioRegistro = c.IdUsuarioRegistro,
                IdUsuarioActual = c.IdUsuarioActual,
                FechaRegistro = c.FechaRegistro,
                FechaActual = c.FechaActual,
                Tipo = c.Tipo,
            }).ToList();

            return Ok(comprobantesDTOs);
        }
    }
}
