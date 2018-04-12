using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using HPV_Entidades.HistoriasDeVidaWS;
using HPV_Datos.HistoriaVida;

namespace HPV_Servicios.HistoriaVida
{
    // HPVServiciosHistoriaVida
    // El servicio se publica en HPVServiciosHistoriaVida.svc or HPVServiciosHistoriaVida.svc.cs
    public class HPVServiciosHistoriaVida : IHPVServiciosHistoriaVida
    {
        public OS_ActualizarHistoriaVida ActualizarHistoriaVida(OE_ActualizarHistoriaVida oe)
        {
            return (new FachadaHistoriaVida().ActualizarHistoriaVida(oe));
        }

        public OS_ConsultarHistoriaVida ConsultarHistoriaVida(OE_ConsultarHistoriaVida oe)
        {
            return (new FachadaHistoriaVida().ConsultarHistoriaVida(oe));
        }

        public OS_DarHistoriasDeVida DarHistoriasDeVida(OE_DarHistoriasDeVida oe)
        {
            return (new FachadaHistoriaVida().DarHistoriasDeVida(oe));
        }

        public OS_RegistrarEstadoHistoriaVida RegistrarEstadoHistoriaVida(OE_RegistrarEstadoHistoriaVida oe)
        {
            return (new FachadaHistoriaVida().RegistrarEstadoHistoriaVida(oe));
        }

        public OS_ValidarCantidadHistorias ValidarCantidadHistorias(OE_ValidarCantidadHistorias oe)
        {
            return (new FachadaHistoriaVida().ValidarCantidadHistorias(oe));
        }
    }
}
