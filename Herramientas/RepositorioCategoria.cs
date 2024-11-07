using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clases;

namespace Repositorio
{
   
    public class RepositorioCategoria
    {
        private AccesoDatos accesoDatos = new AccesoDatos();

        public List<Categoria> ObtenerCategorias()
        {
            List<Categoria> categorias = new List<Categoria>();
            string consulta = "SELECT * FROM dbo.Categorias";

            accesoDatos.SetearConsulta(consulta);
            accesoDatos.EjecutarLectura();

            while (accesoDatos.Lector.Read())
            {
                categorias.Add(new Categoria
                {
                    CategoriaID = Convert.ToInt32(accesoDatos.Lector["CategoriaID"]),
                    Nombre = accesoDatos.Lector["Nombre"].ToString(),
                    Descripcion = accesoDatos.Lector["Descripcion"].ToString()
                });
            }

            accesoDatos.CerrarConexion();
            return categorias;
        }

        public void AgregarCategoria(Categoria categoria)
        {
            accesoDatos.SetearSp("dbo.AgregarCategoria");
            accesoDatos.SetearParametros("@Nombre", categoria.Nombre);
            accesoDatos.SetearParametros("@Descripcion", categoria.Descripcion ?? (object)DBNull.Value);
            accesoDatos.EjecutarAccion();
        }

        public void ActualizarCategoria(Categoria categoria)
        {
            accesoDatos.SetearSp("dbo.ActualizarCategoria");
            accesoDatos.SetearParametros("@CategoriaID", categoria.CategoriaID);
            accesoDatos.SetearParametros("@Nombre", categoria.Nombre);
            accesoDatos.SetearParametros("@Descripcion", categoria.Descripcion ?? (object)DBNull.Value);
            accesoDatos.EjecutarAccion();
        }

        public void EliminarCategoria(int categoriaID)
        {
            accesoDatos.SetearSp("dbo.EliminarCategoria");
            accesoDatos.SetearParametros("@CategoriaID", categoriaID);
            accesoDatos.EjecutarAccion();
        }
    }

}
