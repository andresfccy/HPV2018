
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPV_Entidades.General;
using HPV_Entidades.Alistamiento;

namespace HPV_Entidades.GeneralWS
{
    public class OS_DarListaValor
    {

        public Respuesta Respuesta { get; set; }
        public List<ListaValor> ListaValor { get; set; }

        public OS_DarListaValor()
        {
            Respuesta = new Respuesta();
            ListaValor = new List<ListaValor>();
        }

    }
}
