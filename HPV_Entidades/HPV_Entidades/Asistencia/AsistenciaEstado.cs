using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.Asistencia
{
    public class AsistenciaEstado
    {
        public Int64 IdInscrito { get; set; }
        public String NombreInscrito { get; set; }
        public String TipoDocumento { get; set; }
        public String Documento { get; set; }
        public Int64 IndAsistencia { get; set; } // 0: No asistio 1:Asistio

    }
}
