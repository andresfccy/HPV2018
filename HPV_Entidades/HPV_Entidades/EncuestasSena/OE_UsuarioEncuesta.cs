using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.EncuestasSena
{
    public class OE_UsuarioEncuesta
    {
        public int IdUsusarioEncuestaSena { get; set; }
        public string TipoDocumento { get; set; }
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public int EsJovenEnAccion { get; set; }
        public int IdEncuestaPresentar { get; set; }
        public string Consecutivo { get; set; }
    }
}
