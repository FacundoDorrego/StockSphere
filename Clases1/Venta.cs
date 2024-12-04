using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Venta
    {
        public int VentasID { get; set; }
        public decimal Monto { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaVenta { get; set; }
        public Empresa Empresa { get; set; }
        public Usuario Usuario { get; set; }
        public Producto Producto { get; set; }
        public Categoria Categoria { get; set; }
        public int EmpresaID { get; set; }
        public int UsuarioID { get; set; }
        public int ProductoID { get; set; }
        public int CategoriaID { get; set; }

    }
}
