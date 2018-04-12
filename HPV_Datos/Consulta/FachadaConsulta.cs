using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPV_Entidades.ConsultaWS;
using HPV_Datos.Consulta.Entidad;
using HPV_Entidades.General;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using HPV_Datos.General.Entidad;

namespace HPV_Datos.Consulta
{
    public class FachadaConsulta : InterfaceConsulta
    {
        public OS_ConsultarHorarios ConsultarHorarios(OE_ConsultarHorarios oe)
        {
            OS_ConsultarHorarios os = new OS_ConsultarHorarios();
            try
            {

                HorarioDisponibleEntidad entidad = new HorarioDisponibleEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_CONSULTAS.Pr_ConsultarHorarios",
                    new OracleParameter { ParameterName = "p_consulta", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.FiltroBusqueda },
                    new OracleParameter { ParameterName = "p_idPeriodo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdPeriodo },
                    new OracleParameter { ParameterName = "p_idCoordinador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdCoordinador },
                    new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    foreach (EntidadOracle objeto in lista)
                    {
                        HorarioDisponibleEntidad item = (HorarioDisponibleEntidad)objeto;
                        os.ListaHorario.Add(item.HorarioDisponible);
                    }

                }

                entidad.Dispose();

            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Consulta.ConsultarHorarios :" + e.Message;
            }

            return os;
        }

        public OS_ConsultarInscrito ConsultarInscrito(OE_ConsultarInscrito oe)
        {
            OS_ConsultarInscrito os = new OS_ConsultarInscrito();
            try
            {

                InscritoEntidad entidad = new InscritoEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_CONSULTAS.Pr_Consultarinscrito",
                    new OracleParameter { ParameterName = "p_idPeriodo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdPeriodo },
                    new OracleParameter { ParameterName = "p_idInscrito", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdInscrito },
                    new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    foreach (EntidadOracle objeto in lista)
                    {
                        InscritoEntidad item = (InscritoEntidad)objeto;
                        os.Inscrito = item.Inscrito;
                    }

                }

                entidad.Dispose();

            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Consulta.ConsultarInscrito :" + e.Message;
            }

            return os;
        }

        public OS_ConsultarInscritos ConsultarInscritos(OE_ConsultarInscritos oe)
        {
            OS_ConsultarInscritos os = new OS_ConsultarInscritos();
            try
            {

                InscritoEntidad entidad = new InscritoEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_CONSULTAS.Pr_Consultarinscritos",
                    new OracleParameter { ParameterName = "p_consulta", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.FiltroBusqueda },
                    new OracleParameter { ParameterName = "p_idPeriodo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdPeriodo },
                    new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    foreach (EntidadOracle objeto in lista)
                    {
                        InscritoEntidad item = (InscritoEntidad)objeto;
                        os.ListaInscrito.Add (item.Inscrito);
                    }

                }

                entidad.Dispose();

            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Consulta.ConsultarInscritos :" + e.Message;
            }

            return os;
        }
    }
}
