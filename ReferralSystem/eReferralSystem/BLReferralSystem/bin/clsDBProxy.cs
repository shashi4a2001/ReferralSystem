using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Web;

namespace BLCMR
{
    public class clsDBProxy
    {

        public SqlDataReader GetDataReader(string CommandText, string strCon)
        {
            SqlConnection m_Conn = OpenConnection(strCon);
            SqlCommand command = new SqlCommand(CommandText, m_Conn);

            SqlDataReader reader = command.ExecuteReader();
            //m_Conn.Close();
            return reader;
        }

        public SqlDataReader GetDataReaderThroughSP(string SpName, SqlCommand SpCmd, string strCon)
        {
            SqlConnection m_Conn = new SqlConnection();
            try
            {
                m_Conn = OpenConnection(strCon);
                SpCmd.Connection = m_Conn;
                SpCmd.CommandText = SpName;
                SpCmd.CommandType = CommandType.StoredProcedure;
                //Now Executer Query
                SqlDataReader reader = SpCmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if ((m_Conn != null))
                {
                    //m_Conn.Close();
                    //m_Conn = null;
                }
            }
        }

        public int ExecuteQuery(string SpName, SqlCommand SpCmd, string strCon)
        {
            int res = 0;
            try
            {

                SqlConnection m_Conn = OpenConnection(strCon);
                m_Conn = OpenConnection(strCon);
                SpCmd.Connection = m_Conn;
                SpCmd.CommandType = CommandType.StoredProcedure;
                SpCmd.CommandText = SpName;
                res = SpCmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                string s = ex.Message;
                s = "";
            }
            return res;
        }

        public int ExecuteQuery(string sql, string strCon)
        {
            int res;
            SqlConnection m_Conn = OpenConnection(strCon);
            SqlCommand command = new SqlCommand();
            m_Conn = OpenConnection(strCon);
            command.Connection = m_Conn;
            command.CommandType = CommandType.Text;
            command.CommandText = sql;
            res = command.ExecuteNonQuery();
            return res;

        }

        public DataTable ExecuteDataTable(string sql, string strCon)
        {
            SqlConnection m_Conn = new SqlConnection();
            SqlCommand SpCmd = new SqlCommand();
            try
            {
                DataTable dt = new DataTable();
                m_Conn = OpenConnection(strCon);
                SpCmd.Connection = m_Conn;

                SpCmd.CommandText = sql;
                SqlDataAdapter adp = new SqlDataAdapter(SpCmd);
                adp.Fill(dt);
                return dt;
            }
            finally
            {
                if ((m_Conn != null))
                {
                    m_Conn.Close();
                    m_Conn = null;
                }

            }
        }

        private SqlConnection OpenConnection(string strCon)
        {
            SqlConnection con = new SqlConnection();
            try
            {
                //con.ConnectionString = "data source=72.52.252.81;initial catalog=madhavas;user id=madhavas;password=sql2005;"; //Convert.ToString(ConfigurationManager.ConnectionStrings["MyCon"]); //"data source=72.52.252.81;initial catalog=madhavas;user id=madhavas;password=sql2005;";
                con.ConnectionString = strCon;
                con.Open();
            }
            catch (Exception ex)
            {
                //Response.Write("Problem in opening connection: " + ex.Message);
            }
            return con;
        }

        public int InsertValueinDB(string CommandText, string strCon)
        {
            SqlConnection m_Conn = OpenConnection(strCon);
            SqlCommand command = new SqlCommand(CommandText, m_Conn);

            SqlDataReader reader = command.ExecuteReader();
            //m_Conn.Close();
            return 1;
        }

        public int InsertValueinDB(string SpName, SqlCommand SpCmd, string strCon)
        {
            SqlConnection m_Conn = new SqlConnection();
            try
            {
                m_Conn = OpenConnection(strCon);
                SpCmd.Connection = m_Conn;
                SpCmd.CommandText = SpName;
                SpCmd.CommandType = CommandType.StoredProcedure;
                //Now Executer Query
                return SpCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //Response.Write("Problem in opening connection: " + ex.Message);
            }
            finally
            {
                if ((m_Conn != null))
                {
                    m_Conn.Close();
                    m_Conn = null;
                }
            }
            return 1;
        }

        public DataTable SpDataTable(string SpName, SqlCommand SpCmd, string strCon)
        {
            SqlConnection m_Conn = new SqlConnection();
            try
            {
                DataTable dt = new DataTable();
                m_Conn = OpenConnection(strCon);
                SpCmd.Connection = m_Conn;
                SpCmd.CommandType = CommandType.StoredProcedure;
                SpCmd.CommandText = SpName;
                SqlDataAdapter adp = new SqlDataAdapter(SpCmd);
                adp.Fill(dt);
                return dt;
            }
            finally
            {
                if ((m_Conn != null))
                {
                    m_Conn.Close();
                    m_Conn = null;
                }

            }
        }

        public DataSet SpDataSet(string SpName, SqlCommand SpCmd, string strCon)
        {
            SqlConnection m_Conn = new SqlConnection();
            try
            {
                DataSet ds = new DataSet();
                m_Conn = OpenConnection(strCon);
                SpCmd.Connection = m_Conn;
                SpCmd.CommandType = CommandType.StoredProcedure;
                SpCmd.CommandText = SpName;
                SqlDataAdapter adp = new SqlDataAdapter(SpCmd);
                adp.Fill(ds);
                return ds;
            }
            finally
            {
                if ((m_Conn != null))
                {
                    m_Conn.Close();
                    m_Conn = null;
                }

            }
        }

        public string SpExecuteScalar(string SpName, SqlCommand SpCmd, string strCon)
        {
            string s = "";
            SqlConnection m_Conn = new SqlConnection();
            try
            {

                m_Conn = OpenConnection(strCon);
                SpCmd.Connection = m_Conn;
                SpCmd.CommandType = CommandType.StoredProcedure;
                SpCmd.CommandText = SpName;
                s = SpCmd.ExecuteScalar().ToString();

            }
            finally
            {
                if ((m_Conn != null))
                {
                    m_Conn.Close();
                    m_Conn = null;
                }

            }
            return s;
        }

        public string SpExecuteScalar(string sqlString,  string strCon)
        {
            string strReturn = "";
            SqlConnection m_Conn = new SqlConnection();
            SqlCommand SpCmd = new SqlCommand();
            try
            {

                m_Conn = OpenConnection(strCon);
                SpCmd.Connection = m_Conn;
                SpCmd.CommandType = CommandType.Text;
                SpCmd.CommandText = sqlString;
                strReturn = SpCmd.ExecuteScalar().ToString();

            }
            finally
            {
                if ((m_Conn != null))
                {
                    m_Conn.Close();
                    m_Conn = null;
                }

                if ((SpCmd != null))
                {
                    SpCmd = null;
                }

            }
            return strReturn;
        }

    }
}
