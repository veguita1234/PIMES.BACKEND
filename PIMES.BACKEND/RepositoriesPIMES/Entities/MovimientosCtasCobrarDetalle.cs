using System.ComponentModel.DataAnnotations;

namespace Biblioteca.RepositoriesPIMES.Entities
{
    public class MovimientosCtasCobrarDetalle
    {
        [Key]
        public Guid IdCtasCobrarDetalle { get; set; }
        public Guid IdCtasCobrarCabecera { get; set; }
        public Guid IdProducto { get; set; }
        public double Precio { get; set; }
        public double SubTotal { get; set; }
        public double Descuento { get; set; }
        public double Cantidad { get; set; }
        public Guid IdUsuarioRegistro { get; set; }
        public Guid IdUsuarioActual { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaActual { get; set; }

        public MovimientosCtasCobrarCabecera CobrarCabecera { get; set; }
        public MaestrosProducto CobrarProducto { get; set; }
        public SeguridadUsuarios UsuarioRegistro { get; set; }
        public SeguridadUsuarios UsuarioActual { get; set; }
    }
}
