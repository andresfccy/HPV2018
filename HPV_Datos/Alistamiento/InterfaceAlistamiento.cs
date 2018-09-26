using HPV_Entidades.AlistamientoWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.Alistamiento
{
    interface InterfaceAlistamiento
    {
        OS_DarPeriodoVigente DarPeriodoVigente();
        OS_ValidarBeneficiario ValidarBeneficiario(OE_ValidarBeneficiario oe);
        OS_ValidarCorreo ValidarCorreo(OE_ValidarCorreo oe);
        OS_DarDepartamentos DarDepartamentos();
        OS_DarMunicipiosXDepto DarMunicipiosXDepto(OE_DarMunicipiosXDepto oe);
        OS_DarDiasDisponibles DarDiasDisponibles(OE_DarDiasDisponibles oe);
        OS_DarHorariosDisponibles DarHorariosDisponibles(OE_DarHorariosDisponibles oe);
        OS_DarLugaresDisponibles DarLugaresDisponibles(OE_DarLugaresDisponibles oe);
        OS_GuardarInscripcion GuardarInscripcion(OE_GuardarInscripcion oe);
        OS_DarEncuesta DarEncuesta(OE_DarEncuesta oe);
        OS_GenerarCertificado GenerarCertificado(OE_GenerarCertificado oe);
        OS_RegistrarEncuestaFinal RegistrarEncuestaFinal(OE_RegistrarEncuestaFinal oe);
        OS_DarParametrosCertificado DarParametrosCertificado(OE_DarParametrosCertificado oe);
    }
}
