using HPV_Entidades.HistoriasDeVidaWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.HistoriaVida
{
    interface InterfaceHistoriaVida
    {
        OS_ActualizarHistoriaVida ActualizarHistoriaVida(OE_ActualizarHistoriaVida oe);

        OS_ConsultarHistoriaVida ConsultarHistoriaVida(OE_ConsultarHistoriaVida oe);

        OS_DarHistoriasDeVida DarHistoriasDeVida(OE_DarHistoriasDeVida oe);

        OS_ValidarCantidadHistorias ValidarCantidadHistorias(OE_ValidarCantidadHistorias oe);

        OS_RegistrarEstadoHistoriaVida RegistrarEstadoHistoriaVida(OE_RegistrarEstadoHistoriaVida oe);
    }
}
