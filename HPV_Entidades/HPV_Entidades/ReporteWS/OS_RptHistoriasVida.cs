using HPV_Entidades.General;
using HPV_Entidades.Reporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.ReporteWS
{
    public class OS_RptHistoriasVida
    {
        public Respuesta Respuesta { get; set; }
        public List<HistoriasVida> ListaHistoriasVida { get; set; }

        public OS_RptHistoriasVida()
        {
            Respuesta = new Respuesta();
            ListaHistoriasVida = new List<HistoriasVida>();
        }

    }
}
