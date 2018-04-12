using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using HPV_Entidades.AlistamientoWS;

namespace HPV_Servicios.Alistamiento
{
    [ServiceContract]
    public interface IHPVServiciosAli
    {

        [OperationContract]
        OS_DarPeriodoVigente DarPeriodoVigente();

        [OperationContract]
        OS_ValidarBeneficiario ValidarBeneficiario(OE_ValidarBeneficiario oe);

        [OperationContract]
        OS_ValidarCorreo ValidarCorreo(OE_ValidarCorreo oe);

        [OperationContract]
        OS_DarDepartamentos DarDepartamentos();

        [OperationContract]
        OS_DarMunicipiosXDepto DarMunicipiosXDepto(OE_DarMunicipiosXDepto oe);

        [OperationContract]
        OS_DarDiasDisponibles DarDiasDisponibles(OE_DarDiasDisponibles oe);

        [OperationContract]
        OS_DarHorariosDisponibles DarHorariosDisponibles(OE_DarHorariosDisponibles oe);

        [OperationContract]
        OS_DarLugaresDisponibles DarLugaresDisponibles(OE_DarLugaresDisponibles oe);

        [OperationContract]
        OS_GuardarInscripcion GuardarInscripcion(OE_GuardarInscripcion oe);

        [OperationContract]
        OS_DarEncuesta DarEncuesta(OE_DarEncuesta oe);

        [OperationContract]
        OS_GenerarCertificado GenerarCertificado(OE_GenerarCertificado oe);

        [OperationContract]
        OS_RegistrarEncuestaFinal RegistrarEncuestaFinal(OE_RegistrarEncuestaFinal oe);


    }


}
