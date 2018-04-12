using HPV_Entidades.AdmParametroWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.AdmParametro
{
    interface InterfaceAdmParametro
    {
        OS_DarParametros DarParametros();
        OS_ConsultarParametro ConsultarParametro(OE_ConsultarParametro oe);
        OS_ActualizarParametro ActualizarParametro(OE_ActualizarParametro oe);

        OS_DarPeriodos DarPeriodos();
        OS_ConsultarPeriodo ConsultarPeriodo(OE_ConsultarPeriodo oe);
        OS_ActualizarPeriodo ActualizarPeriodo(OE_ActualizarPeriodo oe);
    }
}
