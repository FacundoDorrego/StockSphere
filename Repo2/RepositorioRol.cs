using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clases;

namespace Repositorios
{
    public class RepositorioRol
    {
        public List<Rol> ObtenerRoles()
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            List<Rol> roles = new List<Rol>();
            try
            {
                accesoDatos.SetearConsulta("SELECT RolID, Descripcion FROM Roles");
                accesoDatos.EjecutarLectura();
                while (accesoDatos.Lector.Read())
                {
                    Rol rol = new Rol
                    {
                        RolID = (int)accesoDatos.Lector["RolID"],
                        Descripcion = accesoDatos.Lector["Descripcion"].ToString()
                    };
                    roles.Add(rol);
                }
                return roles;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar obtener los roles: " + ex.Message, ex);
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }
        }
    }
}
