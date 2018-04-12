using HPV_Entidades.AdmParametro;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AdmParametroWS
{
    public class OS_ActualizarPeriodo
    {
        public Respuesta Respuesta { get; set; }

        public OS_ActualizarPeriodo()
        {
            Respuesta = new Respuesta();
        }
    }
}
