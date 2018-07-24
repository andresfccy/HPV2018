using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.SistematizacionWS
{
    public class OS_ConsultarSistematizacion
    {
        public HPV_Entidades.Sistematizacion.Sistematizacion Sistematizacion { get; set; }
        public Respuesta Respuesta { get; set; }

        public OS_ConsultarSistematizacion()
        {
            Respuesta = new Respuesta();
            Sistematizacion = new HPV_Entidades.Sistematizacion.Sistematizacion();
        }
    }
}
