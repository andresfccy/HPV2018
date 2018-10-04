using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.Sistematizacion
{
    public class Categoria
    {
        public Int64 Id { get; set; }
        public Int64 IdPeriodo { get; set; }
        public String NomCategoria { get; set; }
        public Int64 IdInstrumento { get; set; }
        public String NomInstrumento { get; set; }
    }
}
