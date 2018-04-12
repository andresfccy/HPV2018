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
    public class AsistenciaEstadoEntidad : EntidadOracle
    {
        public AsistenciaEstado AsistenciaEstado { get; set; }


        public AsistenciaEstadoEntidad(string connectionStringName) : base(connectionStringName)
        {
            AsistenciaEstado = new AsistenciaEstado();
        }

        public AsistenciaEstadoEntidad()
        {
            AsistenciaEstado = new AsistenciaEstado();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            AsistenciaEstadoEntidad entidad = new AsistenciaEstadoEntidad();
            entidad.AsistenciaEstado.IdInscrito = Int64.Parse(row["IdInscrito"].ToString());
            entidad.AsistenciaEstado.NombreInscrito = row["NombreInscrito"].ToString();
            entidad.AsistenciaEstado.TipoDocumento = row["TipoDocumento"].ToString();
            entidad.AsistenciaEstado.Documento = row["Documento"].ToString();
            entidad.AsistenciaEstado.IndAsistencia = Int64.Parse(row["IndAsistencia"].ToString());

            return entidad;
        }
    }
}
