using HPV_Entidades.AdmParametroWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace HPV_Servicios.AdmParametro
{
    [ServiceContract]
    public interface IHPVServiciosAdmParametro_JSON
    {
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarParametros")]
        OS_DarParametros DarParametros();

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarParametros")]
        OS_DarParametros DarParametrosOptions();

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ConsultarParametro")]
        OS_ConsultarParametro ConsultarParametro(OE_ConsultarParametro oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ConsultarParametro")]
        OS_ConsultarParametro ConsultarParametroOptions(OE_ConsultarParametro oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ActualizarParametro")]
        OS_ActualizarParametro ActualizarParametro(OE_ActualizarParametro oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ActualizarParametro")]
        OS_ActualizarParametro ActualizarParametroOptions(OE_ActualizarParametro oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarPeriodos")]
        OS_DarPeriodos DarPeriodos();

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarPeriodos")]
        OS_DarPeriodos DarPeriodosOptions();

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ConsultarPeriodo")]
        OS_ConsultarPeriodo ConsultarPeriodo(OE_ConsultarPeriodo oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ConsultarPeriodo")]
        OS_ConsultarPeriodo ConsultarPeriodoOptions(OE_ConsultarPeriodo oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ActualizarPeriodo")]
        OS_ActualizarPeriodo ActualizarPeriodo(OE_ActualizarPeriodo oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ActualizarPeriodo")]
        OS_ActualizarPeriodo ActualizarPeriodoOptions(OE_ActualizarPeriodo oe);

    }
}
