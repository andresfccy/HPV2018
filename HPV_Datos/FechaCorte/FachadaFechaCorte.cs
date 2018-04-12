using HPV_Datos.FechaCorte.Entidad;
using HPV_Datos.General.Entidad;
using HPV_Entidades.FechaCorteWS;
using HPV_Entidades.General;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.FechaCorte
{
    public class FachadaFechaCorte : InterfaceFechaCorte
    {
        public OS_DarFechaCorte DarFechaCorte(OE_DarFechaCorte oe)
        {
            OS_DarFechaCorte os = new OS_DarFechaCorte();
            try
            {
                FechaCorteEntidad entidad = new FechaCorteEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_PAR_ADMPARAMETRO.Pr_DarFechasCorte",
                    new OracleParameter { ParameterName = "p_idperiodo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdPeriodo },
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
                        FechaCorteEntidad item = (FechaCorteEntidad)objeto;
                        os.ListaFechaCorte.Add(item.FechaCorte);
                    }
                }
                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.FechaCorte.FachadaFechaCorte.DarFechaCorte :" + e.Message;
            }

            return os;
        }
    }
}
