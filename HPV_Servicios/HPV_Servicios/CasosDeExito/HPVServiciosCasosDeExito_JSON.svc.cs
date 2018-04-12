using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using HPV_Entidades.CasosDeExitoWS;
using HPV_Datos.CasosDeExito;

namespace HPV_Servicios.CasosDeExito
{
    // HPVServiciosCasosDeExito_JSON
    // El servicio se publica en HPVServiciosCasosDeExito_JSON.svc or HPVServiciosCasosDeExito_JSON.svc.cs
    public class HPVServiciosCasosDeExito_JSON : IHPVServiciosCasosDeExito_JSON
    {
        public OS_ActualizarCasoDeExito ActualizarCasoDeExito(OE_ActualizarCasoDeExito oe)
        {
            return (new FachadaCasoDeExito().ActualizarCasoDeExito(oe));
        }

        public OS_ActualizarCasoDeExito ActualizarCasoDeExitoOptions(OE_ActualizarCasoDeExito oe)
        {
            return null;
        }

        public OS_ConsultarCasoDeExito ConsultarCasoDeExito(OE_ConsultarCasoDeExito oe)
        {
            return (new FachadaCasoDeExito().ConsultarCasoDeExito(oe));

        }

        public OS_ConsultarCasoDeExito ConsultarCasoDeExitoOptions(OE_ConsultarCasoDeExito oe)
        {
            return null;
        }

        public OS_DarCasosDeExito DarCasosDeExito(OE_DarCasosDeExito oe)
        {
            return (new FachadaCasoDeExito().DarCasosDeExito(oe));
        }

        public OS_DarCasosDeExito DarCasosDeExitoOptions(OE_DarCasosDeExito oe)
        {
            return null;
        }

        public OS_DarLogros DarLogros()
        {
            return (new FachadaCasoDeExito().DarLogros());
        }

        public OS_DarLogros DarLogrosOptions()
        {
            return null;
        }

        public OS_RegistrarEstadoCasoDeExito RegistrarEstadoCasoDeExito(OE_RegistrarEstadoCasoDeExito oe)
        {
            return (new FachadaCasoDeExito().RegistrarEstadoCasoDeExito(oe));
        }

        public OS_RegistrarEstadoCasoDeExito RegistrarEstadoCasoDeExitoOptions(OE_RegistrarEstadoCasoDeExito oe)
        {
            return null;
        }

        public OS_ValidarCasoDeExito ValidarCasoDeExito(OE_ValidarCasoDeExito oe)
        {
            return (new FachadaCasoDeExito().ValidarCasoDeExito(oe));
        }

        public OS_ValidarCasoDeExito ValidarCasoDeExitoOptions(OE_ValidarCasoDeExito oe)
        {
            return null;
        }
    }
}
