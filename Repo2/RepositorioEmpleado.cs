using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clases;

namespace Repositorios
{
    public class RepositorioEmpleado
    {
        public void AgregarEmpleado(Empleado auxEmpleado)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                accesoDatos.SetearSp("AgregarEmpleado");
                accesoDatos.SetearParametros("@UsuarioID", auxEmpleado.Usuario.UsuarioID);
                accesoDatos.SetearParametros("@EmpresaID", auxEmpleado.Empresa.EmpresaID);
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

        public Empleado ObtenerEmpleadoxIDUsu(int UsuarioID)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            RepositorioEmpresa repoEmpresa = new RepositorioEmpresa();
            RepositorioUsuario repoUsuario = new RepositorioUsuario();
            Empleado auxEmpleado = new Empleado();
            try
            {
                accesoDatos.SetearSp("ObtenerEmpleadoxIDUsu");
                accesoDatos.SetearParametros("@UsuarioID", UsuarioID);
                accesoDatos.EjecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    auxEmpleado.Usuario = repoUsuario.ObtenerUsuarioxID((int)accesoDatos.Lector["UsuarioID"]);
                    auxEmpleado.Empresa = repoEmpresa.ObtenerEmpresaxID((int)accesoDatos.Lector["EmpresaID"]);
                    auxEmpleado.EmpleadoID = (int)accesoDatos.Lector["EmpleadoID"];
                }
                return auxEmpleado;
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

        public Empleado ObtenerEmpleadoxID(int EmpleadoID)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            RepositorioEmpresa repoEmpresa = new RepositorioEmpresa();
            RepositorioUsuario repoUsuario = new RepositorioUsuario();
            Empleado auxEmpleado = new Empleado();
            try
            {
                accesoDatos.SetearSp("ObtenerEmpleadoxID");
                accesoDatos.SetearParametros("@EmpleadoID", EmpleadoID);
                accesoDatos.EjecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    auxEmpleado.Usuario = repoUsuario.ObtenerUsuarioxID((int)accesoDatos.Lector["UsuarioID"]);
                    auxEmpleado.Empresa = repoEmpresa.ObtenerEmpresaxID((int)accesoDatos.Lector["EmpresaID"]);
                    auxEmpleado.EmpleadoID = (int)accesoDatos.Lector["EmpleadoID"];
                }
                return auxEmpleado;
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

        public List<Empleado> ObtenerEmpleadosxEmpresa(int empresaID)
        {
            List<Empleado> listaEmpleados = new List<Empleado>();
            try
            {
                AccesoDatos accesoDatos = new AccesoDatos();
                accesoDatos.SetearSp("ObtenerEmpleadosxEmpresa");
                accesoDatos.SetearParametros("@EmpresaID", empresaID);
                accesoDatos.EjecutarLectura();
                while (accesoDatos.Lector.Read())
                {
                    Empleado auxEmpleado = new Empleado();
                    RepositorioUsuario repoUsuario = new RepositorioUsuario();
                    RepositorioEmpresa repoEmpresa = new RepositorioEmpresa();
                    auxEmpleado.Usuario = repoUsuario.ObtenerUsuarioxID((int)accesoDatos.Lector["UsuarioID"]);
                    auxEmpleado.Empresa = repoEmpresa.ObtenerEmpresaxID((int)accesoDatos.Lector["EmpresaID"]);
                    auxEmpleado.EmpleadoID = (int)accesoDatos.Lector["EmpleadoID"];
                    listaEmpleados.Add(auxEmpleado);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listaEmpleados;
        }

        public void EliminarEmpleado(int EmpleadoID)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                accesoDatos.SetearSp("EliminarEmpleado");
                accesoDatos.SetearParametros("@EmpleadoID", EmpleadoID);
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
