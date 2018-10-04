using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.SistematizacionWS
{
    public class OE_ActualizarCategorizacion
    {
        public Int64 IdSistematizacion { get; set; }
        public String ListaCategorizaciones { get; set; }
        public String DescripcionOtros { get; set; }
        public Int64 IdUsuario { get; set; }
    }
}
