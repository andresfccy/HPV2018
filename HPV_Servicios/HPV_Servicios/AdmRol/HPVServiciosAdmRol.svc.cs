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
    public class HPVServiciosAdmRol : IHPVServiciosAdmRol
    {
        public OS_ActualizarRol ActualizarRol(OE_ActualizarRol oe)
        {
            return (new FachadaAdmRol().ActualizarRol(oe));
        }

        public OS_ConsultarRol ConsultarRol(OE_ConsultarRol oe)
        {
            return (new FachadaAdmRol().ConsultarRol(oe));
        }

        public OS_DarRoles DarRoles()
        {
            return (new FachadaAdmRol().DarRoles());
        }

    }
}
