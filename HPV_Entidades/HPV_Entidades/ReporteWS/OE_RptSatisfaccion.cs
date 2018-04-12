using HPV_Entidades.Reporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.ReporteWS
{
    public class OE_RptSatisfaccion
    {
        public ParametroRpt ParametroRpt { get; set; }

        public OE_RptSatisfaccion()
        {
            ParametroRpt = new ParametroRpt();
        }
    }
}
