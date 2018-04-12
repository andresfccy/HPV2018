using HPV_Entidades.EncuestasSena;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.IncripcionEncuestasWS
{
    public class OS_Consultar_Preguntas
    {
        public Respuesta Respuesta { get; set; }
        public  List<OE_Pregunta> Preguntas { get; set; }

        public OS_Consultar_Preguntas()
        {
            Respuesta = new Respuesta();
            Preguntas = new List<OE_Pregunta>();
        }
    }
}
