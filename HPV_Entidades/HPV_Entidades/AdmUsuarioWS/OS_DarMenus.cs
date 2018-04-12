using HPV_Entidades.AdmUsuario;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AdmUsuarioWS
{
    public class OS_DarMenus
    {
        public Respuesta Respuesta { get; set; }
        public List<Menu> ListaMenu { get; set; }

        public OS_DarMenus()
        {
            Respuesta = new Respuesta();
            ListaMenu = new List<Menu>();
        }

    }
}
