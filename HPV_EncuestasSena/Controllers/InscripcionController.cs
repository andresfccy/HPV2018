using HPV_EncuestasSena.Models;
using HPV_EncuestasSena.ServicioEncuestasSena;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HPV_EncuestasSena.Controllers
{
    public class InscripcionController : Controller
    {
        string codEncuestaEntrada = ConfigurationManager.AppSettings["codencuestaentrada"];
        string codEncuestaSalida = ConfigurationManager.AppSettings["codencuestasalida"];
        string MsjEncuestaEntrada = ConfigurationManager.AppSettings["msjEntrada"];
        string MsjEncuestaSalida = ConfigurationManager.AppSettings["msjSalida"];
        string MsgEncuestaDiligenciada = ConfigurationManager.AppSettings["MensajeEncuesta"];


        public ActionResult DatosBasicos()
        {
            HPVServicioEncuestasClient cliente = new HPVServicioEncuestasClient();
            InscripcionModel usuario = new InscripcionModel();
            string paramEncuesta = Convert.ToString(Request.QueryString["key"]);
            ViewBag.showSuccessAlert = false;
            if (!string.IsNullOrEmpty(paramEncuesta))
            {
                if (paramEncuesta.Trim().Equals("entrada"))
                    usuario.Mensaje = MsjEncuestaEntrada;
                else if(paramEncuesta.Trim().Equals("salida"))
                    usuario.Mensaje = MsjEncuestaSalida;
            }
            var tiposIdentificacion = cliente.ObtenerTiposDocumento().ListaTiposDocumento;
            if (tiposIdentificacion != null)
            {
                usuario.TiposIdentificacion = tiposIdentificacion.Where(t => !t.Nombre.Equals("OTRO") && !t.Nombre.Equals("DNI") && !t.Nombre.Equals("NUIP") && !t.Nombre.Equals("PASAPORTE")).Select(t => new SelectListItem()
                {
                    Value = t.TipoDocumento,
                    Text = t.Nombre
                }).ToList();
            }

            return View("DatosBasicos", usuario);   
        }

        [HttpPost]
        public ActionResult DatosBasicos(InscripcionModel usuario)
        {
            if (ModelState.IsValid)
            {
                OS_CrearDatosBasicos UsuarioSena = ConverInscripcionModelToEntidad(usuario);
                HPVServicioEncuestasClient cliente = new HPVServicioEncuestasClient();
                UsuarioSena = cliente.CrearDatosBasicos(UsuarioSena);
                if (UsuarioSena.Respuesta.Mensaje.Equals("encuestaDiligenciada"))
                {
                    ViewBag.showSuccessAlert = true;
                    ViewBag.Message = MsgEncuestaDiligenciada;
                    cliente = new HPVServicioEncuestasClient();
                    
                    var tiposIdentificacion = cliente.ObtenerTiposDocumento().ListaTiposDocumento;
                    if (tiposIdentificacion != null)
                    {
                        usuario.TiposIdentificacion = tiposIdentificacion.Select(t => new SelectListItem()
                        {
                            Value = t.TipoDocumento,
                            Text = t.Nombre
                        }).ToList();
                    }
                    return View(usuario);
                }
                else
                {
                    TempData["InscripcionModel"] = UsuarioSena;
                    return RedirectToAction("PreguntasEncuesta", "Encuesta");
                }
            }
            else
            {
                HPVServicioEncuestasClient cliente = new HPVServicioEncuestasClient();
                ViewBag.showSuccessAlert = false;
                var tiposIdentificacion = cliente.ObtenerTiposDocumento().ListaTiposDocumento;
                if (tiposIdentificacion != null)
                {
                    usuario.TiposIdentificacion = tiposIdentificacion.Select(t => new SelectListItem()
                    {
                        Value = t.TipoDocumento,
                        Text = t.Nombre
                    }).ToList();
                }
                return View(usuario);
            }
        }

        private OS_CrearDatosBasicos ConverInscripcionModelToEntidad(InscripcionModel usuario)
        {
            OS_CrearDatosBasicos entidad = new OS_CrearDatosBasicos();
            entidad.UsuarioEncuestaSena = new OE_UsuarioEncuesta();
            if (usuario.Mensaje.Equals(MsjEncuestaEntrada))
                entidad.UsuarioEncuestaSena.IdEncuestaPresentar = int.Parse(codEncuestaEntrada);
            else if (usuario.Mensaje.Equals(MsjEncuestaSalida))
                entidad.UsuarioEncuestaSena.IdEncuestaPresentar = int.Parse(codEncuestaSalida);
            entidad.UsuarioEncuestaSena.TipoDocumento = usuario.TipoDocumento;
            entidad.UsuarioEncuestaSena.Documento = usuario.NumeroDocumento.ToString ();
            entidad.UsuarioEncuestaSena.Nombre = usuario.Nombre;
            entidad.UsuarioEncuestaSena.PrimerApellido = usuario.PrimerApellido;
            entidad.UsuarioEncuestaSena.SegundoApellido = usuario.SegundoApellido;
            entidad.UsuarioEncuestaSena.Email = usuario.Email;
            entidad.UsuarioEncuestaSena.Celular = usuario.Celular.ToString ();
            entidad.UsuarioEncuestaSena.EsJovenEnAccion = Convert.ToInt16 (usuario.Esjoven);

            return entidad;
        }
    }
}