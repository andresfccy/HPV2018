using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AlistamientoWS
{
    public class OS_GuardarInscripcion
    {
        public Respuesta Respuesta { get; set; }
        public Int64 CodigoInscripcion { get; set; }

        public OS_GuardarInscripcion()
        {
            Respuesta = new Respuesta();
        }
    }
}
