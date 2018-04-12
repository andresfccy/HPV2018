using HPV_Entidades.HistoriasDeVidaWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HPV_Servicios.HistoriaVida
{
    // IHPVServiciosHistoriaVida
    [ServiceContract]
    public interface IHPVServiciosHistoriaVida
    {
        [OperationContract]
        OS_ActualizarHistoriaVida ActualizarHistoriaVida(OE_ActualizarHistoriaVida oe);

        [OperationContract]
        OS_ConsultarHistoriaVida ConsultarHistoriaVida(OE_ConsultarHistoriaVida oe);

        [OperationContract]
        OS_DarHistoriasDeVida DarHistoriasDeVida(OE_DarHistoriasDeVida oe);

        [OperationContract]
        OS_ValidarCantidadHistorias ValidarCantidadHistorias(OE_ValidarCantidadHistorias oe);

        [OperationContract]
        OS_RegistrarEstadoHistoriaVida RegistrarEstadoHistoriaVida(OE_RegistrarEstadoHistoriaVida oe);
    }
}
