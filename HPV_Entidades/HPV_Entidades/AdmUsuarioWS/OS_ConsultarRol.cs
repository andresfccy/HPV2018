using HPV_Entidades.AdmUsuario;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AdmUsuarioWS
{
    public class OS_ConsultarRol
    {
        public Respuesta Respuesta { get; set; }
        public Rol Rol { get; set; }

        public OS_ConsultarRol()
        {
            Respuesta = new Respuesta();
            Rol = new Rol();
        }

    }
}
