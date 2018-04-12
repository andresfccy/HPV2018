using HPV_Entidades.General;
using HPV_Entidades.Reporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.ReporteWS
{
    public class OS_ConsultarAuditoria
    {
        public Respuesta Respuesta { get; set; }
        public List<Auditoria> ListaAuditorias { get; set; }

        public OS_ConsultarAuditoria()
        {
            Respuesta = new Respuesta();
            ListaAuditorias = new List<Auditoria>();
        }
    }
}
