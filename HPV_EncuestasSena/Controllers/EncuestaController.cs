using HPV_EncuestasSena.Models;
using HPV_EncuestasSena.ServicioEncuestasSena;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HPV_EncuestasSena.Controllers
{
    public class EncuestaController : Controller
    {
        string codEncuestaEntrada = ConfigurationManager.AppSettings["codencuestaentrada"];
        string codEncuestaSalida = ConfigurationManager.AppSettings["codencuestasalida"];
        string MsjEncuestaEntrada = ConfigurationManager.AppSettings["msjEntrada"];
        string MsjEncuestaSalida = ConfigurationManager.AppSettings["msjSalida"];
        string cuestionarioEntrada = ConfigurationManager.AppSettings["actividad1"];
        string cuestionarioSalida = ConfigurationManager.AppSettings["actividad4"];
        string CrearLog = ConfigurationManager.AppSettings["CrearLog"];

        public ActionResult PreguntasEncuesta()
        {
            OS_CrearDatosBasicos datosUsuario = new OS_CrearDatosBasicos();
            int IdEncuesta = 0;
            if (TempData["InscripcionModel"] != null)
            {
                datosUsuario = TempData["InscripcionModel"] as OS_CrearDatosBasicos;
                Session["idUsuarioSena"] = datosUsuario.UsuarioEncuestaSena.IdUsusarioEncuestaSena;
                Session["idEncuesta"] = datosUsuario.UsuarioEncuestaSena.IdEncuestaPresentar;
                Session["idEncuesta"] = datosUsuario.UsuarioEncuestaSena.IdEncuestaPresentar;
                if (Session["idEncuesta"].ToString().Equals(codEncuestaEntrada))
                    Session["nombreEncuesta"] = MsjEncuestaEntrada;
                else
                    Session["nombreEncuesta"] = MsjEncuestaSalida;
            }
            EncuestaModel em = new EncuestaModel();
            HPVServicioEncuestasClient cliente = new HPVServicioEncuestasClient();
            IdEncuesta = int.Parse(Session["idEncuesta"].ToString());
            
            var Lstpreguntas = cliente.ObtenerPreguntas(IdEncuesta);
            if (Lstpreguntas != null)
            {
                em.Preguntas = ConvertEtidadToPreguntasModel(Lstpreguntas.Preguntas);
                em.Nombre = Session["nombreEncuesta"].ToString();
                if (em.Nombre.Equals(MsjEncuestaEntrada))
                    em.NombreEncuesta = cuestionarioEntrada;
                else if (em.Nombre.Equals(MsjEncuestaSalida))
                    em.NombreEncuesta = cuestionarioSalida;
            }
                                   

            return View(em);
        }

        
        private IEnumerable<PreguntasModel> ConvertEtidadToPreguntasModel(OE_Pregunta[] oE_Preguntas)
        {
            var preguntasModel = oE_Preguntas.Select(p => new PreguntasModel()
            {
                Id = p.IdPregunta,
                IdEncuesta = p.IdEncuesta,
                punto = ".",
                NumeroPregunta = p.NumeroPregunta,
                Pregunta = p.Pregunta,
                OpcionesPorPregunta = p.Respuestas.Select(op => new OpcionesPregunta()
                {
                    Id = op.IdRespuesta,
                    Nombre = op.Respuesta,
                    CodPregunta = op.IdPregunta,
                    Letra = op.Letra,
                    Seleccionado = op.Seleccionado
                }).ToList()
            }).ToList();

            return preguntasModel;
        }

        [HttpPost]
        public ActionResult PreguntasEncuesta(List<OpcionesPregunta> respuestas)
        {
            if (respuestas != null)
            {
                OS_Guardar_Encuesta encuesta = new OS_Guardar_Encuesta();
                string idUsuarioSena = string.Empty;
                string idEncuesta = string.Empty;
                if (Session["idUsuarioSena"] != null)
                {
                    idUsuarioSena = Session["idUsuarioSena"].ToString();
                }
                else
                    idUsuarioSena = "0";
                if (Session["idUsuarioSena"] != null)
                {
                    idEncuesta = Session["idEncuesta"].ToString();
                }
                else
                    idEncuesta = "0";
                
                if (!string.IsNullOrEmpty(idUsuarioSena) && !string.IsNullOrEmpty(idEncuesta))
                    encuesta.RespuestasEncuesta = CovertModelToEntity(idUsuarioSena, idEncuesta, respuestas);

                HPVServicioEncuestasClient cliente = new HPVServicioEncuestasClient();
                OS_CrearDatosBasicos UsuarioSena = cliente.GuardarRespuestasEncuesta(encuesta);
                UsuarioSena.UsuarioEncuestaSena.IdEncuestaPresentar = int.Parse(idEncuesta);

                TempData["EncuestaModel"] = UsuarioSena;
                if (UsuarioSena.UsuarioEncuestaSena.IdUsusarioEncuestaSena > 0)
                    return Json(new { url = "../Certificado/GenerarCertificado", Mensaje = "OK" });
                else
                    if (idEncuesta.Equals("14"))
                        return Json(new { url = "../Inscripcion/DatosBasicos?key=entrada", Mensaje = "ERROR" });
                    else
                        return Json(new { url = "../Inscripcion/DatosBasicos?key=salida", Mensaje = "ERROR" });
            }
            else
            {
                EncuestaModel em = new EncuestaModel();
                HPVServicioEncuestasClient cliente = new HPVServicioEncuestasClient();
                var Lstpreguntas = cliente.ObtenerPreguntas(14);
                if (Lstpreguntas != null)
                {
                    em.Preguntas = ConvertEtidadToPreguntasModel(Lstpreguntas.Preguntas);
                }
                return View(em);
            }
        }

        private OE_RespuestasEncuesta[] CovertModelToEntity(string idUsuarioSena, string idEncuesta, List<OpcionesPregunta> respuestas)
        {
            ICollection<OE_RespuestasEncuesta> Lista = new List<OE_RespuestasEncuesta>();
            foreach (var item in respuestas)
            {
                Lista.Add(
                    new OE_RespuestasEncuesta
                    {
                        CodUsuarioSena = Convert.ToInt32(idUsuarioSena),
                        CodEncuesta = int.Parse(idEncuesta),
                        CodPregunta = item.CodPregunta,
                        CodRespuesta = item.Id
                    });
            }
            return Lista.ToArray();

        }

        public ActionResult PruebaCertificado()
        {
            return RedirectToAction("GenerarCertificado", "Certificado");
        }

        
    }

}