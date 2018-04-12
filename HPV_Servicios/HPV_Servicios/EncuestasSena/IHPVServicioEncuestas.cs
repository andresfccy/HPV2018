using HPV_Entidades.IncripcionEncuestasWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HPV_Servicios.EncuestasSena
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IHPVServicioEncuestas" in both code and config file together.
    [ServiceContract]
    public interface IHPVServicioEncuestas
    {
        [OperationContract]
        OS_ConsultarTiposDocumento ObtenerTiposDocumento();

        [OperationContract]
        OS_CrearDatosBasicos CrearDatosBasicos(OS_CrearDatosBasicos UsuarioSena);

        [OperationContract]
        OS_Consultar_Preguntas ObtenerPreguntas(int IdEncuesta);

        [OperationContract]
        OS_CrearDatosBasicos GuardarRespuestasEncuesta(OS_Guardar_Encuesta respuestas);
    }
}
