using HPV_Entidades.Alistamiento;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AdmEspacioFisicoWS
{
    public class OS_DarMunicipiosFacilitador
    {
        public Respuesta Respuesta { get; set; }
        public List<Municipio> ListaMunicipio { get; set; }

        public OS_DarMunicipiosFacilitador()
        {
            Respuesta = new Respuesta();
            ListaMunicipio = new List<Municipio>();
        }
    }
}
