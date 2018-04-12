
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPV_Entidades.General;
using HPV_Entidades.Alistamiento;

namespace HPV_Entidades.AlistamientoWS
{
    public class OS_ValidarBeneficiario
    {

        public Respuesta Respuesta { get; set; }
        public Beneficiario Beneficiario { get; set; }

        public OS_ValidarBeneficiario()
        {
            Respuesta = new Respuesta();
            Beneficiario = new Beneficiario();
        }

    }
}
