using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.Reporte
{
    public class ParametroRpt
    {
        public Int64 IdUsuario { get; set; }

        public Int64 IdPeriodo { get; set; }
        public Int64 IdFacilitador { get; set; }
        public Int64 IdCoordinador { get; set; }
        public Int64 IdGrupo { get; set; }
        public Int64 IdDepartamento { get; set; }
        public Int64 IdMunicipio { get; set; }
        public String FechaCorte { get; set; } //Formato aaaa/mm/dd
        public Int16 IdTipoInstrumento { get; set; }
    }
}
