using Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorios
{
    public class RepositorioUsuario
    {
        public bool Loguear(Usuario usuario)
        {
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
                    usuario.RolID = (int)(accesoDatos.Lector["RolID"]);
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
                        RolID = Convert.ToInt32(accesoDatos.Lector["RolID"]),
                        NombreUsuario = accesoDatos.Lector["NombreUsuario"].ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }
            return aux;
        }

        public void CrearUsuario(Usuario aux)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                accesoDatos.SetearSp("CrearUsuario");
                accesoDatos.SetearParametros("@Correo", aux.CorreoElectronico);
                accesoDatos.SetearParametros("@Clave", aux.Clave);
                accesoDatos.SetearParametros("@NombreUsuario", aux.NombreUsuario);
                accesoDatos.SetearParametros("@RolID", aux.RolID);
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

        public List<Usuario> ObtenerUsuarios()
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            List<Usuario> lista = new List<Usuario>();
            try
            {
                accesoDatos.SetearSp("ObtenerUsuarios");
                accesoDatos.EjecutarLectura();
                while (accesoDatos.Lector.Read())
                {
                    Usuario aux = new Usuario
                    {
                        UsuarioID = Convert.ToInt32(accesoDatos.Lector["UsuarioID"]),
                        CorreoElectronico = accesoDatos.Lector["CorreoElectronico"].ToString(),
                        Clave = accesoDatos.Lector["Clave"].ToString(),
                        RolID = Convert.ToInt32(accesoDatos.Lector["RolID"]),
                        NombreUsuario = accesoDatos.Lector["NombreUsuario"].ToString()
                    };
                    lista.Add(aux);
                }
                return lista;
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

        public void ModificarUsuario(Usuario aux)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                accesoDatos.SetearSp("ModificarUsuario");
                accesoDatos.SetearParametros("@NombreUsuario", aux.NombreUsuario);
                accesoDatos.SetearParametros("@Correo", aux.CorreoElectronico);
                accesoDatos.SetearParametros("@Clave", aux.Clave);
                accesoDatos.SetearParametros("@RolID", aux.RolID);
                accesoDatos.SetearParametros("@UsuarioID", aux.UsuarioID);
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

        public void EliminarUsuario(int usuarioID)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                accesoDatos.SetearSp("EliminarUsuario");
                accesoDatos.SetearParametros("@UsuarioID", usuarioID);
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
