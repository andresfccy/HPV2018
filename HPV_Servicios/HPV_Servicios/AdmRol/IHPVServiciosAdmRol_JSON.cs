using HPV_Entidades.AdmRolWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace HPV_Servicios.AdmRol
{
    // IHPVServiciosAdmRol_JSON
    [ServiceContract]
    public interface IHPVServiciosAdmRol_JSON
    {
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarRoles")]
        OS_DarRoles DarRoles();

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarRoles")]
        OS_DarRoles DarRolesOptions();

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ConsultarRol")]
        OS_ConsultarRol ConsultarRol(OE_ConsultarRol oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ConsultarRol")]
        OS_ConsultarRol ConsultarRolOptions(OE_ConsultarRol oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarOpcionesUsuario")]
        OS_DarOpcionesUsuario DarOpcionesUsuario(OE_DarOpcionesUsuario oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarOpcionesUsuario")]
        OS_DarOpcionesUsuario DarOpcionesUsuarioOptions(OE_DarOpcionesUsuario oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ActualizarRol")]
        OS_ActualizarRol ActualizarRol(OE_ActualizarRol oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ActualizarRol")]
        OS_ActualizarRol ActualizarRolOptions(OE_ActualizarRol oe);
    }
}
