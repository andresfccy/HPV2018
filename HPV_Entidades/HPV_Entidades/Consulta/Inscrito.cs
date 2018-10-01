using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.Consulta
{
    public class Inscrito
    {
        public Int64 IdInscrito { get; set; }
        public String TipoDocumento { get; set; }
        public String NomTipoDocumento { get; set; }
        public String Documento { get; set; }
        public String NombreInscrito { get; set; }
        public Int64 CodigoBeneficiario { get; set; }
        public String FechaNacimiento { get; set; }  // yyyy-mm-dd
        public String Correo { get; set; }
        public String Telefono { get; set; }
        public String Movil { get; set; }
        public String MovilAlt { get; set; }
        public String CondicionEspecial { get; set; }

        public String NomDepartamento { get; set; }
        public String NomMunicipio{ get; set; }
        public String Dia { get; set; }
        public String Horario { get; set; }
        public String Grupo { get; set; }
        public String SiglaGrupo { get; set; }
        public String Lugar { get; set; }
        public String Direccion { get; set; }
        public String NombreFacilitador { get; set; }
        public String NombreCoordinador { get; set; }
        public String FechaRegistro { get; set; } // yyyy-mm-dd
        public String FechaCertificado { get; set; } // yyyy-mm-dd
        public String Vigencia { get; set; }

        public List<AsistenciaTaller> ListaAsistenciaTaller { get; set; }

        public Inscrito()
        {
            ListaAsistenciaTaller = new List<AsistenciaTaller>();
        }

    }
}
