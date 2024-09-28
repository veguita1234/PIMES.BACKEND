using Biblioteca.RepositoriesPIMES.Entities;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.RepositoriesPIMES
{
    public class AppDbContextPIMES : DbContext
    {
        public AppDbContextPIMES(DbContextOptions<AppDbContextPIMES> options) : base(options)
        {

        }

        public DbSet<SeguridadUsuarios> SeguridadUsuarios { get; set; }
        public DbSet<MaestrosCliente> MaestrosCliente { get; set; }
        public DbSet<MaestrosProveedor> MaestrosProveedor { get; set; }
        public DbSet<MaestrosProducto> MaestrosProducto { get; set; }
        public DbSet<MovimientosCtasCobrarCabecera> MovimientosCtasCobrarCabecera { get; set; }
        public DbSet<MovimientosCtasPagarCabecera> MovimientosCtasPagarCabecera { get; set; }
        public DbSet<MovimientosCtasCobrarDetalle> MovimientosCtasCobrarDetalle { get; set; }
        public DbSet<MovimientosCtasPagarDetalle> MovimientosCtasPagarDetalle { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración para la tabla Seguridad.Usuarios
            modelBuilder.Entity<SeguridadUsuarios>()
                .ToTable("Usuarios", "Seguridad");

            modelBuilder.Entity<SeguridadUsuarios>()
                .HasMany(u => u.ClientesRegistrados)
                .WithOne(c => c.UsuarioRegistro)
                .HasForeignKey(c => c.IdUsuarioRegistro);

            modelBuilder.Entity<SeguridadUsuarios>()
                .HasMany(u => u.ClientesActualizados)
                .WithOne(c => c.UsuarioActual)
                .HasForeignKey(c => c.IdUsuarioActual);

            modelBuilder.Entity<SeguridadUsuarios>()
                .HasMany(u => u.ProveedoresRegistrados)
                .WithOne(p => p.UsuarioRegistro)
                .HasForeignKey(p => p.IdUsuarioRegistro);

            modelBuilder.Entity<SeguridadUsuarios>()
                .HasMany(u => u.ProveedoresActualizados)
                .WithOne(p => p.UsuarioActual)
                .HasForeignKey(p => p.IdUsuarioActual);

            modelBuilder.Entity<SeguridadUsuarios>()
                .HasMany(u => u.ProductosRegistrados)
                .WithOne(p => p.UsuarioRegistro)
                .HasForeignKey(p => p.IdUsuarioRegistro);

            modelBuilder.Entity<SeguridadUsuarios>()
                .HasMany(u => u.ProductosActualizados)
                .WithOne(p => p.UsuarioActual)
                .HasForeignKey(p => p.IdUsuarioActual);

            modelBuilder.Entity<SeguridadUsuarios>()
                .HasMany(u => u.CabeceraCobrarRegistrados)
                .WithOne(c => c.UsuarioRegistro)
                .HasForeignKey(c => c.IdUsuarioRegistro);

            modelBuilder.Entity<SeguridadUsuarios>()
                .HasMany(u => u.CabeceraCobrarActualizados)
                .WithOne(c => c.UsuarioActual)
                .HasForeignKey(c => c.IdUsuarioActual);

            modelBuilder.Entity<SeguridadUsuarios>()
                .HasMany(u => u.CabeceraPagarRegistrados)
                .WithOne(p => p.UsuarioRegistro)
                .HasForeignKey(p => p.IdUsuarioRegistro);

            modelBuilder.Entity<SeguridadUsuarios>()
                .HasMany(u => u.CabeceraPagarActualizados)
                .WithOne(p => p.UsuarioActual)
                .HasForeignKey(p => p.IdUsuarioActual);

            modelBuilder.Entity<SeguridadUsuarios>()
                .HasMany(u => u.DetalleCobrarRegistrados)
                .WithOne(d => d.UsuarioRegistro)
                .HasForeignKey(d => d.IdUsuarioRegistro);

            modelBuilder.Entity<SeguridadUsuarios>()
                .HasMany(u => u.DetalleCobrarActualizados)
                .WithOne(d => d.UsuarioActual)
                .HasForeignKey(d => d.IdUsuarioActual);

            modelBuilder.Entity<SeguridadUsuarios>()
                .HasMany(u => u.DetallePagarRegistrados)
                .WithOne(d => d.UsuarioRegistro)
                .HasForeignKey(d => d.IdUsuarioRegistro);

            modelBuilder.Entity<SeguridadUsuarios>()
                .HasMany(u => u.DetallePagarActualizados)
                .WithOne(d => d.UsuarioActual)
                .HasForeignKey(d => d.IdUsuarioActual);

            // Configuración para la tabla Movimientos.CtasPagarDetalle
            modelBuilder.Entity<MovimientosCtasPagarDetalle>()
                .ToTable("CtasPagarDetalle", "Movimientos");

            modelBuilder.Entity<MovimientosCtasPagarDetalle>()
                .HasOne(d => d.PagarCabecera)
                .WithMany(c => c.movimientosCtasPagarDetalles)
                .HasForeignKey(d => d.IdCtasPagarCabecera);

            modelBuilder.Entity<MovimientosCtasPagarDetalle>()
                .HasOne(d => d.PagarProducto)
                .WithMany(p => p.MovimientosPagarDetalle)
                .HasForeignKey(d => d.IdProducto);

            modelBuilder.Entity<MovimientosCtasPagarDetalle>()
                .HasOne(d => d.UsuarioRegistro)
                .WithMany(u => u.DetallePagarRegistrados)
                .HasForeignKey(d => d.IdUsuarioRegistro);

            modelBuilder.Entity<MovimientosCtasPagarDetalle>()
                .HasOne(d => d.UsuarioActual)
                .WithMany(u => u.DetallePagarActualizados)
                .HasForeignKey(d => d.IdUsuarioActual);

            // Configuración para la tabla Movimientos.CtasPagarCabecera
            modelBuilder.Entity<MovimientosCtasPagarCabecera>()
                .ToTable("CtasPagarCabecera", "Movimientos");

            modelBuilder.Entity<MovimientosCtasPagarCabecera>()
                .HasOne(c => c.Proveedor)
                .WithMany(p => p.MovimientosCtasCobrar)
                .HasForeignKey(c => c.IdProveedor);

            modelBuilder.Entity<MovimientosCtasPagarCabecera>()
                .HasOne(c => c.UsuarioRegistro)
                .WithMany(u => u.CabeceraPagarRegistrados)
                .HasForeignKey(c => c.IdUsuarioRegistro);

            modelBuilder.Entity<MovimientosCtasPagarCabecera>()
                .HasOne(c => c.UsuarioActual)
                .WithMany(u => u.CabeceraPagarActualizados)
                .HasForeignKey(c => c.IdUsuarioActual);

            // Configuración para la tabla Movimientos.CtasCobrarDetalle
            modelBuilder.Entity<MovimientosCtasCobrarDetalle>()
                .ToTable("CtasCobrarDetalle", "Movimientos");

            modelBuilder.Entity<MovimientosCtasCobrarDetalle>()
                .HasOne(d => d.CobrarCabecera)
                .WithMany(c => c.movimientosCtasCobrarDetalles)
                .HasForeignKey(d => d.IdCtasCobrarCabecera);

            modelBuilder.Entity<MovimientosCtasCobrarDetalle>()
                .HasOne(d => d.CobrarProducto)
                .WithMany(p => p.MovimentosCobrarDetalle)
                .HasForeignKey(d => d.IdProducto);

            modelBuilder.Entity<MovimientosCtasCobrarDetalle>()
                .HasOne(d => d.UsuarioRegistro)
                .WithMany(u => u.DetalleCobrarRegistrados)
                .HasForeignKey(d => d.IdUsuarioRegistro);

            modelBuilder.Entity<MovimientosCtasCobrarDetalle>()
                .HasOne(d => d.UsuarioActual)
                .WithMany(u => u.DetalleCobrarActualizados)
                .HasForeignKey(d => d.IdUsuarioActual);

            // Configuración para la tabla Movimientos.CtasCobrarCabecera
            modelBuilder.Entity<MovimientosCtasCobrarCabecera>()
                .ToTable("CtasCobrarCabecera", "Movimientos");

            modelBuilder.Entity<MovimientosCtasCobrarCabecera>()
                .HasOne(c => c.Cliente)
                .WithMany(cl => cl.MovimientosCtasCobrar)
                .HasForeignKey(c => c.IdCliente);

            modelBuilder.Entity<MovimientosCtasCobrarCabecera>()
                .HasOne(c => c.UsuarioRegistro)
                .WithMany(u => u.CabeceraCobrarRegistrados)
                .HasForeignKey(c => c.IdUsuarioRegistro);

            modelBuilder.Entity<MovimientosCtasCobrarCabecera>()
                .HasOne(c => c.UsuarioActual)
                .WithMany(u => u.CabeceraCobrarActualizados)
                .HasForeignKey(c => c.IdUsuarioActual);

            // Configuración para la tabla Maestros.Proveedor
            modelBuilder.Entity<MaestrosProveedor>()
                .ToTable("Proveedor", "Maestros");

            modelBuilder.Entity<MaestrosProveedor>()
                .HasMany(p => p.MovimientosCtasCobrar)
                .WithOne(c => c.Proveedor)
                .HasForeignKey(c => c.IdProveedor);

            // Configuración para la tabla Maestros.Producto
            modelBuilder.Entity<MaestrosProducto>()
                .ToTable("Producto", "Maestros");

            modelBuilder.Entity<MaestrosProducto>()
                .HasMany(p => p.MovimientosPagarDetalle)
                .WithOne(d => d.PagarProducto)
                .HasForeignKey(d => d.IdProducto);

            modelBuilder.Entity<MaestrosProducto>()
                .HasMany(p => p.MovimentosCobrarDetalle)
                .WithOne(d => d.CobrarProducto)
                .HasForeignKey(d => d.IdProducto);

            // Configuración para la tabla Maestros.Cliente
            modelBuilder.Entity<MaestrosCliente>()
                .ToTable("Cliente", "Maestros");

            modelBuilder.Entity<MaestrosCliente>()
                .HasMany(c => c.MovimientosCtasCobrar)
                .WithOne(c => c.Cliente)
                .HasForeignKey(c => c.IdCliente);

            base.OnModelCreating(modelBuilder);
        }

    }
}
