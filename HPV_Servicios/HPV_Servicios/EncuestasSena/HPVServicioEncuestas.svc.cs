using HPV_Datos.EncuestasSena;
using HPV_Entidades.IncripcionEncuestasWS;
using HPV_Servicios.EncuestasSena;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HPV_Servicios.IncripcionEncuestas
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "HPVServicioEncuestas" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select HPVServicioEncuestas.svc or HPVServicioEncuestas.svc.cs at the Solution Explorer and start debugging.
    public class HPVServicioEncuestas : IHPVServicioEncuestas
    {
        public OS_ConsultarTiposDocumento ObtenerTiposDocumento()
        {
            return (new FachadaInscripcion().ConsultarTiposDocumento());
        }

        public OS_CrearDatosBasicos CrearDatosBasicos(OS_CrearDatosBasicos UsuarioSena)
        {
            FachadaInscripcion fachada = new FachadaInscripcion ();

            //Se valida si el usuario ya exsiste
            OS_CrearDatosBasicos usuSena = fachada.ValidarExistenciaUsuario(UsuarioSena);
            if (usuSena.UsuarioEncuestaSena.IdUsusarioEncuestaSena  > 0)
            {
                if (usuSena.UsuarioEncuestaSena.IdUsusarioEncuestaSena > 0)
                {
                    usuSena.UsuarioEncuestaSena.IdEncuestaPresentar = UsuarioSena.UsuarioEncuestaSena.IdEncuestaPresentar;
                    //Si el usuario existe se valida si ya presento la encuesta
                    bool result = fachada.ValidarEncuestaUsuario(usuSena);
                    if (result)
                        usuSena.Respuesta.Mensaje = "encuestaDiligenciada";                    
                }   
                else
                    usuSena = fachada.CrearUsuariosEncuestasSena(UsuarioSena);
            }
            else
                usuSena = fachada.CrearUsuariosEncuestasSena(UsuarioSena);
            usuSena.UsuarioEncuestaSena.IdEncuestaPresentar = UsuarioSena.UsuarioEncuestaSena.IdEncuestaPresentar;
            return usuSena;
        }
        
        public OS_Consultar_Preguntas ObtenerPreguntas(int IdEncuesta)
        {
            return (new FachadaInscripcion().ConsultarPreguntas(IdEncuesta));
        }
        
        public OS_CrearDatosBasicos GuardarRespuestasEncuesta(OS_Guardar_Encuesta respuestas)
        {
            return (new FachadaInscripcion().GuardarRespuestasEncuesta(respuestas));
        }
    }
}
