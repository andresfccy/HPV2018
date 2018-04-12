using HPV_Entidades.LiderazgoWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HPV_Servicios.Liderazgo
{
    // IHPVServiciosLiderazgo
    [ServiceContract]
    public interface IHPVServiciosLiderazgo
    {
        [OperationContract]
        OS_ActualizarLiderazgo ActualizarLiderazgo(OE_ActualizarLiderazgo oe);

        [OperationContract]
        OS_ConsultarLiderazgo ConsultarLiderazgo(OE_ConsultarLiderazgo oe);

        [OperationContract]
        OS_DarLiderazgos DarLiderazgos(OE_DarLiderazgos oe);

        [OperationContract]
        OS_ValidarCantidadLiderazgo ValidarCantidadLiderazgo(OE_ValidarCantidadLiderazgo oe);

        [OperationContract]
        OS_RegistrarEstadoLiderazgo RegistrarEstadoLiderazgo(OE_RegistrarEstadoLiderazgo oe);
    }
}
