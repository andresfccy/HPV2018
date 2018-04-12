using HPV_Entidades.Asistencia;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AsistenciaWS
{
    public class OS_DarAsistenciaEncabezado
    {
        public Respuesta Respuesta { get; set; }
        public AsistenciaEncabezado AsistenciaEncabezado { get; set; }

        public OS_DarAsistenciaEncabezado()
        {
            Respuesta = new Respuesta();
            AsistenciaEncabezado = new AsistenciaEncabezado();
        }
    }
}
