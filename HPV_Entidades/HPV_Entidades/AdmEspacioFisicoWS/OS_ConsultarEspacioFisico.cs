using HPV_Entidades.AdmEspacioFisico;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AdmEspacioFisicoWS
{
    public class OS_ConsultarEspacioFisico
    {
        public EspacioFisico EspacioFisico { get; set; }
        public Respuesta Respuesta { get; set; }

        public OS_ConsultarEspacioFisico()
        {
            Respuesta = new Respuesta();
            EspacioFisico = new EspacioFisico();
        }
    }
}
