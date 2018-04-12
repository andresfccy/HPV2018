using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AdmEspacioFisicoWS
{
    public class OE_ReasignarEspacio
    {
        public Int64 IdPeriodo { get; set; }
        public Int64 IdGrupoFacilitador { get; set; }
        public Int64 IdInscrito { get; set; }
        public Int64 IdUsuarioRegistra { get; set; }

    }
}
