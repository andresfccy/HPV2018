using HPV_Entidades.Alistamiento;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AlistamientoWS
{
    public class OS_DarDepartamentos
    {
        public Respuesta Respuesta { get; set; }
        public List<Departamento> ListaDepartamento { get; set; }

        public OS_DarDepartamentos()
        {
            Respuesta = new Respuesta();
            ListaDepartamento = new List<Departamento>();
        }
    }
}
