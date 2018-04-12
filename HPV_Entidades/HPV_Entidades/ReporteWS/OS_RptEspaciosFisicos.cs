using HPV_Entidades.General;
using HPV_Entidades.Reporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.ReporteWS
{
    public class OS_RptEspaciosFisicos
    {
        public Respuesta Respuesta { get; set; }
        public List<EspaciosFisicos> ListaEspaciosFisicos { get; set; }

        public OS_RptEspaciosFisicos()
        {
            Respuesta = new Respuesta();
            ListaEspaciosFisicos = new List<EspaciosFisicos>();
        }

    }
}
