using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace HPV_Datos.General.Entidad
{
    class ListaValorEntidad : EntidadOracle
    {
        public ListaValor ListaValor { get; set; }


        public ListaValorEntidad(string connectionStringName) : base(connectionStringName)
        {
            ListaValor = new ListaValor();
        }

        public ListaValorEntidad()
        {
            ListaValor = new ListaValor();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            ListaValorEntidad entidad = new ListaValorEntidad();
            entidad.ListaValor.Valor = row["CODIGO"].ToString();
            entidad.ListaValor.Descripcion = row["NOMBRE"].ToString();

            return entidad;
        }
    }
}
