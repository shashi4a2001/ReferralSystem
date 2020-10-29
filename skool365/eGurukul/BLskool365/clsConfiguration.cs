using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLCMR;
using BLCMR;
using System.Data.SqlClient;

namespace BLCMR
{
    public class clsConfiguration
    {
        private string _Mode;
        private string _ID;
        private string _Value;
        private string _SMSLink;
        private string _SMTPHost;
        private int _SMTPPort;
        private string _SMTPUserID;
        private string _SMTPPassword;
        private string _SMTPSSL;
        private string _PO3SSL;
        private string _POP3Host;
        private int _POP3Port;
        private string _POP3UserID;
        private string _POPByDefaultPort;
        private int _POPDefaultPort;
        private string _POP3Password;
        private string _AllowModifyMasterData;
        private string _LoggedInUser;
        private string _ConnectionString;
        private int _intWindowsServiceTime;
        private string _SOVFileExtension;
        private int _SOVFileSize;
        private string _strRestrictedVal;
        private string _strValidateLoginByCaptcha;
        private int _MaxRequestSubmit;

        clsCommon objCommon = new clsCommon();

        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        public string SMSLink
        {
            get { return _SMSLink; }
            set { _SMSLink = value; }
        }

        public string SMTPHost
        {
            get { return _SMTPHost; }
            set { _SMTPHost = value; }
        }

        public int SMTPPort
        {
            get { return _SMTPPort; }
            set { _SMTPPort= value; }
        }

        public string SMTPUserId
        {
            get { return _SMTPUserID; }
            set { _SMTPUserID= value; }
        }

        public string SMTPPasswordInsert
        {
            set { _SMTPPassword = objCommon.EncryptData(value); }
            get { return  _SMTPPassword; }
        }

        public string SMTPPassword
        {
            get { return  objCommon.DecryptData( _SMTPPassword); }
        }

        public string SMTPSSL
        {
            get { return _SMTPSSL; }
            set { _SMTPSSL = value; }
        }


        public string POP3SSL
        {
            get { return _PO3SSL; }
            set { _PO3SSL = value; }
        }

        public string PO3PHost
        {
            get { return _POP3Host; }
            set { _POP3Host = value; }
        }

        public int POP3Port
        {
            get { return _POP3Port; }
            set { _POP3Port = value; }
        }

        public string POP3UserId
        {
            get { return _POP3UserID; }
            set { _POP3UserID = value; }
        }

        public string POP3PasswordInsert
        {
            set { _POP3Password = objCommon.EncryptData(value); }
            get { return _POP3Password; }
        }

        public string POP3Password
        {
            get { return  objCommon.DecryptData( _POP3Password); }
        }

        public string POPByDefaultPort
        {
            get { return _POPByDefaultPort; }
            set { _POPByDefaultPort = value; }
        }

        public int WindowsServiceTime
        {
            get { return _intWindowsServiceTime; }
            set { _intWindowsServiceTime = value; }
        }

        public string SOVFileExtension
        {
            get { return _SOVFileExtension; }
            set { _SOVFileExtension = value; }
        }

        public int SOVFileSize
        {
            get { return _SOVFileSize; }
            set { _SOVFileSize = value; }
        }

        public int MaxRequestSubmit
        {
            get { return _MaxRequestSubmit; }
            set { _MaxRequestSubmit = value; }
        }

        public int POPDefaultPort
        {
            get { return _POPDefaultPort; }
            set { _POPDefaultPort = value; }
        }

        public string AllowModifyMasterData
        {
            get { return _AllowModifyMasterData; }
            set { _AllowModifyMasterData = value; }
        }

        public string LoggedInUser
        {
            get { return _LoggedInUser; }
            set { _LoggedInUser = value; }
        }

        public string ConnectionString
        {
            get { return _ConnectionString; }
            set { _ConnectionString = value; }
        }

        public string RestrictedVal
        {
            get { return objCommon.DecryptData( _strRestrictedVal); }
            set { _strRestrictedVal = value; }
        }

