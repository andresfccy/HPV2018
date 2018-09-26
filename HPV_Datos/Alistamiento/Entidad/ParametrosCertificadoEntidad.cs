using HPV_Datos.General.Entidad;
using HPV_Entidades.Alistamiento;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.Alistamiento.Entidad
{
    public class ParametrosCertificadoEntidad : EntidadOracle
    {
        public ParametrosCertificado Parametros { get; set; }

        public ParametrosCertificadoEntidad()
        {
            Parametros = new ParametrosCertificado();
        }


        public ParametrosCertificadoEntidad(string connectionStringName) : base(connectionStringName)
        {
            Parametros = new ParametrosCertificado();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            ParametrosCertificadoEntidad entidad = new ParametrosCertificadoEntidad();

            entidad.Parametros.NomAliado = row["a31aliado"].ToString();
            entidad.Parametros.Encabezado = row["a31encabezado"].ToString();
            entidad.Parametros.Firma = row["a31firma"].ToString();
            entidad.Parametros.Renglon1 = row["a31renglon1"].ToString();
            entidad.Parametros.Renglon2 = row["a31renglon2"].ToString();
            entidad.Parametros.Renglon3 = row["a31renglon3"].ToString();
            entidad.Parametros.Renglon4 = row["a31renglon4"].ToString();
            entidad.Parametros.Renglon5 = row["a31renglon5"].ToString();
            entidad.Parametros.Renglon6 = row["a31renglon6"].ToString();

            return entidad;
        }

    }
}
