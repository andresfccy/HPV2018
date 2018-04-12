using HPV_Entidades.CasosDeExito;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.CasosDeExitoWS
{
    public class OS_DarLogros
    {
        public Respuesta Respuesta { get; set; }
        public List<Logro> ListaLogro { get; set; }

        public OS_DarLogros()
        {
            Respuesta = new Respuesta();
            ListaLogro = new List<Logro>();
        }

    }
}
