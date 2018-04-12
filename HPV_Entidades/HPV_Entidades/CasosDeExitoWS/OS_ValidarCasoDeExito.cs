using HPV_Entidades.CasosDeExito;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.CasosDeExitoWS
{
    public class OS_ValidarCasoDeExito
    {
        public Respuesta Respuesta { get; set; } // Si es cero puede registrar mas casos de exito, de lo contrario no puede agregar mas

        public OS_ValidarCasoDeExito()
        {
            Respuesta = new Respuesta();
        }
    }
}
