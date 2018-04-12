using HPV_Entidades.AdmEspacioFisicoWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.AdmEspacioFisico
{
    interface InterfaceAdmEspacioFisico
    {
        OS_DarEspacioFisico DarEspacioFisico(OE_DarEspacioFisico oe);
        OS_DarDias DarDias();
        OS_DarHorarios DarHorarios();
        OS_DarGrupos DarGrupos();
        OS_DarLugaresMunicipio DarLugaresMunicipio(OE_DarLugaresMunicipio oe);
        OS_DarDireccionesLugar DarDireccionesLugar(OE_DarDireccionesLugar oe);
        OS_ActualizarEspacioFisico ActualizarEspacioFisico(OE_ActualizarEspacioFisico oe);
        OS_ConsultarEspacioFisico ConsultarEspacioFisico(OE_ConsultarEspacioFisico oe);
        OS_DarEspacioFisicoXAprobar DarEspacioFisicoXAprobar(OE_DarEspacioFisicoXAprobar oe);
        OS_AprobarEspacioFisico AprobarEspacioFisico(OE_AprobarEspacioFisico oe);
        OS_RechazarEspacioFisico RechazarEspacioFisico(OE_RechazarEspacioFisico oe);
        OS_DarDeptosFacilitador DarDeptosFacilitador(OE_DarDeptosFacilitador oe);
        OS_DarMunicipiosFacilitador DarMunicipiosFacilitador(OE_DarMunicipiosFacilitador oe);
        OS_ReasignarEspacio ReasignarEspacio(OE_ReasignarEspacio oe);

    }
}
