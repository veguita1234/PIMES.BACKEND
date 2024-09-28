using System.ComponentModel.DataAnnotations;

namespace Biblioteca.RepositoriesPIMES.Entities
{
    public class MaestrosCliente
    {
        [Key]
        public Guid IdCliente { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? TipoDoc { get; set; }
        public string? NumDoc { get; set; }
        public string? RazonSocial { get; set; }
        public string? Ubicacion { get; set; }
        public string? Correo { get; set; }
        public string? Email { get; set; }
        public string? TipoCliente { get; set; }
        public int Estado {  get; set; }
        public Guid IdUsuarioRegistro { get; set; }
        public Guid IdUsuarioActual { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaActual { get; set; }

        public SeguridadUsuarios UsuarioRegistro { get; set; }
        public SeguridadUsuarios UsuarioActual { get; set; }

        public ICollection<MovimientosCtasCobrarCabecera> MovimientosCtasCobrar { get; set; }

    }
}
