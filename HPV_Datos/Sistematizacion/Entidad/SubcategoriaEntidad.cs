using HPV_Datos.General.Entidad;
using HPV_Entidades.Sistematizacion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.Sistematizacion.Entidad
{
    public class SubcategoriaEntidad : EntidadOracle
    {
        public Subcategoria Subcategoria { get; set; }

        public SubcategoriaEntidad(string connectionStringName) : base(connectionStringName)
        {
            Subcategoria = new Subcategoria();
        }

        public SubcategoriaEntidad()
        {
            Subcategoria = new Subcategoria();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            SubcategoriaEntidad entidad = new SubcategoriaEntidad();

            entidad.Subcategoria.Id = Int64.Parse(row["Id"].ToString());
            entidad.Subcategoria.NomSubcategoria = row["NomSubcategoria"].ToString();
            entidad.Subcategoria.IdCategoria = Int64.Parse(row["IdCategoria"].ToString());
            entidad.Subcategoria.NomCategoria = row["NomCategoria"].ToString();
            entidad.Subcategoria.DescOtra = row["NomCategoria"]?.ToString();

            return entidad;
        }
    }
}
