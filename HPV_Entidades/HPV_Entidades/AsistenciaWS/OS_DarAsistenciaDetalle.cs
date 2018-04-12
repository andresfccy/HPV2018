using HPV_Entidades.Asistencia;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AsistenciaWS
{
    public class OS_DarAsistenciaDetalle
    {
        public Respuesta Respuesta { get; set; }
        public List<AsistenciaDetalle> ListaAsistenciaDetalle { get; set; }

        public OS_DarAsistenciaDetalle()
        {
            Respuesta = new Respuesta();
            ListaAsistenciaDetalle = new List<AsistenciaDetalle>();
        }
    }
}
