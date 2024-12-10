using System;
using Microsoft.Data.SqlClient;

namespace Repositorios
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;
        public SqlDataReader Lector
        {
            get { return lector; }
        }

        public AccesoDatos()
        {
            //conexion = new SqlConnection("server=FACU; database=StockSphere; Integrated Security=True; Encrypt=False;");
            conexion = new SqlConnection("server=FACUHP; database=StockSphere; Integrated Security=True; Encrypt=False;");
            comando = new SqlCommand();
        }

        public void SetearSp(string sp)
        {
            comando.Parameters.Clear();
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = sp;
        }

        public void SetearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void EjecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar la lectura: " + ex.Message);
            }
        }

        public void CerrarConexion()
        {
            if (lector != null)
                lector.Close();
            if (conexion.State == System.Data.ConnectionState.Open)
                conexion.Close();
        }

        public void SetearParametros(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor ?? DBNull.Value);

        }

        public void EjecutarAccion()
        {
            comando.Connection = conexion;
            try
            {
                if (conexion.State != System.Data.ConnectionState.Open)
                    conexion.Open();

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar la acción: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }

    }
}
