using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AsistenciaWS
{
    public class OE_DarEntregableEstadoDetalle
    {
        public Int64 IdPeriodo { get; set; }
        public Int64 IdEntregable { get; set; }
        public Int64 IdTaller { get; set; }
        public Int64 IdGrupoFacilitador { get; set; }

    }
}
