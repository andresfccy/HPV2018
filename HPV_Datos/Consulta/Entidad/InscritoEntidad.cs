using HPV_Datos.General.Entidad;
using HPV_Entidades.Consulta;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.Consulta.Entidad
{
    public class InscritoEntidad : EntidadOracle
    {
        public Inscrito Inscrito { get; set; }


        public InscritoEntidad(string connectionStringName) : base(connectionStringName)
        {
            Inscrito = new Inscrito();
        }

        public InscritoEntidad()
        {
            Inscrito = new Inscrito();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            InscritoEntidad entidad = new InscritoEntidad();
            try
            {
                entidad.Inscrito.IdInscrito = Int64.Parse(row["IdInscrito"].ToString());
                entidad.Inscrito.TipoDocumento = row["TipoDocumento"].ToString();
                entidad.Inscrito.Documento = row["Documento"].ToString();
                entidad.Inscrito.NombreInscrito = row["Nombreinscrito"].ToString();

                if (row["CodigoBeneficiario"] != System.DBNull.Value)
                    entidad.Inscrito.CodigoBeneficiario = Int64.Parse(row["CodigoBeneficiario"].ToString());
                else
                    entidad.Inscrito.CodigoBeneficiario = 0;

                entidad.Inscrito.FechaNacimiento = row["FechaNacimiento"].ToString();
                entidad.Inscrito.Correo = row["Correo"].ToString();
                entidad.Inscrito.Telefono = row["Telefono"].ToString();

                entidad.Inscrito.Movil = row["Movil"].ToString();
                entidad.Inscrito.MovilAlt = row["MovilAlt"].ToString();
                entidad.Inscrito.CondicionEspecial = row["CondicionEspecial"].ToString();

                entidad.Inscrito.NomDepartamento = row["NomDepartamento"].ToString();
                entidad.Inscrito.NomMunicipio = row["NomMunicipio"].ToString();
                entidad.Inscrito.Dia = row["Dia"].ToString();
                entidad.Inscrito.Horario = row["Horario"].ToString();
                entidad.Inscrito.Grupo = row["Grupo"].ToString();
                entidad.Inscrito.SiglaGrupo = row["SiglaGrupo"].ToString();
                entidad.Inscrito.Lugar = row["Lugar"].ToString();
                entidad.Inscrito.Direccion = row["Direccion"].ToString();
                entidad.Inscrito.NombreFacilitador = row["NombreFacilitador"].ToString();
                entidad.Inscrito.NombreCoordinador = row["NombreCoordinador"].ToString();
                entidad.Inscrito.NomTipoDocumento = row["NomTipoDocumento"].ToString();
                entidad.Inscrito.FechaRegistro = row["FechaRegistro"].ToString();

                entidad.Inscrito.FechaCertificado = row["FechaCertificado"].ToString();
                entidad.Inscrito.Vigencia = row["vigencia"].ToString();

                String listaTaller = row["listaTaller"].ToString();

                if (listaTaller.Length > 0)
                {
                    char[] delimiterChars = { ';' };
                    String[] itemTaller = listaTaller.Split(delimiterChars);
                    foreach (string item in itemTaller)
                    {
                        char[] delimiterChars2 = { ',' };
                        String[] strAsistencia = item.Split(delimiterChars2);

                        AsistenciaTaller asis = new AsistenciaTaller();
                        asis.IdTaller = long.Parse(strAsistencia[0]);
                        asis.FechaRealizacion = strAsistencia[1];
                        asis.IndAsistencia = short.Parse(strAsistencia[2]);
                        entidad.Inscrito.ListaAsistenciaTaller.Add(asis);
                    }
                }

            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Error en " + entidad.Inscrito.IdInscrito + " " + e.Message);
            }
            return entidad;
        }
    }
}
