using HPV_Entidades.AdmEspacioFisico;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AdmEspacioFisicoWS
{
    public class OS_DarEspacioFisico
    {
        public Respuesta Respuesta { get; set; }
        public List<EspacioFisico> ListaEspacioFisico { get; set; }

        public OS_DarEspacioFisico()
        {
            Respuesta = new Respuesta();
            ListaEspacioFisico = new List<EspacioFisico>();
        }
    }
}
