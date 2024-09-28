using Biblioteca.RepositoriesPIMES.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.RepositoriesPIMES.Configurations
{
    public class ConfigurationPIMES : IEntityTypeConfiguration<SeguridadUsuarios>
    {
        public void Configure(EntityTypeBuilder<SeguridadUsuarios> builder)
        {
            builder.ToTable("SeguridadUsuarios");
            builder.HasKey(x => x.IdUsuario);
            builder.Property(x => x.IdUsuario).HasColumnName("IdUsuario");
            builder.Property(x => x.Nombres).HasColumnName("Nombres");
            builder.Property(x => x.Apellidos).HasColumnName("Apellidos");
            builder.Property(x => x.Estado).HasColumnName("Estado");
            builder.Property(x => x.Email).HasColumnName("Email");
            builder.Property(x => x.UserName).HasColumnName("UserName");
            builder.Property(x => x.Password).HasColumnName("Password");
        }

        public void Configure(EntityTypeBuilder<MaestrosCliente> builder)
        {
            builder.ToTable("MaestrosCliente");
            builder.HasKey(x => x.IdCliente);
            builder.Property(x => x.IdCliente).HasColumnName("IdCliente");
            builder.Property(x => x.Nombres).HasColumnName("Nombres");
            builder.Property(x => x.Apellidos).HasColumnName("Apellidos");
            builder.Property(x => x.TipoDoc).HasColumnName("TipoDoc");
            builder.Property(x => x.NumDoc).HasColumnName("NumDoc");
            builder.Property(x => x.RazonSocial).HasColumnName("RazonSocial");
            builder.Property(x => x.Ubicacion).HasColumnName("Ubicacion");
            builder.Property(x => x.Correo).HasColumnName("Correo");
            builder.Property(x => x.Email).HasColumnName("Email");
            builder.Property(x => x.TipoCliente).HasColumnName("TipoCliente");
            builder.Property(x => x.Estado).HasColumnName("Estado");
            builder.Property(x => x.IdUsuarioRegistro).HasColumnName("IdUsuarioRegistro");
            builder.Property(x => x.IdUsuarioActual).HasColumnName("IsUsuarioActual");
            builder.Property(x => x.FechaRegistro).HasColumnName("FechaRegistro");
            builder.Property(x => x.FechaActual).HasColumnName("FechaActual");
        }

        public void Configure(EntityTypeBuilder<MaestrosProveedor> builder)
        {
            builder.ToTable("MaestrosProveedor");
            builder.HasKey(x => x.IdProveedor);
            builder.Property(x => x.IdProveedor).HasColumnName("IdProveedor");
            builder.Property(x => x.Nombres).HasColumnName("Nombres");
            builder.Property(x => x.Apellidos).HasColumnName("Apellidos");
            builder.Property(x => x.TipoDoc).HasColumnName("TipoDoc");
            builder.Property(x => x.NumDoc).HasColumnName("NumDoc");
            builder.Property(x => x.RazonSocial).HasColumnName("RazonSocial");
            builder.Property(x => x.Ubicacion).HasColumnName("Ubicacion");
            builder.Property(x => x.Correo).HasColumnName("Correo");
            builder.Property(x => x.Email).HasColumnName("Email");
            builder.Property(x => x.TipoProveedor).HasColumnName("TipoProveedor");
            builder.Property(x => x.Estado).HasColumnName("Estado");
            builder.Property(x => x.IdUsuarioRegistro).HasColumnName("IdUsuarioRegistro");
            builder.Property(x => x.IdUsuarioActual).HasColumnName("IsUsuarioActual");
            builder.Property(x => x.FechaRegistro).HasColumnName("FechaRegistro");
            builder.Property(x => x.FechaActual).HasColumnName("FechaActual");
        }

        public void Configure(EntityTypeBuilder<MaestrosProducto> builder)
        {
            builder.ToTable("MaestrosProducto");
            builder.HasKey(x => x.IdProducto);
            builder.Property(x => x.IdProducto).HasColumnName("IdProducto");
            builder.Property(x => x.Nombre).HasColumnName("Nombre");
            builder.Property(x => x.Marca).HasColumnName("Marca");
            builder.Property(x => x.Modelo).HasColumnName("Modelo");
            builder.Property(x => x.Precio).HasColumnName("Precio");
            builder.Property(x => x.Stock).HasColumnName("Stock");
            builder.Property(x => x.Estado).HasColumnName("Estado");
            builder.Property(x => x.IdUsuarioRegistro).HasColumnName("IdUsuarioRegistro");
            builder.Property(x => x.IdUsuarioActual).HasColumnName("IsUsuarioActual");
            builder.Property(x => x.FechaRegistro).HasColumnName("FechaRegistro");
            builder.Property(x => x.FechaActual).HasColumnName("FechaActual");
        }

        public void Configure(EntityTypeBuilder<MovimientosCtasCobrarCabecera> builder)
        {
            builder.ToTable("MovimientosCtasCobrarCabecera");
            builder.HasKey(x => x.IdCtasCobrarCabecera);
            builder.Property(x => x.IdCtasCobrarCabecera).HasColumnName("IdCtasCobrarCabecera");
            builder.Property(x => x.Fecha).HasColumnName("Fecha");
            builder.Property(x => x.IdCliente).HasColumnName("IdCliente");
            builder.Property(x => x.NumDoc).HasColumnName("NumDoc");
            builder.Property(x => x.Total).HasColumnName("Total");
            builder.Property(x => x.Tipo).HasColumnName("Tipo");
            builder.Property(x => x.IdUsuarioRegistro).HasColumnName("IdUsuarioRegistro");
            builder.Property(x => x.IdUsuarioActual).HasColumnName("IsUsuarioActual");
            builder.Property(x => x.FechaRegistro).HasColumnName("FechaRegistro");
            builder.Property(x => x.FechaActual).HasColumnName("FechaActual");
        }

        public void Configure(EntityTypeBuilder<MovimientosCtasCobrarDetalle> builder)
        {
            builder.ToTable("MovimientosCtasCobrarDetalle");
            builder.HasKey(x => x.IdCtasCobrarDetalle);
            builder.Property(x => x.IdCtasCobrarDetalle).HasColumnName("IdCtasCobrarDetalle");
            builder.Property(x => x.IdCtasCobrarCabecera).HasColumnName("IdCtasCobrarCabecera");
            builder.Property(x => x.IdProducto).HasColumnName("IdProducto");
            builder.Property(x => x.Precio).HasColumnName("Precio");
            builder.Property(x => x.SubTotal).HasColumnName("SubTotal");
            builder.Property(x => x.Descuento).HasColumnName("Descuento");
            builder.Property(x => x.Cantidad).HasColumnName("Cantidad");
            builder.Property(x => x.IdUsuarioRegistro).HasColumnName("IdUsuarioRegistro");
            builder.Property(x => x.IdUsuarioActual).HasColumnName("IsUsuarioActual");
            builder.Property(x => x.FechaRegistro).HasColumnName("FechaRegistro");
            builder.Property(x => x.FechaActual).HasColumnName("FechaActual");
        }

        public void Configure(EntityTypeBuilder<MovimientosCtasPagarCabecera> builder)
        {
            builder.ToTable("MovimientosCtasPagarCabecera");
            builder.HasKey(x => x.IdCtasPagarCabecera);
            builder.Property(x => x.IdCtasPagarCabecera).HasColumnName("IdCtasPagarCabecera");
            builder.Property(x => x.Fecha).HasColumnName("Fecha");
            builder.Property(x => x.IdProveedor).HasColumnName("IdProveedor");
            builder.Property(x => x.NumDoc).HasColumnName("NumDoc");
            builder.Property(x => x.Total).HasColumnName("Total");
            builder.Property(x => x.IdUsuarioRegistro).HasColumnName("IdUsuarioRegistro");
            builder.Property(x => x.IdUsuarioActual).HasColumnName("IsUsuarioActual");
            builder.Property(x => x.FechaRegistro).HasColumnName("FechaRegistro");
            builder.Property(x => x.FechaActual).HasColumnName("FechaActual");
        }

        public void Configure(EntityTypeBuilder<MovimientosCtasPagarDetalle> builder)
        {
            builder.ToTable("MovimientosCtasPagarDetalle");
            builder.HasKey(x => x.IdCtasPagarDetalle);
            builder.Property(x => x.IdCtasPagarDetalle).HasColumnName("IdCtasPagarDetalle");
            builder.Property(x => x.IdCtasPagarCabecera).HasColumnName("IdCtasPagarCabecera");
            builder.Property(x => x.IdProducto).HasColumnName("IdProducto");
            builder.Property(x => x.Precio).HasColumnName("Precio");
            builder.Property(x => x.SubTotal).HasColumnName("SubTotal");
            builder.Property(x => x.Descuento).HasColumnName("Descuento");
            builder.Property(x => x.IdUsuarioRegistro).HasColumnName("IdUsuarioRegistro");
            builder.Property(x => x.IdUsuarioActual).HasColumnName("IsUsuarioActual");
            builder.Property(x => x.FechaRegistro).HasColumnName("FechaRegistro");
            builder.Property(x => x.FechaActual).HasColumnName("FechaActual");
        }
    }
}
