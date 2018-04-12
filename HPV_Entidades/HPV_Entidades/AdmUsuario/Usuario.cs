using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AdmUsuario
{
    public class Usuario

    {
        public Int64 IdUsuario { get; set; }
        public Int64 IdRol { get; set; }
        public Int64 IdPerfil { get; set; }
        public Int64 IdCoordinador { get; set; }
        public String AliasUsuario { get; set; }
        public String Clave { get; set; }
        public String PrimerNombre { get; set; }
        public String SegundoNombre { get; set; }
        public String PrimerApellido { get; set; }
        public String SegundoApellido { get; set; }
        public String Correo { get; set; }
        public String TipoDocumento { get; set; }
        public String Identificacion { get; set; }
        public String FechaNacimiento { get; set; } // yyyy-mm-dd
        public String Telefono { get; set; }
        public String Movil { get; set; }
        public String MovilAlt { get; set; }
        public String CodEstado { get; set; }
        public String Estado { get; set; }
        public String Ciudad { get; set; } 
        public String NomRol { get; set; }

    }
}
