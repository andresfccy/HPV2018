using HPV_Entidades.HistoriasDeVidaWS;
using HPV_Entidades.General;
using HPV_Entidades.HistoriasDeVida;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.HistoriasDeVidaWS
{
    public class OS_ConsultarHistoriaVida
    {
        public HistoriaDeVida HistoriaDeVida { get; set; }
        public Respuesta Respuesta { get; set; }

        public OS_ConsultarHistoriaVida()
        {
            Respuesta = new Respuesta();
            HistoriaDeVida = new HistoriaDeVida();
        }
    }
}
