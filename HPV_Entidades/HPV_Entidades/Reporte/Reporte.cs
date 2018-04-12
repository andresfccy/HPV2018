using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.Reporte
{
    public class Reporte
    {
        public Int64 IdReporte { get; set; }
        public String Nombre { get; set; }
        public String Descripcion { get; set; }

        public String Categoria { get; set; }

        public String URL { get; set; }
    }
}
