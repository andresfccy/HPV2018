using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.LiderazgoWS
{
    public class OE_RegistrarEstadoLiderazgo
    {
        public Int64 IdUsuario { get; set; }
        public Int64 IdLiderazgo { get; set; }
        public String MotivoRechazo { get; set; }
        public String Estado { get; set; }

    }
}
