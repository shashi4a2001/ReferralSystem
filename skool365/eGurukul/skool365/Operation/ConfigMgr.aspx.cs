using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLCMR;
using System.Configuration;
using System.Data;
using System.Globalization;
 
    public partial class Operation_ConfigMgr : System.Web.UI.Page
    {
        ObjPortalUser user;
    string _strAlertMessage = string.Empty;
    string _strMsgType = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objCommon = new clsCommon();
 
        try
        {
            //Validate User Authetication Starts
            if (Context.User.Identity.IsAuthenticated)
            {
                //Check User Session and Connection String 
                if (Session["PortalUserDtl"] != null)
                {
                    user = (ObjPortalUser)Session["PortalUserDtl"];
                    //Check Browser Address
                    if (objCommon.GetIPAddress() != user.IPAddress)
                    {
                        Session.Abandon();
                        Response.Redirect("login.aspx?Exp=Y", false);
                    }
                }
                else
                {
                    Session.Abandon();
                    Response.Redirect("login.aspx?Exp=Y", false);
                    return;
                }
            }
            else
            {
                Session.Abandon();
                Response.Redirect("login.aspx?Exp=Y", false);
                return;
            }
            //Validate User Authetication Ends

            if (IsPostBack == false)
            {
                txtSMTPPort.Attributes.Add("onkeypress", "javascript:return IsNumeric( )");
                //Get Configuration Settings value
                getConfigurationSettings();
                btnSubmit.Attributes.Add("OnClick", "javascript:Confirm('Do you really want to continue the process?');");
            }
            //Check Page Accessibility
            if (user != null)
            {
                if (objCommon.checkPageAccessiblity(user.EmailId, System.IO.Path.GetFileName(Request.Path), user.ConnectionString) != "Y")
                    Response.Redirect("~/Operation/AccessDenied.aspx", false);
            }
        }
        catch (Exception ex)
        {
            _strMsgType = "Error";
            _strAlertMessage = ex.Message;
            objclsErrorLogger.WriteError(System.IO.Path.GetFileName(Request.Path), "", user == null ? "" : user.ConnectionString, ex);
            Response.Redirect("~/Operation/Err.aspx");
        }
        finally
        {
            //Alert Message
            if (_strAlertMessage != string.Empty && _strAlertMessage.Trim() != "")
            {
                lblMessage.Text = _strAlertMessage;
                objCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);

                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }
            objclsErrorLogger = null;
            objCommon = null;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dtInfo;
        clsConfiguration objEC_Settings = new clsConfiguration();
        clsCommon objclsCommon = new clsCommon();
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        int smtpPort, pop3Port, pop3DefaultPort, windowstimer, SOVFileSize,MaxRequestSubmit;

        try
        {
            //User Confirmation
            string confirmValue = objclsCommon.getConfirmVal(Request.Form["confirm_value"]);
            if (confirmValue == "No") return;//Process Aborted

            //Validations Starts
            if (txtSMTPHost.Text.Trim() == string.Empty)
            {
                _strMsgType = "Info";
                _strAlertMessage = "SMTP Host Address can not be left blank";
                txtSMTPHost.Focus();
                return;
            }
            if (txtSMTPPort.Text.Trim() == string.Empty)
            {
                _strMsgType = "Info";
                _strAlertMessage = "SMTP Port No can not be left blank";
                txtSMTPPort.Focus();
                return;
            }
            if (txtSMTPUSerID.Text.Trim() == string.Empty)
            {
                _strMsgType = "Info";
                _strAlertMessage = "SMTP User ID can not be left blank";
                txtSMTPUSerID.Focus();
                return;
            }
            if (txtSMTPPassword.Text.Trim() == string.Empty)
            {
                _strMsgType = "Info";
                _strAlertMessage = "SMTP Password can not be left blank";
                txtSMTPPassword.Focus();
                return;
            }

            if (txtPOP3Host.Text.Trim() == string.Empty)
            {
                _strMsgType = "Info";
                _strAlertMessage = "POP3 Host Address can not be left blank";
                txtPOP3Host.Focus();
                return;
            }
            if (txtPOP3Port.Text.Trim() == string.Empty)
            {
                _strMsgType = "Info";
                _strAlertMessage = "POP3 Port No can not be left blank";
                txtPOP3Port.Focus();
                return;
            }
            if (txtPOP3User.Text.Trim() == string.Empty)
            {
                _strMsgType = "Info";
                _strAlertMessage = "POP3 User ID can not be left blank";
                txtPOP3User.Focus();
                return;
            }
            if (txtPOP3Password.Text.Trim() == string.Empty)
            {
                _strMsgType = "Info";
                _strAlertMessage = "POP3 Password can not be left blank";
                txtPOP3Password.Focus();
                return;
            }

            if (chkPOPByDefualt.Checked == true && txtDefaultPOP3Port.Text.Trim() == string.Empty)
            {
                _strAlertMessage = "POP3 Port No can not be left blank";
                txtDefaultPOP3Port.Focus();
                return;
            }

            if (txtWidnowsTimer.Text.Trim() == string.Empty)
            {
                _strMsgType = "Info";
                _strAlertMessage = "Windows Timer can not be blank";
                txtWidnowsTimer.Focus();
                return;
            }

            if (txtSOVExtesntion.Text.Trim() == string.Empty)
            {
                _strMsgType = "Info";
                _strAlertMessage = "SOV File Extension can not be blank";
                txtSOVExtesntion.Focus();
                return;
            }

            int.TryParse(txtSOVFileSize.Text.Trim(), out SOVFileSize);


            if (SOVFileSize <= 0)
            {
                _strMsgType = "Info";
                _strAlertMessage = "Please neter Valid File Size";
                txtSOVExtesntion.Focus();
                return;
            }

            if (SOVFileSize / 1024 > 10)
            {
                _strMsgType = "Info";
                _strAlertMessage = "File Size can not be greater than 10 MB";
                txtSOVExtesntion.Focus();
                return;
            }

            if (txtMaxRequestSubmit.Text.Trim() == string.Empty)
            {
                _strMsgType = "Info";
                _strAlertMessage = "Max Request Submission count can not be blank";
                txtMaxRequestSubmit.Focus();
                return;
            }

            int.TryParse(txtMaxRequestSubmit.Text.Trim(), out MaxRequestSubmit);


            if ( MaxRequestSubmit <= 0)
            {
                _strMsgType = "Info";
                _strAlertMessage = "Please neter Valid count";
                txtMaxRequestSubmit.Focus();
                return;
            }

            

            //Validations Ends
            int.TryParse(txtSMTPPort.Text.Trim(), out smtpPort);
            int.TryParse(txtSMTPPort.Text.Trim(), out pop3Port);
            int.TryParse(txtDefaultPOP3Port.Text.Trim(), out pop3DefaultPort);
            int.TryParse(txtWidnowsTimer.Text.Trim(), out windowstimer);


            objEC_Settings.SMTPHost = txtSMTPHost.Text.Trim();
            objEC_Settings.SMTPPort = smtpPort;
            objEC_Settings.SMTPUserId = txtSMTPUSerID.Text.Trim();
            objEC_Settings.SMTPPasswordInsert = txtSMTPPassword.Text.Trim();
            objEC_Settings.SMTPSSL = chkEnabelSSL.Checked ? "Y" : "N";

            //POP3
            objEC_Settings.PO3PHost = txtPOP3Host.Text.Trim();
            objEC_Settings.POP3Port = pop3Port;
            objEC_Settings.POP3UserId = txtPOP3User.Text.Trim();
            objEC_Settings.POP3PasswordInsert = txtPOP3Password.Text.Trim();
            objEC_Settings.POPByDefaultPort = chkPOPByDefualt.Checked ? "Y" : "N";
            objEC_Settings.POPDefaultPort = pop3DefaultPort;
            objEC_Settings.POP3SSL = chkPOP3SSL.Checked ? "Y" : "N";


            objEC_Settings.WindowsServiceTime = windowstimer;
            objEC_Settings.SOVFileExtension = txtSOVExtesntion.Text.Trim();
            objEC_Settings.SOVFileSize = SOVFileSize;

            objEC_Settings.MaxRequestSubmit  = MaxRequestSubmit ;

            objEC_Settings.SMSLink = "";
            objEC_Settings.AllowModifyMasterData = "Y";
            objEC_Settings.RestrictedValInsert = txtInputRestVal.Text;

            objEC_Settings.LoggedInUser = user.UserId;
            objEC_Settings.ConnectionString = user.ConnectionString;
            objEC_Settings.Mode = "U";

            objEC_Settings.ValidateLoginByCaptcha = chkAplliedLoginCaptcha.Checked ? "Y" : "N";

            dtInfo = objEC_Settings.InsertDeleteUpdateSettings(objEC_Settings);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    if ((dtInfo.Columns.Contains("Error") == true) && (dtInfo.Rows[0]["Error"].ToString().Trim() != ""))  //Return Either Error or Db Information
                    {
                        _strMsgType = "Error_DB";
                        _strAlertMessage = dtInfo.Rows[0]["Error"].ToString();
                        return;
                    }

                    _strMsgType = "Success";
                    _strAlertMessage = dtInfo.Rows[0]["MSG"].ToString();
                }
            }

        }
        catch (Exception ex)
        {
            _strAlertMessage = ex.Message;
            _strMsgType = "Error";
            objclsErrorLogger.WriteError(System.IO.Path.GetFileName(Request.Path), "", user == null ? "" : user.ConnectionString, ex);
            Response.Redirect("~/Operation/Err.aspx");
        }
        finally
        {
            if (_strAlertMessage != string.Empty && _strAlertMessage != "")
            {
                lblMessage.Text = _strAlertMessage;
                objclsCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);

                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }

            dtInfo = null;
            objEC_Settings = null;
            objclsErrorLogger = null;
            objclsCommon = null;
        }
    }
        #region"Methods & Function"
        private void getConfigurationSettings()
        {

            DataTable dtInfo;
            clsConfiguration objEC_Settings = new clsConfiguration();

        try
        {
            objEC_Settings.Mode = "S";
            objEC_Settings.ConnectionString = user.ConnectionString;

            dtInfo = objEC_Settings.getSettingsDetails(objEC_Settings);
            if (dtInfo != null)
            {
                for (int intLoop = 0; intLoop <= dtInfo.Rows.Count - 1; intLoop++)
                {
                    //SMTP HOST NAME
                    if (dtInfo.Rows[intLoop]["KEY"].ToString().ToUpper() == "SMTP_HOST_ADDRESS")
                    {
                        txtSMTPHost.Text = dtInfo.Rows[intLoop]["Value"].ToString();
                    }
                    //SMTP PORT NO
                    if (dtInfo.Rows[intLoop]["KEY"].ToString().ToUpper() == "SMTP_PORT_NO")
                    {
                        txtSMTPPort.Text = dtInfo.Rows[intLoop]["Value"].ToString();
                    }
                    //SMTP USER ID
                    if (dtInfo.Rows[intLoop]["KEY"].ToString().ToUpper() == "SMTP_USER_ID")
                    {
                        txtSMTPUSerID.Text = dtInfo.Rows[intLoop]["Value"].ToString();
                    }
                    //SMTP PASSWORD
                    if (dtInfo.Rows[intLoop]["KEY"].ToString().ToUpper() == "SMTP_PASSWORD")
                    {
                        txtSMTPPassword.Text = dtInfo.Rows[intLoop]["Value"].ToString();
                    }
                    //SMTP SSL
                    if (dtInfo.Rows[intLoop]["KEY"].ToString().ToUpper() == "SMTP_SSL")
                    {
                        if (dtInfo.Rows[intLoop]["Value"].ToString().ToUpper() == "Y")
                        {
                            chkEnabelSSL.Checked = true;
                        }
                        else
                        {
                            chkEnabelSSL.Checked = false;
                        }
                    }

                    //POP3 HOST NAME
                    if (dtInfo.Rows[intLoop]["KEY"].ToString().ToUpper() == "POP3_HOST_ADDRESS")
                    {
                        txtPOP3Host.Text = dtInfo.Rows[intLoop]["Value"].ToString();
                    }
                    //POP3 PORT NO
                    if (dtInfo.Rows[intLoop]["KEY"].ToString().ToUpper() == "POP3_PORT_NO")
                    {
                        txtPOP3Port.Text = dtInfo.Rows[intLoop]["Value"].ToString();
                    }
                    //POP3 USER ID
                    if (dtInfo.Rows[intLoop]["KEY"].ToString().ToUpper() == "POP3_USER_ID")
                    {
                        txtPOP3User.Text = dtInfo.Rows[intLoop]["Value"].ToString();
                    }
                    //POP3 PASSWORD
                    if (dtInfo.Rows[intLoop]["KEY"].ToString().ToUpper() == "POP3_PASSWORD")
                    {
                        txtPOP3Password.Text = dtInfo.Rows[intLoop]["Value"].ToString();
                    }

                    //SMTP SSL
                    if (dtInfo.Rows[intLoop]["KEY"].ToString().ToUpper() == "POP3_SSL")
                    {
                        if (dtInfo.Rows[intLoop]["Value"].ToString().ToUpper() == "Y")
                        {
                            chkPOP3SSL.Checked = true;
                        }
                        else
                        {
                            chkPOP3SSL.Checked = false;
                        }
                    }


                    //RETRIEVE_USING_TCIP_CLIENT
                    if (dtInfo.Rows[intLoop]["KEY"].ToString().ToUpper() == "RETRIEVE_USING_TCIP_CLIENT")
                    {
                        chkPOPByDefualt.Checked = dtInfo.Rows[intLoop]["Value"].ToString() == "Y" ? true : false; ;
                    }

                    //PORP 3DEFAULT PORT
                    if (dtInfo.Rows[intLoop]["KEY"].ToString().ToUpper() == "TCIP_DEFAULT_PORT")
                    {
                        txtDefaultPOP3Port.Text = dtInfo.Rows[intLoop]["Value"].ToString();
                    }

                    //PORP 3DEFAULT PORT
                    if (dtInfo.Rows[intLoop]["KEY"].ToString().ToUpper() == "TCIP_DEFAULT_PORT")
                    {
                        txtDefaultPOP3Port.Text = dtInfo.Rows[intLoop]["Value"].ToString();
                    }

                    //Windows Timer
                    if (dtInfo.Rows[intLoop]["KEY"].ToString().ToUpper() == "WIN_SERVICE_TIMER")
                    {
                        txtWidnowsTimer.Text = dtInfo.Rows[intLoop]["Value"].ToString();
                    }

                    //SOV Files
                    if (dtInfo.Rows[intLoop]["KEY"].ToString().ToUpper() == "SOV_FILE_EXTENSIONS")
                    {
                        txtSOVExtesntion.Text = dtInfo.Rows[intLoop]["Value"].ToString();
                    }

                    if (dtInfo.Rows[intLoop]["KEY"].ToString().ToUpper() == "SOV_FILE_SIZE")
                    {
                        txtSOVFileSize.Text = dtInfo.Rows[intLoop]["Value"].ToString();
                    }

                    if (dtInfo.Rows[intLoop]["KEY"].ToString().ToUpper() == "RESTRICTED_INPUT_CHARACTERS")
                    {
                        txtInputRestVal.Text = dtInfo.Rows[intLoop]["Value"].ToString();
                    }
                    //MAX_REQUEST_SUBMIT
                    if (dtInfo.Rows[intLoop]["KEY"].ToString().ToUpper() == "MAX_REQUEST_SUBMIT")
                    {
                        txtMaxRequestSubmit.Text = dtInfo.Rows[intLoop]["Value"].ToString();
                    }

                    //LOGIN RE VALIDATE USING CAPTCHA
                    if (dtInfo.Rows[intLoop]["KEY"].ToString().ToUpper() == "RELOGIN_USING_CAPTCHA_VALIDATION")
                    {
                        if (dtInfo.Rows[intLoop]["Value"].ToString().ToUpper() == "Y")
                        {
                            chkAplliedLoginCaptcha.Checked = true;
                        }
                        else
                        {
                            chkAplliedLoginCaptcha.Checked = false;
                        }
                    }
                }
                txtSMTPHost.Focus();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dtInfo = null;
            objEC_Settings = null;
        }
        }
        #endregion
    }
 