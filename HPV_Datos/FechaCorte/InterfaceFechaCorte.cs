using HPV_Entidades.FechaCorteWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.FechaCorte
{
    interface InterfaceFechaCorte
    {
        OS_DarFechaCorte DarFechaCorte(OE_DarFechaCorte oe);
    }
}
