using HPV_Entidades.General;
using HPV_Entidades.Sistematizacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.SistematizacionWS
{
    public class OS_DarCategorizacion
    {
        public Respuesta Respuesta { get; set; }
        public List<Subcategoria> ListaCategorizacion { get; set; }

        public OS_DarCategorizacion()
        {
            Respuesta = new Respuesta();
            ListaCategorizacion = new List<Subcategoria>();
        }
    }
}
