using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using HPV_Entidades.AdmUsuarioWS;
using HPV_Datos.AdmUsuario;
using HPV_Entidades.General;
using System.Net;
using System.Net.Mail;

namespace HPV_Servicios.AdmUsuario
{
    // HPVServiciosAdmUsuario_JSON
    // El servicio se publica en HPVServiciosAdmUsuario_JSON.svc or HPVServiciosAdmUsuario_JSON.svc.cs
    public class HPVServiciosAdmUsuario_JSON : IHPVServiciosAdmUsuario_JSON
    {
        public OS_ActualizarRol ActualizarRol(OE_ActualizarRol oe)
        {
            return(new FachadaAdmUsuario().ActualizarRol(oe));
        }

        public OS_ActualizarRol ActualizarRolOptions(OE_ActualizarRol oe)
        {
            return null;
        }

        public OS_ActualizarUsuario ActualizarUsuario(OE_ActualizarUsuario oe)
        {
            return (new FachadaAdmUsuario().ActualizarUsuario(oe));
        }

        public OS_ActualizarUsuario ActualizarUsuarioOptions(OE_ActualizarUsuario oe)
        {
            return null;
        }

        public OS_AutenticarUsuario AutenticarUsuario(OE_AutenticarUsuario oe)
        {
            return (new FachadaAdmUsuario().AutenticarUsuario(oe));
        }

        public OS_AutenticarUsuario AutenticarUsuarioOptions(OE_AutenticarUsuario oe)
        {
            return null;
        }

        public OS_ConsultarRol ConsultarRol(OE_ConsultarRol oe)
        {
            return (new FachadaAdmUsuario().ConsultarRol(oe));
        }

        public OS_ConsultarRol ConsultarRolOptions(OE_ConsultarRol oe)
        {
            return null;
        }

        public OS_ConsultarUsuario ConsultarUsuario(OE_ConsultarUsuario oe)
        {
            return (new FachadaAdmUsuario().ConsultarUsuario(oe));
        }

        public OS_ConsultarUsuario ConsultarUsuarioOptions(OE_ConsultarUsuario oe)
        {
            return null;
        }

        public OS_DarCoordinadores DarCoordinadores()
        {
            return (new FachadaAdmUsuario().DarCoordinadores());
        }

        public OS_DarCoordinadores DarCoordinadoresOptions()
        {
            return null;
        }

        public OS_DarMenus DarMenus()
        {
            return (new FachadaAdmUsuario().DarMenus());
        }

        public OS_DarMenus DarMenusOptions()
        {
            return null;
        }

        public OS_DarMunicipios DarMunicipios()
        {
            return (new FachadaAdmUsuario().DarMunicipios());
        }

        public OS_DarMunicipios DarMunicipiosOptions()
        {
            return null;
        }

        public OS_DarOpcionesMenu DarOpcionesMenu(OE_DarOpcionesMenu oe)
        {
            return (new FachadaAdmUsuario().DarOpcionesMenu(oe));
        }

        public OS_DarOpcionesMenu DarOpcionesMenuOptions(OE_DarOpcionesMenu oe)
        {
            return null;
        }

        public OS_DarOpcionesMenuRol DarOpcionesMenuRol(OE_DarOpcionesMenuRol oe)
        {
            return (new FachadaAdmUsuario().DarOpcionesMenuRol(oe));
        }

        public OS_DarOpcionesMenuRol DarOpcionesMenuRolOptions(OE_DarOpcionesMenuRol oe)
        {
            return null;
        }

        public OS_DarRoles DarRoles(OE_DarRoles oe)
        {
            return (new FachadaAdmUsuario().DarRoles(oe));
        }

        public OS_DarRoles DarRolesOptions(OE_DarRoles oe)
        {
            return null;
        }

        public OS_DarUsuarios DarUsuarios(OE_DarUsuarios oe)
        {
            return (new FachadaAdmUsuario().DarUsuarios(oe));
        }

        public OS_DarUsuarios DarUsuariosOptions(OE_DarUsuarios oe)
        {
            return null;
        }

        public OS_RecuperarContrasena RecuperarContrasenaOptions(OE_RecuperarContrasena oe)
        {
            return null;
        }

        public OS_RecuperarContrasena RecuperarContrasena(OE_RecuperarContrasena oe)
        {
            return (new FachadaAdmUsuario().RecuperarContrasena(oe));
        }
 
        public OS_AsignarContrasenaXToken AsignarContrasenaXTokenOptions(OE_AsignarContrasenaXToken oe)
        {
            return null;
        }


        public OS_AsignarContrasenaXToken AsignarContrasenaXToken(OE_AsignarContrasenaXToken oe)
        {
            return (new FachadaAdmUsuario().AsignarContrasenaXToken(oe));
        }

    }
}
