using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Web;
using System.Security.Cryptography;
using System.IO;

namespace DLCMR
{
    public class DLClsGeneric
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
            int res=0;
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

        public int ExecuteQuery(string sql , string strCon)
        {
            try
            {
                int res;
                SqlConnection m_Conn = OpenConnection(strCon);
                SqlCommand command = new SqlCommand();
                command.Connection = m_Conn;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                res = command.ExecuteNonQuery();
                command.Connection.Close();
                return res;
            }
            catch (Exception EX)
            {
                return -1;
            }
        }

        private SqlConnection OpenConnection(string strCon)
        {
            SqlConnection con = new SqlConnection();
            try
            {
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
            int res;
            SqlConnection m_Conn = new SqlConnection();
            try
            {
                res = 1;
                m_Conn = OpenConnection(strCon);
                SpCmd.Connection = m_Conn;
                SpCmd.CommandText = SpName;
                SpCmd.CommandType = CommandType.StoredProcedure;
                //Now Executer Query
                return SpCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                res = 99;
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
            return res;
        }


        public DataTable SpDataTable(string SpName, SqlCommand SpCmd, string strCon)
        {
            SqlConnection m_Conn=new SqlConnection() ;
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

        public int SpExecuteNonQuery(string SpName, SqlCommand SpCmd, string strCon)
        {
            int rowaffected=0;
            SqlConnection m_Conn = new SqlConnection();
            try
            {

                m_Conn = OpenConnection(strCon);
                SpCmd.Connection = m_Conn;
                SpCmd.CommandType = CommandType.StoredProcedure;
                SpCmd.CommandText = SpName;
                SpCmd.CommandTimeout = 0;
                rowaffected = SpCmd.ExecuteNonQuery();

            }
            finally
            {
                if ((m_Conn != null))
                {
                    m_Conn.Close();
                    m_Conn = null;
                }

            }
            return rowaffected;
        }

        public DataTable SpDataTableByCommandText(SqlCommand SpCmd, string strCon)
        {
            SqlConnection m_Conn = new SqlConnection();
            try
            {
                DataTable dt = new DataTable();
                m_Conn = OpenConnection(strCon);
                SpCmd.Connection = m_Conn;
                SpCmd.CommandType = CommandType.Text;
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



        /// <summary>
        /// To Encrypt Data
        /// </summary>
        /// <param name="clearText"></param>
        /// <returns></returns>
        public  string EncryptData(string clearText)
        {
            string EncryptionKey = "^GuN!Shi~PalLak$3008#";// "MAKV2SPBNI99212";
 
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        /// <summary>
        /// To Decrypt Data
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public string DecryptData(string cipherText)
        {
            string EncryptionKey = "^GuN!Shi~PalLak$3008#";// "MAKV2SPBNI99212"; 
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

    }
}
