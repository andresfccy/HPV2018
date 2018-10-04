using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.Sistematizacion
{
    public class Subcategoria
    {
        public Int64 Id { get; set; }
        public String NomSubcategoria { get; set; }
        public Int64 IdCategoria { get; set; }
        public String NomCategoria { get; set; }
        public String DescOtra { get; set; }
    }
}
