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
    class AsistenciaDetalleEntidad : EntidadOracle
    {
        public AsistenciaDetalle AsistenciaDetalle { get; set; }


        public AsistenciaDetalleEntidad(string connectionStringName) : base(connectionStringName)
        {
            AsistenciaDetalle = new AsistenciaDetalle();
        }

        public AsistenciaDetalleEntidad()
        {
            AsistenciaDetalle = new AsistenciaDetalle();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            AsistenciaDetalleEntidad entidad = new AsistenciaDetalleEntidad();
            entidad.AsistenciaDetalle.IdRegistro = Int64.Parse(row["IdRegistro"].ToString());
            entidad.AsistenciaDetalle.IdInscrito = Int64.Parse(row["IdInscrito"].ToString());
            entidad.AsistenciaDetalle.TipoDocumento = row["TipoDocumento"].ToString();
            entidad.AsistenciaDetalle.Documento = row["Documento"].ToString();
            entidad.AsistenciaDetalle.CodigoBeneficiario = Int64.Parse(row["CodigoBeneficiario"].ToString());
            entidad.AsistenciaDetalle.Nombre = row["Nombre"].ToString();

            return entidad;
        }
    }
}
