using HPV_Entidades.General;
using HPV_Entidades.Reporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.ReporteWS
{
    public class OS_RptSatisfaccion
    {
        public Respuesta Respuesta { get; set; }
        public List<EncuestaSatisfaccion> ListaSatisfaccion { get; set; }

        public OS_RptSatisfaccion()
        {
            Respuesta = new Respuesta();
            ListaSatisfaccion = new List<EncuestaSatisfaccion>();
        }

    }
}
