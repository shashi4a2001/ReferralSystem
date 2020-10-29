/**
 *  @2019 RESERVED PALLAV SOLUTIONS
 *  PURPOSE : ALLOW VALID USER TO LOGIN INTO THE SYSTEM
 *  CHANGE HISTORY :
 **/
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using BLCMR;
using System.Threading;
using System.Net;
using System.Web.Security;
using System.Security ;
using System.Security.Authentication;

public partial class Operation_Login : System.Web.UI.Page
{
    string M_strcon = string.Empty;
    string M_UserId = string.Empty;
    string _strAlertMessage = string.Empty;
    string _strMsgType = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objClsCommon = new clsCommon();
        try
        {
            if (IsPostBack == false )
            {
                lblWorkLocation.Text = objClsCommon.DecryptData(ConfigurationManager.AppSettings["Organization"].ToString());
                Page.Title = "@2019" + lblWorkLocation.Text;
            }
        }
        catch (ThreadAbortException)
        {
        }
        catch (Exception ex)
        {
            _strAlertMessage = ex.Message;
            objclsErrorLogger.WriteError(System.IO.Path.GetFileName(Request.Path), "", M_strcon, ex);
        }
        finally
        {
 
            //Alert Message
            if (_strAlertMessage != string.Empty && _strAlertMessage.Trim() != "")
            {
                lblMessage.Text = _strAlertMessage;
                objClsCommon.setLabelMessage(lblMessage, "");
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);

                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }

