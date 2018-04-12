using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.HistoriasDeVidaWS
{
    public class OE_RegistrarEstadoHistoriaVida
    {
        public Int64 IdUsuario { get; set; }
        public Int64 IdHistoriaVida { get; set; }
        public String MotivoRechazo { get; set; }
        public String Estado { get; set; }
    }
}
