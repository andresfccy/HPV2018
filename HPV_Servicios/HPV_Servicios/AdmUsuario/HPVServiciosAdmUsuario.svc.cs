using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using HPV_Entidades.AdmUsuarioWS;
using HPV_Datos.AdmUsuario;
using System.Net.Mail;
using System.Net;

namespace HPV_Servicios.AdmUsuario
{
    // HPVServiciosAdmUsuario
    // El servicio se publica en HPVServiciosAdmUsuario.svc or HPVServiciosAdmUsuario.svc.cs
    public class HPVServiciosAdmUsuario : IHPVServiciosAdmUsuario
    {
        public OS_ActualizarRol ActualizarRol(OE_ActualizarRol oe)
        {
            return (new FachadaAdmUsuario().ActualizarRol(oe));
        }

        public OS_ActualizarUsuario ActualizarUsuario(OE_ActualizarUsuario oe)
        {
            return (new FachadaAdmUsuario().ActualizarUsuario(oe));
        }

        public OS_AutenticarUsuario AutenticarUsuario(OE_AutenticarUsuario oe)
        {
            return (new FachadaAdmUsuario().AutenticarUsuario(oe));
        }

        public OS_ConsultarRol ConsultarRol(OE_ConsultarRol oe)
        {
            return (new FachadaAdmUsuario().ConsultarRol(oe));
        }

        public OS_ConsultarUsuario ConsultarUsuario(OE_ConsultarUsuario oe)
        {
            return (new FachadaAdmUsuario().ConsultarUsuario(oe));
        }

        public OS_DarCoordinadores DarCoordinadores()
        {
            return (new FachadaAdmUsuario().DarCoordinadores());
        }

        public OS_DarMenus DarMenus()
        {
            return (new FachadaAdmUsuario().DarMenus());
        }

        public OS_DarMunicipios DarMunicipios()
        {
            return (new FachadaAdmUsuario().DarMunicipios());
        }

        public OS_DarOpcionesMenu DarOpcionesMenu(OE_DarOpcionesMenu oe)
        {
            return (new FachadaAdmUsuario().DarOpcionesMenu(oe));
        }

        public OS_DarOpcionesMenuRol DarOpcionesMenuRol(OE_DarOpcionesMenuRol oe)
        {
            return (new FachadaAdmUsuario().DarOpcionesMenuRol(oe));
        }

        public OS_DarRoles DarRoles(OE_DarRoles oe)
        {
            return (new FachadaAdmUsuario().DarRoles(oe));
        }

        public OS_DarUsuarios DarUsuarios(OE_DarUsuarios oe)
        {
            return (new FachadaAdmUsuario().DarUsuarios(oe));
        }

        public OS_RecuperarContrasena RecuperarContrasena(OE_RecuperarContrasena oe)
        {
            return( new FachadaAdmUsuario().RecuperarContrasena(oe));
        }

        public OS_AsignarContrasenaXToken AsignarContrasenaXToken(OE_AsignarContrasenaXToken oe)
        {
            return (new FachadaAdmUsuario().AsignarContrasenaXToken(oe));
        }

    }
}
