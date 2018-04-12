using HPV_Entidades.AdmUsuario;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AdmUsuarioWS
{
    public class OS_DarOpcionesMenuRol
    {
        public Respuesta Respuesta { get; set; }
        public List<Opcion> ListaOpcion { get; set; }

        public OS_DarOpcionesMenuRol()
        {
            Respuesta = new Respuesta();
            ListaOpcion = new List<Opcion>();
        }

    }
}
