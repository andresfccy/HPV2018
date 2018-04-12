using HPV_Datos.General.Entidad;
using HPV_Entidades.AdmEspacioFisico;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.AdmEspacioFisico.Entidad
{
    class EspacioFisicoEntidad : EntidadOracle
    {
        public EspacioFisico EspacioFisico { get; set; }

        public EspacioFisicoEntidad(string connectionStringName) : base(connectionStringName)
        {
            EspacioFisico = new EspacioFisico();
        }

        public EspacioFisicoEntidad()
        {
            EspacioFisico = new EspacioFisico();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            EspacioFisicoEntidad entidad = new EspacioFisicoEntidad();
            entidad.EspacioFisico.IdEspacioFisico = Int64.Parse(row["idespaciofisico"].ToString());
            entidad.EspacioFisico.CodEstado = row["codestado"].ToString();

            entidad.EspacioFisico.Estado = row["estado"].ToString();
            entidad.EspacioFisico.IdDepartamento = Int64.Parse(row["iddepartamento"].ToString());
            entidad.EspacioFisico.NomDepartamento = row["nomdepartamento"].ToString();
            entidad.EspacioFisico.IdMunicipio = Int64.Parse(row["idmunicipio"].ToString());
            entidad.EspacioFisico.NomMunicipio = row["nommunicipio"].ToString();
            entidad.EspacioFisico.IdDia = Int64.Parse(row["iddia"].ToString());
            entidad.EspacioFisico.Dia = row["dia"].ToString();
            entidad.EspacioFisico.IdHorario = Int64.Parse(row["idhorario"].ToString());
            entidad.EspacioFisico.Horario = row["horario"].ToString();
            entidad.EspacioFisico.IdGrupo = Int64.Parse(row["idgrupo"].ToString());
            entidad.EspacioFisico.Grupo = row["grupo"].ToString();
            entidad.EspacioFisico.SiglaGrupo = row["siglagrupo"].ToString();
            entidad.EspacioFisico.Lugar = row["lugar"].ToString();
            entidad.EspacioFisico.Direccion = row["direccion"].ToString();
            entidad.EspacioFisico.MotivoRechazo = row["motivorechazo"].ToString();
            entidad.EspacioFisico.IdFacilitador = Int64.Parse(row["idfacilitador"].ToString());
            entidad.EspacioFisico.NomFacilitador = row["nombrefacilitador"].ToString();


            return entidad;

        }
    }
}
