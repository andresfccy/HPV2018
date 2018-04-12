using HPV_Entidades.General;
using HPV_Entidades.Liderazgos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.LiderazgoWS
{
    public class OS_ConsultarLiderazgo
    {
        public Liderazgo Liderazgo { get; set; }
        public Respuesta Respuesta { get; set; }

        public OS_ConsultarLiderazgo()
        {
            Respuesta = new Respuesta();
            Liderazgo = new Liderazgo();
        }

    }
}
