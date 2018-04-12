using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.GeneralWS
{
    public class OS_ValidarToken
    {
        public Respuesta Respuesta { get; set; } // En el codigo de respuesta se tiene 0: Token correcto, 1: Token vencido, 2: Token ya utilizado 3:Token no existe
        
        public OS_ValidarToken()
        {
            Respuesta = new Respuesta();
        
        }
    }
}
