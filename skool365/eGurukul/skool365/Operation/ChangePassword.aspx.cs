using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLCMR;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Security;
using System.Security;
using System.Security.Authentication;

public partial class Operation_ChangePassword : System.Web.UI.Page
{
    ObjPortalUser user;
    string _strAlertMessage = string.Empty;
    string _strMsgType = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objClsCommon = new clsCommon();

        try
        {
            user = new ObjPortalUser();

            //Validate User Authetication Starts
            if (Context.User.Identity.IsAuthenticated)
            {
                //Check User Session and Connection String 
                if (Session["PortalUserDtl"] != null)
                {
                    user = (ObjPortalUser)Session["PortalUserDtl"];

                    //Check Browser Address
                    if (objClsCommon.GetIPAddress() != user.IPAddress)
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
                    return;
                }
            }
            else
            {
                Session.Clear();
                Session.RemoveAll();
                Session.Abandon();
                Response.Redirect("login.aspx?Exp=Y", false);
                return;
            }

            if (Page.IsPostBack == false)
            {
                if (Request.QueryString.Keys.Count != 0)
                {
                    txtOldPassword.Focus();
                }
            }
            //Check Page Accessibility
            if (Request.QueryString.Keys.Count == 0)//ByPass when redirected from login page
            {
                if (user != null)
                {
                    if (objClsCommon.checkPageAccessiblity(user.EmailId, System.IO.Path.GetFileName(Request.Path), user.ConnectionString) != "Y")
                        Response.Redirect("~/Operation/AccessDenied.aspx", false);
                }
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
                objClsCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }

            objclsErrorLogger = null;
            objClsCommon = null;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dtResult;
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        STPclsLogin objSTPclsLogin = new STPclsLogin();
        clsCommon objclsCommon = new clsCommon();

        try
        {
            _strMsgType = "Info";
            if (txtOldPassword.Text.Trim() == "")
            {
                _strAlertMessage = "Please enter old password.";
                txtOldPassword.Focus();
                return;
            }

            if (txtNewPassword.Text.Trim() == "")
            {
                _strAlertMessage = "Please enter New password.";
                txtNewPassword.Focus();
                return;
            }

            if (txtConfirmPassword.Text.Trim().Length < 6)
            {
                _strAlertMessage = "Minimum password length should be 6";
                txtConfirmPassword.Focus();
                return;
            }

            if (txtNewPassword.Text.Trim().Length < 6)
            {
                _strAlertMessage = "Minimum password length should be 6";
                txtNewPassword.Focus();
                return;
            }

            if (txtConfirmPassword.Text.Trim() == "")
            {
                _strAlertMessage = "Please enter Confirm password.";
                txtConfirmPassword.Focus();
                return;
            }

            if (txtConfirmPassword.Text.Trim() != txtNewPassword.Text.Trim())
            {
                _strAlertMessage = "New Password and Confirm Password must be same";
                txtConfirmPassword.Focus();
                return;
            }

            if (validatePassword(txtConfirmPassword.Text.Trim()) == false)
            {
                txtConfirmPassword.Focus();
                _strAlertMessage = "Passowrd is not strong.";
                return;
            }

            if (objclsCommon.isValidInput(user.RestrictedInputChar, txtNewPassword.Text.Trim()) == false)
            {
                _strAlertMessage = "Paswword contains Invalid Characters";
                txtNewPassword.Focus();
                return;
            }

            if (objclsCommon.isValidInput(user.RestrictedInputChar, txtConfirmPassword.Text.Trim()) == false)
            {
                _strAlertMessage = "Paswword Branch contains Invalid Characters";
                txtConfirmPassword.Focus();
                return;
            }

            lblMessage.Text = "";

            string UserID = user.EmailId;
            string NewPassword = objclsCommon.EncryptData(txtNewPassword.Text.Trim());
            string OldPassword = objclsCommon.EncryptData(txtOldPassword.Text.Trim());

            dtResult = objSTPclsLogin.changePassword(UserID, OldPassword, NewPassword, user.IPAddress, user.ConnectionString);

            if (dtResult != null)
            {
                if (dtResult.Columns.Contains("ERROR"))
                {
                    if (dtResult.Rows[0]["ERROR"].ToString().Trim() != "")
                    {
                        _strMsgType = "Error_DB";
                        _strAlertMessage = dtResult.Rows[0]["ERROR"].ToString().Trim();
                        return;
                    }
                }
                FormsAuthentication.SignOut();
                Session.Clear();
                Session.Abandon();

                _strMsgType = "Success";
                _strAlertMessage = dtResult.Rows[0]["SUCCESS"].ToString().Trim();
                Response.Redirect("../Login.aspx", false);
            }

        }
        catch (ThreadAbortException)
        {
        }
        catch (Exception ex)
        {
            _strAlertMessage = ex.Message;
            objclsErrorLogger.WriteError(System.IO.Path.GetFileName(Request.Path), "", user == null ? "" : user.ConnectionString, ex);
            Response.Redirect("~/Operation/Err.aspx");
        }
        finally
        {
            if (_strAlertMessage != string.Empty)
            {
                lblMessage.Text = _strAlertMessage;
                objclsCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }
            dtResult = null;
            objSTPclsLogin = null;
            objclsErrorLogger = null;
        }
    }
    private Boolean validatePassword(string strPassword)
    {
        Boolean isValidFormat;
        isValidFormat = false;
        isValidFormat = Regex.IsMatch(strPassword, @"^.*(?=.{7,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@'#.$;%^&+=!""()*,-/:<>?]).*$");
        return isValidFormat;
    }
}