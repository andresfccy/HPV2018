using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.SistematizacionWS
{
    public class OE_ActualizarSistematizacion
    {
        public Int64 IdUsuario { get; set; } // Id del usuario logeado
        public HPV_Entidades.Sistematizacion.Sistematizacion Sistematizacion { get; set; }
    }
}
