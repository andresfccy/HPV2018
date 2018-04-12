using HPV_Entidades.ConsultaWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HPV_Servicios.Consulta
{
    // IHPVServicioConsulta
    [ServiceContract]
    public interface IHPVServicioConsulta
    {
        [OperationContract]
        OS_ConsultarInscritos ConsultarInscritos(OE_ConsultarInscritos oe);

        [OperationContract]
        OS_ConsultarInscrito ConsultarInscrito(OE_ConsultarInscrito oe);

        [OperationContract]
        OS_ConsultarHorarios ConsultarHorarios(OE_ConsultarHorarios oe);

    }
}
