using HPV_Entidades.AdmRol;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AdmRolWS
{
    public class OS_DarOpcionesUsuario
    {
        public Respuesta Respuesta { get; set; }
        public Rol Rol { get; set; }

        public OS_DarOpcionesUsuario()
        {
            Respuesta = new Respuesta();
            Rol = new Rol();
        }
    }
}
