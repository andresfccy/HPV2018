using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AlistamientoWS
{
    public class OE_DarEncuestaInscrito
    {
        public String TipoDocumento { get; set; }
        public String Documento { get; set; }
        public Int64 CodigoBeneficiario { get; set; }
        public String FechaNacimiento { get; set; }
        public Int32 Vigencia { get; set; }
        public String IdTipoEncuesta { get; set; } // I: Inicial S:Satisfaccion F:Final
    }
}
