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
    // HPVServicioConsulta
    // El servicio se publica en HPVServicioConsulta.svc or HPVServicioConsulta.svc.cs
    public class HPVServicioConsulta : IHPVServicioConsulta
    {
        public OS_ConsultarHorarios ConsultarHorarios(OE_ConsultarHorarios oe)
        {
            return(new FachadaConsulta().ConsultarHorarios(oe));
        }

        public OS_ConsultarInscrito ConsultarInscrito(OE_ConsultarInscrito oe)
        {
            return (new FachadaConsulta().ConsultarInscrito(oe));
        }

        public OS_ConsultarInscritos ConsultarInscritos(OE_ConsultarInscritos oe)
        {
            return (new FachadaConsulta().ConsultarInscritos(oe));
        }
    }
}
