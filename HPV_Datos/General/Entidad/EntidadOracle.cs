using HPV_Entidades.General;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.General.Entidad
{
    public abstract class EntidadOracle
    {
        public abstract EntidadOracle ParseFromDataRow(System.Data.DataRow row);

        private OracleConnection Connection { get; set; }
        private OracleCommand Command { get; set; }


        public EntidadOracle()
        {

        }

        public EntidadOracle(string connectionStringName)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            Connection = new OracleConnection(connectionString);
        }

        public EntidadOracle(OracleConnection connection)
        {
            Connection = connection;
        }

        ~EntidadOracle()
        {
            Dispose(false);
        }


        public DataTable ExecuteQuery(string query, params OracleParameter[] parameters)
        {
            OpenClosedConnection();

            var resultTable = new DataTable();
            Command = new OracleCommand(query, Connection) { CommandType = CommandType.Text };

            Command.Parameters.AddRange(parameters);

            var dataAdapter = new OracleDataAdapter(Command);

            try
            {
                dataAdapter.Fill(resultTable);
            }
            catch (OracleException ex)
            {
                if (ex.Number == 4068)
                {
                    dataAdapter.Fill(resultTable);
                }
                else
                {
                    throw;
                }
            }

            return resultTable;
        }

        public object ExecuteScalarQuery(string query, params OracleParameter[] parameters)
        {
            OpenClosedConnection();

            object result;
            Command = new OracleCommand(query, Connection) { CommandType = CommandType.Text };

            Command.Parameters.AddRange(parameters);

            try
            {
                result = Command.ExecuteScalar();
            }
            catch (OracleException ex)
            {
                if (ex.Number == 4068)
                {
                    result = Command.ExecuteScalar();
                }
                else
                {
                    throw ex;
                }
            }

            return result;
        }

        public T ExecuteScalarQuery<T>(string query, params OracleParameter[] parameters)
        {
            return (T)ExecuteScalarQuery(query, parameters);
        }

        public int ExecuteNonQuery(string query, params OracleParameter[] parameters)
        {
            OpenClosedConnection();

            int result;
            Command = new OracleCommand(query, Connection) { CommandType = CommandType.Text };

            Command.Parameters.AddRange(parameters);

            try
            {
                result = Command.ExecuteNonQuery();
            }
            catch (OracleException ex)
            {
                if (ex.Number == 4068)
                {
                    result = Command.ExecuteNonQuery();
                }
                else
                {
                    throw ex;
                }
            }

            return result;
        }

        public int ExecuteStoreProcedure(string storeProcedure, params OracleParameter[] parameters)
        {
            OpenClosedConnection();

            int result;
            Command = new OracleCommand(storeProcedure, Connection) { CommandType = CommandType.StoredProcedure };

            Command.Parameters.AddRange(parameters);

            try
            {
                result = Command.ExecuteNonQuery();
            }
            catch (OracleException ex)
            {
                
                if (ex.Number == 4068)
                {
                    result = Command.ExecuteNonQuery();
                }
                else
                {
                    throw;
                }
            }

            return result;
        }

        public string GetParameterString (string nomParameter)
        {
            if (IsNUllParameter(nomParameter))
                throw new Exception("Parametro nulo:" + nomParameter);

            return Command.Parameters[nomParameter].Value.ToString();

        }

        public int? GetParameterInt(string nomParameter)
        {
            if (IsNUllParameter(nomParameter))
                throw new Exception("Parametro nulo:" + nomParameter);

            return int.Parse(Command.Parameters[nomParameter].Value.ToString());
        }

        public long GetParameterLong(string nomParameter)
        {
            if (IsNUllParameter(nomParameter))
                throw new Exception("Parametro nulo:" + nomParameter);

            return long.Parse(Command.Parameters[nomParameter].Value.ToString());
        }

        public decimal GetParameterDecimal(string nomParameter)
        {
            if (IsNUllParameter(nomParameter))
                throw new Exception("Parametro nulo:" + nomParameter);

            return decimal.Parse(Command.Parameters[nomParameter].Value.ToString());
        }

        public DateTime GetParameterDateTime(string nomParameter)
        {
            if (IsNUllParameter(nomParameter))
                throw new Exception("Parametro nulo:" + nomParameter);

            return DateTime.Parse(Command.Parameters[nomParameter].Value.ToString());
        }

        public double GetParameterDouble(string nomParameter)
        {
            if (IsNUllParameter(nomParameter))
                throw new Exception("Parametro nulo:" + nomParameter);

            return Double.Parse(Command.Parameters[nomParameter].Value.ToString());
        }

        public List<EntidadOracle> CursorToList(string nomCursor)
        {
            List<EntidadOracle> lista = new List<EntidadOracle>();

            if (Command == null)
                return lista;

            OracleDataAdapter da = new OracleDataAdapter(Command);
            DataSet ds = new DataSet();
            da.Fill(ds, "nomCursor", (OracleRefCursor)Command.Parameters[nomCursor].Value);
            if (ds.Tables.Count > 0)
                foreach (DataRow row in ds.Tables[0].Rows)
                    lista.Add(ParseFromDataRow(row));
               
            return lista;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Connection != null)
                {
                    Connection.Close();
                    Connection.Dispose();
                    Connection = null;
                }
            }
        }

        private void OpenClosedConnection()
        {
            if (Connection != null)
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }
        }

        private bool IsNUllParameter(string nomParameter)
        {
            if (Command == null)
                return true;

            if (DBNull.Value.Equals(Command.Parameters[nomParameter].Value))
                return true;

            return false;

        }
    }
}
