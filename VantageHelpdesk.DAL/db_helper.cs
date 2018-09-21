using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Text;
using System.Diagnostics;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;

namespace VantageHelpdesk.DAL
{
    public class db_helper : IDisposable
    {
        private DbConnection objConn;
        private DbCommand objComm;
        private DbProviderFactory objFactory = null;

        public db_helper(ConnectionStr ConnStr)
        {
            objFactory = SqlClientFactory.Instance;
            objConn = objFactory.CreateConnection();
            objComm = objFactory.CreateCommand();
            if (ConnStr == ConnectionStr.HelpdeskConnStr)
            {
                objConn.ConnectionString = ConfigurationManager.ConnectionStrings["HelpdeskConnStr"].ToString();
            }
            else if (ConnStr == ConnectionStr.vAppUsersConnStr)
            {
                objConn.ConnectionString = ConfigurationManager.ConnectionStrings["vAppUsersConnStr"].ToString();
            }
            else if (ConnStr == ConnectionStr.vPortalConnStr)
            {
                objConn.ConnectionString = ConfigurationManager.ConnectionStrings["vPortalConnStr"].ToString();
            }
            else if (ConnStr == ConnectionStr.HelpdeskConnStrLive)
            {
                objConn.ConnectionString = ConfigurationManager.ConnectionStrings["HelpdeskConnStrLive"].ToString();
            }
            objComm.Connection = objConn;
        }

        public db_helper(string ConnStr)
        {
            objFactory = SqlClientFactory.Instance;
            objConn = objFactory.CreateConnection();
            objComm = objFactory.CreateCommand();
            objConn.ConnectionString = ConnStr;            
            objComm.Connection = objConn;
        }

        public int AddParameterNull(string name, DbType db)
        {
            DbParameter p = objFactory.CreateParameter();
            p.ParameterName = name;
            p.Value = DBNull.Value;
            p.Size = -1;
            p.DbType = db;
            return objComm.Parameters.Add(p);
        }
        public int AddParameter(string name, object value, DbType db)
        {
            DbParameter p = objFactory.CreateParameter();
            p.ParameterName = name;
            p.Value = value;
            p.DbType = db;
            return objComm.Parameters.Add(p);
        }

        public int ExecuteNonQuery(string SQLQuery, CommandType CommType)
        {
            objComm.CommandText = SQLQuery;
            objComm.CommandType = CommType;
            int i = -1;
            try
            {
                if (objConn.State == System.Data.ConnectionState.Closed)
                {
                    objConn.Open();
                }
                i = objComm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogErrors(ex);
            }
            finally
            {
                objComm.Parameters.Clear();
                if (objConn.State == System.Data.ConnectionState.Open)
                {
                    objConn.Close();
                }
            }
            return i;
        }

        public object ExecuteScalar(string SQLQuery, CommandType CommType)
        {
            objComm.CommandText = SQLQuery;
            objComm.CommandType = CommType;
            object o = null;
            try
            {
                if (objConn.State == System.Data.ConnectionState.Closed)
                {
                    objConn.Open();
                }
                o = objComm.ExecuteScalar();
            }
            catch (Exception ex)
            {
                LogErrors(ex);
            }
            finally
            {
                objComm.Parameters.Clear();
                if (objConn.State == System.Data.ConnectionState.Open)
                {
                    objConn.Close();
                }
            }

            return o;
        }

        public DbDataReader ExecuteReader(string SQLQuery, CommandType CommType)
        {
            objComm.CommandText = SQLQuery;
            objComm.CommandType = CommType;
            DbDataReader reader = null;
            try
            {
                if (objConn.State == System.Data.ConnectionState.Closed)
                {
                    objConn.Open();
                }
                reader = objComm.ExecuteReader(CommandBehavior.CloseConnection);
                //reader = objComm.ExecuteReader();
            }            
            catch (Exception ex)
            {
                LogErrors(ex);
            }
            finally
            {
                objComm.Parameters.Clear();
            }
            return reader;
        }

        public DataSet ExecuteDataSet(string SQLQuery, CommandType CommType)
        {
            DbDataAdapter adapter = objFactory.CreateDataAdapter();
            objComm.CommandText = SQLQuery;
            objComm.CommandType = CommType;
            objComm.CommandTimeout = 0;
            adapter.SelectCommand = objComm;
            DataSet ds = new DataSet();
            try
            {
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                LogErrors(ex);
            }
            finally
            {
                objComm.Parameters.Clear();
                if (objConn.State == System.Data.ConnectionState.Open)
                {
                    objConn.Close();
                }
            }
            return ds;
        }

        public DataView ExecuteDataView(string SQLQuery, CommandType CommType)
        {
            DbDataAdapter adapter = objFactory.CreateDataAdapter();
            objComm.CommandText = SQLQuery;
            objComm.CommandType = CommType;
            objComm.CommandTimeout = 0;
            adapter.SelectCommand = objComm;
            DataSet ds = new DataSet();
            DataView dv = new DataView();
            try
            {
                adapter.Fill(ds);
                dv.Table = ds.Tables[0];
                ds.Dispose();
            }
            catch (Exception ex)
            {
                LogErrors(ex);
            }
            finally
            {
                objComm.Parameters.Clear();
                if (objConn.State == System.Data.ConnectionState.Open)
                {
                    objConn.Close();
                }
            }
            return dv;
        }

        private void LogErrors(Exception ex)
        {
            throw ex;       
        }

        public enum ConnectionStr
        {
            HelpdeskConnStr,
            HelpdeskConnStrLive,
            vAppUsersConnStr,
            vPortalConnStr
        }

        public void Dispose()
        {
            objConn.Close();
            objConn.Dispose();
            objComm.Dispose();
        }
    }
}