using HPV_Entidades.ConsultaWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace HPV_Servicios.Consulta
{
    // IHPVServiciosConsulta_JSON
    [ServiceContract]
    public interface IHPVServiciosConsulta_JSON
    {

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ConsultarInscritos")]
        OS_ConsultarInscritos ConsultarInscritos(OE_ConsultarInscritos oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ConsultarInscritos")]
        OS_ConsultarInscritos ConsultarInscritosOptions(OE_ConsultarInscritos oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ConsultarInscrito")]
        OS_ConsultarInscrito ConsultarInscrito(OE_ConsultarInscrito oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ConsultarInscrito")]
        OS_ConsultarInscrito ConsultarInscritoOptions(OE_ConsultarInscrito oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ConsultarHorarios")]
        OS_ConsultarHorarios ConsultarHorarios(OE_ConsultarHorarios oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ConsultarHorarios")]
        OS_ConsultarHorarios ConsultarHorariosOptions(OE_ConsultarHorarios oe);


    }
}
