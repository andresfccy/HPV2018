using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AdmEspacioFisicoWS
{
    public class OS_AprobarEspacioFisico
    {
        public Respuesta Respuesta { get; set; }

        public OS_AprobarEspacioFisico()
        {
            Respuesta = new Respuesta();
        }
    }
}
