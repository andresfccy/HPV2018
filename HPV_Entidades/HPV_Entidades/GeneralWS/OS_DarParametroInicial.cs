using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.GeneralWS
{
    public class OS_DarParametroInicial
    {
        public Respuesta Respuesta { get; set; } 
        public ParametroInicial ParametroInicial { get; set; }
        public OS_DarParametroInicial()
        {
            Respuesta = new Respuesta();
            ParametroInicial = new ParametroInicial();
        }

    }
}
