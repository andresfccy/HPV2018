using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AsistenciaWS
{
    public class OE_DarEntregableEstado
    {
        public Int64 IdUsuario { get; set; } // Id del usuario en sesion
        public Int64 IdTaller { get; set; } // 0:Todos los talleres
        public Int64 IdEntregable { get; set; }
        public Int64 IdPeriodo { get; set; }
        public String IdEstadoDocumento { get; set; }
        public String IdEstadoAsistencia { get; set; }
        public String FiltroBusqueda { get; set; }
    }
}
