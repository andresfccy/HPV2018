using HPV_Entidades.AdmUsuario;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AdmUsuarioWS
{
    public class OS_DarUsuarios
    {
        public Respuesta Respuesta { get; set; }
        public List<Usuario> ListaUsuario { get; set; }

        public OS_DarUsuarios()
        {
            Respuesta = new Respuesta();
            ListaUsuario = new List<Usuario>();
        }
    }
}
