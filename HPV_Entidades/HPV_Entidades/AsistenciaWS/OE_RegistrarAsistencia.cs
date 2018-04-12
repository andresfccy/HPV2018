using HPV_Entidades.Asistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AsistenciaWS
{
    public class OE_RegistrarAsistencia
    {
        public Int64 IdFacilitador { get; set; }
        public Int64 IdTaller { get; set; }
        public Int64 IdEntregable { get; set; }
        public Int64 IdPeriodo { get; set; }
        public Int64 IdGrupoFacilitador { get; set; }
        public String FechaRealizacion { get; set; } // yyyy-mm-dd

        public List<AsistenciaInfo> ListaAsistenciaInfo { get; set; }
    }
}
