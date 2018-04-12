using HPV_Entidades.CasosDeExitoWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.CasosDeExito
{
    interface InterfaceCasoDeExito
    {
        OS_ActualizarCasoDeExito ActualizarCasoDeExito(OE_ActualizarCasoDeExito oe);

        OS_ConsultarCasoDeExito ConsultarCasoDeExito(OE_ConsultarCasoDeExito oe);

        OS_DarCasosDeExito DarCasosDeExito(OE_DarCasosDeExito oe);

        OS_ValidarCasoDeExito ValidarCasoDeExito(OE_ValidarCasoDeExito oe);

        OS_DarLogros DarLogros();

        OS_RegistrarEstadoCasoDeExito RegistrarEstadoCasoDeExito(OE_RegistrarEstadoCasoDeExito oe);
    }
}
