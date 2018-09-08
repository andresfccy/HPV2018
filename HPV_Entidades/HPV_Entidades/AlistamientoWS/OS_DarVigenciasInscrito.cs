using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AlistamientoWS
{
    public class OS_DarVigenciasInscrito
    {
        public Respuesta Respuesta { get; set; }
        public List<int> ListaVigencia { get; set; }

        public OS_DarVigenciasInscrito()
        {
            Respuesta = new Respuesta();
            ListaVigencia = new List<int>();
        }
    }
}
