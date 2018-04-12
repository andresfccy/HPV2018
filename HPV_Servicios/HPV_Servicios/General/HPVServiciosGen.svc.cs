using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using HPV_Entidades.GeneralWS;
using HPV_Datos.General;
using HPV_Entidades.General;

namespace HPV_Servicios.General
{
    // HPVServiciosGen
    // El servicio se publica en HPVServiciosGen.svc or HPVServiciosGen.svc.cs
    public class ServiciosGen : IHPVServiciosGen
    {

        public OS_DarListaValor DarListaValor(OE_DarListaValor oe)
        {
            return (new FachadaGeneral().DarListaValor(oe));
        }

        public OS_DarParametroInicial DarParametroInicial()
        {
            return (new FachadaGeneral().DarParametroInicial());
        }

        public OS_EnviarCorreo EnviarCorreo(OE_EnviarCorreo oe)
        {
            return (new FachadaGeneral().EnviarCorreo(oe));
        }

        public OS_RegistrarEncuestaSatisfaccion RegistrarEncuestaSatisfaccion(OE_RegistrarEncuestaSatisfaccion oe)
        {
            return (new FachadaGeneral().RegistrarEncuestaSatisfaccion(oe));
        }

        public OS_ValidarToken ValidarToken(OE_ValidarToken oe)
        {
            return (new FachadaGeneral().ValidarToken(oe));
        }
    }
}
