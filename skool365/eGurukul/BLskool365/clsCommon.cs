using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLCMR;
using System.Data.SqlClient;
using System.Security.Cryptography;
 
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web.UI.WebControls;
using System.IO ;
 
namespace BLCMR
{
    public class clsCommon
    {

        //Get Student Information
        public DataTable GetStudentDetails(string strCon, int RegNo)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dt;
                cmd.Parameters.AddWithValue("@RegNo", RegNo);
                dt = objDLGeneric.SpDataTable("usp_GetStudentInfo", cmd, strCon);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public string Encrypt(string val)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(val);
            var encBytes = System.Security.Cryptography.ProtectedData.Protect(bytes, new byte[0], System.Security.Cryptography.DataProtectionScope.LocalMachine);
            return Convert.ToBase64String(encBytes);
        }
        public string Decrypt(string val)
        {
            var bytes = Convert.FromBase64String(val);
            var encBytes = System.Security.Cryptography.ProtectedData.Unprotect(bytes, new byte[0], System.Security.Cryptography.DataProtectionScope.LocalMachine);
            return System.Text.Encoding.UTF8.GetString(encBytes);
        }
        public string getDynamicPassWord( string strConn)
        {
            clsDBProxy objclsDBProxy = new clsDBProxy();
            string strPassword = string.Empty;
            string strSQL= string.Empty;

            try
            {
                 strSQL = "SELECT dbo.[FNGENERATEPASSWORD]()";
                 strPassword =  objclsDBProxy.SpExecuteScalar(strSQL, strConn);

            }
            catch (Exception ex)
            {
                return "LNVM@123";
            }
            finally
            {
                objclsDBProxy = null;
            }
            return strPassword;
        }
        public string ConvertDateForInsert(string str)
        {
            int iDate, iMonthNo, iYear;
            char[] splitchar = { '/' };
            string[] dt = str.Split(splitchar);

            int.TryParse (dt[0], out iDate) ;
            int.TryParse(dt[1], out iMonthNo);  
            int.TryParse(dt[2], out iYear);  

            DateTime dtnew = new DateTime(iYear, iMonthNo, iDate);

            return dtnew.ToString("dd-MMM-yyyy");

        }
        public string ConvertDateForSelect(DateTime dt)
        {
            return dt.ToString("dd/MM/yyyy");
        }
        public void SendEmail(ObjSendEmail SendMail )
        {
            SendingMail(SendMail );
        }
        public void SendingMail(ObjSendEmail SendMail)
        {
            MailMessage mail = new MailMessage();
            string strStatus = string.Empty;
            string strErrorMsg = string.Empty;
            string strQuery = string.Empty;
            clsCommon objCommon = new clsCommon();
           
            try
            {

                MailMessage message;
                SmtpClient mySmtpClient;
                message = new MailMessage(SendMail.From, SendMail.To, SendMail.Subject, SendMail.MailBody);
                mySmtpClient = new SmtpClient(SendMail.SMTPHost, SendMail.SMTPPort);

                try
                {

                    mySmtpClient.Credentials = new System.Net.NetworkCredential(SendMail.UserId, SendMail.Password);
                    message.IsBodyHtml = true;
                    message.Priority = MailPriority.High;

                    mySmtpClient.EnableSsl = SendMail.SMTPSSL;
                   
                    // Check for CC addresses
                    if (SendMail.CC != null && SendMail.CC != string.Empty)
                    {
                        string[] emailCC = SendMail.CC.Trim().Split(',');
                        foreach (string addressCC in emailCC)
                        {
                            if (addressCC.Contains(SendMail.To) == false)
                            {
                                message.CC.Add(addressCC);
                            }
                        }
                    }

                    // Check for BCC addresses and consider in CC List
                    if (SendMail.BCC != null && SendMail.BCC != string.Empty)
                    {
                        string[] emailBCC = SendMail.BCC.Trim().Split(',');
                        foreach (string addressBCC in emailBCC)
                        {
                            if (addressBCC.Contains(SendMail.To) == false)
                            {
                                message.CC.Add(addressBCC);
                            }
                        }
                    }


                    mySmtpClient.Timeout = 1000000;
                    mySmtpClient.Send (message );
                    strStatus = "Success";

                }
                catch (FormatException ex)
                {
                    strErrorMsg = ex.Message;
                    strStatus = "Failed";
                }
                catch (SmtpException ex)
                {
                    strErrorMsg = ex.Message;
                    strStatus = "Failed";
                }
                catch (Exception ex)
                {
                    strErrorMsg = ex.Message;
                    strStatus = "Failed";
                }
                finally
                {
                    //SMTP LOG
                    if (SendMail.ConnectionString != "" && SendMail.ConnectionString != "")
                    {
                        if (SendMail.RequestID == null)
                        {
                            SendMail.RequestID = "";
                        }

                        if (SendMail.CC == null)
                        {
                            SendMail.CC = "";
                        }


                        if (SendMail.MailType == null)
                        {
                            SendMail.MailType = "";
                        }

                        strQuery = "INSERT INTO dbo.STMP_MAIL_ERROR_LOG([TO],[CC],[BCC],[FROM],[SUBJECT],[MAIL_BODY],[STATUS],[ERROR],[LOG_DATE],[request_id],[MAIL_TYPE])";
                        strQuery = strQuery + " SELECT '" + SendMail.To + "','" + SendMail.CC + "', '" + SendMail.BCC + "', '" + SendMail.From + "', '" + SendMail.Subject + "', '" + SendMail.MailBody.Replace("'", "''") + "','" + strStatus + "','" + strErrorMsg + "',getdate(),'" + SendMail.RequestID + "' , '"+ SendMail.MailType +"'";
                        objCommon.executeQuery(SendMail.ConnectionString, strQuery);
                    }
                    message.Dispose();
                    mySmtpClient = null;
                }
            }
            catch (Exception ex)
            {
                strErrorMsg = ex.Message;
                strStatus = "Failed";
            }
        }
        public string getConfirmVal(string strAnswer)
        {
            try
            {
                string strAns = "N";
                string[] strValues;
                strValues = strAnswer.Split(',');
                if (strValues.Length > 0)
                {
                    strAns = strValues[strValues.Length - 1];
                }

                return strAns;
            }
            catch (Exception ex)
            {
                return "N";
            }
        }
        public int executeQuery(string strCon, string strSQL)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                int intRes;
                SqlCommand cmd = new SqlCommand();
                intRes = objDLGeneric.ExecuteQuery(strSQL, strCon);
                return intRes;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public DataTable getMailBody(string strMode, string strKey, string strCon )
        {
            try
            {
                string strResponse = string.Empty;
                DataTable dtDetails = new DataTable();
                DLClsGeneric objDLGeneric = new DLClsGeneric();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@P_MODE", strMode);
                cmd.Parameters.AddWithValue("@P_KEY", strKey);
                cmd.Parameters.AddWithValue("@P_ACTION", "");
                dtDetails = objDLGeneric.SpDataTable("USP_MAIL_BODY_DETAIL", cmd, strCon);
                return dtDetails;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public string getConfigKeyValue(string strKey, string strDesc, string strCon)
        {
            string strResponse = string.Empty;
            DataTable dtDetails = new DataTable();
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Parameters.AddWithValue("@P_KEY", strKey);
                cmd.Parameters.AddWithValue("@P_DESC", strDesc);
                dtDetails = objDLGeneric.SpDataTable("USP_GET_CONFIG_DETAILS", cmd, strCon);
                strResponse= dtDetails.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                  dtDetails = null;
                  objDLGeneric = null;
                  cmd = null;
            }
            return strResponse;
        }
        public string EncryptData(string clearText)
        {
            string EncryptionKey = "^GuN!Shi~PalLak$3008#";
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
        public string DecryptData(string cipherText)
        {
            string EncryptionKey = "^GuN!Shi~PalLak$3008#"; 
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
        public string GetIPAddress()
        {
            string strHostName = "";
            strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;
            return addr[addr.Length - 1].ToString();
        }
        public bool isValidInput(string strRegexVal, string strInput)
        {
            bool isValidateInput;
            isValidateInput=true;
            
            try
            {
                string[] RegexVal = strRegexVal.Split('~');
                foreach (string Val in RegexVal)
                {
                    if (strInput.Contains(Val)) 
                    { 
                        isValidateInput = false;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                isValidateInput = false;
            }

            return isValidateInput;
        }
        public void setLabelMessage(Label lblMessage, string type)
        {
            lblMessage.Visible = true;

            if ((type == null )|| (type == string.Empty))
            {
                type = "INFO";
            }

            if (type.ToUpper().Trim()=="INFO")
            {
                lblMessage.Attributes.Add("style", "color:Blue;");
            }

            if (type.ToUpper().Trim() == "SUCCESS")
            {
                lblMessage.Attributes.Add("style", "color:Green;");
            }

            if (type.ToUpper().Trim() == "ERROR_DB")
            {
                lblMessage.Attributes.Add("style", "color:Orange;");
            }
            
            if (type.ToUpper().Trim() == "ERROR")
            {
                lblMessage.Attributes.Add("style", "color:Red;");
            }
 
        }
        public string checkPageAccessiblity(string strUserID, string strPageName, string strConn)
        {
            string strResponse = string.Empty;
            DataTable dtDetails = new DataTable();
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Parameters.AddWithValue("@P_USER_ID", strUserID);
                cmd.Parameters.AddWithValue("@P_PAGE_NAME", strPageName);
                dtDetails = objDLGeneric.SpDataTable("usp_CHECK_PAGE_ACCESSIBILITY", cmd, strConn);

                if ( dtDetails==null)
                {
                    strResponse="N";
                }
                else
                {
                    if (dtDetails.Rows[0][0].ToString() == "1")
                        strResponse = "Y";

                    if (dtDetails.Rows[0][0].ToString() == "0")
                        strResponse = "N";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtDetails = null;
                objDLGeneric = null;
                cmd = null;
            }
            return strResponse;
        }
 
        //migration
        /// <summary>
        /// Get Mail Body  
        /// </summary>
        /// <param name="strMode"></param>
        /// <param name="strKey"></param>
        /// <param name="strCon"></param>
        /// <returns></returns>
        public DataTable getMailBody(string strProcess, string strMode, string strKey, string strCon)
        {
            try
            {
                string strResponse = string.Empty;
                DataTable dtDetails = new DataTable();
                DLClsGeneric objDLGeneric = new DLClsGeneric();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@P_MODE", strMode);
                cmd.Parameters.AddWithValue("@P_KEY", strKey);
                cmd.Parameters.AddWithValue("@P_ACTION", "");
                dtDetails = objDLGeneric.SpDataTable("USP_GETSMTP_MAILBODY_DETAILS", cmd, strCon);
                return dtDetails;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
     
        /// <summary>
        /// Get SMS/Mail Body Details 
        /// </summary>
        /// <param name="strModeSMS"></param>
        /// <param name="strKeySMS"></param>
        /// <param name="strModeMAIL"></param>
        /// <param name="strKeyMAIL"></param>
        /// <param name="strCon"></param>
        /// <returns></returns>
        public DataTable GetSMS_Mail_Body_Details(string strModeSMS, string strKeySMS, string strModeMAIL, string strKeyMAIL, string strCon)
        {
            try
            {
                string strResponse = string.Empty;
                DataTable dtDetails = new DataTable();
                DLClsGeneric objDLGeneric = new DLClsGeneric();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@P_KEY_SMS", strKeySMS);
                cmd.Parameters.AddWithValue("@P_DESC_SMS", strModeSMS);
                cmd.Parameters.AddWithValue("@P_KEY_MAIL", strKeyMAIL);
                cmd.Parameters.AddWithValue("@P_DESC_MAIL", strModeMAIL);
                dtDetails = objDLGeneric.SpDataTable("USP_GET_SMS_MAIL_BODY_DETAILS", cmd, strCon);
                return dtDetails;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// Get Mail Body  
        /// </summary>
        /// <param name="strDesc"></param>
        /// <param name="strKey"></param>
        /// <param name="strCon"></param>
        /// <returns></returns>
        public DataTable getSMSBody(string strDesc, string strKey, string strCon)
        {
            try
            {
                string strResponse = string.Empty;
                DataTable dtDetails = new DataTable();
                DLClsGeneric objDLGeneric = new DLClsGeneric();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@P_KEY", strKey);
                cmd.Parameters.AddWithValue("@P_DESC", strDesc);
                dtDetails = objDLGeneric.SpDataTable("USP_GET_SMS_DETAILS", cmd, strCon);
                return dtDetails;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public string getTransactionID(string strSessionId)
        {
            try
            {
                var chars = "A0B1C2D3E4F5G6H7IJ8KL9M0N1O2P3Q4R5S6T7U8V9W0X1Y2Z3a4b5c6d7e8f9g0h1i2jk3l4m5n6o7p8q0r9s0t1u2v3w4x5y6z0123456789" + strSessionId;

                var stringChars = new char[15];
                var random = new Random();

                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }

                var finalString = new String(stringChars);

                return "TR" + finalString;

            }

            catch (Exception ex)
            {
                return "";
            }
        }
    }
}