using HPV_EncuestasSena.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HPV_EncuestasSena.Models
{
    public class PreguntasModel
    {

        public int Id { get; set; }
        public string punto { get; set; }
        public int IdEncuesta { get; set; }
        public int NumeroPregunta { get; set; }
        public string Pregunta { get; set; }
        
        public List<OpcionesPregunta> OpcionesPorPregunta { get; set; }

        //public List<SelectListItem> Preguntas { get; set; }
        //List<RespuestaPregunta> Respuestas { get; set; }
    }

    public class OpcionesPregunta
    {
        public int Id { get; set; }
        public int CodPregunta { get; set; }
        public string Nombre { get; set; }
        public string  Letra { get; set; }
        public bool Seleccionado { get; set; }
    }
}