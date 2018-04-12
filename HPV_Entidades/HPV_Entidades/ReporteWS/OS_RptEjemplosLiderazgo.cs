using HPV_Entidades.General;
using HPV_Entidades.Reporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.ReporteWS
{
    public class OS_RptEjemplosLiderazgo
    {
        public Respuesta Respuesta { get; set; }
        public List<EjemplosLiderazgo> ListaEjemplosLiderazgo { get; set; }

        public OS_RptEjemplosLiderazgo()
        {
            Respuesta = new Respuesta();
            ListaEjemplosLiderazgo = new List<EjemplosLiderazgo>();
        }

    }
}
