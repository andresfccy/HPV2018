using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.HistoriasDeVida
{
    public class HistoriaDeVida
    {
        public Int64 IdHistoriaDeVida { get; set; } // Si es un nuevo registro va en cero
        public Int64 IdPeriodo { get; set; }

        public Int64 IdGrupoFacilitador { get; set; }
        public String SiglaGrupo { get; set; }
        public Int64 IdGrupo { get; set; }
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
        public String MotivoRechazo { get; set; }

        public Int64 IdTipoHistoriaDeVida { get; set; }
        public String NomTipoHistoriaDeVida { get; set; }

        public String RazonDeConsideracion { get; set; }
        public String CircunstanciasSuperadas { get; set; }
        public String HabilidadesPracticadas { get; set; }
        public String Leccion { get; set; }

        public Int64 IdInscrito { get; set; }
        public String NomInscrito { get; set; }

    }
}
