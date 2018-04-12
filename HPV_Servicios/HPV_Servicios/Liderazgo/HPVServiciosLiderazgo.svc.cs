using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using HPV_Entidades.LiderazgoWS;
using HPV_Datos.Liderazgos;

namespace HPV_Servicios.Liderazgo
{
    // HPVServiciosLiderazgo
    // El servicio se publica en HPVServiciosLiderazgo.svc or HPVServiciosLiderazgo.svc.cs
    public class HPVServiciosLiderazgo : IHPVServiciosLiderazgo
    {
        public OS_ActualizarLiderazgo ActualizarLiderazgo(OE_ActualizarLiderazgo oe)
        {
            return (new FachadaLiderazgo().ActualizarLiderazgo(oe));
        }

        public OS_ConsultarLiderazgo ConsultarLiderazgo(OE_ConsultarLiderazgo oe)
        {
            return (new FachadaLiderazgo().ConsultarLiderazgo(oe));
        }

        public OS_DarLiderazgos DarLiderazgos(OE_DarLiderazgos oe)
        {
            return (new FachadaLiderazgo().DarLiderazgos(oe));
        }

        public OS_RegistrarEstadoLiderazgo RegistrarEstadoLiderazgo(OE_RegistrarEstadoLiderazgo oe)
        {
            return (new FachadaLiderazgo().RegistrarEstadoLiderazgo(oe));
        }

        public OS_ValidarCantidadLiderazgo ValidarCantidadLiderazgo(OE_ValidarCantidadLiderazgo oe)
        {
            return (new FachadaLiderazgo().ValidarCantidadLiderazgo(oe));
        }
    }
}
