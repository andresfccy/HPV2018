using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.Asistencia
{
    public class EntregableEstado
    {
        public Int64 IdTaller { get; set; }
        public String NomTaller { get; set; }
        public String DescripcionTaller { get; set; }
        public Int64 IdGrupoFacilitador { get; set; }
        public String NombreGrupo { get; set; }
        public String FechaRealizacion { get; set; } // yyyy-mm-dd
        public Int64 IdDocumento { get; set; } // Si esta en 0 no esta cargado
        public String EstadoDocumento { get; set; }
        public String MotivoRechazoDocumento { get; set; }

        public Int64 NumeroAsistentes { get; set; }
        public String EstadoAsistencia { get; set; }
        public String MotivoRechazoAsistencia { get; set; }

        public Int64 IdFacilitador { get; set; }
        public String NombreFacilitador { get; set; }
    }
}
