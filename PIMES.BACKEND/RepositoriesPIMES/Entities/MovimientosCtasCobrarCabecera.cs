using System.ComponentModel.DataAnnotations;

namespace Biblioteca.RepositoriesPIMES.Entities
{
    public class MovimientosCtasCobrarCabecera
    {
        [Key]
        public Guid IdCtasCobrarCabecera { get; set; }
        public DateTime Fecha { get; set; }
        public Guid IdCliente { get; set; }
        public string NumDoc { get; set; }
        public double Total { get; set; }
        public string Tipo { get; set; }
        public Guid IdUsuarioRegistro { get; set; }
        public Guid IdUsuarioActual { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaActual { get; set; }

        public MaestrosCliente Cliente { get; set; }
        public SeguridadUsuarios UsuarioRegistro { get; set; }
        public SeguridadUsuarios UsuarioActual { get; set; }
        public ICollection<MovimientosCtasCobrarDetalle> movimientosCtasCobrarDetalles { get; set; }
    }
}
