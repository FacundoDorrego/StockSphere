using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Almacenes
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int EmpresaId { get; set; }
        public bool Estatus { get; set; }
        public DateTime Fecha { get; set; }

        public Almacenes(int Id_, string Nombre_, string Direccion_, int EmpresaId_, bool Estatus_, DateTime Fecha_)
        {
            this.Id = Id_;
            this.Nombre = Nombre_;
            this.Direccion = Direccion_;
            this.EmpresaId = EmpresaId_;
            this.Estatus = Estatus_;
            this.Fecha = Fecha_;
        }
    }

}
