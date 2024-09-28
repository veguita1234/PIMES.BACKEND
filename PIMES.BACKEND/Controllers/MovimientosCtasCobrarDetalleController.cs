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
    public class MovimientosCtasCobrarDetalleController : ControllerBase
    {
        private readonly AppDbContextPIMES _context;
        private readonly IConfiguration _configuration;

        public MovimientosCtasCobrarDetalleController(AppDbContextPIMES context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> CreateComprobante(Guid id, [FromBody] MovimientosCtasCobrarDetalleDTO cobrarDetalleDto)
        {
            if (cobrarDetalleDto == null)
            {
                return BadRequest("Producto DTO no puede ser nulo.");
            }

            var usuario = await _context.SeguridadUsuarios.FindAsync(id);
            if (usuario == null)
            {
                return BadRequest("El usuario proporcionado no existe.");
            }

            var producto = await _context.MaestrosProducto
                .FirstOrDefaultAsync(p => p.IdProducto == cobrarDetalleDto.IdProducto);

            if (producto == null)
            {
                return BadRequest("El producto con el Id proporcionado no existe.");
            }

            if (producto.Stock < cobrarDetalleDto.Cantidad)
            {
                return BadRequest("No hay suficiente stock para el producto.");
            }

            var cabecera = await _context.MovimientosCtasCobrarCabecera
                .FindAsync(cobrarDetalleDto.IdCtasCobrarCabecera);

            if (cabecera == null)
            {
                return BadRequest("La cabecera con el Id proporcionado no existe.");
            }

            var cobrarDetalle = new MovimientosCtasCobrarDetalle
            {
                IdCtasCobrarDetalle = Guid.NewGuid(),
                IdCtasCobrarCabecera = cobrarDetalleDto.IdCtasCobrarCabecera,
                IdProducto = cobrarDetalleDto.IdProducto,
                Precio = cobrarDetalleDto.Precio,
                SubTotal = cobrarDetalleDto.SubTotal,
                Descuento = cobrarDetalleDto.Descuento,
                Cantidad = cobrarDetalleDto.Cantidad,
                IdUsuarioRegistro = id,
                IdUsuarioActual = id,
                FechaRegistro = DateTime.UtcNow,
                FechaActual = DateTime.UtcNow
            };

            var strategy = _context.Database.CreateExecutionStrategy();

            try
            {
                await strategy.ExecuteAsync(async () =>
                {
                    using (var transaction = await _context.Database.BeginTransactionAsync())
                    {
                        _context.MovimientosCtasCobrarDetalle.Add(cobrarDetalle);

                        producto.Stock -= cobrarDetalleDto.Cantidad;
                        _context.MaestrosProducto.Update(producto);

                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                });

                return StatusCode(StatusCodes.Status201Created, new { mensaje = "Detalle de comprobante creado correctamente." });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al guardar el comprobante.", error = ex.Message });
            }
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] MovimientosCtasCobrarDetalleDTO cobrarDetalleDto)
        {
            if (cobrarDetalleDto == null)
            {
                return BadRequest("producto DTO no puede ser nulo.");
            }


            var productoteExistente = await _context.MovimientosCtasCobrarDetalle.FindAsync(id);
            if (productoteExistente == null)
            {
                return NotFound("producto no encontrado.");
            }

            productoteExistente.SubTotal = cobrarDetalleDto.SubTotal;
            productoteExistente.Descuento = cobrarDetalleDto.Descuento;
            productoteExistente.Cantidad = cobrarDetalleDto.Cantidad;


            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al actualizar el producto." });
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var productos = await _context.MovimientosCtasCobrarDetalle
                .ToListAsync();

            var comprobantesDTOs = productos.Select(c => new MovimientosCtasCobrarDetalle
            {
                IdCtasCobrarDetalle = c.IdCtasCobrarDetalle,
                IdCtasCobrarCabecera = c.IdCtasCobrarCabecera,
                IdProducto = c.IdProducto,
                Precio = c.Precio,
                SubTotal = c.SubTotal,
                Descuento = c.Descuento,
                Cantidad = c.Cantidad,
                IdUsuarioRegistro = c.IdUsuarioRegistro,
                IdUsuarioActual = c.IdUsuarioActual,
                FechaRegistro = c.FechaRegistro,
                FechaActual = c.FechaActual,
            }).ToList();

            return Ok(comprobantesDTOs);
        }
    }
}
