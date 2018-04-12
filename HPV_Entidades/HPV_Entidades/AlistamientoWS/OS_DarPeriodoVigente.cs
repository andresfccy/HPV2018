using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPV_Entidades.General;
using HPV_Entidades.Alistamiento;

namespace HPV_Entidades.AlistamientoWS
{
    public class OS_DarPeriodoVigente
    {
        public Respuesta Respuesta { get; set; }
        public Periodo Periodo { get; set; }

        public OS_DarPeriodoVigente()
        {
            Respuesta = new Respuesta();
            Periodo = new Periodo();
        }

        
    }
}
