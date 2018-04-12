using HPV_Entidades.CasosDeExitoWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HPV_Servicios.CasosDeExito
{
    // IHPVServiciosCasosDeExito
    [ServiceContract]
    public interface IHPVServiciosCasosDeExito
    {
        [OperationContract]
        OS_ActualizarCasoDeExito ActualizarCasoDeExito(OE_ActualizarCasoDeExito oe);

        [OperationContract]
        OS_ConsultarCasoDeExito ConsultarCasoDeExito(OE_ConsultarCasoDeExito oe);

        [OperationContract]
        OS_DarCasosDeExito DarCasosDeExito(OE_DarCasosDeExito oe);

        [OperationContract]
        OS_ValidarCasoDeExito ValidarCasoDeExito(OE_ValidarCasoDeExito oe);

        [OperationContract]
        OS_DarLogros DarLogros();

        [OperationContract]
        OS_RegistrarEstadoCasoDeExito RegistrarEstadoCasoDeExito(OE_RegistrarEstadoCasoDeExito oe);
    }
}
