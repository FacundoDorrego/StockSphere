using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clases;
using Repositorios;

namespace Repositorios
{
   
    public class RepositorioProveedor
    {
        private AccesoDatos accesoDatos = new AccesoDatos();

        public List<Proveedor> ObtenerProveedoresxEmpresa(int empresaID)
        {
            List<Proveedor> proveedores = new List<Proveedor>();
            accesoDatos.SetearSp("ObtenerProveedoresxEmpresa");
            accesoDatos.SetearParametros("@empresaID", empresaID);
            accesoDatos.EjecutarLectura();

            while (accesoDatos.Lector.Read())
            {
                proveedores.Add(new Proveedor
                {
                    ProveedorID = Convert.ToInt32(accesoDatos.Lector["ProveedorID"]),
                    Nombre = accesoDatos.Lector["Nombre"].ToString(),
                    Telefono = accesoDatos.Lector["Telefono"].ToString(),
                    Email = accesoDatos.Lector["Email"].ToString(),
                    Direccion = accesoDatos.Lector["Direccion"].ToString(),
                    EmpresaID = Convert.ToInt32(accesoDatos.Lector["EmpresaID"])
                });
            }

            accesoDatos.CerrarConexion();
            return proveedores;
        }

        public void AgregarProveedor(Proveedor proveedor)
        {
            accesoDatos.SetearSp("AgregarProveedor");
            accesoDatos.SetearParametros("@Nombre", proveedor.Nombre);
            accesoDatos.SetearParametros("@Telefono", proveedor.Telefono );
            accesoDatos.SetearParametros("@Email", proveedor.Email);
            accesoDatos.SetearParametros("@Direccion", proveedor.Direccion);
            accesoDatos.SetearParametros("@EmpresaId", proveedor.EmpresaID);
            accesoDatos.EjecutarAccion();
            accesoDatos.CerrarConexion();
        }

        public void ActualizarProveedor(Proveedor proveedor)
        {
            accesoDatos.SetearSp("ActualizarProveedor");
            accesoDatos.SetearParametros("@ProveedorID", proveedor.ProveedorID);
            accesoDatos.SetearParametros("@Nombre", proveedor.Nombre);
            accesoDatos.SetearParametros("@Telefono", proveedor.Telefono);
            accesoDatos.SetearParametros("@Email", proveedor.Email);
            accesoDatos.SetearParametros("@Direccion", proveedor.Direccion);
            accesoDatos.SetearParametros("@EmpresaId", proveedor.EmpresaID);
            accesoDatos.EjecutarAccion();
            accesoDatos.CerrarConexion();
        }

        public void EliminarProveedor(int proveedorID)
        {
            accesoDatos.SetearSp("EliminarProveedor");
            accesoDatos.SetearParametros("@ProveedorID", proveedorID);
            accesoDatos.EjecutarAccion();
            accesoDatos.CerrarConexion();
        }
    }

}
