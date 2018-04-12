using HPV_Entidades.General;
using HPV_Entidades.Reporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.ReporteWS
{
    public class OS_RptSatisfaccionObservaciones
    {
        public Respuesta Respuesta { get; set; }
        public List<EncuestaSatisfaccionObservaciones> ListaSatisfaccionObservaciones { get; set; }

        public OS_RptSatisfaccionObservaciones()
        {
            Respuesta = new Respuesta();
            ListaSatisfaccionObservaciones = new List<EncuestaSatisfaccionObservaciones>();
        }

    }
}
