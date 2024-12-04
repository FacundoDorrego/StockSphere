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
        public bool Loguear(Usuario usuario){
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                accesoDatos.SetearSp("LoguearUsuario");

                accesoDatos.SetearParametros("@Correo", usuario.CorreoElectronico);
                accesoDatos.SetearParametros("@Clave", usuario.Clave);


                accesoDatos.EjecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    usuario.UsuarioID = (int)accesoDatos.Lector["UsuarioID"];
                    usuario.Rol = (int)(accesoDatos.Lector["RolID"]);
                    usuario.NombreUsuario = accesoDatos.Lector["NombreUsuario"].ToString();
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
                accesoDatos.CerrarConexion();
            }
        }

        public Usuario ObtenerUsuarioxID(int usuarioID)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            Usuario aux = new Usuario();
            try
            {
                accesoDatos.SetearSp("ObtenerUsuarioxID");
                accesoDatos.SetearParametros("@UsuarioID", usuarioID);
                accesoDatos.EjecutarLectura();
                while (accesoDatos.Lector.Read())
                {
                    aux = new Usuario
                    {
                        UsuarioID = Convert.ToInt32(accesoDatos.Lector["UsuarioID"]),
                        CorreoElectronico = accesoDatos.Lector["CorreoElectronico"].ToString(),
                        Clave = accesoDatos.Lector["Clave"].ToString(),
                        Rol = Convert.ToInt32(accesoDatos.Lector["RolID"]),
                        NombreUsuario = accesoDatos.Lector["NombreUsuario"].ToString()
                    };
                }
            } catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }
            return aux;
        }
        
        public void CrearUsuario(string correo,string clave,string nombreUsuario,int rol)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                accesoDatos.SetearSp("CrearUsuario");
                accesoDatos.SetearParametros("@Correo", correo);
                accesoDatos.SetearParametros("@Clave", clave);
                accesoDatos.SetearParametros("@NombreUsuario", nombreUsuario);
                accesoDatos.SetearParametros("@Rol", rol);
                accesoDatos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }
        }
    }


}
