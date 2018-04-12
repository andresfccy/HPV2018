using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.ConsultaWS
{
    public class OE_ConsultarHorarios
    {
        public Int64 IdPeriodo { get; set; }
        public String FiltroBusqueda { get; set; } // Criterios separados por coma (,)
        public Int64? IdCoordinador { get; set; }
    }
}
