using HPV_Entidades.GeneralWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace HPV_Servicios.General
{
    // IHPVServiciosGen
    [ServiceContract]
    public interface IHPVServiciosGen
    {

        [OperationContract]
        OS_DarListaValor DarListaValor(OE_DarListaValor oe);

        [OperationContract]
        OS_EnviarCorreo EnviarCorreo(OE_EnviarCorreo oe);

        [OperationContract]
        OS_DarParametroInicial DarParametroInicial();

        [OperationContract]
        OS_ValidarToken ValidarToken(OE_ValidarToken oe);

        [OperationContract]
        OS_RegistrarEncuestaSatisfaccion RegistrarEncuestaSatisfaccion(OE_RegistrarEncuestaSatisfaccion oe);


    }
}
