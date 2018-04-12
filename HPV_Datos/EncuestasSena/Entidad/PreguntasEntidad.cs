using HPV_Datos.General.Entidad;
using HPV_Entidades.EncuestasSena;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.EncuestasSena.Entidad
{
    public class PreguntasEntidad : EntidadOracle
    {
        public OE_Pregunta Pregunta { get; set; }

        public PreguntasEntidad(string connectionStringName) : base(connectionStringName)
        {
            Pregunta = new OE_Pregunta();
        }

        public PreguntasEntidad()
        {
            Pregunta = new OE_Pregunta();
        }

        public override EntidadOracle ParseFromDataRow(System.Data.DataRow row)
        {
            if (row == null)
                return null;

            PreguntasEntidad entidad = new PreguntasEntidad();

            entidad.Pregunta.IdPregunta = int.Parse(row["A13CODIGO"].ToString());
            entidad.Pregunta.NumeroPregunta = int.Parse(row["A13NUMERO"].ToString());
            entidad.Pregunta.Pregunta = row["A13PREGUNTA"].ToString();
            entidad.Pregunta.IdEncuesta = int.Parse(row["A13ENCUESTA"].ToString());

            return entidad;
        }
    }
}
