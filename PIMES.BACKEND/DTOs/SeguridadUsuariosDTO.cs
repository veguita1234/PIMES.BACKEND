namespace Biblioteca.DTOsPIMES
{
    public class SeguridadUsuariosDTO
    {
        public Guid IdUsuario { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Estado { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
