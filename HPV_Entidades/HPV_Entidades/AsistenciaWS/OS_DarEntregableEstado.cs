using HPV_Entidades.Asistencia;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AsistenciaWS
{
    public class OS_DarEntregableEstado
    {
        public Respuesta Respuesta { get; set; }
        public List<EntregableEstado> ListaEntregableEstado { get; set; }

        public OS_DarEntregableEstado()
        {
            Respuesta = new Respuesta();
            ListaEntregableEstado = new List<EntregableEstado>();
        }
    }
}
