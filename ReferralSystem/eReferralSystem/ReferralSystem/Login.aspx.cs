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
using DLCMR;
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
                lblWorkLocation.Text = "Referral System";
                Page.Title = "@2020" + lblWorkLocation.Text;
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
      
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objclsCommon = new clsCommon();
        HttpCookie cookie;
      

        string _strUserID = string.Empty;
        string _strEmailID = string.Empty;
        string _strPassword = string.Empty;
        string _strAlertMessage = string.Empty;
        

        //GST SPECIFIC
        string strGSTNO = string.Empty;

        try
        {

            lblMessage.Text = string.Empty;

            _strEmailID = txtEmail.Text.Trim();
           
 
            user.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["CMRConnection"]);
            user.IPAddress = objclsCommon.EncryptData(objclsCommon.GetIPAddress());

            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@LoginId", txtEmail.Text);
            cmd.Parameters.AddWithValue("@LoginPassword", objclsCommon.EncryptData(txtPassword.Text.Trim()));
            
            DataTable dt = objDLGeneric.SpDataTable("USP_VALIDATE_USER_LOGIN", cmd, user.ConnectionString);
            
             
            if (dt!=null && dt.Rows.Count>0)
            {
                if (dt.Rows[0][0].ToString() == "USEREXISTS")
                {
                    if (dt.Rows[0]["IsFirstTimeLogin"].ToString() != "0")
                    {
                        //First Time Login
                    }
                    if (dt.Rows[0]["IsPasswordExpired"].ToString() != "0")
                    {
                        //Password Expired
                    }

                    string strUrl = "Operation/dashboard.aspx";
                    user.LogId = Convert.ToInt16( dt.Rows[0]["ClientId"]);
                    user.UserId = _strEmailID;
                    user.UserRole = dt.Rows[0]["ClientTypeCode"].ToString();
                    user.UserName  = dt.Rows[0]["ClientName"].ToString();

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


                    // Don't call FormsAuthentication.RedirectFromLoginPage since it
                    // could
                    // replace the authentication ticket (cookie) we just added

                    FormsAuthentication.SetAuthCookie(user.UserId, true);

                    FormsAuthentication.RedirectFromLoginPage(user.UserName, false);
                    Response.Redirect(strUrl, false);

                }
                else
                {
                    _strMsgType = "Info";
                    _strAlertMessage = "Invalid Login...";
                }


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
            
            objclsErrorLogger = null;
          
            objclsCommon = null;
            cookie = null;
        }
    }

 
}