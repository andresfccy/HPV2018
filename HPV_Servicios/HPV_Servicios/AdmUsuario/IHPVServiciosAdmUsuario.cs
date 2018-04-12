using HPV_Entidades.AdmUsuarioWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HPV_Servicios.AdmUsuario
{
    // IHPVServiciosAdmUsuario
    [ServiceContract]
    public interface IHPVServiciosAdmUsuario
    {
        [OperationContract]
        OS_AutenticarUsuario AutenticarUsuario(OE_AutenticarUsuario oe);

        [OperationContract]
        OS_DarUsuarios DarUsuarios(OE_DarUsuarios oe);

        [OperationContract]
        OS_DarRoles DarRoles(OE_DarRoles oe);

        [OperationContract]
        OS_DarCoordinadores DarCoordinadores();

        [OperationContract]
        OS_DarMunicipios DarMunicipios();

        [OperationContract]
        OS_ConsultarUsuario ConsultarUsuario(OE_ConsultarUsuario oe);

        [OperationContract]
        OS_DarMenus DarMenus();

        [OperationContract]
        OS_DarOpcionesMenu DarOpcionesMenu(OE_DarOpcionesMenu oe);

        [OperationContract]
        OS_ConsultarRol ConsultarRol(OE_ConsultarRol oe);

        [OperationContract]
        OS_DarOpcionesMenuRol DarOpcionesMenuRol(OE_DarOpcionesMenuRol oe);

        [OperationContract]
        OS_ActualizarUsuario ActualizarUsuario(OE_ActualizarUsuario oe);

        [OperationContract]
        OS_ActualizarRol ActualizarRol(OE_ActualizarRol oe);

        [OperationContract]
        OS_RecuperarContrasena RecuperarContrasena(OE_RecuperarContrasena oe);

        [OperationContract]
        OS_AsignarContrasenaXToken AsignarContrasenaXToken(OE_AsignarContrasenaXToken oe);

    }
}
