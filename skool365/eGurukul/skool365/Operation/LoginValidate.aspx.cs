/**
 *  @2019 RESERVED PALLAV SOLUTIONS
 *  PURPOSE : VALIDATE USER WITH CAPTCHA ID. CASE WHEN USER STATE IS ACTIVE AND LOGIN IS MADE IN SAME PERIOD
 *  CHANGE HISTORY :
 * 
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
using System.Web.Security;
using System.Threading;
using System.Net;
using System.Security;
using System.Security.Authentication;
using BLCMR;


public partial class Operation_LoginValidate : System.Web.UI.Page
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

            //Check User Session and Connection String 
            if (Session["PortalUserDtl"] != null)
            {
                user = (ObjPortalUser)Session["PortalUserDtl"];

                //Check Browser Address
                if (objCommon.GetIPAddress() != user.IPAddress)
                {
                    Session.Clear();
                    Session.RemoveAll();
                    Session.Abandon();
                    Response.Redirect("login.aspx?Exp=Y", false);
                }
            }
            else
            {
                Session.Clear();
                Session.RemoveAll();
                Session.Abandon();
                Response.Redirect("login.aspx?Exp=Y", false);
            }

            if (IsPostBack == false)
            {
                _strAlertMessage = "Already have the active user session at this time. Please refer your mail for Loging validation using Captcha Code.";
                _strMsgType = "Error";
            }
            //if (objCommon.checkPageAccessiblity(user.EmailId, System.IO.Path.GetFileName(Request.Path), objCommon.DecryptData(Convert.ToString(ConfigurationManager.ConnectionStrings["CMRConnection"]))) != "Y")
            //    Response.Redirect("~/Operation/AccessDenied.aspx", false);

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

            objCommon = null;
            objclsErrorLogger = null;
        }
    }
 
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        STPclsLogin objSTPclsLogin = new STPclsLogin();
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objclsCommon = new clsCommon();
        HttpCookie cookie;
        DataTable dtInfo;

        string _strUserID = string.Empty;
        string _strEmailID = string.Empty;
        string _strPassword = string.Empty;
        string _strAlertMessage = string.Empty;
        int uID, schoolID;

        try
        {

            lblMessage.Text = string.Empty;

            _strEmailID = user.EmailId.Trim();
            _strPassword =  user.Password.Trim(); ;

            user.IPAddress = objclsCommon.EncryptData(objclsCommon.GetIPAddress());

            dtInfo = objSTPclsLogin.GetLogin(_strUserID, _strEmailID, _strPassword, user.IPAddress,  txtCaptcha.Text , user.ConnectionString);

            if (dtInfo != null && dtInfo.Rows.Count > 0)
            {
                if ((dtInfo.Columns.Contains("Error") == true) && (dtInfo.Rows[0]["Error"].ToString().Trim() != ""))  //Return Either Error or Db Information
                {
                    _strMsgType = "Error_DB";
                    _strAlertMessage = dtInfo.Rows[0]["Error"].ToString();
                    return;
                }

                int.TryParse(dtInfo.Rows[0]["ID"].ToString(), out uID);
                user.UId = uID ;
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
                user.ActingUW = dtInfo.Rows[0]["USER_NAME"].ToString();
                user.ActingUW_ID = user.UId;
                int.TryParse(dtInfo.Rows[0]["SCHOOL_ID"].ToString(), out schoolID);
                user.SchoolID = schoolID;

                user.ReqDashobard = dtInfo.Rows[0]["REQ_DASHBOARD"].ToString();
                user.ReqChangePassword = dtInfo.Rows[0]["REQ_CHANGE_PASSWORD"].ToString();
                user.LandingPage = dtInfo.Rows[0]["LANDING_PAGE"].ToString();
                user.SelectUW = dtInfo.Rows[0]["SELECT_UW"].ToString();
                user.FirstTimeLoggedIn = dtInfo.Rows[0]["FirstTimeLoggedIn"].ToString();
                user.SOVFileExtns = dtInfo.Rows[0]["SOVFileExtns"].ToString();
                user.RestrictedInputChar = dtInfo.Rows[0]["Restricted_Input_Char"].ToString();

                int SOVFileSize;
                int.TryParse(dtInfo.Rows[0]["SOVFileSize"].ToString(), out SOVFileSize);
                user.SOVFileSize = SOVFileSize;

                int LogId;
                int.TryParse(dtInfo.Rows[0]["LOG_ID"].ToString(), out LogId);
                user.LogId = LogId;

                string strUrl = string.Empty;

                if (dtInfo.Rows[0]["FirstTimeLoggedIn"].ToString() == "Y")
                {
                    strUrl = "ChangePassword.aspx?UserID=";
                }
                else
                {
                    strUrl = user.LandingPage;
                    strUrl = strUrl.Replace("Operation/",String.Empty );
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
            objSTPclsLogin = null;
            objclsErrorLogger = null;
            dtInfo = null;
            objclsCommon = null;
            cookie = null;
        }
    }
}
 