        public string RestrictedValInsert
        {
            get { return  _strRestrictedVal ; }
            set { _strRestrictedVal = value; }
        }

        public string ValidateLoginByCaptcha
        {
            get { return _strValidateLoginByCaptcha; }
            set { _strValidateLoginByCaptcha = value; }
        }
 

        public DataTable InsertDeleteUpdateSettings( clsConfiguration  Settings)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            DataTable dtInfo;
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (Settings.ID == null || Settings.ID.Trim() == "")
                {
                    cmd.Parameters.AddWithValue("@P_SMTP_HOST", Settings.SMTPHost);
                    cmd.Parameters.AddWithValue("@P_SMTP_PORT", Settings.SMTPPort);
                    cmd.Parameters.AddWithValue("@P_SMTP_USER_ID", Settings.SMTPUserId);
                    cmd.Parameters.AddWithValue("@P_SMTP_PASSWORD", Settings.SMTPPasswordInsert);
                    cmd.Parameters.AddWithValue("@P_SMTP_SSL", Settings.SMTPSSL);

                    cmd.Parameters.AddWithValue("@P_POP3_SSL", Settings.POP3SSL);
                    cmd.Parameters.AddWithValue("@P_POP3_HOST", Settings.PO3PHost);
                    cmd.Parameters.AddWithValue("@P_POP3_PORT", Settings.POP3Port);
                    cmd.Parameters.AddWithValue("@P_POP3_USER_ID", Settings.POP3UserId);
                    cmd.Parameters.AddWithValue("@P_POP3_PASSWORD", Settings.POP3PasswordInsert);
                    cmd.Parameters.AddWithValue("@P_POP3_BY_DEFAULT_PORT", Settings.POPByDefaultPort);
                    cmd.Parameters.AddWithValue("@P_POP3_DEFAULT_PORT", Settings.POPDefaultPort);
                    cmd.Parameters.AddWithValue("@P_ALLOW_MODIFY_MASTER_DATA", Settings.AllowModifyMasterData);
                    cmd.Parameters.AddWithValue("@P_WIN_SERVICE_TIMER", Settings.WindowsServiceTime);
                    cmd.Parameters.AddWithValue("@P_SOV_FILE_EXTENSIONS", Settings.SOVFileExtension);
                    cmd.Parameters.AddWithValue("@P_SOV_FILE_SIZE", Settings.SOVFileSize);
                    cmd.Parameters.AddWithValue("@P_RESTRICTED_INPUT_CHARACTERS", Settings.RestrictedValInsert);
                    cmd.Parameters.AddWithValue("@P_LOGIN_VALIDATE_CAPTCHA", Settings.ValidateLoginByCaptcha);
                    cmd.Parameters.AddWithValue("@P_MAX_REQUEST_SUBMIT", Settings.MaxRequestSubmit); 
                }
                else
                {
                    cmd.Parameters.AddWithValue("@P_ID", Settings.ID);
                    cmd.Parameters.AddWithValue("@P_VALUE", Settings.Value);
                }

                cmd.Parameters.AddWithValue("@P_USER_ID", Settings.LoggedInUser);
                cmd.Parameters.AddWithValue("@P_Mode", Settings.Mode);
                dtInfo = objDLGeneric.SpDataTable("dbo.USP_INS_DEL_UPD_CONFIGURATION", cmd, Settings.ConnectionString );

                return dtInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
              objDLGeneric = null;
              dtInfo = null;
              cmd  = null;
            }
        }

        public DataTable getSettingsDetails( clsConfiguration Settings)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            DataTable dtInfo;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Parameters.AddWithValue("@P_Mode", Settings.Mode);
                dtInfo = objDLGeneric.SpDataTable("dbo.USP_INS_DEL_UPD_CONFIGURATION", cmd, Settings.ConnectionString);
                return dtInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDLGeneric = null;
                dtInfo = null;
                cmd = null;
            }
        }

     
    }
}