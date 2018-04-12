using HPV_Entidades.ConsultaWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.Consulta
{
    interface InterfaceConsulta
    {

        OS_ConsultarInscritos ConsultarInscritos(OE_ConsultarInscritos oe);
        OS_ConsultarInscrito ConsultarInscrito(OE_ConsultarInscrito oe);
        OS_ConsultarHorarios ConsultarHorarios(OE_ConsultarHorarios oe);
    }
}
