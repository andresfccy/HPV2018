using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AdmParametro
{
    public class Periodo
    {
        public Int64 Codigo { get; set; }
        public String Nombre { get; set; }
        public Int64 Numero { get; set; }
        public String FechaInicio { get; set; }
        public String FechaFin { get; set; }
        public String Estado { get; set; }
        public String FechaIniTaller { get; set; }
    }
}
