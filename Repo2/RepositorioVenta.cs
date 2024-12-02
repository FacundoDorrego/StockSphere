using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clases;
using Repositorio;

namespace Repositorios
{
    public class RepositorioVenta
    {
        public void GuardarVenta(Venta venta)
        {
            Categoria auxCategoria = new Categoria();
            Empresa auxEmpresa = new Empresa();
            Producto auxProducto = new Producto();
            Usuario auxUsuario = new Usuario();
            Venta auxVenta = new Venta();

        }

        public List<Venta> ObtenerVentasxEmpresa(int empresaID)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            List<Venta> listaVentas = new List<Venta>();
            try
            {
                accesoDatos.SetearSp("ObtenerVentasxEmpresa");
                accesoDatos.SetearParametros("@EmpresaID", empresaID);
                accesoDatos.EjecutarLectura();
                while (accesoDatos.Lector.Read())
                {
                    listaVentas.Add(new Venta
                    {
                        VentaID = Convert.ToInt32(accesoDatos.Lector["VentaID"]),
                        Monto = Convert.ToDecimal(accesoDatos.Lector["Monto"]),
                        FechaVenta = Convert.ToDateTime(accesoDatos.Lector["FechaVenta"]),
                        EmpresaID = Convert.ToInt32(accesoDatos.Lector["EmpresaID"]),
                        UsuarioID = Convert.ToInt32(accesoDatos.Lector["UsuarioID"]),
                        ProductoID = Convert.ToInt32(accesoDatos.Lector["ProductoID"]),
                        CategoriaID = Convert.ToInt32(accesoDatos.Lector["CategoriaID"])
                    });

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las ventas", ex);
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }
            return listaVentas;
        }


    }
}
