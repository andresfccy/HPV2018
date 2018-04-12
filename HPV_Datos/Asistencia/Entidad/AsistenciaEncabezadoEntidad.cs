using HPV_Datos.General.Entidad;
using HPV_Entidades.Asistencia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.Asistencia.Entidad
{
    public class AsistenciaEncabezadoEntidad : EntidadOracle
    {
        public AsistenciaEncabezado AsistenciaEncabezado { get; set; }


        public AsistenciaEncabezadoEntidad(string connectionStringName) : base(connectionStringName)
        {
            AsistenciaEncabezado = new AsistenciaEncabezado();
        }

        public AsistenciaEncabezadoEntidad()
        {
            AsistenciaEncabezado = new AsistenciaEncabezado();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            AsistenciaEncabezadoEntidad entidad = new AsistenciaEncabezadoEntidad();
            entidad.AsistenciaEncabezado.IdDepartamento = Int64.Parse(row["IdDepartamento"].ToString());
            entidad.AsistenciaEncabezado.NomDepartamento = row["NomDepartamento"].ToString();

            entidad.AsistenciaEncabezado.IdMunicipio = Int64.Parse(row["IdMunicipio"].ToString());
            entidad.AsistenciaEncabezado.NomMunicipio = row["NomMunicipio"].ToString();
            entidad.AsistenciaEncabezado.IdFacilitador = Int64.Parse(row["IdFacilitador"].ToString());
            entidad.AsistenciaEncabezado.NomFacilitador = row["NomFacilitador"].ToString();
            entidad.AsistenciaEncabezado.IdCoordinador = Int64.Parse(row["IdCoordinador"].ToString());
            entidad.AsistenciaEncabezado.NomCoordinador = row["NomCoordinador"].ToString();
            entidad.AsistenciaEncabezado.IdTaller = Int64.Parse(row["IdTaller"].ToString());
            entidad.AsistenciaEncabezado.NomTaller = row["NomTaller"].ToString();
            entidad.AsistenciaEncabezado.DescripcionTaller = row["DescripcionTaller"].ToString();
            entidad.AsistenciaEncabezado.NomPeriodoVigente = row["NomPeriodoVigente"].ToString();
            entidad.AsistenciaEncabezado.SiglaGrupo = row["SiglaGrupo"].ToString();
            entidad.AsistenciaEncabezado.IdHorario = Int64.Parse(row["IdHorario"].ToString());
            entidad.AsistenciaEncabezado.NomHorario = row["NomHorario"].ToString();
            entidad.AsistenciaEncabezado.IdGrupo = Int64.Parse(row["IdGrupo"].ToString());
            entidad.AsistenciaEncabezado.NomGrupo = row["NomGrupo"].ToString();
            entidad.AsistenciaEncabezado.Lugar = row["Lugar"].ToString();
            entidad.AsistenciaEncabezado.Direccion = row["Direccion"].ToString();

            return entidad;
        }
    }
}
