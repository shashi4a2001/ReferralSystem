using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Services;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using BLCMR;
using System.Text.RegularExpressions;
using System.Security;
using System.Security.Authentication;

public partial class Operation_ManageUsers : System.Web.UI.Page
{
    string M_strAlertMessage = string.Empty;
    string _strAlertMessage = string.Empty;
    string _strMsgType = string.Empty;
    ObjPortalUser user;

    protected void Page_Load(object sender, EventArgs e)
    {
        clsCommon objCommon = new clsCommon();
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        string _strAlertMessage = string.Empty;
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
                        Session.Clear();
                        Session.RemoveAll();
                        Session.Abandon();
                        FormsAuthentication.SignOut();
                        Response.Redirect("login.aspx?Exp=Y", false);
                    }
                }
                else
                {
                    Session.Clear();
                    Session.RemoveAll();
                    Session.Abandon();
                    FormsAuthentication.SignOut();
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

            if (IsPostBack == false)
            {
                bindRoles();
                getDetails("");
                txtName.Focus();
            }

            //Check Page Accessibility
            if (user != null)
            {
                if (objCommon.checkPageAccessiblity(user.EmailId, System.IO.Path.GetFileName(Request.Path), user.ConnectionString) != "Y")
                    Response.Redirect("~/Operation/AccessDenied.aspx", false);
            }

            Page.Form.Attributes.Add("enctype", "multipart/form-data");

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
        clsUserMaintainance objclsUser = new clsUserMaintainance();
        ObjUser objuser = new ObjUser();
        DataTable dtResult;
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objclsCommon = new clsCommon();

        string _strAlertMessage = string.Empty;
        string confirmValue = string.Empty;
        string strPassword = string.Empty;
        int UID;
 
        try
        {
            lblMessage.Text = "";

            //Vlidation
            if (txtName.Text.Trim() == "")
            {
                _strMsgType = "Info";
                _strAlertMessage = "Enter User Name.";
                txtName.Focus();
                return;
            }

            if (objclsCommon.isValidInput(user.RestrictedInputChar, txtName.Text.Trim()) == false)
            {
                _strMsgType = "Info";
                _strAlertMessage = "User Name contains Invalid Characters";
                txtName.Focus();
                return;
            }

            if (txtEEmail.Text.Trim() == "")
            {
                _strMsgType = "Info";
                _strAlertMessage = "Enter Email Id.";
                txtEEmail.Focus();
                return;
            }

            if (objclsCommon.isValidInput(user.RestrictedInputChar, txtregion.Text.Trim()) == false)
            {
                _strAlertMessage = "Region contains Invalid Characters";
                txtregion.Focus();
                return;
            }

            if (objclsCommon.isValidInput(user.RestrictedInputChar, txtSubmissionBranch.Text.Trim()) == false)
            {
                _strMsgType = "Info";
                _strAlertMessage = "Submission Branch contains Invalid Characters";
                txtSubmissionBranch.Focus();
                return;
            }

            if (objclsCommon.isValidInput(user.RestrictedInputChar, txtEEmail.Text.Trim()) == false)
            {
                _strMsgType = "Info";
                _strAlertMessage = "Email Id contains Invalid Characters";
                txtEEmail.Focus();
                return;
            }


            int RoleID;
            int.TryParse(ddlUserRole.SelectedValue, out RoleID);

            objuser.UserId = txtUSerID.Text.Trim();
            objuser.UserName = txtName.Text.Trim();


            if (btnSubmit.Text == "Save")
            {
                objuser.Mode = "C";
            }
            else
            {
                objuser.Mode = "U";
                int.TryParse(hdId.Value, out UID);
                objuser.UId = UID;
            }

            //Change Password in every Modification
            strPassword = objclsCommon.getDynamicPassWord(user.ConnectionString);
            objuser.Password = objclsCommon.EncryptData(strPassword);

            if (objuser.Mode == "U")
            {
                //If status is going to make Active. Generate and Send Password
                if (chkActivated.Visible == true)
                {
                    if (chkActivated.Checked == true) objuser.Mode = "A";
                }
            }

            objuser.EmailId = txtEEmail.Text.Trim();
            objuser.SubmissionBranch = txtSubmissionBranch.Text.Trim();
            objuser.Region = txtregion.Text.Trim();
            objuser.MobileNo = txtMobileNo.Text.Trim();
            objuser.UserRoleId = RoleID;
            objuser.LogInMachine = getMachineName();
            objuser.ModifiedBy = user.UserId;
            objuser.CreatedBy = user.UserId;
            objuser.ConnectionString = user.ConnectionString;
            objuser.AccountLock = chkByPassAccountLock.Checked ? "Y" : "N";
            objuser.Status = "0";

            dtResult = objclsUser.SaveUser(objuser);

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
                _strMsgType = "Success";
                _strAlertMessage = dtResult.Rows[0]["SUCCESS"].ToString().Trim();
            }

            if (dtResult.Rows.Count > 0)
            {
                if (objuser.Mode == "C" || objuser.Mode == "U")
                {
                    //Send Mail to User
                    int smtpPort;
                    string strEmail = txtEEmail.Text;
                    if (strEmail.Trim() != "")
                    {
                        ObjSendEmail SendMail = new ObjSendEmail();
                        DataTable dtInformation = null;
                        clsCommon objClsCommon = new clsCommon();

                        if (objuser.Mode == "C")//For New User
                        {
                            dtInformation = objClsCommon.getMailBody("MailFormat", "MAIL ON USER CREATION", user.ConnectionString);
                        }

                        if (objuser.Mode == "U")//When information is updated for active users
                        {
                            dtInformation = objClsCommon.getMailBody("MailFormat", "MAIL ON USER ACCOUNT RESET", user.ConnectionString);
                        }

                        if (objuser.Mode == "A")//When existing user is going to make active
                        {
                            dtInformation = objClsCommon.getMailBody("MailFormat", "MAIL ON USER ACCOUNT ACTIVATION", user.ConnectionString);
                        }

                        try
                        {
                            if (dtInformation != null & dtInformation.Rows.Count > 0)
                            {
                                SendMail.From = user.SMTPUSerID;
                                SendMail.To = strEmail;
                                SendMail.CC = objClsCommon.getConfigKeyValue("EMAIL IDS FOR CC OF MAIL MESSAGE FOR EACH OPERATION", "EMAIL RECEPIENTS GROUP", user.ConnectionString);
                                SendMail.SMTPHost = user.SMTPServer;
                                int.TryParse(user.SMTPPort, out smtpPort);
                                SendMail.SMTPPort = smtpPort; 
                                SendMail.UserId = user.SMTPUSerID;
                                SendMail.SMTPSSL = user.SMTPSSL == "Y" ? true : false; ;

                                if (objuser.Mode == "C")//For New User
                                {
                                    SendMail.Subject =user.Orig_Name +": New User Creation Acknowledgement";
                                }

                                if (objuser.Mode == "U")//When information is updated for active users
                                {
                                    SendMail.Subject = user.Orig_Name + ": User Account Reset Acknowledgement";
                                }

                                if (objuser.Mode == "A")//When existing user is going to make active
                                {
                                    SendMail.Subject = user.Orig_Name + ": User Account Activation Acknowledgement";
                                }

                                SendMail.Password = user.SMTPPassword;
                                SendMail.MailBody = dtInformation.Rows[0]["MAIL_BODY"].ToString();
                                SendMail.MailBody = SendMail.MailBody.Replace("#UserID#", objuser.EmailId);
                                SendMail.MailBody = SendMail.MailBody.Replace("#Name#", objuser.UserName);
                                SendMail.MailBody = SendMail.MailBody.Replace("#UserName#", objuser.UserName);
                                SendMail.MailBody = SendMail.MailBody.Replace("#Role#", ddlUserRole.SelectedItem.Text);
                                SendMail.MailBody = SendMail.MailBody.Replace("#Password#", strPassword);
                                SendMail.MailBody = SendMail.MailBody.Replace("#Url#", "<a href='http://www.soaneemrana.com/crm/login.aspx'>Portal Login</a>");

                                //Organization Info
                                SendMail.MailBody = SendMail.MailBody.Replace("#Organization#", user.Orig_Name);
                                SendMail.MailBody = SendMail.MailBody.Replace("#Orig_Address1#", user.Orig_Address1);
                                SendMail.MailBody = SendMail.MailBody.Replace("#Orig_Address2#", user.Orig_Address2);
                                SendMail.MailBody = SendMail.MailBody.Replace("#Orig_Phone1#", user.Orig_Phone1);
                                SendMail.MailBody = SendMail.MailBody.Replace("#Orig_Phone2#", user.Orig_Phone2);
                                SendMail.MailBody = SendMail.MailBody.Replace("#Orig_Email1#", user.Orig_Email1);
                                SendMail.MailBody = SendMail.MailBody.Replace("#Orig_Email2#", user.Orig_Email2);
                                SendMail.MailBody = SendMail.MailBody.Replace("#Orig_Url#", user.Orig_Url);

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
                        }
                    }
                    //Send Mail Ends
                }
                //btnSubmit.Text = "Update";
                lblMessage.Text = _strAlertMessage;
                getDetails(txtSearch.Text.Trim());
                UpdatePanel2.Update();

            }

            clearControls();
            UpdatePanel1.Update();
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "setFocusToEntryTab('EntryTab')", true);

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
                UpdatePanel1.Update();

                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "setFocusToEntryTab('EntryTab')", true);
            objclsUser = null;
            dtResult = null;
            objclsCommon = null;
            objuser = null;
            objclsErrorLogger = null;
        }
    }
 

    private void getDetails(string strText)
    {
        clsUserMaintainance objclsUser = new clsUserMaintainance();
        ObjUser userM = new ObjUser();
        DataTable dt;
        try
        {
            userM.SearchText = strText;
            userM.ConnectionString = user.ConnectionString;
            dt = objclsUser.ShowUsers(userM);

            grdStyled.DataSource = dt;
            grdStyled.DataBind();

            btnSearch.Enabled = true;
            UpdatePanel2.Update();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            userM = null;
            objclsUser = null;
            dt = null;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clearControls();
        lblMessage.Text = "";
        UpdatePanel1.Update();

        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "setFocusToEntryTab('EntryTab')", true);
    }
    protected void grdStyled_PageIndexChanging(Object sender, GridViewPageEventArgs e)
    {
        grdStyled.PageIndex = e.NewPageIndex;
        getDetails(txtSearch.Text.Trim());
    } 

    protected void grdStyled_SelectedIndexChanged(object sender, EventArgs e)
    {
        clsCommon objclsCommon = new clsCommon();
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();

        string _strAlertMessage=string.Empty ;
        string ImgPath = string.Empty;
        int UID, RoleID;

        try
        {
            clearControls();
            lblMessage.Text = string.Empty;

            int.TryParse(grdStyled.DataKeys[grdStyled.SelectedIndex]["UID"].ToString(), out UID);
            int.TryParse(grdStyled.DataKeys[grdStyled.SelectedIndex]["RoleId"].ToString(), out RoleID);
            if (UID>0) 
            {
                hdId.Value = Convert.ToString(UID);
                txtUSerID.Text = Server.HtmlDecode(grdStyled.SelectedRow.Cells[1].Text.Trim());
                txtUSerID.Enabled = false;
                txtName.Enabled = false;
                txtName.Text =  Server.HtmlDecode(grdStyled.SelectedRow.Cells[2].Text.Trim());
                txtEEmail.Text = grdStyled.SelectedRow.Cells[4].Text.Trim();
                txtMobileNo.Text = grdStyled.SelectedRow.Cells[5].Text.Trim();
                //txtSubmissionBranch.Text = grdStyled.SelectedRow.Cells[5].Text.Trim().Replace("&nbsp;", "");
                //txtregion.Text = grdStyled.SelectedRow.Cells[6].Text.Trim().Replace("&nbsp;", "");
                chkByPassAccountLock.Checked  = grdStyled.SelectedRow.Cells[8].Text=="Y"? true:false;
                ddlUserRole.SelectedValue = Convert.ToString(RoleID);
                ddlUserRole.Enabled = false;
                btnSubmit.Text = "Update";
                rwStatus.Visible = true;

                chkActivated.Checked = false;
                chkActivated.Visible = false;

                lblAccountStatus.Text = "Status: Active";
                objclsCommon.setLabelMessage(lblAccountStatus, "Info");

                if (grdStyled.SelectedRow.Cells[6].Text == "I")
                {
                    lblAccountStatus.Text ="Status: Inactive";
                    objclsCommon.setLabelMessage(lblAccountStatus, "Error");
                    chkActivated.Enabled = true;
                    chkActivated.Checked = false;
                    chkActivated.Visible = true;
                }

                if ( grdStyled.SelectedRow.Cells[6].Text == "L")
                {
                    lblAccountStatus.Text =  "Status: Locked";
                    objclsCommon.setLabelMessage(lblAccountStatus, "Error_DB");
                }

            }
            UpdatePanel1.Update();

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "setFocusToEntryTab('EntryTab')", true);
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
            if (_strAlertMessage != string.Empty)
            {
                lblMessage.Text = _strAlertMessage;
                objclsCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);

                UpdatePanel1.Update();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }
            objclsCommon = null;
            objclsErrorLogger = null;
        }
    }
    protected void grdStyled_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        lblMessage.Text = "";
        int UID;
        clsCommon objClsCommon = new clsCommon();
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsUserMaintainance objclsUser = new clsUserMaintainance();
        ObjUser userM = new ObjUser();
        DataTable dtResult;

        try
        {
            int.TryParse(grdStyled.DataKeys[e.RowIndex]["UID"].ToString(), out UID);
            userM.UId = UID;
            userM.ModifiedBy = user.UserId;
            userM.ConnectionString = user.ConnectionString;
            dtResult = objclsUser.DeleteUser(userM);

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
                _strMsgType = "Success";
                _strAlertMessage = dtResult.Rows[0]["SUCCESS"].ToString().Trim();
            }

            //Dind Details
            getDetails(txtSearch.Text.Trim());

            clearControls();
            UpdatePanel2.Update();
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
            if (_strAlertMessage != string.Empty)
            {
                lblMessage.Text = _strAlertMessage;
                objClsCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);

                UpdatePanel1.Update();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "setFocusToEntryTab('ListUser')", true);
            objClsCommon = null;
            objclsUser = null;
            userM = null;
            objclsErrorLogger = null;
            dtResult = null;
        }
    }
    protected void grdStyled_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strActive = DataBinder.Eval(e.Row.DataItem, "STATUS").ToString().Trim();
           
            if (strActive == "A")
            {
                e.Row.BackColor = System.Drawing.Color.LightGreen;
            }
            else if (strActive == "I")
            {
                e.Row.BackColor = System.Drawing.Color.LightGray;
            }
            else if (strActive == "L")
            {
                e.Row.BackColor = System.Drawing.Color.OrangeRed;
            }
        }
    }
    private void clearControls()
    {
        clsCommon objClsCommon = new clsCommon();
        try
        {
            lblAccountStatus.Text = "";
            hdId.Value = "0";
            txtUSerID.Text = "";
            txtUSerID.Enabled = true;
            chkActivated.Checked = true;
            chkActivated.Enabled = false;
            txtName.Text = "";
            txtSearch.Text = "";
            txtMobileNo.Text = "";
            txtName.Enabled = true;
            hdUserGroupId.Value = "0";
            txtEEmail.Text = "";
            txtSubmissionBranch.Text = "";
            ddlUserRole.SelectedIndex = 0;
            ddlUserRole.Enabled = true;
            btnSubmit.Text = "Save";
            txtregion.Text = "";
            UpdatePanel1.Update();
            rwStatus.Visible = false ;
            chkActivated.Checked = false;
            chkByPassAccountLock.Checked = false;
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message ;
            objClsCommon.setLabelMessage(lblMessage, "Error");
            ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);
            UpdatePanel1.Update();
        }
        finally
        {
            objClsCommon = null;
        }
    }

    private void bindRoles()
    {
        clsUserMenuPrevilege objclsUserMenuPrevilege = new clsUserMenuPrevilege();
        DataTable dtInfo;
        try
        {
            dtInfo = objclsUserMenuPrevilege.ShowRole(user.ConnectionString);

            if (dtInfo != null)
            {
                ddlUserRole.DataSource = dtInfo;
                ddlUserRole.DataValueField = "RoleID";
                ddlUserRole.DataTextField = "RoleName";
                ddlUserRole.DataBind();
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dtInfo = null;
            objclsUserMenuPrevilege = null;
        }
    }
    private string getMachineName()
        {
            string clientPCName;
            clientPCName = "NA";
            try
            {
                string[] computer_name = System.Net.Dns.GetHostEntry(
                Request.ServerVariables["remote_host"]).HostName.Split(new Char[] { '.' });
                clientPCName = computer_name[0].ToString();
                return clientPCName;
            }
            catch (Exception ex)
            {
                return clientPCName;
            }
        }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objclsCommon = new clsCommon();
        string _strAlertMessage = string.Empty;

        try
        {
            getDetails(txtSearch.Text.Trim());
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
            if (_strAlertMessage != string.Empty && _strAlertMessage.Trim() != "")
            {
                lblMessage.Text = _strAlertMessage;
                objclsCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }
            objclsCommon = null;
            objclsErrorLogger = null;
        }
    }
 
}
 