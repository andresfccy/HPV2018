using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.Asistencia
{
    public class AsistenciaEncabezado
    {
        public Int64 IdDepartamento { get; set; }
        public String NomDepartamento { get; set; }
        public Int64 IdMunicipio { get; set; }
        public String NomMunicipio { get; set; }
        public Int64 IdFacilitador { get; set; }
        public String NomFacilitador { get; set; }
        public Int64 IdCoordinador { get; set; }
        public String NomCoordinador { get; set; }
        public Int64 IdTaller { get; set; }
        public String NomTaller { get; set; }
        public String DescripcionTaller { get; set; }
        public String NomPeriodoVigente { get; set; }
        public String SiglaGrupo { get; set; }
        public Int64 IdHorario { get; set; }
        public String NomHorario { get; set; }
        public Int64 IdGrupo { get; set; }
        public String NomGrupo { get; set; }
        public String Lugar { get; set; }
        public String Direccion { get; set; }

    }
}
