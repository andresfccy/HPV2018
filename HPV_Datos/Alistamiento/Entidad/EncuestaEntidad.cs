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
    public class EncuestaEntidad : EntidadOracle
    {
        public Encuesta Encuesta { get; set; }


        public EncuestaEntidad(string connectionStringName) : base(connectionStringName)
        {
            Encuesta = new Encuesta();
        }

        public EncuestaEntidad()
        {
            Encuesta = new Encuesta();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            EncuestaEntidad entidad = new EncuestaEntidad();
            entidad.Encuesta.Codigo = Int64.Parse(row["a50codigo"].ToString());
            entidad.Encuesta.Descripcion = row["a50descripcion"].ToString();
            entidad.Encuesta.CodigoPregunta = Int64.Parse(row["a13codigo"].ToString());
            entidad.Encuesta.NumeroPregunta = Int64.Parse(row["a13numero"].ToString());
            entidad.Encuesta.DescripcionPregunta = row["a13pregunta"].ToString();
            entidad.Encuesta.TipoPregunta = row["a13tipopregunta"].ToString();
            entidad.Encuesta.CodigoRespuesta = Int64.Parse(row["a14codigo"].ToString());
            entidad.Encuesta.LetraPregunta = row["a14letra"].ToString();
            entidad.Encuesta.DescripcionRespuesta = row["a14respuesta"].ToString();
            entidad.Encuesta.Correcta = row["a14correcta"].ToString();

            return entidad;
        }

    }
}
