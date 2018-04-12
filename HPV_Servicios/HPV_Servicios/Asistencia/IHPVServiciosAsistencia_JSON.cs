using HPV_Entidades.AsistenciaWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace HPV_Servicios.Asistencia
{
    // IHPVServiciosAsistencia_JSON
    [ServiceContract]
    public interface IHPVServiciosAsistencia_JSON
    {
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarInscritos")]
        OS_DarInscritos DarInscritos(OE_DarInscritos oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarInscritos")]
        OS_DarInscritos DarInscritosOptions(OE_DarInscritos oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarGruposXFacilitador")]
        OS_DarGruposXFacilitador DarGruposXFacilitador(OE_DarGruposXFacilitador oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarGruposXFacilitador")]
        OS_DarGruposXFacilitador DarGruposXFacilitadorOptions(OE_DarGruposXFacilitador oe);

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarTalleres")]
        OS_DarTalleres DarTalleres();

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarTalleres")]
        OS_DarTalleres DarTalleresOptions();

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "GenerarListaAsistencia")]
        OS_GenerarListaAsistencia GenerarListaAsistencia(OE_GenerarListaAsistencia oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "GenerarListaAsistencia")]
        OS_GenerarListaAsistencia GenerarListaAsistenciaOptions(OE_GenerarListaAsistencia oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarFacilitadorXCoordinador")]
        OS_DarFacilitadorXCoordinador DarFacilitadorXCoordinador(OE_DarFacilitadorXCoordinador oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarFacilitadorXCoordinador")]
        OS_DarFacilitadorXCoordinador DarFacilitadorXCoordinadorOptions(OE_DarFacilitadorXCoordinador oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarEntregableEstado")]
        OS_DarEntregableEstado DarEntregableEstadoOptions(OE_DarEntregableEstado oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarEntregableEstado")]
        OS_DarEntregableEstado DarEntregableEstado(OE_DarEntregableEstado oe);


        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarEntregableEstadoDetalle")]
        OS_DarEntregableEstadoDetalle DarEntregableEstadoDetalle(OE_DarEntregableEstadoDetalle oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarEntregableEstadoDetalle")]
        OS_DarEntregableEstadoDetalle DarEntregableEstadoDetalleOptions(OE_DarEntregableEstadoDetalle oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarAsistenciaEstado")]
        OS_DarAsistenciaEstado DarAsistenciaEstado(OE_DarAsistenciaEstado oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarAsistenciaEstado")]
        OS_DarAsistenciaEstado DarAsistenciaEstadoOptions(OE_DarAsistenciaEstado oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarAsistenteXFacilitador")]
        OS_DarAsistenteXFacilitador DarAsistenteXFacilitador(OE_DarAsistenteXFacilitador oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarAsistenteXFacilitador")]
        OS_DarAsistenteXFacilitador DarAsistenteXFacilitadorOptions(OE_DarAsistenteXFacilitador oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "RegistrarAsistencia")]
        OS_RegistrarAsistencia RegistrarAsistencia(OE_RegistrarAsistencia oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "RegistrarAsistencia")]
        OS_RegistrarAsistencia RegistrarAsistenciaOptions(OE_RegistrarAsistencia oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "RegistrarAprobacion")]
        OS_RegistrarAprobacion RegistrarAprobacion(OE_RegistrarAprobacion oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "RegistrarAprobacion")]
        OS_RegistrarAprobacion RegistrarAprobacionOptions(OE_RegistrarAprobacion oe);
    }
}
