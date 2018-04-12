using HPV_Entidades.Consulta;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.ConsultaWS
{
    public class OS_ConsultarHorarios
    {
        public Respuesta Respuesta { get; set; }
        public List<HorarioDisponible> ListaHorario { get; set; }

        public OS_ConsultarHorarios()
        {
            Respuesta = new Respuesta();
            ListaHorario = new List<HorarioDisponible>();
        }
    }
}
