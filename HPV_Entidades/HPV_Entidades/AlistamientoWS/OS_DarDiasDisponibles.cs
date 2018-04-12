using HPV_Entidades.Alistamiento;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AlistamientoWS
{
    public class OS_DarDiasDisponibles
    {
        public Respuesta Respuesta { get; set; }
        public List<Dia> ListaDia { get; set; }

        public OS_DarDiasDisponibles()
        {
            Respuesta = new Respuesta();
            ListaDia = new List<Dia>();
        }

    }
}
