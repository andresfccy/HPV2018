using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AdmEspacioFisico
{
    public class EspacioFisico
    {
        public Int64 IdEspacioFisico { get; set; }
        public String CodEstado { get; set; }
        public String Estado { get; set; }
        public Int64 IdDepartamento { get; set; }
        public String NomDepartamento { get; set; }
        public Int64 IdMunicipio { get; set; }
        public String NomMunicipio { get; set; }
        public Int64 IdDia { get; set; }
        public String Dia { get; set; }
        public Int64 IdHorario { get; set; }
        public String Horario { get; set; }
        public Int64 IdGrupo { get; set; }
        public String Grupo { get; set; }
        public String SiglaGrupo { get; set; }
        public String Lugar { get; set; }
        public String Direccion { get; set; }
        public String MotivoRechazo { get; set; }
        public Int64 IdFacilitador { get; set; }
        public String NomFacilitador { get; set; }


    }
}
