using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AdmEspacioFisicoWS
{
    public class OS_ReasignarEspacio
    {
        public Respuesta Respuesta { get; set; }

        public OS_ReasignarEspacio()
        {
            Respuesta = new Respuesta();
        }

    }
}
