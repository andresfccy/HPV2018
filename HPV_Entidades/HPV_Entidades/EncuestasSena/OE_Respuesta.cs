using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.EncuestasSena
{
    public class OE_Respuesta
    {
        public int IdRespuesta { get; set; }
        public int IdPregunta { get; set; }
        public string Letra { get; set; }
        public string Respuesta { get; set; }
        public bool Seleccionado { get; set; }
    }
}
