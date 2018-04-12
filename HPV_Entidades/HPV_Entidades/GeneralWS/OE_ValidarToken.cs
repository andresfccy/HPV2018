using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.GeneralWS
{
    public class OE_ValidarToken
    {
        public String Categoria { get; set; } // Una de las siguientes categorias RECUPERARCONTRASENA, ENCUESTASATISFACION, ENCUESTAFINAL
        public String Token { get; set; }
    }
}
