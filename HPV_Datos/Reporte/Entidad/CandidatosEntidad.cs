using HPV_Datos.General.Entidad;
using HPV_Entidades.Reporte;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.Reporte.Entidad
{
    public class CandidatosEntidad : EntidadOracle
    {
        public Candidatos Candidatos { get; set; }
        public CandidatosEntidad(string connectionStringName) : base(connectionStringName)
        {
            Candidatos = new Candidatos();
        }

        public CandidatosEntidad()
        {
            Candidatos = new Candidatos();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
           
            if (row == null)
                return null;

            CandidatosEntidad entidad = new CandidatosEntidad();
            try
            {
                //Se asignan los atributos según las filas en la variable row
                foreach (var att in row.ItemArray)
                {
                    String test = att.ToString();
                    entidad.Candidatos.Atributos.Add(att.ToString());
                }

                foreach (var r in row.Table.Columns)
                {
                    entidad.Candidatos.NomAtributos.Add(r.ToString());
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Error en Candidatos" + e.Message);
            }
            return entidad;
        }

    }
}
