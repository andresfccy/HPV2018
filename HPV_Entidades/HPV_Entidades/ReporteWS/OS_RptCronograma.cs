using HPV_Entidades.General;
using HPV_Entidades.Reporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.ReporteWS
{
    public class OS_RptCronograma
    {
        public Respuesta Respuesta { get; set; }
        public List<CronogramaTaller> ListaCronograma { get; set; }

        public OS_RptCronograma()
        {
            Respuesta = new Respuesta();
            ListaCronograma = new List<CronogramaTaller>();
        }

    }
}
