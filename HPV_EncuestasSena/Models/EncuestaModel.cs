using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HPV_EncuestasSena.Models
{
    public class EncuestaModel
    {
        public string NombreEncuesta { get; set; }
        public string Nombre { get; set; }
        public IEnumerable<PreguntasModel> Preguntas { get; set; }
        
    }
}