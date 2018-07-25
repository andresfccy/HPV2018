using HPV_Entidades.General;
using HPV_Entidades.Reporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.ReporteWS
{
    public class OS_RptSistematizacion
    {
        public Respuesta Respuesta { get; set; }
        public List<HPV_Entidades.Reporte.Sistematizacion> ListaSistematizacion { get; set; }

        public OS_RptSistematizacion()
        {
            Respuesta = new Respuesta();
            ListaSistematizacion = new List<HPV_Entidades.Reporte.Sistematizacion>();
        }

    }
}
