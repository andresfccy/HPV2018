using HPV_Entidades.AdmUsuarioWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace HPV_Servicios.AdmUsuario
{
    // IHPVServiciosAdmUsuario_JSON
    [ServiceContract]
    public interface IHPVServiciosAdmUsuario_JSON
    {

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "AutenticarUsuario")]
        OS_AutenticarUsuario AutenticarUsuario(OE_AutenticarUsuario oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "AutenticarUsuario")]
        OS_AutenticarUsuario AutenticarUsuarioOptions(OE_AutenticarUsuario oe);


        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarUsuarios")]
        OS_DarUsuarios DarUsuarios(OE_DarUsuarios oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarUsuarios")]
        OS_DarUsuarios DarUsuariosOptions(OE_DarUsuarios oe);


        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarRoles")]
        OS_DarRoles DarRoles(OE_DarRoles oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarRoles")]
        OS_DarRoles DarRolesOptions(OE_DarRoles oe);

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarCoordinadores")]
        OS_DarCoordinadores DarCoordinadores();

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarCoordinadores")]
        OS_DarCoordinadores DarCoordinadoresOptions();

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarMunicipios")]
        OS_DarMunicipios DarMunicipios();

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarMunicipios")]
        OS_DarMunicipios DarMunicipiosOptions();

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ConsultarUsuario")]
        OS_ConsultarUsuario ConsultarUsuario(OE_ConsultarUsuario oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ConsultarUsuario")]
        OS_ConsultarUsuario ConsultarUsuarioOptions(OE_ConsultarUsuario oe);

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarMenus")]
        OS_DarMenus DarMenus();

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarMenus")]
        OS_DarMenus DarMenusOptions();

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarOpcionesMenu")]
        OS_DarOpcionesMenu DarOpcionesMenu(OE_DarOpcionesMenu oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarOpcionesMenu")]
        OS_DarOpcionesMenu DarOpcionesMenuOptions(OE_DarOpcionesMenu oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ConsultarRol")]
        OS_ConsultarRol ConsultarRol(OE_ConsultarRol oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ConsultarRol")]
        OS_ConsultarRol ConsultarRolOptions(OE_ConsultarRol oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarOpcionesMenuRol")]
        OS_DarOpcionesMenuRol DarOpcionesMenuRol(OE_DarOpcionesMenuRol oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarOpcionesMenuRol")]
        OS_DarOpcionesMenuRol DarOpcionesMenuRolOptions(OE_DarOpcionesMenuRol oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ActualizarUsuario")]
        OS_ActualizarUsuario ActualizarUsuario(OE_ActualizarUsuario oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ActualizarUsuario")]
        OS_ActualizarUsuario ActualizarUsuarioOptions(OE_ActualizarUsuario oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ActualizarRol")]
        OS_ActualizarRol ActualizarRol(OE_ActualizarRol oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ActualizarRol")]
        OS_ActualizarRol ActualizarRolOptions(OE_ActualizarRol oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "RecuperarContrasena")]
        OS_RecuperarContrasena RecuperarContrasena(OE_RecuperarContrasena oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "RecuperarContrasena")]
        OS_RecuperarContrasena RecuperarContrasenaOptions(OE_RecuperarContrasena oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "AsignarContrasenaXToken")]
        OS_AsignarContrasenaXToken AsignarContrasenaXToken(OE_AsignarContrasenaXToken oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "AsignarContrasenaXToken")]
        OS_AsignarContrasenaXToken AsignarContrasenaXTokenOptions(OE_AsignarContrasenaXToken oe);

    }
}
