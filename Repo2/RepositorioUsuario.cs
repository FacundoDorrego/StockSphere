using Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Repositorio
{
    public class RepositorioUsuario
    {
        public bool Loguear(Usuario usuario)
        {

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT UsuarioID, RolID, CorreoElectronico FROM Usuarios where CorreoElectronico = @Nombre and Clave = @Contra");

                datos.SetearParametros("@Contra", usuario.Clave);
                datos.SetearParametros("@Nombre", usuario.CorreoElectronico);


                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    usuario.UsuarioID = (int)datos.Lector["UsuarioID"];
                    usuario.Rol = (int)(datos.Lector["RolID"]);
                    return true;

                }

                return false;

            }
            catch (Exception ex)
            {

                throw new Exception("Error al intentar loguear el usuario: " + ex.Message, ex);
            }

            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}
