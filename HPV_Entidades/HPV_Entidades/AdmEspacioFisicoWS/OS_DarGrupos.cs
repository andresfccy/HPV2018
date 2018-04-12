using HPV_Entidades.AdmEspacioFisico;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AdmEspacioFisicoWS
{
    public class OS_DarGrupos
    {
        public Respuesta Respuesta { get; set; }
        public List<Grupo> ListaGrupo { get; set; }

        public OS_DarGrupos()
        {
            Respuesta = new Respuesta();
            ListaGrupo = new List<Grupo>();
        }


    }
}
