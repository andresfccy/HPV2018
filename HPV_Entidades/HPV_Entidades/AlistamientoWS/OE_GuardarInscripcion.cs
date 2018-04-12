using HPV_Entidades.Alistamiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AlistamientoWS
{
    public class OE_GuardarInscripcion
    {
        public Int16 EsBeneficiario { get; set; }
        public Int64 GrupoFacilitador { get; set; }
        public Beneficiario Beneficiario { get; set; }
        public String RespuestaEncuesta { get; set; }
    }
}
