using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AsistenciaWS
{
    public class OS_RegistrarAprobacion
    {
        public Respuesta Respuesta { get; set; }

        public OS_RegistrarAprobacion()
        {
            Respuesta = new Respuesta();
        }
    }
}
