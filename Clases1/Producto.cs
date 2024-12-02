using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Producto
    {
        public int ProductoID { get; set; }
        public string Descripcion { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int CategoriaID { get; set; }
        public int ProveedorID { get; set; }
        public int EmpresaID { get; set; }
        public string Marca { get; set; }
    }

}
