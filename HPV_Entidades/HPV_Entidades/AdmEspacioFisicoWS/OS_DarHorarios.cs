using HPV_Entidades.Alistamiento;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AdmEspacioFisicoWS
{
    public class OS_DarHorarios
    {
        public Respuesta Respuesta { get; set; }
        public List<Horario> ListaHorario { get; set; }

        public OS_DarHorarios()
        {
            Respuesta = new Respuesta();
            ListaHorario = new List<Horario>();
        }

    }
}
