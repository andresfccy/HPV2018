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
    public class AsistenteEntidad : EntidadOracle
    {
        public Asistente Asistente { get; set; }


        public AsistenteEntidad(string connectionStringName) : base(connectionStringName)
        {
            Asistente = new Asistente();
        }

        public AsistenteEntidad()
        {
            Asistente = new Asistente();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            AsistenteEntidad entidad = new AsistenteEntidad();
            entidad.Asistente.IdInscrito = Int64.Parse(row["IdInscrito"].ToString());
            entidad.Asistente.NombreInscrito = row["NombreInscrito"].ToString();
            entidad.Asistente.TipoDocumento = row["TipoDocumento"].ToString();
            entidad.Asistente.Documento = row["Documento"].ToString();

            return entidad;
        }
    }
}
