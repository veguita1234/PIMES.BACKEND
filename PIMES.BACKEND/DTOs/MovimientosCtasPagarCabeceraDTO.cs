﻿namespace Biblioteca.DTOsPIMES
{
    public class MovimientosCtasPagarCabeceraDTO
    {
        public Guid IdCtasPagarCabecera { get; set; }
        public DateTime Fecha { get; set; }
        public Guid IdProveedor { get; set; }
        public string NumDoc { get; set; }
        public double Total { get; set; }
        public Guid IdUsuarioRegistro { get; set; }
        public Guid IdUsuarioActual { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaActual { get; set; }
    }
}