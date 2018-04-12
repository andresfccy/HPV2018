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
    // HPVServiciosCasosDeExito
    // El servicio se publica en HPVServiciosCasosDeExito.svc or HPVServiciosCasosDeExito.svc.cs
    public class HPVServiciosCasosDeExito : IHPVServiciosCasosDeExito
    {
        public OS_ActualizarCasoDeExito ActualizarCasoDeExito(OE_ActualizarCasoDeExito oe)
        {
            return (new FachadaCasoDeExito().ActualizarCasoDeExito(oe));
        }

        public OS_ConsultarCasoDeExito ConsultarCasoDeExito(OE_ConsultarCasoDeExito oe)
        {
            return (new FachadaCasoDeExito().ConsultarCasoDeExito(oe));
        }

        public OS_DarCasosDeExito DarCasosDeExito(OE_DarCasosDeExito oe)
        {
            return (new FachadaCasoDeExito().DarCasosDeExito(oe));
        }

        public OS_DarLogros DarLogros()
        {
            return (new FachadaCasoDeExito().DarLogros());
        }

        public OS_RegistrarEstadoCasoDeExito RegistrarEstadoCasoDeExito(OE_RegistrarEstadoCasoDeExito oe)
        {
            return (new FachadaCasoDeExito().RegistrarEstadoCasoDeExito(oe));
        }

        public OS_ValidarCasoDeExito ValidarCasoDeExito(OE_ValidarCasoDeExito oe)
        {
            return (new FachadaCasoDeExito().ValidarCasoDeExito(oe));
        }
    }
}
