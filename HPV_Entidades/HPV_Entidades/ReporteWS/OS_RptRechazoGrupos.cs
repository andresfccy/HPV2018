using HPV_Entidades.General;
using HPV_Entidades.Reporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.ReporteWS
{
    public class OS_RptRechazoGrupos
    {
        public Respuesta Respuesta { get; set; }
        public List<RechazoGrupos> ListaRechazoGrupos { get; set; }

        public OS_RptRechazoGrupos()
        {
            Respuesta = new Respuesta();
            ListaRechazoGrupos = new List<RechazoGrupos>();
        }
    }
}
