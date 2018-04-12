using HPV_Entidades.CasosDeExito;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.CasosDeExitoWS
{
    public class OS_ConsultarCasoDeExito
    {
        public CasoDeExito CasoDeExito { get; set; }
        public Respuesta Respuesta { get; set; }

        public OS_ConsultarCasoDeExito()
        {
            Respuesta = new Respuesta();
            CasoDeExito = new CasoDeExito();
        }
    }
}
