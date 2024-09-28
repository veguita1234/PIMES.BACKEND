using System.ComponentModel.DataAnnotations;

namespace Biblioteca.RepositoriesPIMES.Entities
{
    public class MovimientosCtasPagarCabecera
    {
        [Key]
        public Guid IdCtasPagarCabecera { get; set; }
        public DateTime Fecha { get; set; }
        public Guid IdProveedor { get; set; }
        public string NumDoc { get; set; }
        public double Total { get; set; }
        public Guid IdUsuarioRegistro { get; set; }
        public Guid IdUsuarioActual { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaActual { get; set; }

        public MaestrosProveedor Proveedor { get; set; }
        public SeguridadUsuarios UsuarioRegistro { get; set; }
        public SeguridadUsuarios UsuarioActual { get; set; }
        public ICollection<MovimientosCtasPagarDetalle> movimientosCtasPagarDetalles { get; set; }
    }
}
