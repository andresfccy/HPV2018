using HPV_Entidades.General;
using HPV_Entidades.Reporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.ReporteWS
{
    public class OS_RptUsuariosSistema
    {
        public Respuesta Respuesta { get; set; }
        public List<UsuariosSistema> ListaUsuariosSistema { get; set; }

        public OS_RptUsuariosSistema()
        {
            Respuesta = new Respuesta();
            ListaUsuariosSistema = new List<UsuariosSistema>();
        }

    }
}
