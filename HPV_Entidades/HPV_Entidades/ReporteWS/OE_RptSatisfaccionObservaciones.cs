using HPV_Entidades.Reporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.ReporteWS
{
    public class OE_RptSatisfaccionObservaciones
    {
        public ParametroRpt ParametroRpt { get; set; }

        public OE_RptSatisfaccionObservaciones()
        {
            ParametroRpt = new ParametroRpt();
        }
    }
}
