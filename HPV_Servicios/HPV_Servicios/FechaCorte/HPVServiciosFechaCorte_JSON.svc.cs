using HPV_Datos.FechaCorte;
using HPV_Entidades.FechaCorteWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HPV_Servicios.FechaCorte
{
    // HPVServiciosFechaCorte_JSON
    // El servicio se publica en HPVServiciosFechaCorte_JSON.svc or HPVServiciosFechaCorte_JSON.svc.cs
    public class HPVServiciosFechaCorte_JSON : IHPVServiciosFechaCorte_JSON
    {
        public OS_DarFechaCorte DarFechaCorteOptions(OE_DarFechaCorte OE_DarFechaCorte)
        {
            return null;
        }

        public OS_DarFechaCorte DarFechaCorte(OE_DarFechaCorte OE_DarFechaCorte)
        {
            return (new FachadaFechaCorte().DarFechaCorte(OE_DarFechaCorte));
        }
    }
}
