using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.CasosDeExitoWS
{
    public class OS_RegistrarEstadoCasoDeExito
    {
        public Respuesta Respuesta { get; set; }

        public OS_RegistrarEstadoCasoDeExito()
        {
            Respuesta = new Respuesta();
        }
    }
}