            objclsErrorLogger = null;
            objClsCommon = null;
        }

    }
    /// <summary>
    /// Proceed to login into the system
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        ObjPortalUser user = new ObjPortalUser();
        STPclsLogin objSTPclsLogin = new STPclsLogin();
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objclsCommon = new clsCommon();
        HttpCookie cookie;
        DataTable dtInfo;

        string _strUserID = string.Empty;
        string _strEmailID = string.Empty;
        string _strPassword = string.Empty;
        string _strAlertMessage = string.Empty;
        int uID,schoolID;

        //GST SPECIFIC
        double dblCGST = 0;
        double dblSGST = 0;
        double dblIGST = 0;
        string strGSTNO = string.Empty;

        try
        {

            lblMessage.Text = string.Empty;

            _strEmailID = txtEmail.Text.Trim();
            _strPassword = objclsCommon.EncryptData(txtPassword.Text.Trim());
 
            user.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["CMRConnection"]);
            user.IPAddress = objclsCommon.EncryptData(objclsCommon.GetIPAddress());

            dtInfo = objSTPclsLogin.GetLogin(_strUserID, _strEmailID, _strPassword, user.IPAddress, user.ConnectionString);

            if (dtInfo != null && dtInfo.Rows.Count > 0)
            {

                if ((dtInfo.Columns.Contains("Error") == true) && (dtInfo.Rows[0]["Error"].ToString().Trim() != ""))  //Return Either Error or Db Information
                {
                    _strMsgType = "Error_DB";
                    _strAlertMessage = dtInfo.Rows[0]["Error"].ToString();

                    //Check Captcha
                    if ((dtInfo.Columns.Contains("CAPTCHA") == true) && (dtInfo.Rows[0]["CAPTCHA"].ToString().Trim() != ""))  //Return Either Error or Db Information
                    {
                        user.UserId = _strEmailID;
                        user.EmailId = _strEmailID;
                        user.Password = _strPassword;
                        user.CaptchaCode = dtInfo.Rows[0]["CAPTCHA"].ToString().Trim();
                        user.UserName = dtInfo.Rows[0]["USER_NAME"].ToString().Trim() ;
                        user.CaptchaValidity = dtInfo.Rows[0]["CAPTCHA_VALIDITY"].ToString().Trim();

                        //Organizational details
                        user.Orig_Code = Convert.ToString(dtInfo.Rows[0]["Orig_Code"].ToString());
                        user.Orig_Name = Convert.ToString(dtInfo.Rows[0]["Orig_Name"].ToString());
                        user.Orig_Address1 = Convert.ToString(dtInfo.Rows[0]["Orig_Address1"].ToString());
                        user.Orig_Address2 = Convert.ToString(dtInfo.Rows[0]["Orig_Address2"].ToString());
                        user.Orig_Address3 = Convert.ToString(dtInfo.Rows[0]["Orig_Address3"].ToString());
                        user.Orig_Phone1 = Convert.ToString(dtInfo.Rows[0]["Orig_Phone1"].ToString());
                        user.Orig_Phone2 = Convert.ToString(dtInfo.Rows[0]["Orig_Phone2"].ToString());
                        user.Orig_Url = Convert.ToString(dtInfo.Rows[0]["Orig_Url"].ToString());
                        user.Orig_Email1 = Convert.ToString(dtInfo.Rows[0]["Orig_Email1"].ToString());
                        user.Orig_Email2 = Convert.ToString(dtInfo.Rows[0]["Orig_Email2"].ToString());
                        user.Orig_Logo1 = Convert.ToString(dtInfo.Rows[0]["Orig_Logo1"].ToString());
                        user.Orig_Logo2 = Convert.ToString(dtInfo.Rows[0]["Orig_Logo2"].ToString());
                        user.Orig_Year_Reserved = Convert.ToString(dtInfo.Rows[0]["Orig_Year_Reserved"].ToString());

                        double.TryParse(dtInfo.Rows[0]["Orig_CGST"].ToString(), out dblCGST);
                        double.TryParse(dtInfo.Rows[0]["Orig_SGST"].ToString(), out dblSGST);
                        double.TryParse(dtInfo.Rows[0]["Orig_IGST"].ToString(), out dblIGST);


                        //GST INFORMATION
                        user.CGSTPer  = dblCGST;
                        user.SGSTPer = dblSGST;
                        user.IGSTPer = dblIGST ;
                        user.Orig_GSTNO = Convert.ToString(dtInfo.Rows[0]["Orig_GSTNO"].ToString());  

                        //Store in Session Object
                        Session["PortalUserDtl"] = user;
                        sendCaptchCode(user);

                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
                        Response.Redirect("~/Operation/LoginValidate.aspx", false);
                    }
                    return;
                }


                int.TryParse(dtInfo.Rows[0]["ID"].ToString(), out uID);
                user.UId = uID; 
                user.UserId = dtInfo.Rows[0]["USER_ID"].ToString();
                user.UserName = dtInfo.Rows[0]["USER_NAME"].ToString();
                user.UserRole = dtInfo.Rows[0]["ROLE_NAME"].ToString();
                user.EmailId = _strEmailID;

                //smtp/po3
                user.SMTPPort = dtInfo.Rows[0]["SMTP_PORT"].ToString();
                user.SMTPUSerID = dtInfo.Rows[0]["SMTP_USER"].ToString();
                user.SMTPPassword = string.Empty;
                user.SMTPPassword = dtInfo.Rows[0]["SMTP_PASSWORD"].ToString();
                user.SMTPServer = dtInfo.Rows[0]["SMTP_SERVER"].ToString();
                user.SMTPSSL = dtInfo.Rows[0]["SMTP_SSL"].ToString();

                user.POPSSL = dtInfo.Rows[0]["POP3_SSL"].ToString();
                user.POPPort = dtInfo.Rows[0]["POP3_PORT"].ToString();
                user.POPUSerID = dtInfo.Rows[0]["POP3_USER"].ToString(); ;
                user.POPPassword = string.Empty;
                user.POPPassword = dtInfo.Rows[0]["POP3_PASSWORD"].ToString();
                user.POPServer = dtInfo.Rows[0]["POP3_SERVER"].ToString();
                user.POPByDefaultPort = dtInfo.Rows[0]["POPByDefaultPort"].ToString();
                user.POPDefaultPort = dtInfo.Rows[0]["POPDefaultPort"].ToString();

                user.IsUW = dtInfo.Rows[0]["IS_UW"].ToString();
                //int.TryParse(dtInfo.Rows[0]["SCHOOL_ID"].ToString(), out schoolID);
                //user.SchoolID = schoolID;
                user.ActingUW = dtInfo.Rows[0]["USER_NAME"].ToString();
                user.ActingUW_ID = user.UId;

                user.ReqDashobard = dtInfo.Rows[0]["REQ_DASHBOARD"].ToString();
                user.ReqChangePassword = dtInfo.Rows[0]["REQ_CHANGE_PASSWORD"].ToString();
                user.LandingPage = dtInfo.Rows[0]["LANDING_PAGE"].ToString();
                user.SelectUW = dtInfo.Rows[0]["SELECT_UW"].ToString();
                user.FirstTimeLoggedIn = dtInfo.Rows[0]["FirstTimeLoggedIn"].ToString();
                user.SOVFileExtns = dtInfo.Rows[0]["SOVFileExtns"].ToString();
                user.RestrictedInputChar = dtInfo.Rows[0]["Restricted_Input_Char"].ToString();

                user.LastLogInTime = Convert.ToString(dtInfo.Rows[0]["LAST_LOGIN"].ToString());
                user.LastPasswordChangeTime = Convert.ToString(dtInfo.Rows[0]["LAST_PASSWORD_CHANGE"].ToString());

                int SOVFileSize;
                int.TryParse(dtInfo.Rows[0]["SOVFileSize"].ToString(), out SOVFileSize);
                user.SOVFileSize = SOVFileSize;

                //Organizational details
                user.Orig_Code = Convert.ToString(dtInfo.Rows[0]["Orig_Code"].ToString());
                user.Orig_Name = Convert.ToString(dtInfo.Rows[0]["Orig_Name"].ToString());
                user.Orig_Address1 = Convert.ToString(dtInfo.Rows[0]["Orig_Address1"].ToString());
                user.Orig_Address2 = Convert.ToString(dtInfo.Rows[0]["Orig_Address2"].ToString());
                user.Orig_Address3 = Convert.ToString(dtInfo.Rows[0]["Orig_Address3"].ToString());
                user.Orig_Phone1 = Convert.ToString(dtInfo.Rows[0]["Orig_Phone1"].ToString());
                user.Orig_Phone2 = Convert.ToString(dtInfo.Rows[0]["Orig_Phone2"].ToString());
                user.Orig_Url = Convert.ToString(dtInfo.Rows[0]["Orig_Url"].ToString());
                user.Orig_Email1 = Convert.ToString(dtInfo.Rows[0]["Orig_Email1"].ToString());
                user.Orig_Email2 = Convert.ToString(dtInfo.Rows[0]["Orig_Email2"].ToString());
                user.Orig_Logo1 = Convert.ToString(dtInfo.Rows[0]["Orig_Logo1"].ToString());
                user.Orig_Logo2 = Convert.ToString(dtInfo.Rows[0]["Orig_Logo2"].ToString());
                user.Orig_Year_Reserved = Convert.ToString(dtInfo.Rows[0]["Orig_Year_Reserved"].ToString());
                double.TryParse(dtInfo.Rows[0]["Orig_CGST"].ToString(), out dblCGST);
                double.TryParse(dtInfo.Rows[0]["Orig_SGST"].ToString(), out dblSGST);
                double.TryParse(dtInfo.Rows[0]["Orig_IGST"].ToString(), out dblIGST);

                //GST INFORMATION
                user.CGSTPer = dblCGST;
                user.SGSTPer = dblSGST;
                user.IGSTPer = dblIGST;
                user.Orig_GSTNO = Convert.ToString(dtInfo.Rows[0]["Orig_GSTNO"].ToString());  


                int LogId;
                int.TryParse(dtInfo.Rows[0]["LOG_ID"].ToString(), out LogId);
                user.LogId = LogId;

                string strUrl = string.Empty;

                if (dtInfo.Rows[0]["FirstTimeLoggedIn"].ToString() == "Y")
                {
                    strUrl = "Operation/ChangePassword.aspx?UserID=";
                }
                else
                {
                    strUrl = user.LandingPage;
                }

                // Create a new ticket used for authentication
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                   user.LogId, // Ticket version
                   user.UserId, // Username associated with ticket
                   DateTime.Now, // Date/time issued
                   DateTime.Now.AddMinutes(30), // Date/time to expire
                   false, // "true" for a persistent user cookie
                  user.UserRole, // User-data, in this case the roles
                   FormsAuthentication.FormsCookiePath);// Path cookie valid for

                //Store in Session Object
                Session["PortalUserDtl"] = user;

                // Encrypt the cookie using the machine key for secure transport
                string hash = FormsAuthentication.Encrypt(ticket);
                cookie = new HttpCookie(
                  FormsAuthentication.FormsCookieName, // Name of auth cookie
                  hash); // Hashed ticket

                // Set the cookie's expiration time to the tickets expiration time
                if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;

                //Add the cookie to the list for outgoing response
                //Response.Cookies.Add(cookie);

                // Redirect to requested URL, or homepage if no previous page
                // requested
                string returnUrl = Request.QueryString["ReturnUrl"];
                if (returnUrl == null) returnUrl = strUrl;

                // Don't call FormsAuthentication.RedirectFromLoginPage since it
                // could
                // replace the authentication ticket (cookie) we just added

                FormsAuthentication.RedirectFromLoginPage(user.UserName, false);
                Response.Redirect(returnUrl, false);
            }
            else
            {
                _strMsgType = "Info";
                _strAlertMessage = "Invalid Login...";
            }
        }
        catch (ThreadAbortException)
        {
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
                objclsCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }

            user = null;
            objSTPclsLogin = null;
            objclsErrorLogger = null;
            dtInfo = null;
            objclsCommon = null;
            cookie = null;
        }
    }

    private void sendCaptchCode(ObjPortalUser user)
    {
        if (user.EmailId.Trim() != "")
        {
            ObjSendEmail SendMail = new ObjSendEmail();
            DataTable dtInformation = null;
            clsCommon objClsCommon = new clsCommon();
            int smtpPort;

            dtInformation = objClsCommon.getMailBody("MailFormat", "MAIL ON LOGIN VALIDATE BY CAPTCHA", user.ConnectionString);

            try
            {
                if (dtInformation != null & dtInformation.Rows.Count > 0)
                {
                    int.TryParse(dtInformation.Rows[0]["PORT"].ToString(), out smtpPort);
                    SendMail.From = dtInformation.Rows[0]["FROM"].ToString();
                    SendMail.To = user.EmailId.Trim();
                    SendMail.CC = objClsCommon.getConfigKeyValue("EMAIL IDS FOR CC OF MAIL MESSAGE FOR EACH OPERATION", "EMAIL RECEPIENTS GROUP", user.ConnectionString);

                    SendMail.SMTPHost = dtInformation.Rows[0]["HOST"].ToString();
                    SendMail.SMTPPort = smtpPort;
                    SendMail.UserId = dtInformation.Rows[0]["USER_ID"].ToString();
                    SendMail.Password = objClsCommon.DecryptData ( dtInformation.Rows[0]["PASSWORD"].ToString());
                    SendMail.SMTPSSL = dtInformation.Rows[0]["SMTP_SSL"].ToString() == "Y" ? true : false;

                    SendMail.Subject = "Institute of Paramedical Science & Management: login verification by Captcha";
                    SendMail.MailBody = dtInformation.Rows[0]["MAIL_BODY"].ToString();
                    SendMail.MailBody = SendMail.MailBody.Replace("#ValidTill#", user.CaptchaValidity);
                    SendMail.MailBody = SendMail.MailBody.Replace("#Name#", user.UserName);
                    SendMail.MailBody = SendMail.MailBody.Replace("#CaptchaCode#", user.CaptchaCode);

                    SendMail.ConnectionString = user.ConnectionString;
                    objClsCommon.SendEmail(SendMail);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                SendMail = null;
                dtInformation = null;
                objClsCommon = null;
            }
        }
        //Send Mail Ends
    }
}