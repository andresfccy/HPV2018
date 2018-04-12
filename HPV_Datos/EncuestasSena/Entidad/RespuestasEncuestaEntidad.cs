using HPV_Datos.General.Entidad;
using HPV_Entidades.EncuestasSena;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.EncuestasSena.Entidad
{
    class RespuestasEncuestaEntidad : EntidadOracle
    {
        public OE_RespuestasEncuesta RespuestaEncuesta { get; set; }

        public RespuestasEncuestaEntidad(string connectionStringName) : base(connectionStringName)
        {
            RespuestaEncuesta = new OE_RespuestasEncuesta();
        }

        public RespuestasEncuestaEntidad()
        {
            RespuestaEncuesta = new OE_RespuestasEncuesta();
        } 

        public override EntidadOracle ParseFromDataRow(System.Data.DataRow row)
        {
            throw new NotImplementedException();
        }
    }
}
