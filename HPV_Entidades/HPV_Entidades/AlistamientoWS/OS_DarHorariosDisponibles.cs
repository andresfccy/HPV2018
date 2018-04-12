using HPV_Entidades.Alistamiento;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AlistamientoWS
{
    public class OS_DarHorariosDisponibles
    {
        public Respuesta Respuesta { get; set; }
        public List<Horario> ListaHorario { get; set; }

        public OS_DarHorariosDisponibles()
        {
            Respuesta = new Respuesta();
            ListaHorario = new List<Horario>();
        }

    }
}
