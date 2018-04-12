using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.ReporteWS
{
    public class OS_DarReportes
    {
        public List<HPV_Entidades.Reporte.Reporte> ListaReporte { get; set; }
        public Respuesta Respuesta { get; set; }

        public OS_DarReportes()
        {
            Respuesta = new Respuesta();
            ListaReporte = new List<HPV_Entidades.Reporte.Reporte>();
        }
    }
}
