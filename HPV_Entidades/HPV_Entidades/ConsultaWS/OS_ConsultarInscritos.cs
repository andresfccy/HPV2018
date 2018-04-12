using HPV_Entidades.Consulta;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.ConsultaWS
{
    public class OS_ConsultarInscritos
    {
        public Respuesta Respuesta { get; set; }
        public List<Inscrito> ListaInscrito { get; set; }

        public OS_ConsultarInscritos()
        {
            Respuesta = new Respuesta();
            ListaInscrito = new List<Inscrito>();
        }

    }
}
