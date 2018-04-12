using HPV_Entidades.IncripcionEncuestasWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.EncuestasSena
{
    interface InterfaceInscripcion
    {
        OS_ConsultarTiposDocumento ConsultarTiposDocumento();
        OS_CrearDatosBasicos ValidarExistenciaUsuario(OS_CrearDatosBasicos UsuarioSena);
        bool ValidarEncuestaUsuario(OS_CrearDatosBasicos UsuarioSena);
        OS_CrearDatosBasicos CrearUsuariosEncuestasSena(OS_CrearDatosBasicos UsuarioSena);
        OS_Consultar_Preguntas ConsultarPreguntas(int IdEncuesta);
        OS_CrearDatosBasicos GuardarRespuestasEncuesta(OS_Guardar_Encuesta repuestasEncuesta);

    }
}
