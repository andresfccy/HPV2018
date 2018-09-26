using HPV_Entidades.Alistamiento;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AlistamientoWS
{
    public class OS_DarParametrosCertificado
    {
        public Respuesta Respuesta { get; set; }
        public ParametrosCertificado Parametros { get; set; }

        public OS_DarParametrosCertificado()
        {
            Respuesta = new Respuesta();
            Parametros = new ParametrosCertificado();
        }

    }
}
