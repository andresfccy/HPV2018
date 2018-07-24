using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using HPV_Entidades.SistematizacionWS;
using HPV_Datos.Sistematizacion;

namespace HPV_Servicios.Sistematizacion
{
    // HPVServiciosSistematizacion_JSON
    // El servicio se publica en HPVServiciosSistematizacion_JSON.svc or HPVServiciosSistematizacion_JSON.svc.cs
    public class HPVServiciosSistematizacion_JSON : IHPVServiciosSistematizacion_JSON
    {
        public OS_ActualizarSistematizacion ActualizarSistematizacion(OE_ActualizarSistematizacion oe)
        {
            return (new FachadaSistematizacion().ActualizarSistematizacion(oe));
        }

        public OS_ActualizarSistematizacion ActualizarSistematizacionOptions(OE_ActualizarSistematizacion oe)
        {
            return null;
        }

        public OS_ConsultarSistematizacion ConsultarSistematizacion(OE_ConsultarSistematizacion oe)
        {
            return (new FachadaSistematizacion().ConsultarSistematizacion(oe));
        }

        public OS_ConsultarSistematizacion ConsultarSistematizacionOptions(OE_ConsultarSistematizacion oe)
        {
            return null;
        }

        public OS_DarSistematizacion DarSistematizacion(OE_DarSistematizacion oe)
        {
            return (new FachadaSistematizacion().DarSistematizacion(oe));
        }

        public OS_DarSistematizacion DarSistematizacionOptions(OE_DarSistematizacion oe)
        {
            return null;
        }
    }
}
