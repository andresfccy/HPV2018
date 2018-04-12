using HPV_Entidades.AdmUsuario;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AdmUsuarioWS
{
    public class OS_ConsultarUsuario
    {
        public Respuesta Respuesta { get; set; }
        public Usuario Usuario { get; set; }

        public OS_ConsultarUsuario()
        {
            Respuesta = new Respuesta();
            Usuario = new Usuario();

        }


    }
}
