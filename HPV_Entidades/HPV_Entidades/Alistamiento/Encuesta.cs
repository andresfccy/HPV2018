using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.Alistamiento
{
    public class Encuesta
    {
        public Int64 Codigo { get; set; }
        public String Descripcion { get; set; }

        public Int64 CodigoPregunta { get; set; }
        public Int64 NumeroPregunta { get; set; }
        public String DescripcionPregunta { get; set; }
        public String TipoPregunta { get; set; }

        public Int64 CodigoRespuesta { get; set; }
        public String LetraPregunta { get; set; }
        public String DescripcionRespuesta { get; set; }

        public String Correcta { get; set; }
    }
}
