using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using HPV_Entidades.ConsultaWS;
using HPV_Datos.Consulta;

namespace HPV_Servicios.Consulta
{
    // HPVServiciosConsulta_JSON
    // El servicio se publica en HPVServiciosConsulta_JSON.svc or HPVServiciosConsulta_JSON.svc.cs
    public class HPVServiciosConsulta_JSON : IHPVServiciosConsulta_JSON
    {
        public OS_ConsultarHorarios ConsultarHorarios(OE_ConsultarHorarios oe)
        {
            return (new FachadaConsulta().ConsultarHorarios(oe));
        }

        public OS_ConsultarHorarios ConsultarHorariosOptions(OE_ConsultarHorarios oe)
        {
            return null;
        }

        public OS_ConsultarInscrito ConsultarInscrito(OE_ConsultarInscrito oe)
        {
            return (new FachadaConsulta().ConsultarInscrito(oe));
        }

        public OS_ConsultarInscrito ConsultarInscritoOptions(OE_ConsultarInscrito oe)
        {
            return null;
        }

        public OS_ConsultarInscritos ConsultarInscritos(OE_ConsultarInscritos oe)
        {
            return (new FachadaConsulta().ConsultarInscritos(oe));
        }

        public OS_ConsultarInscritos ConsultarInscritosOptions(OE_ConsultarInscritos oe)
        {
            return null;
        }

    }
}
