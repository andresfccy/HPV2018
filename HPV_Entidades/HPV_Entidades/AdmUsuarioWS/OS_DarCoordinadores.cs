using HPV_Entidades.AdmUsuario;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AdmUsuarioWS
{
    public class OS_DarCoordinadores
    {
        public Respuesta Respuesta { get; set; }
        public List<Coordinador> ListaCoordinador { get; set; }

        public OS_DarCoordinadores()
        {
            Respuesta = new Respuesta();
            ListaCoordinador = new List<Coordinador>();
        }

    }
}
