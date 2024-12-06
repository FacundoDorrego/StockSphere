using Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clases;

namespace Repositorios
{
    public class RepositorioMovimientoInventario
    {
        private AccesoDatos accesoDatos = new AccesoDatos();

        public List<MovimientoInventario> ObtenerMovimientos()
        {
            List<MovimientoInventario> movimientos = new List<MovimientoInventario>();
            accesoDatos.SetearSp("ObtenerMovimientos");
            accesoDatos.EjecutarLectura();

            while (accesoDatos.Lector.Read())
            {
                movimientos.Add(new MovimientoInventario
                {
                    MovimientoID = Convert.ToInt32(accesoDatos.Lector["MovimientoID"]),
                    ProductoID = Convert.ToInt32(accesoDatos.Lector["ProductoID"]),
                    Cantidad = Convert.ToInt32(accesoDatos.Lector["Cantidad"]),
                    Fecha = Convert.ToDateTime(accesoDatos.Lector["FechaMovimiento"]),
                    TipoMovimiento = accesoDatos.Lector["TipoMovimiento"].ToString(),
                    Observaciones = accesoDatos.Lector["Observaciones"].ToString(),
                    UsuarioID = Convert.ToInt32(accesoDatos.Lector["UsuarioID"]),
                    EmpresaID = Convert.ToInt32(accesoDatos.Lector["EmpresaID"])
                });
            }

            accesoDatos.CerrarConexion();
            return movimientos;
        }

        public void MovimientoInventario(int productoID, int cantidad, string tipomovimiento, string obs, DateTime fechamov, int usuarioid,int empresaid)
        {
            accesoDatos.SetearSp("MovimientoInventarioSP");
            accesoDatos.SetearParametros("@ProductoID", productoID);
            accesoDatos.SetearParametros("@Tipomovimiento", tipomovimiento);
            accesoDatos.SetearParametros("@Cantidad", cantidad);
            accesoDatos.SetearParametros("@FechaMov", fechamov);
            accesoDatos.SetearParametros("@Obs", obs);
            accesoDatos.SetearParametros("@UsuarioID", usuarioid);
            accesoDatos.SetearParametros("@EmpresaID", empresaid);
            accesoDatos.EjecutarAccion();
            accesoDatos.CerrarConexion();
        }

       
    }
}
