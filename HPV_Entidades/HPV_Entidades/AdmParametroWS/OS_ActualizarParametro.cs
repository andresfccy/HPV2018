using HPV_Entidades.AdmParametro;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AdmParametroWS
{
    public class OS_ActualizarParametro
    {
        public Respuesta Respuesta { get; set; }

        public OS_ActualizarParametro()
        {
            Respuesta = new Respuesta();
        }
    }
}
