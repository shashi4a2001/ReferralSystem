using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Security;
using System.Security;
using System.Security.Authentication;
using BLCMR;
using DLCMR;

public partial class Operation_ChangePassword : System.Web.UI.Page
{
    
    string _strAlertMessage = string.Empty;
    string _strMsgType = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Context.User.Identity.IsAuthenticated || Session["PortalUserDtl"] == null)
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("SessionExpired.aspx", false);
            return;
        }

        if (Page.IsPostBack == false)
        {
            txtOldPassword.Focus();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblmsg.ForeColor = System.Drawing.Color.Red;
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        //STPclsLogin objSTPclsLogin = new STPclsLogin();
        clsCommon objclsCommon = new clsCommon();

        try
        {
            _strMsgType = "Info";
            if (txtOldPassword.Text.Trim() == "")
            {
                _strAlertMessage = "Please enter old password.";
                lblmsg.Text = _strAlertMessage;
                txtOldPassword.Focus();
                return;
            }

            if (txtNewPassword.Text.Trim() == "")
            {
                _strAlertMessage = "Please enter New password.";
                lblmsg.Text = _strAlertMessage;
                txtNewPassword.Focus();
                return;
            }

            if (txtConfirmPassword.Text.Trim().Length < 6)
            {
                _strAlertMessage = "Minimum password length should be 6";
                lblmsg.Text = _strAlertMessage;
                txtConfirmPassword.Focus();
                return;
            }

            if (txtNewPassword.Text.Trim().Length < 6)
            {
                _strAlertMessage = "Minimum password length should be 6";
                lblmsg.Text = _strAlertMessage;
                txtNewPassword.Focus();
                return;
            }

            if (txtConfirmPassword.Text.Trim() == "")
            {
                _strAlertMessage = "Please enter Confirm password.";
                lblmsg.Text = _strAlertMessage;
                txtConfirmPassword.Focus();
                return;
            }

            if (txtConfirmPassword.Text.Trim() != txtNewPassword.Text.Trim())
            {
                _strAlertMessage = "New Password and Confirm Password must be same";
                lblmsg.Text = _strAlertMessage;
                txtConfirmPassword.Focus();
                return;
            }

            if (validatePassword(txtConfirmPassword.Text.Trim()) == false)
            {
                txtConfirmPassword.Focus();
                _strAlertMessage = "Passowrd is not strong.";
                lblmsg.Text = _strAlertMessage;
                return;
            }

            //if (objclsCommon.isValidInput(user.RestrictedInputChar, txtNewPassword.Text.Trim()) == false)
            //{
            //    _strAlertMessage = "Paswword contains Invalid Characters";
            //    txtNewPassword.Focus();
            //    return;
            //}

            //if (objclsCommon.isValidInput(user.RestrictedInputChar, txtConfirmPassword.Text.Trim()) == false)
            //{
            //    _strAlertMessage = "Paswword Branch contains Invalid Characters";
            //    txtConfirmPassword.Focus();
            //    return;
            //}

            lblMessage.Text = "";

            ObjPortalUser user;
            user = (ObjPortalUser)Session["PortalUserDtl"];

            string UserID = user.UserId ;
            string ClientId = user.LogId.ToString();
            string OldPassword = objclsCommon.EncryptData(txtOldPassword.Text.Trim());
            string NewPassword = objclsCommon.EncryptData(txtNewPassword.Text.Trim());
           

            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@UserId", UserID);
            cmd.Parameters.AddWithValue("@OldPassword", OldPassword);
            cmd.Parameters.AddWithValue("@NewPassword", NewPassword);
            string msg= objDLGeneric.SpExecuteScalar ("usp_UpdateUserPassword", cmd, user.ConnectionString);
            if (msg == "Success")
            {
                lblmsg.ForeColor = System.Drawing.Color.Green;
                lblmsg.Text = "Password Changed Successfully...";
                lblmsg.ForeColor = System.Drawing.Color.Green;
                SendMail(ClientId);
                Response.Redirect("LogOut.aspx?msg=2");
            }
            else
            {
                lblmsg.Text = msg;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }

        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message.ToString();
            lblmsg.ForeColor = System.Drawing.Color.Red;
        }
    }
    private Boolean validatePassword(string strPassword)
    {
        Boolean isValidFormat;
        isValidFormat = false;
        isValidFormat = Regex.IsMatch(strPassword, @"^.*(?=.{7,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@'#.$;%^&+=!""()*,-/:<>?]).*$");
        return isValidFormat;
    }

    private bool SendMail(string clientid)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        ObjPortalUser user;
        user = (ObjPortalUser)Session["PortalUserDtl"];

        bool status = false;
        try
        {
            ObjSendEmail SendMail = new ObjSendEmail();

            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@ClientId", clientid);
            cmd.Parameters.AddWithValue("@TemplateType", "PasswordChanged");
            DataSet ds = objDLGeneric.SpDataSet("usp_SelectEmailMsg", cmd, user.ConnectionString);

            DataTable dtSMTP = ds.Tables[0];
            DataTable dtEmailTemplate = ds.Tables[1];

            if (ds != null && dtSMTP != null && dtEmailTemplate != null && 
                dtSMTP.Rows.Count>0 && dtEmailTemplate.Rows.Count > 0 &&
                Convert.ToString(dtEmailTemplate.Rows[0]["ToEmailId"])!=""
                )
            {
                SendMail.From = Convert.ToString(dtSMTP.Rows[0]["SenderEmailID"]);
                //SendMail.CC = objClsCommon.getConfigKeyValue("EMAIL IDS FOR CC OF MAIL MESSAGE FOR EACH OPERATION", "EMAIL RECEPIENTS GROUP", user.ConnectionString);
                SendMail.SMTPHost = Convert.ToString(dtSMTP.Rows[0]["HostName"]);
                SendMail.SMTPPort = Convert.ToInt16(dtSMTP.Rows[0]["PortNo"]);
                SendMail.UserId = Convert.ToString(dtSMTP.Rows[0]["UserId"]);
                SendMail.Password = Convert.ToString(dtSMTP.Rows[0]["UserPwd"]);
                string enablessl = Convert.ToString(dtSMTP.Rows[0]["EnableSSL"]).ToLower();
                if (enablessl == "y" || enablessl == "yes" || enablessl == "1")
                    SendMail.SMTPSSL = true;
                else
                    SendMail.SMTPSSL = false;
               
                

                SendMail.Subject = Convert.ToString(dtEmailTemplate.Rows[0]["SubjectLine"]);
                SendMail.To = Convert.ToString(dtEmailTemplate.Rows[0]["ToEmailId"]);
                SendMail.MailBody = Convert.ToString(dtEmailTemplate.Rows[0]["TemplateText"]);

                SendMail.ConnectionString = user.ConnectionString;

                clsCommon objclsCommon = new clsCommon();
                objclsCommon.SendEmail(SendMail);
            }
        }
        catch (Exception ex)
        {
            status = false;
            objclsErrorLogger.WriteError(System.IO.Path.GetFileName(Request.Path), "Problem In Sending Mail To ClientId: "+ clientid, user == null ? "" : user.ConnectionString, ex);

            return status;
        }
        
        status = true;
        return status;
    }
}