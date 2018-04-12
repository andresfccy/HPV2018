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
    public class CasosExitoEntidad : EntidadOracle
    {
        public CasosExito CasosExito { get; set; }
        public CasosExitoEntidad(string connectionStringName) : base(connectionStringName)
        {
            CasosExito = new CasosExito();
        }

        public CasosExitoEntidad()
        {
            CasosExito = new CasosExito();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
           

            if (row == null)
                return null;

            CasosExitoEntidad entidad = new CasosExitoEntidad();
            try
            {
                //Se asignan los atributos según las filas en la variable row
                foreach (var att in row.ItemArray)
                {
                    String test = att.ToString();
                    entidad.CasosExito.Atributos.Add(att.ToString());
                }

                foreach (var r in row.Table.Columns)
                {
                    entidad.CasosExito.NomAtributos.Add(r.ToString());
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Error en CasosExito" + e.Message);
            }
            return entidad;
        }

    }
}
