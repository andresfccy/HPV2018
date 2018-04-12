using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPV_Entidades.GeneralWS;

namespace HPV_Datos.General
{
    interface InterfaceGeneral
    {

        OS_DarListaValor DarListaValor(OE_DarListaValor oe);
        OS_EnviarCorreo EnviarCorreo(OE_EnviarCorreo oe);
        OS_DarParametroInicial DarParametroInicial();
        OS_ValidarToken ValidarToken(OE_ValidarToken oe);

        OS_RegistrarEncuestaSatisfaccion RegistrarEncuestaSatisfaccion(OE_RegistrarEncuestaSatisfaccion oe);
    }
}
