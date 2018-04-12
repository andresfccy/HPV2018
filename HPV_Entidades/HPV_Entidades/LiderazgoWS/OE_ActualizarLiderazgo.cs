using HPV_Entidades.Liderazgos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.LiderazgoWS
{
    public class OE_ActualizarLiderazgo
    {
        public Int64 IdUsuario { get; set; } // Id del usuario logeado
        public Liderazgo Liderazgo { get; set; }

    }
}
