using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AsistenciaWS
{
    public class OE_GenerarListaAsistencia
    {
        public Int64 IdFacilitador { get; set; }
        public Int64 IdGrupo { get; set; }
        public String Taller { get; set; } // Lista de id taller a generar separados por comas (,)
    }
}
