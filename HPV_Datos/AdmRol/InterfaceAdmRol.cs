using HPV_Entidades.AdmRolWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.AdmRol
{
    interface InterfaceAdmRol
    {
        OS_DarRoles DarRoles();
        OS_ConsultarRol ConsultarRol(OE_ConsultarRol oe);
        OS_ActualizarRol ActualizarRol(OE_ActualizarRol oe);
        OS_DarOpcionesUsuario DarOpcionesUsuario(OE_DarOpcionesUsuario oe);
    }
}
