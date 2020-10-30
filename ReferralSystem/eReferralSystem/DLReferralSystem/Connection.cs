using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace DLCMR
{
    class Connection
    {
        SqlConnection m_Conn; 
        private string m_sConnStr; 
        private string m_sDatasource = "MSACCESS";
        SqlCommand cmd = new SqlCommand();
        static string GlobalConnectionString = "";

        //---- To open a connection for MSACCESS/SQL SERVER
        public void GetConn()
        {
            try
            {
                if (m_Conn != null)
                {
                    m_Conn.Close();
                    m_Conn = null;
                }
                //m_Conn = new OleDbConnection(m_sConnStr);

                m_Conn = new SqlConnection();
                m_Conn.ConnectionString = GlobalConnectionString;
                cmd.Connection = m_Conn;
                m_Conn.Open();
            }
            catch (SqlException ex)
            {
                // objCore.ShowException(ex)
            }
        }

        //---- To close a connection for MSACCESS/SQL SERVER
        public void CloseCon()
        {
            try
            {
                m_Conn.Close();
            }
            catch (Exception e)
            { }
        }
    }

    
}
