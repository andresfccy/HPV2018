using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using HPV_Entidades.AdmEspacioFisicoWS;
using HPV_Datos.AdmEspacioFisico;

namespace HPV_Servicios.AdmEspacioFisico
{
    public class HPVServiciosAdmEspacioFisico : IHPVServiciosAdmEspacioFisico
    {
        public OS_ActualizarEspacioFisico ActualizarEspacioFisico(OE_ActualizarEspacioFisico oe)
        {
            return (new FachadaAdmEspacioFisico().ActualizarEspacioFisico(oe));
        }

        public OS_AprobarEspacioFisico AprobarEspacioFisico(OE_AprobarEspacioFisico oe)
        {
            return (new FachadaAdmEspacioFisico().AprobarEspacioFisico(oe));
        }

        public OS_ConsultarEspacioFisico ConsultarEspacioFisico(OE_ConsultarEspacioFisico oe)
        {
            return (new FachadaAdmEspacioFisico().ConsultarEspacioFisico(oe));
        }

        public OS_DarDeptosFacilitador DarDeptosFacilitador(OE_DarDeptosFacilitador oe)
        {
            return (new FachadaAdmEspacioFisico().DarDeptosFacilitador(oe));
        }

        public OS_DarDias DarDias()
        {
            return (new FachadaAdmEspacioFisico().DarDias());
        }

        public OS_DarDireccionesLugar DarDireccionesLugar(OE_DarDireccionesLugar oe)
        {
            return (new FachadaAdmEspacioFisico().DarDireccionesLugar(oe));
        }

        public OS_DarEspacioFisico DarEspacioFisico(OE_DarEspacioFisico oe)
        {
            return (new FachadaAdmEspacioFisico().DarEspacioFisico(oe));
        }

        public OS_DarEspacioFisicoXAprobar DarEspacioFisicoXAprobar(OE_DarEspacioFisicoXAprobar oe)
        {
            return (new FachadaAdmEspacioFisico().DarEspacioFisicoXAprobar(oe));
        }

        public OS_DarGrupos DarGrupos()
        {
            return (new FachadaAdmEspacioFisico().DarGrupos());
        }

        public OS_DarHorarios DarHorarios()
        {
            return (new FachadaAdmEspacioFisico().DarHorarios());
        }

        public OS_DarLugaresMunicipio DarLugaresMunicipio(OE_DarLugaresMunicipio oe)
        {
            return (new FachadaAdmEspacioFisico().DarLugaresMunicipio(oe));
        }

        public OS_DarMunicipiosFacilitador DarMunicipiosFacilitador(OE_DarMunicipiosFacilitador oe)
        {
            return (new FachadaAdmEspacioFisico().DarMunicipiosFacilitador(oe));
        }

        public OS_ReasignarEspacio ReasignarEspacio(OE_ReasignarEspacio oe)
        {
            return (new FachadaAdmEspacioFisico().ReasignarEspacio(oe));
        }

        public OS_RechazarEspacioFisico RechazarEspacioFisico(OE_RechazarEspacioFisico oe)
        {
            return (new FachadaAdmEspacioFisico().RechazarEspacioFisico(oe));
        }
    }
}
