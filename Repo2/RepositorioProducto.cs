﻿using System;
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
                ProveedorID = (int)(accesoDatos.Lector["ProveedorID"]),
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
        accesoDatos.SetearParametros("ProveedorID", producto.ProveedorID);
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

    public void ActualizarStock(int productoID, int cantidad, int empresaId)
    {
        accesoDatos.SetearSp("ActualizarStock");
        accesoDatos.SetearParametros("@ProductoID", productoID);
        accesoDatos.SetearParametros("@Cantidad", cantidad);
        accesoDatos.SetearParametros("@EmpresaID", empresaId);
        accesoDatos.EjecutarAccion();
        accesoDatos.CerrarConexion();
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

}