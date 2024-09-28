namespace Biblioteca.DTOsPIMES
{
    public class MovimientosCtasCobrarCabeceraDTO
    {
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
    }
}
