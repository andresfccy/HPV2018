using HPV_Entidades.CasosDeExito;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.CasosDeExitoWS
{
    public class OE_ActualizarCasoDeExito
    {
        public Int64 IdUsuario { get; set; } // Id del usuario logeado
        public CasoDeExito CasoDeExito { get; set; }
    }
}
