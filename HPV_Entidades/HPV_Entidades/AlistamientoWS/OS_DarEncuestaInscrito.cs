using HPV_Entidades.Alistamiento;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AlistamientoWS
{
    public class OS_DarEncuestaInscrito
    {
        public Respuesta Respuesta { get; set; }
        public List<Encuesta> ListaEncuesta { get; set; }

        public OS_DarEncuestaInscrito()
        {
            Respuesta = new Respuesta();
            ListaEncuesta = new List<Encuesta>();
        }
    }
}
