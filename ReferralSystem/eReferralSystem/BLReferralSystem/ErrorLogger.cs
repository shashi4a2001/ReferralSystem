using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using DLCMR;

using System.Net;
using System.Web.SessionState;
using System.Diagnostics;

namespace BLCMR
{
    public class clsErrorLogger: System.Web.HttpApplication
    {
        public void WriteError(string FormName, string CustomMsg,string strConn,  Exception ex)
        {
            clsDBProxy objclsDBProxy = new clsDBProxy();
            SqlCommand cmd = new SqlCommand();
            try
            {
                //Writing in Log
                LogErrorToText(ex);

                //DB OPeration
                if (strConn != null && strConn != string.Empty)
                {
                    cmd.Parameters.AddWithValue("@Form_Name", FormName);
                    cmd.Parameters.AddWithValue("@Custom_Msg", CustomMsg);
                    cmd.Parameters.AddWithValue("@Error_Msg", ex.Message.ToString().Replace(".", ""));
                    cmd.Parameters.AddWithValue("@Stack_Trace", ex.StackTrace.ToString());
                    cmd.Parameters.AddWithValue("@Ip_Address", GetLanIPAddress());
                    objclsDBProxy.SpExecuteScalar("dbo.USP_INSERT_ERRORLOG", cmd, strConn);
                }

                ////For Event Log
                //Exception ErrorDescription = Server.GetLastError();
                ////Creation of event log if it does not exist  
                //String EventLogName = "Xccedance_Event_Log";

                //if (EventLog.SourceExists(EventLogName)==false)
                //{
                //    EventLog.CreateEventSource(EventLogName, EventLogName);
                //}
                ////Inserting into event log
                //EventLog Log = new EventLog();
                //Log.Source = EventLogName;
                //Log.WriteEntry(ErrorDescription.ToString(), EventLogEntryType.Error);
            }
            catch { }
            finally
            {
                objclsDBProxy = null;
                cmd = null;
            }
        }

        public String GetLanIPAddress()
        {
            try
            {
                ////The X-Forwarded-For (XFF) HTTP header field is a de facto standard for identifying the originating IP address of a 
                ////client connecting to a web server through an HTTP proxy or load balancer
                //String ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                //if (string.IsNullOrEmpty(ip))
                //{
                //    ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                //}

                //return ip;
 
                string strHostName = "";
                strHostName = System.Net.Dns.GetHostName();
                IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);
                IPAddress[] addr = ipEntry.AddressList;
                return addr[addr.Length - 1].ToString();

            }
            catch (Exception)
            {

                return "";
            }
        }

        public static void LogErrorToText(Exception ex)
        {
            StreamWriter writer;
            StringBuilder sb;
            try
            {
                sb = new StringBuilder();
                sb.Append("********************" + " Error Log - " + DateTime.Now + "*********************");
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Append("Exception Type : " + ex.GetType().Name);
                sb.Append(Environment.NewLine);
                sb.Append("Error Message : " + ex.Message);
                sb.Append(Environment.NewLine);
                sb.Append("Error Source : " + ex.Source);
                sb.Append(Environment.NewLine);
                if (ex.StackTrace != null)
                {
                    sb.Append("Error Trace : " + ex.StackTrace);
                }
                Exception innerEx = ex.InnerException;

                while (innerEx != null)
                {
                    sb.Append(Environment.NewLine);
                    sb.Append(Environment.NewLine);
                    sb.Append("Exception Type : " + innerEx.GetType().Name);
                    sb.Append(Environment.NewLine);
                    sb.Append("Error Message : " + innerEx.Message);
                    sb.Append(Environment.NewLine);
                    sb.Append("Error Source : " + innerEx.Source);
                    sb.Append(Environment.NewLine);
                    if (ex.StackTrace != null)
                    {
                        sb.Append("Error Trace : " + innerEx.StackTrace);
                    }
                    innerEx = innerEx.InnerException;
                }
                
                string filePath;
                if(System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("Operation\\Log")))
                {
                    filePath = HttpContext.Current.Server.MapPath("Operation\\Log\\CMRLog" + DateTime.Now.ToString("ddMMyyyy") + ".log");
                }
                else
                {
                    filePath = HttpContext.Current.Server.MapPath("Log\\CMRLog" + DateTime.Now.ToString("ddMMyyyy") + ".log");
                }
                
                writer = new StreamWriter(filePath, true);
                writer.WriteLine(sb.ToString());
                writer.Flush();
                writer.Close();
            }
            catch (Exception ex1)
            {
                string str = ex1.Message;
            }
            finally
            {
                  writer=null;
            }
        }

        public DataTable getExceptionReport( string fromDate, string toDate, string Mode, string strConn)
        {
            DataTable dtExceptionReport;
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Parameters.AddWithValue("@P_DATE_FROM", fromDate);
                cmd.Parameters.AddWithValue("@P_DATE_TO", toDate);
                cmd.Parameters.AddWithValue("@P_MODE", Mode);
                dtExceptionReport = objDLGeneric.SpDataTable("USP_GET_EXCEPTION_REPORT_DETAILS", cmd, strConn);

                return dtExceptionReport;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtExceptionReport = null;
                cmd = null;
                objDLGeneric = null;
            }
        }
    }
}
