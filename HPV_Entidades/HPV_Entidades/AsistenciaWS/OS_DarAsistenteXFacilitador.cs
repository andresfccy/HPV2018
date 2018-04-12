using HPV_Entidades.Asistencia;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AsistenciaWS
{
    public class OS_DarAsistenteXFacilitador
    {
        public Respuesta Respuesta { get; set; }
        public List<Asistente> ListaAsistente { get; set; }

        public OS_DarAsistenteXFacilitador()
        {
            Respuesta = new Respuesta();
            ListaAsistente = new List<Asistente>();
        }
    }
}
