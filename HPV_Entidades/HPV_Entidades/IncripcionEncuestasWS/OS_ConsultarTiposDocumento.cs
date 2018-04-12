using HPV_Entidades.General;
using HPV_Entidades.EncuestasSena;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.IncripcionEncuestasWS
{
    public class OS_ConsultarTiposDocumento
    {
        public Respuesta Respuesta { get; set; }
        public List<OE_TiposDocumento> ListaTiposDocumento { get; set; }

        public OS_ConsultarTiposDocumento()
        {
            Respuesta = new Respuesta();
            ListaTiposDocumento = new List<OE_TiposDocumento>();
        }
    }
}
