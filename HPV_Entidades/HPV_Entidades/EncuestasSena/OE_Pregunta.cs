using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.EncuestasSena
{
    public class OE_Pregunta
    {
        public int IdPregunta { get; set; }
        public int NumeroPregunta { get; set; }
        public string Pregunta { get; set; }
        public int IdEncuesta { get; set; }
        public List<OE_Respuesta> Respuestas { get; set; }
    }
}
