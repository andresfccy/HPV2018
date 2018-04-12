using HPV_Entidades.Asistencia;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AsistenciaWS
{
    public class OS_DarFacilitadorXCoordinador
    {
        public Respuesta Respuesta { get; set; }
        public List<Facilitador> ListaFacilitador { get; set; }

        public OS_DarFacilitadorXCoordinador()
        {
            Respuesta = new Respuesta();
            ListaFacilitador = new List<Facilitador>();
        }
    }
}
