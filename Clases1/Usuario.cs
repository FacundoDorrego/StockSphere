using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Clases
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string NombreUsuario { get; set; }
        public string CorreoElectronico { get; set; }
        public string Clave { get; set; }
        public int RolID { get; set; }

        public Usuario(string correo, string clave)
        {
            CorreoElectronico = correo;
            Clave = clave;
        }
        
        public Usuario(string nombre, string correo, string clave, int rol)
        {
            NombreUsuario = nombre;
            CorreoElectronico = correo;
            Clave = clave;
            RolID = rol;
        }

        public Usuario()
        {
        }
    }

}
