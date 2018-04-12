using HPV_Entidades.AdmParametro;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AdmParametroWS
{
    public class OS_ConsultarPeriodo
    {
        public Respuesta Respuesta { get; set; }
        public Periodo Periodo { get; set; }

        public OS_ConsultarPeriodo()
        {
            Respuesta = new Respuesta();
            Periodo = new Periodo();
        }
    }
}
