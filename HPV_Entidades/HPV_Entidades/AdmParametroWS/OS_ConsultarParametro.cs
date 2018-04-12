using HPV_Entidades.AdmParametro;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AdmParametroWS
{
    public class OS_ConsultarParametro
    {
        public Respuesta Respuesta { get; set; }
        public Parametro Parametro { get; set; }

        public OS_ConsultarParametro()
        {
            Respuesta = new Respuesta();
            Parametro = new Parametro();
        }
    }
}
