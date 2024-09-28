namespace Biblioteca.DTOsPIMES
{
    public class MovimientosCtasCobrarDetalleDTO
    {
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
    }
}
