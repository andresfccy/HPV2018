using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.FechaCorteWS
{
    public class OS_DarFechaCorte
    {
        public Respuesta Respuesta { get; set; }
        public List<HPV_Entidades.FechaCorte.FechaCorte> ListaFechaCorte { get; set; }

        public OS_DarFechaCorte()
        {
            Respuesta = new Respuesta();
            ListaFechaCorte = new List<HPV_Entidades.FechaCorte.FechaCorte>();
        }
    }
}
