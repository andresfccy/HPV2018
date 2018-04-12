using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.Asistencia
{
    public class AsistenciaDetalle
    {
        public Int64 IdRegistro { get; set; }
        public Int64 IdInscrito { get; set; }
        public String TipoDocumento { get; set; }
        public String Documento { get; set; }
        public Int64 CodigoBeneficiario { get; set; }
        public String Nombre { get; set; }
    }
}
