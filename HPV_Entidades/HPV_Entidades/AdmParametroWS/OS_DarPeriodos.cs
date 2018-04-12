using HPV_Entidades.AdmParametro;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AdmParametroWS
{
    public class OS_DarPeriodos
    {
        public Respuesta Respuesta { get; set; }
        public List<Periodo> ListaPeriodo { get; set; }

        public OS_DarPeriodos()
        {
            Respuesta = new Respuesta();
            ListaPeriodo = new List<Periodo>();
        }
    }
}
