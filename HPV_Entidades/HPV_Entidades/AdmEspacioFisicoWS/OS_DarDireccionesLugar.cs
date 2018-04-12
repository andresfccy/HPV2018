using HPV_Entidades.AdmEspacioFisico;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AdmEspacioFisicoWS
{
    public class OS_DarDireccionesLugar
    {
        public Respuesta Respuesta { get; set; }
        public List<Direccion> ListaDireccion { get; set; }

        public OS_DarDireccionesLugar()
        {
            Respuesta = new Respuesta();
            ListaDireccion = new List<Direccion>();
        }
    }
}
