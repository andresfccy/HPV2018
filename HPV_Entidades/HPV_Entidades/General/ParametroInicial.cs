using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.General
{
    public class ParametroInicial
    {
        public String FechaIniPreaviso { get; set; } // yyyy-mm-dd
        public String FechaFinPreaviso { get; set; } // yyyy-mm-dd
        public String FechaIniInscripcion { get; set; } // yyyy-mm-dd
        public String FechaFinInscripcion { get; set; } // yyyy-mm-dd
        public String NombrePeriodo { get; set; }
        public Int64 IdPeriodo { get; set; }
    }
}
