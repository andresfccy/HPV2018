using HPV_Entidades.Asistencia;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AsistenciaWS
{
    public class OS_DarInscritos
    {
        public Respuesta Respuesta { get; set; }
        public List<AsistenciaEstado> ListaInscrito { get; set; }

        public OS_DarInscritos()
        {
            Respuesta = new Respuesta();
            ListaInscrito = new List<AsistenciaEstado>();
        }

    }
}
