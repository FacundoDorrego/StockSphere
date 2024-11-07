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
        private AccesoDatos accesoDatos = new AccesoDatos();

        // Obtener todas las empresas
        public List<Empresa> ObtenerEmpresasPorUsuario(int idUsu)
        {
            List<Empresa> empresas = new List<Empresa>();
            
            accesoDatos.SetearSp("ObtenerEmpresasPorUsuario");
            accesoDatos.SetearParametros("@UsuarioID", idUsu);
            accesoDatos.EjecutarAccion();

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

            accesoDatos.CerrarConexion();
            return empresas;
        }

        // Agregar una empresa
        public void AgregarEmpresa(Empresa empresa)
        {
            accesoDatos.SetearSp("dbo.AgregarEmpresa"); // Procedimiento almacenado que agrega la empresa
            accesoDatos.SetearParametros("@Nombre", empresa.Nombre);
            accesoDatos.SetearParametros("@UsuarioID", empresa.UsuarioID);
            accesoDatos.SetearParametros("@FechaCreacion", empresa.FechaCreacion);
            accesoDatos.SetearParametros("@Activa", empresa.Activa);
            accesoDatos.EjecutarAccion();
        }

        // Actualizar una empresa
        public void ActualizarEmpresa(Empresa empresa)
        {
            accesoDatos.SetearSp("dbo.ActualizarEmpresa"); // Procedimiento almacenado que actualiza la empresa
            accesoDatos.SetearParametros("@EmpresaID", empresa.EmpresaID);
            accesoDatos.SetearParametros("@Nombre", empresa.Nombre);
          
            accesoDatos.EjecutarAccion();
        }

        // Eliminar una empresa
        public void EliminarEmpresa(int empresaID)
        {
            accesoDatos.SetearSp("dbo.EliminarEmpresa"); // Procedimiento almacenado que elimina la empresa
            accesoDatos.SetearParametros("@EmpresaID", empresaID);
            accesoDatos.EjecutarAccion();
        }
    }

}
