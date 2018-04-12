using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data;
using Oracle.ManagedDataAccess.Types;

namespace HPV_Datos.General

{

    public class OracleDataContext : IDisposable
    {
        public OracleDataContext()
            : this("ApplicationContext")
        {
        }

        public OracleDataContext(string connectionStringName)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            Connection = new OracleConnection(connectionString);
        }

        public OracleDataContext(OracleConnection connection)
        {
            Connection = connection;
        }

        ~OracleDataContext()
        {
            Dispose(false);
        }


        public OracleConnection Connection { get; private set; }

        public DataTable ExecuteQuery(string query, params OracleParameter[] parameters)
        {
            OpenClosedConnection();

            var resultTable = new DataTable();
            var command = new OracleCommand(query, Connection) { CommandType = CommandType.Text };

            command.Parameters.AddRange(parameters);

            var dataAdapter = new OracleDataAdapter(command);

            try
            {
                dataAdapter.Fill(resultTable);
            }
            catch (OracleException ex)
            {
                // Repeating OracleCommand because the procedure has been invalidated.
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
            var command = new OracleCommand(query, Connection) { CommandType = CommandType.Text };

            command.Parameters.AddRange(parameters);

            try
            {
                result = command.ExecuteScalar();
            }
            catch (OracleException ex)
            {
                if (ex.Number == 4068)
                {
                    result = command.ExecuteScalar();
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
            var command = new OracleCommand(query, Connection) { CommandType = CommandType.Text };

            command.Parameters.AddRange(parameters);

            try
            {
                result = command.ExecuteNonQuery();
            }
            catch (OracleException ex)
            {
                // Repeating OracleCommand because the procedure has been invalidated.
                if (ex.Number == 4068)
                {
                    result = command.ExecuteNonQuery();
                }
                else
                {
                    throw ex;
                }
            }

            return result;
        }


        public OracleCommand ExecuteStoreProcedure(string storeProcedure, params OracleParameter[] parameters)
        {
            OpenClosedConnection();

            var resultTable = new DataTable();
            var command = new OracleCommand(storeProcedure, Connection) { CommandType = CommandType.StoredProcedure };

            command.Parameters.AddRange(parameters);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (OracleException ex)
            {
                // Repeating OracleCommand because the procedure has been invalidated.
                if (ex.Number == 4068)
                {
                    command.ExecuteNonQuery();
                }
                else
                {
                    throw;
                }
            }

            return command;
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
    }
}
