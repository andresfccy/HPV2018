using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AlistamientoWS
{
    public class OE_RegistrarEncuestaFinal
    {
        public Int64 IdInscrito { get; set; }
        public String Encuesta { get; set; } // idPregunta, idRespuesta
        public String Observaciones { get; set; }
    }
}
