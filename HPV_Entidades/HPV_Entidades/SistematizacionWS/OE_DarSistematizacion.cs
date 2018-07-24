using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.SistematizacionWS
{
    public class OE_DarSistematizacion
    {
        public Int64 IdUsuario { get; set; }
        public String FiltroBusqueda { get; set; }
        public Int64 InstrumentoXConsultar { get; set; }
        public Int64? IdPeriodo { get; set; }
    }
}
