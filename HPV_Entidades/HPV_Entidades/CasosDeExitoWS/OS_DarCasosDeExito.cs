using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPV_Entidades.CasosDeExito;

namespace HPV_Entidades.CasosDeExitoWS
{
    public class OS_DarCasosDeExito
    {
        public Respuesta Respuesta { get; set; }
        public List<CasoDeExito> ListaCasoDeExito { get; set; }

        public OS_DarCasosDeExito()
        {
            Respuesta = new Respuesta();
            ListaCasoDeExito = new List<CasoDeExito>();
        }
    }
}
