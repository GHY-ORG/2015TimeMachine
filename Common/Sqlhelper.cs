using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.OleDb;
using System.Configuration;

namespace Common
{
    interface IDbHelper
    {

        int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params IDataParameter[] commandParameters);

        object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params IDataParameter[] commandParameters);

        IDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, params IDataParameter[] commandParameters);

        DataTable ExcuteDataTable(string connectionString, CommandType commandType, string commandText, params IDataParameter[] commandParameters);

        DataSet ExcuteDataSet(string connectionString, CommandType commandType, string commandText, string srcTable, params IDataParameter[] commandParameters);
    }
    public class SqlHelper : IDbHelper
    {
        #region IDbHelper 成员

        public int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params IDataParameter[] commandParameters)
        {
            int returnValue;

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(commandText, connection);
                command.CommandType = commandType;
                command.Parameters.AddRange(commandParameters);

                connection.Open();
                returnValue = command.ExecuteNonQuery();
                connection.Close();
            }

            return returnValue;
        }

        public object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params IDataParameter[] commandParameters)
        {
            object returnValue;

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(commandText, connection);
                command.CommandType = commandType;
                command.Parameters.AddRange(commandParameters);

                connection.Open();
                returnValue = command.ExecuteScalar();
                connection.Close();
            }
            return returnValue;
        }

        public IDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, params IDataParameter[] commandParameters)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            OleDbCommand command = new OleDbCommand(commandText, connection);
            command.CommandType = commandType;
            command.Parameters.AddRange(commandParameters);

            connection.Open();
            return command.ExecuteReader();
        }

        public DataTable ExcuteDataTable(string connectionString, CommandType commandType, string commandText, params IDataParameter[] commandParameters)
        {
            DataTable ThisDataTable = new DataTable();
            OleDbConnection conn = new OleDbConnection(connectionString);
            OleDbCommand cmd = new OleDbCommand(commandText, conn);
            cmd.CommandType = commandType;
            OleDbDataAdapter DataAdaper = new OleDbDataAdapter(cmd);
            cmd.Parameters.AddRange(commandParameters);
            conn.Open();
            DataAdaper.Fill(ThisDataTable);
            conn.Close();
            return ThisDataTable;
        }

        public DataSet ExcuteDataSet(string connectionString, CommandType commandType, string commandText, string srcTable, params IDataParameter[] commandParameters)
        {
            DataSet set = new DataSet();

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(commandText, connection);
                command.CommandType = commandType;
                command.Parameters.AddRange(commandParameters);

                OleDbDataAdapter adapter = new OleDbDataAdapter(command);

                connection.Open();
                adapter.Fill(set, srcTable);
                connection.Close();
            }

            return set;
        }
        #endregion
    }
}
