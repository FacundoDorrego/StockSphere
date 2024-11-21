using System;
using System.Collections.Generic;
using Clases;
using Repositorio;

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
                EmpresaID = (int)(accesoDatos.Lector["EmpresaID"])
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

    public void ActualizarStock(int productoID, int cantidad,int empresaId)
    {
        accesoDatos.SetearSp("ActualizarStock");
        accesoDatos.SetearParametros("@ProductoID", productoID);
        accesoDatos.SetearParametros("@Cantidad", cantidad);
        accesoDatos.SetearParametros("@EmpresaID", empresaId);
        accesoDatos.EjecutarAccion();
        accesoDatos.CerrarConexion();
    }

    public void MovimientoInventario(int productoID, int cantidad, string tipoMovimiento, string obs)
    {
        accesoDatos.SetearSp("MovimientoInventarioSP");
        accesoDatos.SetearParametros("@ProductoID", productoID);
        accesoDatos.SetearParametros("@Cantidad", cantidad);
        accesoDatos.SetearParametros("@TipoMovimiento", tipoMovimiento);
        accesoDatos.SetearParametros("@Obs", obs);
        accesoDatos.EjecutarAccion();
        accesoDatos.CerrarConexion();
    }

}
