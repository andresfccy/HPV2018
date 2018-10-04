using HPV_Entidades.SistematizacionWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HPV_Servicios.Sistematizacion
{
    // IHPVServiciosSistematizacion
    [ServiceContract]
    public interface IHPVServiciosSistematizacion
    {
        [OperationContract]
        OS_ActualizarSistematizacion ActualizarSistematizacion(OE_ActualizarSistematizacion oe);

        [OperationContract]
        OS_ConsultarSistematizacion ConsultarSistematizacion(OE_ConsultarSistematizacion oe);

        [OperationContract]
        OS_DarSistematizacion DarSistematizacion(OE_DarSistematizacion oe);

        [OperationContract]
        OS_DarSubcategoriasXInstrumento DarSubcategoriasXInstrumento(OE_DarSubcategoriasXInstrumento oe);

        [OperationContract]
        OS_DarCategoriasXInstrumento DarCategoriasXInstrumento(OE_DarCategoriasXInstrumento oe);

        [OperationContract]
        OS_DarCategorizacion DarCategorizacion(OE_DarCategorizacion oe);

        [OperationContract]
        OS_ActualizarCategorizacion ActualizarCategorizacion(OE_ActualizarCategorizacion oe);
    }
}
