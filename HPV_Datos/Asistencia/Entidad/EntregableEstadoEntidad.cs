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
    public class EntregableEstadoEntidad : EntidadOracle
    {
        public EntregableEstado EntregableEstado { get; set; }


        public EntregableEstadoEntidad(string connectionStringName) : base(connectionStringName)
        {
            EntregableEstado = new EntregableEstado();
        }

        public EntregableEstadoEntidad()
        {
            EntregableEstado = new EntregableEstado();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            EntregableEstadoEntidad entidad = new EntregableEstadoEntidad();
            entidad.EntregableEstado.IdTaller = Int64.Parse(row["IdTaller"].ToString());
            entidad.EntregableEstado.NomTaller = row["NomTaller"].ToString();
            entidad.EntregableEstado.DescripcionTaller = row["DescripcionTaller"].ToString();
            entidad.EntregableEstado.IdGrupoFacilitador = Int64.Parse(row["IdGrupoFacilitador"].ToString());
            entidad.EntregableEstado.NombreGrupo = row["NombreGrupo"].ToString();
            entidad.EntregableEstado.FechaRealizacion = row["FechaRealizacion"].ToString();
            entidad.EntregableEstado.EstadoDocumento = row["EstadoDocumento"].ToString();
            entidad.EntregableEstado.MotivoRechazoDocumento = row["MotivoRechazoDocumento"].ToString();
            entidad.EntregableEstado.IdDocumento = Int64.Parse(row["IdDocumento"].ToString());
            entidad.EntregableEstado.NumeroAsistentes = Int64.Parse(row["NumeroAsistentes"].ToString());
            entidad.EntregableEstado.EstadoAsistencia = row["EstadoAsistencia"].ToString();
            entidad.EntregableEstado.MotivoRechazoAsistencia = row["MotivoRechazoAsistencia"].ToString();
            entidad.EntregableEstado.IdFacilitador = Int64.Parse(row["IdFacilitador"].ToString());
            entidad.EntregableEstado.NombreFacilitador = row["NombreFacilitador"].ToString();

           return entidad;
        }

    }
}
