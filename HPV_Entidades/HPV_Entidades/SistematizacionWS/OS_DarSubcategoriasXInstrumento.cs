using HPV_Entidades.General;
using HPV_Entidades.Sistematizacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.SistematizacionWS
{
    public class OS_DarSubcategoriasXInstrumento
    {
        public Respuesta Respuesta { get; set; }
        public List<Subcategoria> ListaSubcategorias { get; set; }

        public OS_DarSubcategoriasXInstrumento()
        {
            Respuesta = new Respuesta();
            ListaSubcategorias = new List<Subcategoria>();
        }
    }
}
