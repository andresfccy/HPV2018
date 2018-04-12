using HPV_Entidades.General;
using HPV_Entidades.Reporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.ReporteWS
{
    public class OS_RptEntradaSalida
    {
        public Respuesta Respuesta { get; set; }
        public List<EncuestaEntradaSalida> ListaEntradaSalida { get; set; }

        public OS_RptEntradaSalida()
        {
            Respuesta = new Respuesta();
            ListaEntradaSalida = new List<EncuestaEntradaSalida>();
        }

    }
}
