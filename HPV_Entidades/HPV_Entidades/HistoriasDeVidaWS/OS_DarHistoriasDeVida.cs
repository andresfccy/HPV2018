using HPV_Entidades.HistoriasDeVidaWS;
using HPV_Entidades.HistoriasDeVida;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.HistoriasDeVidaWS
{
    public class OS_DarHistoriasDeVida
    {
        public Respuesta Respuesta { get; set; }
        public List<HistoriaDeVida> ListaHistoriaDeVida { get; set; }

        public OS_DarHistoriasDeVida()
        {
            Respuesta = new Respuesta();
            ListaHistoriaDeVida = new List<HistoriaDeVida>();
        }
    }
}
