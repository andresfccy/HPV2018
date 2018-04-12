using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using HPV_Entidades.FechaCorteWS;
using HPV_Datos.FechaCorte;

namespace HPV_Servicios.FechaCorte
{
    // HPVServiciosFechaCorte
    // El servicio se publica en HPVServiciosFechaCorte.svc or HPVServiciosFechaCorte.svc.cs
    public class HPVServiciosFechaCorte : IHPVServiciosFechaCorte
    {
        public OS_DarFechaCorte DarFechaCorte(OE_DarFechaCorte oe)
        {
            return (new FachadaFechaCorte().DarFechaCorte(oe));
        }
    }
}
