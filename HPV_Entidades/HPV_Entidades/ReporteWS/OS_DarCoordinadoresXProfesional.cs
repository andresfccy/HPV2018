using HPV_Entidades.AdmUsuario;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.ReporteWS
{
    public class OS_DarCoordinadoresXProfesional
    {
        public Respuesta Respuesta { get; set; }
        public List<Coordinador> ListaCoordinador { get; set; }

        public OS_DarCoordinadoresXProfesional()
        {
            Respuesta = new Respuesta();
            ListaCoordinador = new List<Coordinador>();
        }
    }
}
