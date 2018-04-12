using HPV_Datos.General.Entidad;
using HPV_Entidades.EncuestasSena;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.EncuestasSena.Entidad
{
    public class UsuarioSenaEncuestaEntidad : EntidadOracle
    {
        public OE_UsuarioEncuesta UsusarioEncuesta { get; set; }

        public UsuarioSenaEncuestaEntidad(string connectionStringName) : base(connectionStringName)
        {
            UsusarioEncuesta = new OE_UsuarioEncuesta();
        }

        public UsuarioSenaEncuestaEntidad()
        {
            UsusarioEncuesta = new OE_UsuarioEncuesta();
        }
        public override EntidadOracle ParseFromDataRow(System.Data.DataRow row)
        {
            if (row == null)
                return null;

            UsuariosSenaEntidad entidad = new UsuariosSenaEntidad();

            entidad.UsusarioEncuesta.IdUsusarioEncuestaSena = Convert.ToInt16(row["A113CODIGO"].ToString());
            entidad.UsusarioEncuesta.TipoDocumento = row["A113TIPODOCUMENTO"].ToString();
            entidad.UsusarioEncuesta.Documento = row["A113DOCUMENTO"].ToString();
            entidad.UsusarioEncuesta.Nombre = row["A113NOMBRE"].ToString();
            entidad.UsusarioEncuesta.PrimerApellido = row["A113PRIMERAPELLIDO"].ToString();
            entidad.UsusarioEncuesta.SegundoApellido = row["A113SEGUNDOAPELLIDO"].ToString();
            entidad.UsusarioEncuesta.Email = row["A113CORREO"].ToString();
            entidad.UsusarioEncuesta.Celular = row["A113CELULAR"].ToString();
            entidad.UsusarioEncuesta.EsJovenEnAccion = Convert.ToInt16(row["A113ESJOVENENACCION"].ToString());
            entidad.UsusarioEncuesta.Consecutivo = row["A115CONSECUTIVO"].ToString();
            return entidad;
        }
    }
}
