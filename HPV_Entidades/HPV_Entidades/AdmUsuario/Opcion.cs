using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AdmUsuario
{
    public class Opcion
    {
        public Int64 IdMenu { get; set; }
        public Int64 IdOpcion { get; set; }
        public String Nombre { get; set; }
        public String Tag { get; set; }
        public Int32 Orden { get; set; }
    }
}
