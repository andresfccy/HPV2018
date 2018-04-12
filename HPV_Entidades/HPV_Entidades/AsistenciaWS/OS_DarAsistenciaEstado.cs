using HPV_Entidades.Asistencia;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AsistenciaWS
{
    public class OS_DarAsistenciaEstado
    {
        public Respuesta Respuesta { get; set; }
        public List<AsistenciaEstado> ListaAsistenciaEstado { get; set; }

        public OS_DarAsistenciaEstado()
        {
            Respuesta = new Respuesta();
            ListaAsistenciaEstado = new List<AsistenciaEstado>();
        }

    }
}
