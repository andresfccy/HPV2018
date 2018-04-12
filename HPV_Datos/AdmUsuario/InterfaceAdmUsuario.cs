using HPV_Entidades.AdmUsuarioWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.AdmUsuario
{
    interface InterfaceAdmUsuario
    {
        OS_AutenticarUsuario AutenticarUsuario(OE_AutenticarUsuario oe);
        OS_DarUsuarios DarUsuarios(OE_DarUsuarios oe);
        OS_DarRoles DarRoles(OE_DarRoles oe);
        OS_DarCoordinadores DarCoordinadores();
        OS_DarMunicipios DarMunicipios();
        OS_ConsultarUsuario ConsultarUsuario(OE_ConsultarUsuario oe);
        OS_DarMenus DarMenus();
        OS_DarOpcionesMenu DarOpcionesMenu(OE_DarOpcionesMenu oe);
        OS_ConsultarRol ConsultarRol(OE_ConsultarRol oe);
        OS_DarOpcionesMenuRol DarOpcionesMenuRol(OE_DarOpcionesMenuRol oe);
        OS_ActualizarUsuario ActualizarUsuario(OE_ActualizarUsuario oe);
        OS_ActualizarRol ActualizarRol(OE_ActualizarRol oe);
        OS_RecuperarContrasena RecuperarContrasena(OE_RecuperarContrasena oe);
        OS_AsignarContrasenaXToken AsignarContrasenaXToken(OE_AsignarContrasenaXToken oe);

    }
}
