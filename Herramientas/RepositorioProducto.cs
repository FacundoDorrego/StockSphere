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
        string consulta = "SELECT * FROM dbo.Productos";

        accesoDatos.SetearConsulta(consulta);
        accesoDatos.EjecutarLectura();

        while (accesoDatos.Lector.Read())
        {
            productos.Add(new Producto
            {
                ProductoID = Convert.ToInt32(accesoDatos.Lector["ProductoID"]),
                Nombre = accesoDatos.Lector["Nombre"].ToString(),
                Descripcion = accesoDatos.Lector["Descripcion"].ToString(),
                Precio = Convert.ToDecimal(accesoDatos.Lector["Precio"]),
                CantidadDisponible = Convert.ToInt32(accesoDatos.Lector["CantidadDisponible"]),
                CategoriaID = (int)(accesoDatos.Lector["CategoriaID"])
            });
        }

        accesoDatos.CerrarConexion();
        return productos;
    }

    public void AgregarProducto(Producto producto)
    {
        accesoDatos.SetearSp("dbo.AgregarProducto");
        accesoDatos.SetearParametros("@Nombre", producto.Nombre);
        accesoDatos.SetearParametros("@Descripcion", producto.Descripcion);
        accesoDatos.SetearParametros("@Precio", producto.Precio);
        accesoDatos.SetearParametros("@CantidadDisponible", producto.CantidadDisponible);
        accesoDatos.SetearParametros("@CategoriaID", producto.CategoriaID);
        accesoDatos.EjecutarAccion();
    }

    public void ActualizarProducto(Producto producto)
    {
        accesoDatos.SetearSp("dbo.ActualizarProducto");
        accesoDatos.SetearParametros("@ProductoID", producto.ProductoID);
        accesoDatos.SetearParametros("@Nombre", producto.Nombre);
        accesoDatos.SetearParametros("@Descripcion", producto.Descripcion);
        accesoDatos.SetearParametros("@Precio", producto.Precio);
        accesoDatos.SetearParametros("@CantidadDisponible", producto.CantidadDisponible);
        accesoDatos.SetearParametros("@CategoriaID", producto.CategoriaID);
        accesoDatos.EjecutarAccion();
    }

    public void EliminarProducto(int productoID)
    {
        accesoDatos.SetearSp("dbo.EliminarProducto");
        accesoDatos.SetearParametros("@ProductoID", productoID);
        accesoDatos.EjecutarAccion();
    }
}
