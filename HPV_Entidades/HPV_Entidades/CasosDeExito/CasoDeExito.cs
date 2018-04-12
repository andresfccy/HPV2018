using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.CasosDeExito
{
    public class CasoDeExito
    {
        public Int64 IdCasoDeExito { get; set; } // Si es un nuevo registro va en cero
        public Int64 IdPeriodo { get; set; }

        public Int64 IdGrupoFacilitador { get; set; }
        public String SiglaGrupo { get; set; }
        public String NomGrupo { get; set; }

        public Int64 IdFacilitador { get; set; }
        public String NomFacilitador { get; set; }
        public Int64 IdCoordinador { get; set; }
        public String NomCoordinador { get; set; }

        public Int64 IdMunicipio { get; set; }
        public String NomMunicipio { get; set; }
        public Int64 IdDepartamento { get; set; }
        public String NomDepartamento { get; set; }

        public String IdEstado { get; set; }
        public String NomEstado { get; set; }

        public Int64 IdInscrito { get; set; }
        public String NomInscrito { get; set; }

        public Int64 IdLogro { get; set; }
        public String NomLogro { get; set; }

        public String Criterios { get; set; }
        public String NomVerbo { get; set; }
        public String NomAdjetivo { get; set; }
        public String NomMedio { get; set; }
        public String Observaciones { get; set; }
        public String MotivoRechazo { get; set; }
        public String Logros { get; set; }
    }
}
