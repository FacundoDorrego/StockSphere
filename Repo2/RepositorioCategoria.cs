using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clases;

namespace Repositorios
{

    public class RepositorioCategoria
    {
        private AccesoDatos accesoDatos = new AccesoDatos();

        public List<Categoria> ObtenerCategorias()
        {
            try
            {
                List<Categoria> categorias = new List<Categoria>();
                accesoDatos.SetearSp("ObtenerCategorias");
                accesoDatos.EjecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    categorias.Add(new Categoria
                    {
                        CategoriaID = Convert.ToInt32(accesoDatos.Lector["CategoriaID"]),
                        Nombre = accesoDatos.Lector["Nombre"].ToString(),
                        Descripcion = accesoDatos.Lector["Descripcion"].ToString(),
                        EmpresaID = Convert.ToInt32(accesoDatos.Lector["EmpresaID"])
                    });
                }
                return categorias;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }
        }

        public Categoria ObtenerCategoriaxID(int CategoriaID)
        {
            Categoria categoria = new Categoria();
            try
            {
                accesoDatos.SetearSp("ObtenerCategoriasxID");
                accesoDatos.SetearParametros("@CategoriaID", CategoriaID);
                accesoDatos.EjecutarLectura();
                while (accesoDatos.Lector.Read())
                {
                    categoria = new Categoria
                    {
                        CategoriaID = Convert.ToInt32(accesoDatos.Lector["CategoriaID"]),
                        Nombre = accesoDatos.Lector["Nombre"].ToString(),
                        Descripcion = accesoDatos.Lector["Descripcion"].ToString(),
                        EmpresaID = Convert.ToInt32(accesoDatos.Lector["EmpresaID"])
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
            return categoria;
        }

        public void AgregarCategoria(Categoria categoria)
        {
            accesoDatos.SetearSp("AgregarCategoria");
            accesoDatos.SetearParametros("@Nombre", categoria.Nombre);
            accesoDatos.SetearParametros("@Descripcion", categoria.Descripcion ?? (object)DBNull.Value);
            accesoDatos.SetearParametros("@EmpresaID", categoria.EmpresaID);
            accesoDatos.EjecutarAccion();
            accesoDatos.CerrarConexion();
        }

        public void ActualizarCategoria(Categoria categoria)
        {
            accesoDatos.SetearSp("ActualizarCategoria");
            accesoDatos.SetearParametros("@CategoriaID", categoria.CategoriaID);
            accesoDatos.SetearParametros("@Nombre", categoria.Nombre);
            accesoDatos.SetearParametros("@Descripcion", categoria.Descripcion ?? (object)DBNull.Value);
            accesoDatos.EjecutarAccion();
            accesoDatos.CerrarConexion();
        }

        public void EliminarCategoria(int categoriaID)
        {
            accesoDatos.SetearSp("EliminarCategoria");
            accesoDatos.SetearParametros("@CategoriaID", categoriaID);
            accesoDatos.EjecutarAccion();
            accesoDatos.CerrarConexion();
        }
    }

}
