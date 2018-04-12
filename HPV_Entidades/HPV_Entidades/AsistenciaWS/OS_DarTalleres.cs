using HPV_Entidades.Asistencia;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AsistenciaWS
{
    public class OS_DarTalleres
    {
        public Respuesta Respuesta { get; set; }
        public List<Taller> ListaTaller { get; set; }

        public OS_DarTalleres()
        {
            Respuesta = new Respuesta();
            ListaTaller = new List<Taller>();
        }
    }
}
