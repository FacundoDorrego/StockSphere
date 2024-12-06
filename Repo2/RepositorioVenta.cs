using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clases;
using Repositorios;

namespace Repositorios
{
    public class RepositorioVenta
    {
        public void AgregarVenta(Venta venta)
        {
            
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                accesoDatos.SetearSp("AgregarVenta");
                accesoDatos.SetearParametros("@Monto", venta.Monto);
                accesoDatos.SetearParametros("@FechaVenta", venta.FechaVenta);
                accesoDatos.SetearParametros("@Cantidad", venta.Cantidad);
                accesoDatos.SetearParametros("@EmpresaID", venta.Empresa.EmpresaID);
                accesoDatos.SetearParametros("@UsuarioID", venta.Usuario.UsuarioID);
                accesoDatos.SetearParametros("@ProductoID", venta.Producto.ProductoID);
                accesoDatos.SetearParametros("@CategoriaID", venta.Categoria.CategoriaID);
                accesoDatos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la venta", ex);
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }

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
                        VentasID = Convert.ToInt32(accesoDatos.Lector["VentasID"]),
                        Monto = Convert.ToDecimal(accesoDatos.Lector["Monto"]),
                        Cantidad = Convert.ToInt32(accesoDatos.Lector["Cantidad"]),
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
            if (CargarObjetos(listaVentas))
            {
                return listaVentas;
            }
            else
            {
                throw new Exception("Error al cargar los objetos relacionados para las ventas.");
            }
        }

        private bool CargarObjetos(List<Venta> auxListaVentas)
        {
            
            try
            {
                RepositorioEmpresa repositorioEmpresa = new RepositorioEmpresa();
                RepositorioUsuario repositorioUsuario = new RepositorioUsuario();
                RepositorioProducto repositorioProducto = new RepositorioProducto();
                RepositorioCategoria repositorioCategoria = new RepositorioCategoria();
                auxListaVentas.ForEach(x =>
                {
                    x.Empresa = repositorioEmpresa.ObtenerEmpresaxID(x.EmpresaID);
                    x.Usuario = repositorioUsuario.ObtenerUsuarioxID(x.UsuarioID);
                    x.Producto = repositorioProducto.ObtenerProductoxID(x.ProductoID);
                    x.Categoria = repositorioCategoria.ObtenerCategoriaxID(x.CategoriaID);
                });

            return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception("Error al cargar los objetos", ex);
            }
            
        }

    }
}
