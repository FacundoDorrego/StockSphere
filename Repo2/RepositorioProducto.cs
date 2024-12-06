using System;
using System.Collections.Generic;
using Clases;
using Repositorios;


namespace Repositorios
{
    public class RepositorioProducto
    {
        private AccesoDatos accesoDatos = new AccesoDatos();

        public List<Producto> ObtenerProductos()
        {
            List<Producto> productos = new List<Producto>();
            accesoDatos.SetearSp("ObtenerProductos");
            accesoDatos.EjecutarLectura();

            while (accesoDatos.Lector.Read())
            {
                productos.Add(new Producto
                {
                    ProductoID = Convert.ToInt32(accesoDatos.Lector["ProductoID"]),
                    Nombre = accesoDatos.Lector["Nombre"].ToString(),
                    Descripcion = accesoDatos.Lector["Descripcion"].ToString(),
                    Precio = Convert.ToDecimal(accesoDatos.Lector["Precio"]),
                    Stock = Convert.ToInt32(accesoDatos.Lector["Stock"]),
                    CategoriaID = (int)(accesoDatos.Lector["CategoriaID"]),
                    ProveedorID = (int)(accesoDatos.Lector["ProveedorID"]),
                    EmpresaID = (int)(accesoDatos.Lector["EmpresaID"]),
                    Marca = accesoDatos.Lector["Marca"].ToString()
                });
            }

            accesoDatos.CerrarConexion();
            return productos;
        }

        public void AgregarProducto(Producto producto)
        {
            accesoDatos.SetearSp("AgregarProducto");
            accesoDatos.SetearParametros("@Nombre", producto.Nombre);
            accesoDatos.SetearParametros("@Descripcion", producto.Descripcion);
            accesoDatos.SetearParametros("@Precio", producto.Precio);
            accesoDatos.SetearParametros("@Stock", producto.Stock);
            accesoDatos.SetearParametros("@CategoriaID", producto.CategoriaID);
            accesoDatos.SetearParametros("@EmpresaID", producto.EmpresaID);
            accesoDatos.SetearParametros("ProveedorID", producto.ProveedorID);
            accesoDatos.SetearParametros("@Marca", producto.Marca);
            accesoDatos.EjecutarAccion();
            accesoDatos.CerrarConexion();
        }

        public void ActualizarProducto(Producto producto)
        {
            accesoDatos.SetearSp("ActualizarProducto");
            accesoDatos.SetearParametros("@ProductoID", producto.ProductoID);
            accesoDatos.SetearParametros("@Nombre", producto.Nombre);
            accesoDatos.SetearParametros("@Descripcion", producto.Descripcion);
            accesoDatos.SetearParametros("@Precio", producto.Precio);
            accesoDatos.SetearParametros("@Stock", producto.Stock);
            accesoDatos.SetearParametros("@CategoriaID", producto.CategoriaID);
            accesoDatos.SetearParametros("ProveedorID", producto.ProveedorID);
            accesoDatos.SetearParametros("@Marca", producto.Marca);
            accesoDatos.EjecutarAccion();
            accesoDatos.CerrarConexion();
        }

        public void EliminarProducto(int productoID)
        {
            accesoDatos.SetearSp("EliminarProducto");
            accesoDatos.SetearParametros("@ProductoID", productoID);
            accesoDatos.EjecutarAccion();
            accesoDatos.CerrarConexion();
        }

        public void ActualizarStock(int productoID, int cantidad)
        {
            accesoDatos.SetearSp("ActualizarStock");
            accesoDatos.SetearParametros("@ProductoID", productoID);
            accesoDatos.SetearParametros("@Cantidad", cantidad);
            accesoDatos.EjecutarAccion();
            accesoDatos.CerrarConexion();
        }

        public int ObtenerStock(int productoID)
        {
            int stock = 0;
            accesoDatos.SetearSp("ObtenerStock");
            accesoDatos.SetearParametros("@ProductoID", productoID);
            accesoDatos.EjecutarLectura();
            while (accesoDatos.Lector.Read())
            {
                stock = Convert.ToInt32(accesoDatos.Lector["Stock"]);
            }
            accesoDatos.CerrarConexion();
            return stock;
        }



        public int ObtenerUltimoIdProducto()
        {
            int id = 0;
            try
            {
                accesoDatos.SetearSp("ObtenerUltimoIdProducto");
                accesoDatos.EjecutarLectura();
                while (accesoDatos.Lector.Read())
                {
                    id = Convert.ToInt32(accesoDatos.Lector["ProductoID"]);
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
            return id;
        }

        public Producto ObtenerProductoxID(int productoID)
        {
            Producto productoSeleccionado = new Producto();
            try
            {
                accesoDatos.SetearSp("ObtenerProductoxID");
                accesoDatos.SetearParametros("@ProductoID", productoID);
                accesoDatos.EjecutarLectura();
                while (accesoDatos.Lector.Read())
                {
                    productoSeleccionado = new Producto
                    {
                        ProductoID = Convert.ToInt32(accesoDatos.Lector["ProductoID"]),
                        Nombre = accesoDatos.Lector["Nombre"].ToString(),
                        Descripcion = accesoDatos.Lector["Descripcion"].ToString(),
                        Precio = Convert.ToDecimal(accesoDatos.Lector["Precio"]),
                        Stock = Convert.ToInt32(accesoDatos.Lector["Stock"]),
                        CategoriaID = (int)(accesoDatos.Lector["CategoriaID"]),
                        ProveedorID = (int)(accesoDatos.Lector["ProveedorID"]),
                        EmpresaID = (int)(accesoDatos.Lector["EmpresaID"]),
                        Marca = accesoDatos.Lector["Marca"].ToString()
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
            return productoSeleccionado;
        }

    }
}