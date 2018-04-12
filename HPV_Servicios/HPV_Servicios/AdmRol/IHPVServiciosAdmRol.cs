using HPV_Entidades.AdmRolWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HPV_Servicios.AdmRol
{
    // IHPVServiciosAdmRol
    [ServiceContract]
    public interface IHPVServiciosAdmRol
    {
        [OperationContract]
        OS_DarRoles DarRoles();

        [OperationContract]
        OS_ConsultarRol ConsultarRol(OE_ConsultarRol oe);

        [OperationContract]
        OS_ActualizarRol ActualizarRol(OE_ActualizarRol oe);
    }
}
