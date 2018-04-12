using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.HistoriasDeVidaWS
{
    public class OS_RegistrarEstadoHistoriaVida
    {
        public Respuesta Respuesta { get; set; }

        public OS_RegistrarEstadoHistoriaVida()
        {
            Respuesta = new Respuesta();
        }
    }
}
