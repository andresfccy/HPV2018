using HPV_Datos.General;
using HPV_Entidades.General;
using HPV_Entidades.GeneralWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;

namespace HPV_Servicios.General
{
    // HPVServiciosGen_JSON
    // El servicio se publica en HPVServiciosGen_JSON.svc or HPVServiciosGen_JSON.svc.cs

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class HPVServiciosGen_JSON : IHPVServiciosGen_JSON
    {
        public OS_DarListaValor DarListaValorOptions(OE_DarListaValor oe)
        {
            return null;
        }

        public OS_DarListaValor DarListaValor(OE_DarListaValor oe)
        {
            return (new FachadaGeneral().DarListaValor(oe));
        }

       
        public OS_EnviarCorreo EnviarCorreo(OE_EnviarCorreo oe)
        {
            return (new FachadaGeneral().EnviarCorreo(oe));
        }

        public OS_EnviarCorreo EnviarCorreoOptions(OE_EnviarCorreo oe)
        {
            return null;
        }

        public OS_DarParametroInicial DarParametroInicial()
        {
            return (new FachadaGeneral().DarParametroInicial());
        }

        public OS_DarParametroInicial DarParametroInicialOptions()
        {
            return null;
        }

        public OS_ValidarToken ValidarToken(OE_ValidarToken oe)
        {
            return (new FachadaGeneral().ValidarToken(oe));
        }

        public OS_ValidarToken ValidarTokenOptions(OE_ValidarToken oe)
        {
            return null;
        }

        public OS_RegistrarEncuestaSatisfaccion RegistrarEncuestaSatisfaccion(OE_RegistrarEncuestaSatisfaccion oe)
        {
            return (new FachadaGeneral().RegistrarEncuestaSatisfaccion(oe));
        }

        public OS_RegistrarEncuestaSatisfaccion RegistrarEncuestaSatisfaccionOptions(OE_RegistrarEncuestaSatisfaccion oe)
        {
            return null;
        }
    }
}
