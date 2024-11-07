namespace Clases
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string NombreUsuario { get; set; }
        public string CorreoElectronico { get; set; }
        public string Clave { get; set; }
        public int Rol { get; set; }

        public Usuario(string user, string pass)
        {
            CorreoElectronico = user;
            Clave = pass;
        }

    }

}
