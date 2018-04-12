using HPV_Entidades.CasosDeExitoWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace HPV_Servicios.CasosDeExito
{
    // IHPVServiciosCasosDeExito_JSON
    [ServiceContract]
    public interface IHPVServiciosCasosDeExito_JSON
    {
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ActualizarCasoDeExito")]
        OS_ActualizarCasoDeExito ActualizarCasoDeExito(OE_ActualizarCasoDeExito oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ActualizarCasoDeExito")]
        OS_ActualizarCasoDeExito ActualizarCasoDeExitoOptions(OE_ActualizarCasoDeExito oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ConsultarCasoDeExito")]
        OS_ConsultarCasoDeExito ConsultarCasoDeExito(OE_ConsultarCasoDeExito oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ConsultarCasoDeExito")]
        OS_ConsultarCasoDeExito ConsultarCasoDeExitoOptions(OE_ConsultarCasoDeExito oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarCasosDeExito")]
        OS_DarCasosDeExito DarCasosDeExito(OE_DarCasosDeExito oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarCasosDeExito")]
        OS_DarCasosDeExito DarCasosDeExitoOptions(OE_DarCasosDeExito oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ValidarCasoDeExito")]
        OS_ValidarCasoDeExito ValidarCasoDeExito(OE_ValidarCasoDeExito oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ValidarCasoDeExito")]
        OS_ValidarCasoDeExito ValidarCasoDeExitoOptions(OE_ValidarCasoDeExito oe);

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarLogros")]
        OS_DarLogros DarLogros();

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarLogros")]
        OS_DarLogros DarLogrosOptions();

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "RegistrarEstadoCasoDeExito")]
        OS_RegistrarEstadoCasoDeExito RegistrarEstadoCasoDeExito(OE_RegistrarEstadoCasoDeExito oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "RegistrarEstadoCasoDeExito")]
        OS_RegistrarEstadoCasoDeExito RegistrarEstadoCasoDeExitoOptions(OE_RegistrarEstadoCasoDeExito oe);
    }
}
