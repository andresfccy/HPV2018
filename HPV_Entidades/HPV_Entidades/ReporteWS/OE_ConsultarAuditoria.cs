using HPV_Entidades.Reporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.ReporteWS
{
    public class OE_ConsultarAuditoria
    {
        public Int64 IdFuncion { get; set; }
        public Int64 IdUsuario { get; set; }
        public String Fec_Ini { get; set; }
        public String Fec_Fin { get; set; }
    }
}
