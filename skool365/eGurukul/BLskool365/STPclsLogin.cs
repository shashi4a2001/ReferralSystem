using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLCMR;
using System.Data.SqlClient;
namespace BLCMR
{
    public class STPclsLogin
    {
 
        /// <summary>
        /// Validate User to login the System
        /// </summary>
        /// <param name="Userid"></param>
        /// <param name="EmailId"></param>
        /// <param name="Password"></param>
        /// <param name="strCon"></param>
        /// <returns></returns>
        public DataTable GetLogin(string Userid, string EmailId, string Password,string IPAddress, string strCon)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataTable dtInfo;
            try
            {
                cmd.Parameters.AddWithValue("@P_USER_ID", Userid);
                cmd.Parameters.AddWithValue("@P_EMAIL", EmailId);
                cmd.Parameters.AddWithValue("@P_PASSWORD", Password);
                cmd.Parameters.AddWithValue("@P_LOGIN_MACHINE", IPAddress );
                dtInfo = objDLGeneric.SpDataTable("USP_VALIDATE_USER_LOGIN", cmd, strCon);
                return dtInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtInfo = null;
                objDLGeneric = null;
                dtInfo = null;
            }
        }

        /// <summary>
        /// Get Defualt Login Detail for School Registration
        /// </summary>
        /// <param name="Userid"></param>
        /// <param name="EmailId"></param>
        /// <param name="Password"></param>
        /// <param name="strCon"></param>
        /// <returns></returns>
        public DataTable GetDefaultLogin(string IPAddress, string strCon)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataTable dtInfo;
            try
            {
                cmd.Parameters.AddWithValue("@P_LOGIN_MACHINE", IPAddress);
                dtInfo = objDLGeneric.SpDataTable("USP_GET_MAIL_SERVER_DETAILS", cmd, strCon);
                return dtInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtInfo = null;
                objDLGeneric = null;
                dtInfo = null;
            }
        }
        /// <summary>
        /// Validate User to login the System
        /// </summary>
        /// <param name="Userid"></param>
        /// <param name="EmailId"></param>
        /// <param name="Password"></param>
        /// <param name="strCon"></param>
        /// <param name="strCaptch"></param>
        /// <returns></returns>
        public DataTable GetLogin(string Userid, string EmailId, string Password, string IPAddress,string strCaptch, string strCon)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataTable dtInfo;
            try
            {
                cmd.Parameters.AddWithValue("@P_USER_ID", Userid);
                cmd.Parameters.AddWithValue("@P_MODE", "C"); 
                cmd.Parameters.AddWithValue("@P_EMAIL", EmailId);
                cmd.Parameters.AddWithValue("@P_PASSWORD", Password);
                cmd.Parameters.AddWithValue("@P_LOGIN_MACHINE", IPAddress);
                cmd.Parameters.AddWithValue("@P_CAPTCHA_CODE", strCaptch);
                dtInfo = objDLGeneric.SpDataTable("USP_VALIDATE_USER_LOGIN", cmd, strCon);
                return dtInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtInfo = null;
                objDLGeneric = null;
                dtInfo = null;
            }
        }


        /// <summary>
        /// Validate User to login the System
        /// </summary>
        /// <param name="Userid"></param>
        /// <param name="EmailId"></param>
        /// <param name="Password"></param>
        /// <param name="strCon"></param>
        /// <returns></returns>
        public DataTable  LogOut(string Userid, int LogID , string strCon)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataTable dtInfo;
            try
            {
                cmd.Parameters.AddWithValue("@P_USER_ID", Userid);
                cmd.Parameters.AddWithValue("@P_EMAIL", Userid);
                cmd.Parameters.AddWithValue("@P_PASSWORD", string.Empty );
                cmd.Parameters.AddWithValue("@P_LOGIN_MACHINE", string.Empty);
                cmd.Parameters.AddWithValue("@P_MODE", "O");
                cmd.Parameters.AddWithValue("@P_LOGID", LogID);
                dtInfo = objDLGeneric.SpDataTable("USP_VALIDATE_USER_LOGIN", cmd, strCon);
                return dtInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtInfo = null;
                objDLGeneric = null;
                dtInfo = null;
            }
        }
        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="OldPassword"></param>
        /// <param name="NewPassword"></param>
        /// <param name="strCon"></param>
        /// <returns></returns>
        public DataTable changePassword(string userID, string OldPassword, string NewPassword, string strLoggedInMachne, string strCon)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataTable dtInfo;
            try
            {
                cmd.Parameters.AddWithValue("@P_UserID", userID);
                cmd.Parameters.AddWithValue("@P_OldPassword", OldPassword);
                cmd.Parameters.AddWithValue("@P_NewPassword", NewPassword);
                cmd.Parameters.AddWithValue("@P_LOGIN_MACHINE", strLoggedInMachne); 
                dtInfo = objDLGeneric.SpDataTable("USP_CHANGE_PASSWORD", cmd, strCon);
                return dtInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtInfo = null;
                objDLGeneric = null;
                dtInfo = null;
            }
        }

 
    }
}
