using HPV_Entidades.LiderazgoWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.Liderazgos
{
    interface InterfaceLiderazgo
    {
        OS_ActualizarLiderazgo ActualizarLiderazgo(OE_ActualizarLiderazgo oe);

        OS_ConsultarLiderazgo ConsultarLiderazgo(OE_ConsultarLiderazgo oe);

        OS_DarLiderazgos DarLiderazgos(OE_DarLiderazgos oe);

        OS_ValidarCantidadLiderazgo ValidarCantidadLiderazgo(OE_ValidarCantidadLiderazgo oe);
        
        OS_RegistrarEstadoLiderazgo RegistrarEstadoLiderazgo(OE_RegistrarEstadoLiderazgo oe);

    }
}
