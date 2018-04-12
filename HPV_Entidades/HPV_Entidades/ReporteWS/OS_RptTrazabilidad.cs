using HPV_Entidades.General;
using HPV_Entidades.Reporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.ReporteWS
{
    public class OS_RptTrazabilidad
    {
        public Respuesta Respuesta { get; set; }
        public List<Trazabilidad> ListaTrazabilidad { get; set; }

        public OS_RptTrazabilidad()
        {
            Respuesta = new Respuesta();
            ListaTrazabilidad = new List<Trazabilidad>();
        }

    }
}
