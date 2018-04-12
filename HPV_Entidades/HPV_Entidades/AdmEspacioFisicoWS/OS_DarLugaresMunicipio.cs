using HPV_Entidades.AdmEspacioFisico;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AdmEspacioFisicoWS
{
    public class OS_DarLugaresMunicipio
    {
        public Respuesta Respuesta { get; set; }
        public List<LugarMunicipio> ListaLugarMunicipio { get; set; }

        public OS_DarLugaresMunicipio()
        {
            Respuesta = new Respuesta();
            ListaLugarMunicipio = new List<LugarMunicipio>();
        }

    }
}
