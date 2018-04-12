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
    // HPVServiciosHistoriaVida_JSON
    // El servicio se publica en HPVServiciosHistoriaVida_JSON.svc or HPVServiciosHistoriaVida_JSON.svc.cs
    public class HPVServiciosHistoriaVida_JSON : IHPVServiciosHistoriaVida_JSON
    {
        public OS_ActualizarHistoriaVida ActualizarHistoriaVida(OE_ActualizarHistoriaVida oe)
        {
            return (new FachadaHistoriaVida().ActualizarHistoriaVida(oe));
        }

        public OS_ActualizarHistoriaVida ActualizarHistoriaVidaOptions(OE_ActualizarHistoriaVida oe)
        {
            return null;
        }
        
        public OS_ConsultarHistoriaVida ConsultarHistoriaVida(OE_ConsultarHistoriaVida oe)
        {
            return (new FachadaHistoriaVida().ConsultarHistoriaVida(oe));
        }

        public OS_ConsultarHistoriaVida ConsultarHistoriaVidaOptions(OE_ConsultarHistoriaVida oe)
        {
            return null;
        }

        public OS_DarHistoriasDeVida DarHistoriasDeVida(OE_DarHistoriasDeVida oe)
        {
            return (new FachadaHistoriaVida().DarHistoriasDeVida(oe));
        }

        public OS_DarHistoriasDeVida DarHistoriasDeVidaOptions(OE_DarHistoriasDeVida oe)
        {
            return null;
        }

        public OS_RegistrarEstadoHistoriaVida RegistrarEstadoHistoriaVida(OE_RegistrarEstadoHistoriaVida oe)
        {
            return (new FachadaHistoriaVida().RegistrarEstadoHistoriaVida(oe));
        }

        public OS_RegistrarEstadoHistoriaVida RegistrarEstadoHistoriaVidaOptions(OE_RegistrarEstadoHistoriaVida oe)
        {
            return null;
        }

        public OS_ValidarCantidadHistorias ValidarCantidadHistorias(OE_ValidarCantidadHistorias oe)
        {
            return (new FachadaHistoriaVida().ValidarCantidadHistorias(oe));
        }

        public OS_ValidarCantidadHistorias ValidarCantidadHistoriasOptions(OE_ValidarCantidadHistorias oe)
        {
            return null;
        }
    }
}
