using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clases;
using Repositorio;

namespace Repositorio
{
   
    public class RepositorioProveedor
    {
        private AccesoDatos accesoDatos = new AccesoDatos();

        public List<Proveedor> ObtenerProveedores()
        {
            List<Proveedor> proveedores = new List<Proveedor>();
            string consulta = "SELECT * FROM dbo.Proveedores";

            accesoDatos.SetearConsulta(consulta);
            accesoDatos.EjecutarLectura();

            while (accesoDatos.Lector.Read())
            {
                proveedores.Add(new Proveedor
                {
                    ProveedorID = Convert.ToInt32(accesoDatos.Lector["ProveedorID"]),
                    Nombre = accesoDatos.Lector["Nombre"].ToString(),
                    Telefono = accesoDatos.Lector["Telefono"].ToString(),
                    Email = accesoDatos.Lector["Email"].ToString(),
                    Direccion = accesoDatos.Lector["Direccion"].ToString()
                });
            }

            accesoDatos.CerrarConexion();
            return proveedores;
        }

        public void AgregarProveedor(Proveedor proveedor)
        {
            accesoDatos.SetearSp("dbo.AgregarProveedor");
            accesoDatos.SetearParametros("@Nombre", proveedor.Nombre);
            accesoDatos.SetearParametros("@Telefono", proveedor.Telefono );
            accesoDatos.SetearParametros("@Email", proveedor.Email);
            accesoDatos.SetearParametros("@Direccion", proveedor.Direccion);
            accesoDatos.EjecutarAccion();
        }

        public void ActualizarProveedor(Proveedor proveedor)
        {
            accesoDatos.SetearSp("dbo.ActualizarProveedor");
            accesoDatos.SetearParametros("@ProveedorID", proveedor.ProveedorID);
            accesoDatos.SetearParametros("@Nombre", proveedor.Nombre);
            accesoDatos.SetearParametros("@Telefono", proveedor.Telefono);
            accesoDatos.SetearParametros("@Email", proveedor.Email);
            accesoDatos.SetearParametros("@Direccion", proveedor.Direccion);
            accesoDatos.EjecutarAccion();
        }

        public void EliminarProveedor(int proveedorID)
        {
            accesoDatos.SetearSp("dbo.EliminarProveedor");
            accesoDatos.SetearParametros("@ProveedorID", proveedorID);
            accesoDatos.EjecutarAccion();
        }
    }

}
