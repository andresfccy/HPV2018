using HPV_Entidades.SistematizacionWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.Sistematizacion
{
    interface InterfaceSistematizacion
    {
        OS_ActualizarSistematizacion ActualizarSistematizacion(OE_ActualizarSistematizacion oe);

        OS_ConsultarSistematizacion ConsultarSistematizacion(OE_ConsultarSistematizacion oe);

        OS_DarSistematizacion DarSistematizacion(OE_DarSistematizacion oe);

        OS_DarCategoriasXInstrumento DarCategoriasXInstrumento(OE_DarCategoriasXInstrumento oe);

        OS_DarSubcategoriasXInstrumento DarSubcategoriasXInstrumento(OE_DarSubcategoriasXInstrumento oe);

        OS_DarCategorizacion DarCategorizacion(OE_DarCategorizacion oe);

        OS_ActualizarCategorizacion ActualizarCategorizacion(OE_ActualizarCategorizacion oe);
    }
}
