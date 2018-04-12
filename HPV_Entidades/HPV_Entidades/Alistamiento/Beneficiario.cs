using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.Alistamiento
{
    public class Beneficiario
    {
        public String TipoDocumento { get; set; }
        public String Documento { get; set; }
        public Int64 CodigoBeneficiario { get; set; }
        public String FechaNacimiento { get; set; }  // yyyy-mm-dd
        public String PrimerNombre { get; set; }
        public String SegundoNombre { get; set; }
        public String PrimerApellido { get; set; }
        public String SegundoApellido { get; set; }
        public String Movil { get; set; }
        public String MovilAlt { get; set; }
        public String Correo { get; set; }
        public String Telefono { get; set; }
        public Int64 CondicionEspecial { get; set; }
        public Int64 IdInscrito { get; set; }
    }
}
