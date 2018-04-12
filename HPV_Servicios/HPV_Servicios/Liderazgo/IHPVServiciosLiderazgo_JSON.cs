using HPV_Entidades.LiderazgoWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace HPV_Servicios.Liderazgo
{
    // IHPVServiciosLiderazgo_JSON
    [ServiceContract]
    public interface IHPVServiciosLiderazgo_JSON
    {
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ActualizarLiderazgo")]
        OS_ActualizarLiderazgo ActualizarLiderazgo(OE_ActualizarLiderazgo oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ActualizarLiderazgo")]
        OS_ActualizarLiderazgo ActualizarLiderazgoOptions(OE_ActualizarLiderazgo oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ConsultarLiderazgo")]
        OS_ConsultarLiderazgo ConsultarLiderazgo(OE_ConsultarLiderazgo oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ConsultarLiderazgo")]
        OS_ConsultarLiderazgo ConsultarLiderazgoOptions(OE_ConsultarLiderazgo oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarLiderazgos")]
        OS_DarLiderazgos DarLiderazgos(OE_DarLiderazgos oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarLiderazgos")]
        OS_DarLiderazgos DarLiderazgosOptions(OE_DarLiderazgos oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ValidarCantidadLiderazgo")]
        OS_ValidarCantidadLiderazgo ValidarCantidadLiderazgo(OE_ValidarCantidadLiderazgo oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ValidarCantidadLiderazgo")]
        OS_ValidarCantidadLiderazgo ValidarCantidadLiderazgoOptions(OE_ValidarCantidadLiderazgo oe);


        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "RegistrarEstadoLiderazgo")]
        OS_RegistrarEstadoLiderazgo RegistrarEstadoLiderazgo(OE_RegistrarEstadoLiderazgo oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "RegistrarEstadoLiderazgo")]
        OS_RegistrarEstadoLiderazgo RegistrarEstadoLiderazgoOptions(OE_RegistrarEstadoLiderazgo oe);
    }
}
