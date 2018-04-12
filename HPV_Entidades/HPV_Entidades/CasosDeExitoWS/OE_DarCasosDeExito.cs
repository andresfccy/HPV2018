using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.CasosDeExitoWS
{
    public class OE_DarCasosDeExito
    {
        public Int64 IdUsuario { get; set; } // Id del usuario logeado
        public String FiltroBusqueda { get; set; } // Filtro de búsqueda en campos propios del objeto
        public String EstadoXConsultar { get; set; } // Filtro de búsqueda en el campo estado
        public Int64? IdPeriodo { get; set; }

    }
}
