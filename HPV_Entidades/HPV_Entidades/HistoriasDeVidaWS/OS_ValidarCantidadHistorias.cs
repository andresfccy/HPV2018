using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.HistoriasDeVidaWS
{
    public class OS_ValidarCantidadHistorias
    {
        public Respuesta Respuesta { get; set; } // Si es cero puede registrar mas historias, de lo contrario no puede agregar mas

        public OS_ValidarCantidadHistorias()
        {
            Respuesta = new Respuesta();
        }

    }
}
