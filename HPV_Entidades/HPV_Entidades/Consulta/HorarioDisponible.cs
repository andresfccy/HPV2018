using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.Consulta
{
    public class HorarioDisponible
    {
        public Int64 IdGrupoFacilitador { get; set; }
        public String NombreFacilitador { get; set; }
        public String NomDepartamento { get; set; }
        public String NomMunicipio { get; set; }
        public String Dia { get; set; }
        public String Horario { get; set; }
        public String SiglaGrupo { get; set; }
        public String Lugar { get; set; }
        public String Direccion { get; set; }
        public Int64 CupoDisponible { get; set; }
    }
}
