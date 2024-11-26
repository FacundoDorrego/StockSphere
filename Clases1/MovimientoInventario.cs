using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class MovimientoInventario
    {
        public int MovimientoID { get; set; }
        public int ProductoID { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoMovimiento { get; set; }
        public string Observaciones { get; set; }
        public int UsuarioID { get; set; }
        public int EmpresaID { get; set; }
    }
}
