using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.SistematizacionWS
{
    public class OS_DarSistematizacion
    {
        public Respuesta Respuesta { get; set; }
        public List<HPV_Entidades.Sistematizacion.Sistematizacion> ListaSistematizacion { get; set; }

        public OS_DarSistematizacion()
        {
            Respuesta = new Respuesta();
            ListaSistematizacion = new List<HPV_Entidades.Sistematizacion.Sistematizacion>();
        }
    }
}
