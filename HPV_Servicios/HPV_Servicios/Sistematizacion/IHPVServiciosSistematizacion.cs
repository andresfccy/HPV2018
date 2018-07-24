using HPV_Entidades.SistematizacionWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HPV_Servicios.Sistematizacion
{
    // IHPVServiciosSistematizacion
    [ServiceContract]
    public interface IHPVServiciosSistematizacion
    {
        [OperationContract]
        OS_ActualizarSistematizacion ActualizarSistematizacion(OE_ActualizarSistematizacion oe);

        [OperationContract]
        OS_ConsultarSistematizacion ConsultarSistematizacion(OE_ConsultarSistematizacion oe);

        [OperationContract]
        OS_DarSistematizacion DarSistematizacion(OE_DarSistematizacion oe);
    }
}
