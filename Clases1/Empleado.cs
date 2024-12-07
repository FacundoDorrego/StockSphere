using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Empleado
    {
        public int EmpleadoID { get; set; }
        public Usuario Usuario { get; set; }
        public Empresa Empresa { get; set; }


        public Empleado(Usuario auxUsu, Empresa auxEmpre)
        {
            Usuario = auxUsu;
            Empresa = auxEmpre;
        }

        public Empleado()
        {

        }


    }
}
