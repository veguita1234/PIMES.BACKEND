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
    public class MaestrosProductoController : ControllerBase
    {
        private readonly AppDbContextPIMES _context;
        private readonly IConfiguration _configuration;

        public MaestrosProductoController(AppDbContextPIMES context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> CreateProducto(Guid id, [FromBody] MaestrosProductoDTO productoDto)
        {
            if (productoDto == null)
            {
                return BadRequest("Proveedor DTO no puede ser nulo.");
            }

            var usuario = await _context.SeguridadUsuarios.FindAsync(id);
            if (usuario == null)
            {
                return BadRequest("El usuario proporcionado no existe.");
            }

            var producto = new MaestrosProducto
            {
                IdProducto = Guid.NewGuid(),
                Nombre = productoDto.Nombre,
                Marca = productoDto.Marca,
                Modelo = productoDto.Modelo,
                Precio = productoDto.Precio,
                Stock = productoDto.Stock,
                Estado = 1,
                IdUsuarioRegistro = id,
                IdUsuarioActual = id,
                FechaRegistro = DateTime.UtcNow,
                FechaActual = DateTime.UtcNow
            };

            try
            {
                _context.MaestrosProducto.Add(producto);
                await _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created, new { mensaje = "Producto creado correctamente." });
            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al guardar el producto." });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] MaestrosProductoDTO productoDTO)
        {
            if (productoDTO == null)
            {
                return BadRequest("Producto DTO no puede ser nulo.");
            }

            // Buscar producto utilizando LINQ
            var productoExistente = await _context.MaestrosProducto
                .FirstOrDefaultAsync(p => p.IdProducto == id);

            if (productoExistente == null)
            {
                return NotFound("Producto no encontrado.");
            }

            // Actualizar los campos del producto existente
            productoExistente.Nombre = productoDTO.Nombre;
            productoExistente.Marca = productoDTO.Marca;
            productoExistente.Modelo = productoDTO.Modelo;
            productoExistente.Precio = productoDTO.Precio;
            productoExistente.Stock = productoDTO.Stock;
            productoExistente.FechaActual = DateTime.UtcNow; 


            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al actualizar el producto.", detalle = ex.Message });
            }
        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // Buscar producto utilizando LINQ
            var productoExistente = await _context.MaestrosProducto
                .FirstOrDefaultAsync(p => p.IdProducto == id);

            if (productoExistente == null)
            {
                return NotFound("Producto no encontrado.");
            }

            // Cambiar el estado a 0 (Eliminado)
            productoExistente.Estado = 0;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al eliminar el producto.", detalle = ex.Message });
            }
        }


        //[HttpGet]
        //public async Task<IActionResult> GetProductos()
        //{
        //    var productos = await _context.MaestrosProducto.ToListAsync();

        //    var productoDTO = productos.Select(c => new MaestrosProductoDTO
        //    {
        //        IdProducto = c.IdProducto,
        //        Nombre = c.Nombre,
        //        Marca = c.Marca,
        //        Modelo = c.Modelo,
        //        Precio = c.Precio,
        //        Stock = c.Stock,
        //        Estado = c.Estado,
        //        IdUsuarioRegistro = c.IdUsuarioRegistro,
        //        IdUsuarioActual = c.IdUsuarioActual,
        //        FechaRegistro = c.FechaRegistro,
        //        FechaActual = c.FechaActual
        //    }).ToList();

        //    return Ok(productoDTO);
        //}

        [HttpGet]
        public async Task<IActionResult> GetProductos()
        {
            // Filtrar productos donde Estado es 1 (activos)
            var productos = await _context.MaestrosProducto
                .Where(c => c.Estado == 1)
                .ToListAsync();

            var productoDTO = productos.Select(c => new MaestrosProductoDTO
            {
                IdProducto = c.IdProducto,
                Nombre = c.Nombre,
                Marca = c.Marca,
                Modelo = c.Modelo,
                Precio = c.Precio,
                Stock = c.Stock,
                Estado = c.Estado,
                IdUsuarioRegistro = c.IdUsuarioRegistro,
                IdUsuarioActual = c.IdUsuarioActual,
                FechaRegistro = c.FechaRegistro,
                FechaActual = c.FechaActual
            }).ToList();

            return Ok(productoDTO);
        }

        [HttpGet("nombre")]
        public async Task<IActionResult> GetProductosByFilter([FromQuery] string producto)
        {
            try
            {
                var query = _context.MaestrosProducto
                .Where(p => p.Estado == 1) // Filtro para productos activos (Estado = 1)
                .AsQueryable();

                if (!string.IsNullOrEmpty(producto))
                {
                    // Convierte el término de búsqueda a minúsculas para la comparación
                    producto = producto.ToLower();
                    query = query.Where(p => p.Nombre.ToLower().Contains(producto));
                }

                var productos = await query.ToListAsync();

                var productoDtos = productos.Select(p => new MaestrosProductoDTO
                {
                    IdProducto = p.IdProducto,
                    Nombre = p.Nombre,
                    Marca = p.Marca,
                    Modelo = p.Modelo,
                    Precio = p.Precio,
                    Stock = p.Stock,
                    Estado = p.Estado,
                    IdUsuarioRegistro = p.IdUsuarioRegistro,
                    IdUsuarioActual = p.IdUsuarioActual,
                    FechaRegistro = p.FechaRegistro,
                    FechaActual = p.FechaActual
                }).ToList();

                return Ok(new
                {
                    Success = true,
                    Productos = productoDtos
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Success = false,
                    Message = "Se produjo un error al obtener los productos filtrados.",
                    Error = ex.Message
                });
            }
        }






    }
}
