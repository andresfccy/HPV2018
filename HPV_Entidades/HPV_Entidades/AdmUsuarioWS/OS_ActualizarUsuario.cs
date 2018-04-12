using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AdmUsuarioWS
{
    public class OS_ActualizarUsuario
    {
        public Respuesta Respuesta { get; set; }

        public OS_ActualizarUsuario()
        {
            Respuesta = new Respuesta();
        }
    }
}
