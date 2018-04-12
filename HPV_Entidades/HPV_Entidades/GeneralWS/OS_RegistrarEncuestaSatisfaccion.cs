using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.GeneralWS
{
    public class OS_RegistrarEncuestaSatisfaccion
    {
        public Respuesta Respuesta { get; set; }

        public OS_RegistrarEncuestaSatisfaccion()
        {
            Respuesta = new Respuesta();
        }
    }
}
