using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.Asistencia
{
    public class GrupoFacilitador
    {
        public Int64 IdGrupo { get; set; }
        public String Nombre { get; set; }
        public String Sigla { get; set; }
        public Int64 IdHorario { get; set; }
        public String NomHorario { get; set; }
        public Int64 IdDia { get; set; }
        public String NomDia { get; set; }
        public Int64 IdGrupoFacilitador { get; set; }
    }
}
