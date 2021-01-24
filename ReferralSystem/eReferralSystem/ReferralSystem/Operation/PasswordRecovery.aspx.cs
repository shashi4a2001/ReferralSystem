using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLCMR;
using DLCMR;
using System.Configuration;

public partial class Operation_PasswordRecovery : System.Web.UI.Page
{
    string _strAlertMessage = string.Empty;
    string _strMsgType = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            try
            {
                clsCommon objclsCommon = new clsCommon();
                string link = Request.QueryString["hlink"];
                link = objclsCommon.DecryptData(link);
                ViewState["PasswordRecoveryClientId"] = "";

                string ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["CMRConnection"]);
                ConnectionString = objclsCommon.DecryptData(ConnectionString);

                DLClsGeneric objDLGeneric = new DLClsGeneric();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@ClientId", link);
                DataTable dt = objDLGeneric.SpDataTable("usp_SelectClientByClientId", cmd, ConnectionString);

                if (dt != null && dt.Rows.Count > 0)
                {
                    lblUserId.Text = dt.Rows[0]["LoginId"].ToString() + "(" + dt.Rows[0]["ClientName"].ToString() + ")";
                    ViewState["PasswordRecoveryClientId"] = link;
                }
                else
                {
                    btnSubmit.Visible = false;
                    lblmsg.Text = "Unathorized Access..";
                    return;
                }
            }
            catch (Exception ex)
            {
                btnSubmit.Visible = false;
                lblmsg.Text = ex.Message.ToString();
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
            
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["PasswordRecoveryClientId"].ToString() == "")
            {
                lblmsg.Text = "Unathorized Access..";
                txtNewPassword.Focus();
                return;
            }
            _strMsgType = "Info";

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

            lblmsg.ForeColor = System.Drawing.Color.Red;
            clsErrorLogger objclsErrorLogger = new clsErrorLogger();
            clsCommon objclsCommon = new clsCommon();

            string ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["CMRConnection"]);
            ConnectionString = objclsCommon.DecryptData(ConnectionString);


            string ClientId = ViewState["PasswordRecoveryClientId"].ToString();
            if (string.IsNullOrEmpty(ClientId))
            {
                lblmsg.Text = "Unathorized Access..";
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                string NewPassword = objclsCommon.EncryptData(txtNewPassword.Text.Trim());

                DLClsGeneric objDLGeneric = new DLClsGeneric();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@ClientId", ClientId);
                cmd.Parameters.AddWithValue("@NewPassword", NewPassword);
                cmd.Parameters.AddWithValue("@Type", "R");
                string msg = objDLGeneric.SpExecuteScalar("usp_UpdateUserPassword", cmd, ConnectionString);
                if (msg == "Success")
                {
                    Panel1.Visible = false;
                    Panel2.Visible = true;
                }
                else
                {
                    lblmsg.Text = msg;
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                }

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
        isValidFormat = System.Text.RegularExpressions.Regex.IsMatch(strPassword, @"^.*(?=.{7,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@'#.$;%^&+=!""()*,-/:<>?]).*$");
        return isValidFormat;
    }
}