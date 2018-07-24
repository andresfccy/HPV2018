using HPV_Entidades.SistematizacionWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace HPV_Servicios.Sistematizacion
{
    // IHPVServiciosSistematizacion_JSON
    [ServiceContract]
    public interface IHPVServiciosSistematizacion_JSON
    {
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ActualizarSistematizacion")]
        OS_ActualizarSistematizacion ActualizarSistematizacion(OE_ActualizarSistematizacion oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ActualizarSistematizacion")]
        OS_ActualizarSistematizacion ActualizarSistematizacionOptions(OE_ActualizarSistematizacion oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ConsultarSistematizacion")]
        OS_ConsultarSistematizacion ConsultarSistematizacion(OE_ConsultarSistematizacion oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ConsultarSistematizacion")]
        OS_ConsultarSistematizacion ConsultarSistematizacionOptions(OE_ConsultarSistematizacion oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarSistematizacion")]
        OS_DarSistematizacion DarSistematizacion(OE_DarSistematizacion oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarSistematizacion")]
        OS_DarSistematizacion DarSistematizacionOptions(OE_DarSistematizacion oe);
    }
}
