using HPV_Entidades.Asistencia;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AsistenciaWS
{
    public class OS_DarEntregableEstadoDetalle
    {
        public Respuesta Respuesta { get; set; }
        public EntregableEstado EntregableEstado { get; set; }

        public OS_DarEntregableEstadoDetalle()
        {
            Respuesta = new Respuesta();
            EntregableEstado = new EntregableEstado();
        }

    }
}
