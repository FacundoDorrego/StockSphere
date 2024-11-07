using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Empresa
    {
        public int EmpresaID { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int UsuarioID { get; set; }
        public bool Activa { get; set; }
    }

}
