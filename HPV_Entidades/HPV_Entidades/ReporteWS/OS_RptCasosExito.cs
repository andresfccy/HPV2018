using HPV_Entidades.General;
using HPV_Entidades.Reporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.ReporteWS
{
    public class OS_RptCasosExito
    {
        public Respuesta Respuesta { get; set; }
        public List<CasosExito> ListaCasosExito { get; set; }

        public OS_RptCasosExito()
        {
            Respuesta = new Respuesta();
            ListaCasosExito = new List<CasosExito>();
        }

    }
}
