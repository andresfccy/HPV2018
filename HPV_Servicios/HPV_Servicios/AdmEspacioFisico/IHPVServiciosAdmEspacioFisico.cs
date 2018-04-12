using HPV_Entidades.AdmEspacioFisicoWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HPV_Servicios.AdmEspacioFisico
{
    [ServiceContract]
    public interface IHPVServiciosAdmEspacioFisico
    {
        [OperationContract]
        OS_DarEspacioFisico DarEspacioFisico(OE_DarEspacioFisico oe);

        [OperationContract]
        OS_DarDias DarDias();

        [OperationContract]
        OS_DarHorarios DarHorarios();

        [OperationContract]
        OS_DarGrupos DarGrupos();

        [OperationContract]
        OS_DarLugaresMunicipio DarLugaresMunicipio(OE_DarLugaresMunicipio oe);

        [OperationContract]
        OS_DarDireccionesLugar DarDireccionesLugar(OE_DarDireccionesLugar oe);

        [OperationContract]
        OS_ActualizarEspacioFisico ActualizarEspacioFisico(OE_ActualizarEspacioFisico oe);

        [OperationContract]
        OS_ConsultarEspacioFisico ConsultarEspacioFisico(OE_ConsultarEspacioFisico oe);

        [OperationContract]
        OS_DarEspacioFisicoXAprobar DarEspacioFisicoXAprobar(OE_DarEspacioFisicoXAprobar oe);

        [OperationContract]
        OS_AprobarEspacioFisico AprobarEspacioFisico(OE_AprobarEspacioFisico oe);

        [OperationContract]
        OS_RechazarEspacioFisico RechazarEspacioFisico(OE_RechazarEspacioFisico oe);

        [OperationContract]
        OS_DarDeptosFacilitador DarDeptosFacilitador(OE_DarDeptosFacilitador oe);

        [OperationContract]
        OS_DarMunicipiosFacilitador DarMunicipiosFacilitador(OE_DarMunicipiosFacilitador oe);

        [OperationContract]
        OS_ReasignarEspacio ReasignarEspacio(OE_ReasignarEspacio oe);

    }
}
