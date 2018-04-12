using HPV_Datos.General.Entidad;
using HPV_Entidades.EncuestasSena;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.EncuestasSena.Entidad
{
    public class TiposDocumentoEntidad : EntidadOracle
    {
        public OE_TiposDocumento TiposDocumento { get; set; }

        public TiposDocumentoEntidad(string connectionStringName) : base(connectionStringName)
        {
            TiposDocumento = new OE_TiposDocumento();
        }

        public TiposDocumentoEntidad()
        {
            TiposDocumento = new OE_TiposDocumento();
        }
        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            TiposDocumentoEntidad entidad = new TiposDocumentoEntidad();

            entidad.TiposDocumento.TipoDocumento = row["A04TIPODOCUMENTO"].ToString();
            entidad.TiposDocumento.Nombre = row["A04NOMBRE"].ToString();           

            return entidad;
        }
    }
}
