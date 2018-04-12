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
    public class BeneficiarioEntidad : EntidadOracle
    {
        public Beneficiario Beneficiario { get; set; }


        public BeneficiarioEntidad(string connectionStringName) : base(connectionStringName)
        {
            Beneficiario = new Beneficiario();
        }

        public BeneficiarioEntidad()
        {
            Beneficiario = new Beneficiario();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            BeneficiarioEntidad entidad = new BeneficiarioEntidad();
            entidad.Beneficiario.TipoDocumento = row["A01TIPODOCUMENTO"].ToString();
            entidad.Beneficiario.Documento = row["A01DOCUMENTO"].ToString();
            entidad.Beneficiario.CodigoBeneficiario = Int64.Parse(row["A01CODIGOBENEFICIARIO"].ToString());
            entidad.Beneficiario.FechaNacimiento = row["A01FECHANACIMIENTO"].ToString();
            entidad.Beneficiario.PrimerNombre = row["A01PRIMERNOMBRE"].ToString();
            entidad.Beneficiario.SegundoNombre = row["A01SEGUNDONOMBRE"].ToString();
            entidad.Beneficiario.PrimerApellido = row["A01PRIMERAPELLIDO"].ToString();
            entidad.Beneficiario.SegundoApellido = row["A01SEGUNDOAPELLIDO"].ToString();
            entidad.Beneficiario.Movil = row["A01MOVIL"].ToString();
            entidad.Beneficiario.MovilAlt = row["A01MOVILALT"].ToString();
            entidad.Beneficiario.Correo = row["A01EMAIL"].ToString();
            entidad.Beneficiario.Telefono = row["A01TELEFONO"].ToString();
            entidad.Beneficiario.IdInscrito = Int64.Parse(row["a01codigo"].ToString());

            return entidad;
        }
    }
}
