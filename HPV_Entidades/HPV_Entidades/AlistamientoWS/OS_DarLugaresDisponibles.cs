using HPV_Entidades.Alistamiento;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AlistamientoWS
{
    public class OS_DarLugaresDisponibles
    {
        public Respuesta Respuesta { get; set; }
        public List<Lugar> ListaLugar { get; set; }

        public OS_DarLugaresDisponibles()
        {
            Respuesta = new Respuesta();
            ListaLugar = new List<Lugar>();
        }

    }
}
