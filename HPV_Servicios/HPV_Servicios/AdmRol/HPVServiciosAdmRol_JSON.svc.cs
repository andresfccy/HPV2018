using HPV_Datos.AdmRol;
using HPV_Entidades.AdmRolWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HPV_Servicios.AdmRol
{
    public class HPVServiciosAdmRol_JSON : IHPVServiciosAdmRol_JSON
    {
        public OS_ActualizarRol ActualizarRol(OE_ActualizarRol oe)
        {
            return (new FachadaAdmRol().ActualizarRol(oe));
        }

        public OS_ActualizarRol ActualizarRolOptions(OE_ActualizarRol oe)
        {
            return null;
        }

        public OS_ConsultarRol ConsultarRol(OE_ConsultarRol oe)
        {
            return (new FachadaAdmRol().ConsultarRol(oe));
        }

        public OS_ConsultarRol ConsultarRolOptions(OE_ConsultarRol oe)
        {
            return null;
        }

        public OS_DarOpcionesUsuario DarOpcionesUsuario(OE_DarOpcionesUsuario oe)
        {
            return (new FachadaAdmRol().DarOpcionesUsuario(oe));
        }

        public OS_DarOpcionesUsuario DarOpcionesUsuarioOptions(OE_DarOpcionesUsuario oe)
        {
            return null;
        }

        public OS_DarRoles DarRoles()
        {
            return (new FachadaAdmRol().DarRoles());
        }

        public OS_DarRoles DarRolesOptions()
        {
            return null;
        }
    }
}
