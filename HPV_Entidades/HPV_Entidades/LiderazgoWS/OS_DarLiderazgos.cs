using HPV_Entidades.General;
using HPV_Entidades.Liderazgos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.LiderazgoWS
{
    public class OS_DarLiderazgos
    {
        public Respuesta Respuesta { get; set; }
        public List<Liderazgo> ListaLiderazgo { get; set; }

        public OS_DarLiderazgos()
        {
            Respuesta = new Respuesta();
            ListaLiderazgo = new List<Liderazgo>();
        }

    }
}
