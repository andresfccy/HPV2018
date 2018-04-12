using HPV_Entidades.Consulta;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.ConsultaWS
{
    public class OS_ConsultarInscrito
    {
        public Respuesta Respuesta { get; set; }
        public Inscrito Inscrito { get; set; }

        public OS_ConsultarInscrito()
        {
            Respuesta = new Respuesta();
            Inscrito = new Inscrito();
        }
    }
}
