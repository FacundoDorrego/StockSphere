using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositorio;
using Clases;

namespace Repositorio
{

    public class RepositorioEmpresa
    {
        

        public List<Empresa> ObtenerEmpresasPorUsuario(int idUsu)
        {
            List<Empresa> empresas = new List<Empresa>();
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                
                accesoDatos.SetearSp("ObtenerEmpresasPorUsuario");
                accesoDatos.SetearParametros("@UsuarioID", idUsu);
                accesoDatos.EjecutarLectura();

                if (accesoDatos.Lector != null && accesoDatos.Lector.HasRows)
                {
                    while (accesoDatos.Lector.Read())
                    {
                        empresas.Add(new Empresa
                        {
                            EmpresaID = Convert.ToInt32(accesoDatos.Lector["EmpresaID"]),
                            Nombre = accesoDatos.Lector["Nombre"].ToString(),
                            UsuarioID = Convert.ToInt32(accesoDatos.Lector["UsuarioID"]),
                            FechaCreacion = Convert.ToDateTime(accesoDatos.Lector["FechaCreacion"]),
                            Activa = Convert.ToBoolean(accesoDatos.Lector["Activa"])
                        });
                    }
                }
                else
                {
                    throw new Exception("No se encontraron empresas para el usuario.");
                }
            }
            catch (Exception ex)
            {
                
                throw new Exception("Error al obtener las empresas", ex);
            }
            finally
            {
                accesoDatos.CerrarConexion(); // Asegurarse de que la conexión se cierre correctamente
            }

            return empresas;
        }

        // Agregar una empresa
        public void AgregarEmpresa(Empresa empresa)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            accesoDatos.SetearSp("dbo.AgregarEmpresa"); // Procedimiento almacenado que agrega la empresa
            accesoDatos.SetearParametros("@Nombre", empresa.Nombre);
            accesoDatos.SetearParametros("@UsuarioID", empresa.UsuarioID);
            accesoDatos.SetearParametros("@FechaCreacion", empresa.FechaCreacion);
            accesoDatos.SetearParametros("@Activa", empresa.Activa);
            accesoDatos.EjecutarAccion();
            accesoDatos.CerrarConexion();
        }

        // Actualizar una empresa
        public void ActualizarEmpresa(Empresa empresa)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            accesoDatos.SetearSp("dbo.ActualizarEmpresa"); // Procedimiento almacenado que actualiza la empresa
            accesoDatos.SetearParametros("@EmpresaID", empresa.EmpresaID);
            accesoDatos.SetearParametros("@Nombre", empresa.Nombre);
            accesoDatos.EjecutarAccion();
            accesoDatos.CerrarConexion();
        }

        // Eliminar una empresa
        public void EliminarEmpresa(int empresaID)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            accesoDatos.SetearSp("dbo.EliminarEmpresa"); // Procedimiento almacenado que elimina la empresa
            accesoDatos.SetearParametros("@EmpresaID", empresaID);
            accesoDatos.EjecutarAccion();
            accesoDatos.CerrarConexion();
        }
    }

}
