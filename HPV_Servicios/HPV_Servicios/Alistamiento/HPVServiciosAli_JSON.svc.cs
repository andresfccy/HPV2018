using HPV_Datos.Alistamiento;
using HPV_Entidades.AlistamientoWS;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HPV_Servicios.Alistamiento
{
    // HPVServiciosAli_JSON
    // El servicio se publica en HPVServiciosAli_JSON.svc or HPVServiciosAli_JSON.svc.cs
    public class HPVServiciosAli_JSON : IHPVServiciosAli_JSON
    {


        public OS_DarPeriodoVigente DarPeriodoVigente()
        {

            return (new FachadaAlistamiento().DarPeriodoVigente());
        }

        public OS_DarPeriodoVigente DarPeriodoVigenteOptions()
        {
            return null;
        }

        public OS_ValidarBeneficiario ValidarBeneficiario(OE_ValidarBeneficiario oe)
        {
            return (new FachadaAlistamiento().ValidarBeneficiario(oe));
        }

        public OS_ValidarBeneficiario ValidarBeneficiarioOptions(OE_ValidarBeneficiario oe)
        {
            return null;
        }

        public OS_ValidarCorreo ValidarCorreo(OE_ValidarCorreo oe)
        {
            return (new FachadaAlistamiento().ValidarCorreo(oe));

        }

        public OS_ValidarCorreo ValidarCorreoOptions(OE_ValidarCorreo oe)
        {
            return null;
        }

        public OS_DarDepartamentos DarDepartamentos()
        {
            return (new FachadaAlistamiento().DarDepartamentos());
        }

        public OS_DarDepartamentos DarDepartamentosOptions()
        {
            return null;
        }

        public OS_DarMunicipiosXDepto DarMunicipiosXDepto(OE_DarMunicipiosXDepto oe)
        {
            return (new FachadaAlistamiento().DarMunicipiosXDepto(oe));
        }

        public OS_DarMunicipiosXDepto DarMunicipiosXDeptoOptions(OE_DarMunicipiosXDepto oe)
        {
            return null;
        }

        public OS_DarDiasDisponibles DarDiasDisponibles(OE_DarDiasDisponibles oe)
        {
            return (new FachadaAlistamiento().DarDiasDisponibles(oe));

        }

        public OS_DarDiasDisponibles DarDiasDisponiblesOptions(OE_DarDiasDisponibles oe)
        {
           return null;
        }

        public OS_DarHorariosDisponibles DarHorariosDisponibles(OE_DarHorariosDisponibles oe)
        {
            return (new FachadaAlistamiento().DarHorariosDisponibles(oe));

        }

        public OS_DarHorariosDisponibles DarHorariosDisponiblesOptions(OE_DarHorariosDisponibles oe)
        {
            return null;
        }

        public OS_DarLugaresDisponibles DarLugaresDisponibles(OE_DarLugaresDisponibles oe)
        {
            return (new FachadaAlistamiento().DarLugaresDisponibles(oe));

        }

        public OS_DarLugaresDisponibles DarLugaresDisponiblesOptions(OE_DarLugaresDisponibles oe)
        {
            return null;
        }

        public OS_GuardarInscripcion GuardarInscripcion(OE_GuardarInscripcion oe)
        {
            return (new FachadaAlistamiento().GuardarInscripcion(oe));
        }

        public OS_GuardarInscripcion GuardarInscripcionOptions(OE_GuardarInscripcion oe)
        {
            return null;
        }

        public OS_DarEncuesta DarEncuesta(OE_DarEncuesta oe)
        {
            return (new FachadaAlistamiento().DarEncuesta(oe));
        }

        public OS_DarEncuesta DarEncuestaOptions(OE_DarEncuesta oe)
        {
            return null;
        }

        public OS_GenerarCertificado GenerarCertificado(OE_GenerarCertificado oe)
        {
            return (new FachadaAlistamiento().GenerarCertificado(oe));
        }

        public OS_GenerarCertificado GenerarCertificadoOptions(OE_GenerarCertificado oe)
        {
            return null;
        }

        public OS_RegistrarEncuestaFinal RegistrarEncuestaFinal(OE_RegistrarEncuestaFinal oe)
        {
            return (new FachadaAlistamiento().RegistrarEncuestaFinal(oe));
        }

        public OS_RegistrarEncuestaFinal RegistrarEncuestaFinalOptions(OE_RegistrarEncuestaFinal oe)
        {
            return null;
        }
    }
}
