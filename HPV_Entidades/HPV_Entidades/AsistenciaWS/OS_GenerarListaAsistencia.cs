using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AsistenciaWS
{
    public class OS_GenerarListaAsistencia
    {
        public Respuesta Respuesta { get; set; }
        public String UrlDescarga { get; set; }
        public OS_GenerarListaAsistencia()
        {
            Respuesta = new Respuesta();
            UrlDescarga = "";
        }
    }
}
