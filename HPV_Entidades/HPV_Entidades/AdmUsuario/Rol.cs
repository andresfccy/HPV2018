using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AdmUsuario
{
    public class Rol
    {
        public Int64 IdRol { get; set; }
        public String Nombre { get; set; }
        public String CodEstado { get; set; }
        public String Estado { get; set; }
        public String Opciones { get; set; } // Valores idMenu,IdOpcion ...

    }
}
