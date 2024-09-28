using System.ComponentModel.DataAnnotations;

namespace Biblioteca.RepositoriesPIMES.Entities
{
    public class SeguridadUsuarios
    {
        [Key]
        public Guid IdUsuario { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Estado { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public ICollection<MaestrosCliente> ClientesRegistrados { get; set; }
        public ICollection<MaestrosCliente> ClientesActualizados { get; set; }

        public ICollection<MaestrosProveedor> ProveedoresRegistrados { get; set; }
        public ICollection<MaestrosProveedor> ProveedoresActualizados { get; set; }

        public ICollection<MaestrosProducto> ProductosRegistrados { get; set; }
        public ICollection<MaestrosProducto> ProductosActualizados { get; set; }

        public ICollection<MovimientosCtasCobrarCabecera> CabeceraCobrarRegistrados { get; set; }
        public ICollection<MovimientosCtasCobrarCabecera> CabeceraCobrarActualizados { get; set; }

        public ICollection<MovimientosCtasPagarCabecera> CabeceraPagarRegistrados { get; set; }
        public ICollection<MovimientosCtasPagarCabecera> CabeceraPagarActualizados { get; set; }

        public ICollection<MovimientosCtasCobrarDetalle> DetalleCobrarRegistrados { get; set; }
        public ICollection<MovimientosCtasCobrarDetalle> DetalleCobrarActualizados { get; set; }

        public ICollection<MovimientosCtasPagarDetalle> DetallePagarRegistrados { get; set; }
        public ICollection<MovimientosCtasPagarDetalle> DetallePagarActualizados { get; set; }

    }
}
