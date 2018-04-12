using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.GeneralWS
{
    public class OE_RegistrarEncuestaSatisfaccion
    {
        public String Token { get; set; }
        public String RespuestaEncuesta { get; set; } // Pareja de CodigoPregunta, CodigoRespuesta
        public String Observaciones { get; set; }

    }
}
