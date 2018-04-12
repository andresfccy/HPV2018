using HPV_Datos.General.Entidad;
using HPV_Entidades.EncuestasSena;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.EncuestasSena.Entidad
{
    public class RespuestaEntidad : EntidadOracle
    {
        public OE_Respuesta Respuesta { get; set; }

        public RespuestaEntidad(string connectionStringName) : base(connectionStringName)
        {
            Respuesta = new OE_Respuesta();
        }

        public RespuestaEntidad()
        {
            Respuesta = new OE_Respuesta();
        }

        public override EntidadOracle ParseFromDataRow(System.Data.DataRow row)
        {
            if (row == null)
                return null;

            RespuestaEntidad entidad = new RespuestaEntidad();

            entidad.Respuesta.IdRespuesta = int.Parse(row["A14CODIGO"].ToString());
            entidad.Respuesta.IdPregunta = int.Parse(row["A14PREGUNTA"].ToString());
            entidad.Respuesta.Letra = row["A14LETRA"].ToString();
            entidad.Respuesta.Respuesta = row["A14RESPUESTA"].ToString();

            return entidad;
        }
    }
}
