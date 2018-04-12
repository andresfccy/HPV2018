using HPV_Entidades.Asistencia;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AsistenciaWS
{
    public class OS_DarGruposXFacilitador
    {
        public Respuesta Respuesta { get; set; }
        public List<GrupoFacilitador> ListaGrupo { get; set; }

        public OS_DarGruposXFacilitador()
        {
            Respuesta = new Respuesta();
            ListaGrupo = new List<GrupoFacilitador>();
        }
    }
}
