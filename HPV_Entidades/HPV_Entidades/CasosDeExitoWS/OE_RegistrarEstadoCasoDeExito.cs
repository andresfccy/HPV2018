using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.CasosDeExitoWS
{
    public class OE_RegistrarEstadoCasoDeExito
    {
        public Int64 IdUsuario { get; set; }
        public Int64 IdCasoDeExito { get; set; }
        public String Estado { get; set; }
        public String MotivoRechazo { get; set; }
    }
}
