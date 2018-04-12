using HPV_Entidades.EncuestasSena;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.IncripcionEncuestasWS
{
    public class OS_CrearDatosBasicos
    {
        public Respuesta Respuesta { get; set; }
        public OE_UsuarioEncuesta UsuarioEncuestaSena { get; set; }

        public OS_CrearDatosBasicos()
        {
            Respuesta = new Respuesta();
            UsuarioEncuestaSena = new OE_UsuarioEncuesta();
        }
    }
}
