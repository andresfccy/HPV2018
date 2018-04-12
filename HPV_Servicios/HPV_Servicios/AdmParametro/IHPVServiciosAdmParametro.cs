using HPV_Entidades.AdmParametroWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HPV_Servicios.AdmParametro
{
    [ServiceContract]
    public interface IHPVServiciosAdmParametro
    {
        [OperationContract]
        OS_DarParametros DarParametros();

        [OperationContract]
        OS_ConsultarParametro ConsultarParametro(OE_ConsultarParametro oe);

        [OperationContract]
        OS_ActualizarParametro ActualizarParametro(OE_ActualizarParametro oe);

        [OperationContract]
        OS_DarPeriodos DarPeriodos();

        [OperationContract]
        OS_ConsultarPeriodo ConsultarPeriodo(OE_ConsultarPeriodo oe);

        [OperationContract]
        OS_ActualizarPeriodo ActualizarPeriodo(OE_ActualizarPeriodo oe);

    }
}
