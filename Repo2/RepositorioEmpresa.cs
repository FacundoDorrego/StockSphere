using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositorios;
using Clases;

namespace Repositorios
{

    public class RepositorioEmpresa
    {

        public List<Empresa> ObtenerEmpresasAdmin()
        {
            List<Empresa> empresas = new List<Empresa>();
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {

                accesoDatos.SetearConsulta("SELECT * FROM Empresas");
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
                accesoDatos.CerrarConexion();
            }

            return empresas;
        }

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
                accesoDatos.CerrarConexion();
            }

            return empresas;
        }

        public Empresa ObtenerEmpresaxID(int idEmpre)
        {
            Empresa aux = new Empresa();
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {

                accesoDatos.SetearSp("ObtenerEmpresaxID");
                accesoDatos.SetearParametros("@EmpresaID", idEmpre);
                accesoDatos.EjecutarLectura();

                if (accesoDatos.Lector != null && accesoDatos.Lector.HasRows)
                {
                    while (accesoDatos.Lector.Read())
                    {
                        aux = new Empresa
                        {

                            EmpresaID = Convert.ToInt32(accesoDatos.Lector["EmpresaID"]),
                            Nombre = accesoDatos.Lector["Nombre"].ToString(),
                            UsuarioID = Convert.ToInt32(accesoDatos.Lector["UsuarioID"]),
                            FechaCreacion = Convert.ToDateTime(accesoDatos.Lector["FechaCreacion"]),
                            Activa = Convert.ToBoolean(accesoDatos.Lector["Activa"])
                        };

                    }
                }
                else
                {
                    throw new Exception("No se encontró la empresa.");
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener la empresa", ex);
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }

            return aux;
        }
        public void AgregarEmpresa(Empresa empresa)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            accesoDatos.SetearSp("AgregarEmpresa");
            accesoDatos.SetearParametros("@Nombre", empresa.Nombre);
            accesoDatos.SetearParametros("@UsuarioID", empresa.UsuarioID);
            accesoDatos.SetearParametros("@FechaCreacion", empresa.FechaCreacion);
            accesoDatos.SetearParametros("@Activa", empresa.Activa);
            accesoDatos.EjecutarAccion();
            accesoDatos.CerrarConexion();
        }


        public void ActualizarEmpresa(Empresa empresa)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            accesoDatos.SetearSp("ActualizarEmpresa");
            accesoDatos.SetearParametros("@EmpresaID", empresa.EmpresaID);
            accesoDatos.SetearParametros("@Nombre", empresa.Nombre);
            accesoDatos.SetearParametros("Activa", empresa.Activa);
            accesoDatos.EjecutarAccion();
            accesoDatos.CerrarConexion();
        }


        public void EliminarEmpresa(int empresaID)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            accesoDatos.SetearSp("EliminarEmpresa");
            accesoDatos.SetearParametros("@EmpresaID", empresaID);
            accesoDatos.EjecutarAccion();
            accesoDatos.CerrarConexion();
        }
    }

}
