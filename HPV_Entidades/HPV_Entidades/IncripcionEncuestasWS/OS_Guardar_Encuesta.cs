using HPV_Entidades.EncuestasSena;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.IncripcionEncuestasWS
{
    public class OS_Guardar_Encuesta
    {
        public Respuesta Respuesta { get; set; }
        public List<OE_RespuestasEncuesta> RespuestasEncuesta { get; set; }
        
        public OS_Guardar_Encuesta()
        {
            Respuesta = new Respuesta ();
            RespuestasEncuesta = new List<OE_RespuestasEncuesta>();
        }
    }
}
