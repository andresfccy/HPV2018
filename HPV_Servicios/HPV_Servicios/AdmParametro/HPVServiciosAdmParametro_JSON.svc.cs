using HPV_Datos.AdmParametro;
using HPV_Entidades.AdmParametroWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HPV_Servicios.AdmParametro
{
    public class HPVServiciosAdmParametro_JSON : IHPVServiciosAdmParametro_JSON
    {
        public OS_ActualizarParametro ActualizarParametro(OE_ActualizarParametro oe)
        {
            return (new FachadaAdmParametro().ActualizarParametro(oe));
        }

        public OS_ActualizarParametro ActualizarParametroOptions(OE_ActualizarParametro oe)
        {
            return null;
        }

        public OS_ConsultarParametro ConsultarParametro(OE_ConsultarParametro oe)
        {
            return (new FachadaAdmParametro().ConsultarParametro(oe));
        }

        public OS_ConsultarParametro ConsultarParametroOptions(OE_ConsultarParametro oe)
        {
            return null;
        }

        public OS_DarParametros DarParametros()
        {
            return (new FachadaAdmParametro().DarParametros());
        }

        public OS_DarParametros DarParametrosOptions()
        {
            return null;
        }

        public OS_ActualizarPeriodo ActualizarPeriodo(OE_ActualizarPeriodo oe)
        {
            return (new FachadaAdmParametro().ActualizarPeriodo(oe));
        }

        public OS_ActualizarPeriodo ActualizarPeriodoOptions(OE_ActualizarPeriodo oe)
        {
            return null;
        }

        public OS_ConsultarPeriodo ConsultarPeriodo(OE_ConsultarPeriodo oe)
        {
            return (new FachadaAdmParametro().ConsultarPeriodo(oe));
        }

        public OS_ConsultarPeriodo ConsultarPeriodoOptions(OE_ConsultarPeriodo oe)
        {
            return null;
        }

        public OS_DarPeriodos DarPeriodos()
        {
            return (new FachadaAdmParametro().DarPeriodos());
        }

        public OS_DarPeriodos DarPeriodosOptions()
        {
            return null;
        }

    }
}
