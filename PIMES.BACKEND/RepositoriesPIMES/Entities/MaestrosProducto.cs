using System.ComponentModel.DataAnnotations;

namespace Biblioteca.RepositoriesPIMES.Entities
{
    public class MaestrosProducto
    {
        [Key]
        public Guid IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public double Precio { get; set; }
        public double Stock { get; set; }
        public Guid IdUsuarioRegistro { get; set; }
        public Guid IdUsuarioActual { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaActual { get; set; }
        public int Estado { get; set; }
        public SeguridadUsuarios UsuarioRegistro { get; set; }
        public SeguridadUsuarios UsuarioActual { get; set; }
        public ICollection<MovimientosCtasCobrarDetalle> MovimentosCobrarDetalle { get; set; }
        public ICollection<MovimientosCtasPagarDetalle> MovimientosPagarDetalle { get; set; }
    }
}
