using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.Consulta
{
    public class AsistenciaTaller
    {
        public Int64 IdTaller { get; set; }
        public String FechaRealizacion { get; set; } // yyyy-mm-dd
        public Int16 IndAsistencia { get; set; } // 1: Asistio , 0:No asistio
    }
}
