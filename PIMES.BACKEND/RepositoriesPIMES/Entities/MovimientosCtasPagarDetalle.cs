using System.ComponentModel.DataAnnotations;

namespace Biblioteca.RepositoriesPIMES.Entities
{
    public class MovimientosCtasPagarDetalle
    {
        [Key]
        public Guid IdCtasPagarDetalle { get; set; }
        public Guid IdCtasPagarCabecera { get; set; }
        public Guid IdProducto { get; set; }
        public double Precio { get; set; }
        public double SubTotal { get; set; }
        public double Descuento { get; set; }
        public Guid IdUsuarioRegistro { get; set; }
        public Guid IdUsuarioActual { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaActual { get; set; }

        public MovimientosCtasPagarCabecera PagarCabecera { get; set; }
        public MaestrosProducto PagarProducto { get; set; }
        public SeguridadUsuarios UsuarioRegistro { get; set; }
        public SeguridadUsuarios UsuarioActual { get; set; }
    }
}
