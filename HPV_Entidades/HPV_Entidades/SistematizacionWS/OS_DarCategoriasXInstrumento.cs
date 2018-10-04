using HPV_Entidades.General;
using HPV_Entidades.Sistematizacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.SistematizacionWS
{
    public class OS_DarCategoriasXInstrumento
    {
        public Respuesta Respuesta { get; set; }
        public List<Categoria> ListaCategorias { get; set; }

        public OS_DarCategoriasXInstrumento()
        {
            Respuesta = new Respuesta();
            ListaCategorias = new List<Categoria>();
        }
    }
}
