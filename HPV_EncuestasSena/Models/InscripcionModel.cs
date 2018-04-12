using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HPV_EncuestasSena.Models
{
    public class InscripcionModel
    {
        [Required(ErrorMessage = "*Falta diligenciar campo obligatorio")]
        public string TipoDocumento { get; set; }
        [Required(ErrorMessage = "*Falta diligenciar campo obligatorio")]
        [MaxLength(11, ErrorMessage = "El numero de documento debe tener maximo 11 digitos")]
        public string NumeroDocumento { get; set; }
        [Required (ErrorMessage="*Falta diligenciar campo obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "*Falta diligenciar campo obligatorio")]
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        [Required(ErrorMessage = "*Falta diligenciar campo obligatorio")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "La estructura del correo electrónico no es válida")]        
        public string Email { get; set; }
        [Required(ErrorMessage = "*Falta diligenciar campo obligatorio")]
        [MinLength (10, ErrorMessage = "El numero de telefono debe tener 10 digitos")]
        [MaxLength(10, ErrorMessage = "El numero de telefono debe tener 10 digitos")]
        public string Celular { get; set; }
        [Required(ErrorMessage = "*Falta diligenciar campo obligatorio")]
        public bool Esjoven { get; set; }
        public List<SelectListItem> TiposIdentificacion { get; set; }
        public string Mensaje { get; set; }
        public string NombreEncuesta { get; set; }
    }
}