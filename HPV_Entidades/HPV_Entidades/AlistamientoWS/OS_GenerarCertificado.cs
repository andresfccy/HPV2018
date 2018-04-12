using HPV_Entidades.Alistamiento;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AlistamientoWS
{
    public class OS_GenerarCertificado
    {

        public Respuesta Respuesta { get; set; }
        public Int64 IdInscrito { get; set; }

        public OS_GenerarCertificado()
        {
            Respuesta = new Respuesta();
        }

    }
}
