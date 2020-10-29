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

public partial class Operation_ActivateRegistration : System.Web.UI.Page
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
                getDetailsPending("");
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
 
    private void getRegistrationStatusCount()
    {
        clsOlympiad objclsOlympiad = new clsOlympiad();
        objOlympiad Olympiad = new objOlympiad();
        DataTable dtDetails;
        try
        {

            Olympiad.ConnectionString = user.ConnectionString;
            dtDetails = objclsOlympiad.getStudentSignupStatusCount(Olympiad);
         
            if (dtDetails!=null && dtDetails.Rows.Count > 0)
            {
                lblAccInfo.Text = "[Active Registration :- <font color='green'>" + dtDetails.Rows[0]["Active_Count"].ToString() + "</font> Pending Registration:<font color='Red'> " + dtDetails.Rows[0]["Pending_Count"].ToString() + "</font> Deactivate Registration:<font color='gray'>" + dtDetails.Rows[0]["InActive_Count"].ToString() + "]";
            }
            else
            { 
                lblAccInfo.Text = "[Active Registration :- 0 Registration:- 0 Registration:- 0]";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objclsOlympiad = null;
            Olympiad = null;
            dtDetails = null;
        }
    }
    private void getDetailsPending(string strText)
    {
        clsOlympiad objclsOlympiad = new clsOlympiad();
        objOlympiad Olympiad = new objOlympiad();
        DataTable dtDetails;
        try
        {
            Olympiad.USerID = string.Empty;
            Olympiad.searchText = strText;
            Olympiad.Status = "1";
            Olympiad.ConnectionString = user.ConnectionString;
            dtDetails = objclsOlympiad.getStudentSignupRequest(Olympiad);

            grdStyled.DataSource = dtDetails;
            grdStyled.DataBind();


            if (grdStyled.Rows.Count > 0)
            {
                grdStyled.Columns[0].Visible = true;
                grdStyled.Columns[1].Visible = false;
            }

            btnSearch.Enabled = true;

            getRegistrationStatusCount();

            UpdatePanel2.Update();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objclsOlympiad = null;
            Olympiad = null;
            dtDetails = null;
        }
    }
    private void getDetailsDeactivated(string strText)
    {
        clsOlympiad objclsOlympiad = new clsOlympiad();
        objOlympiad Olympiad = new objOlympiad();
        DataTable dtDetails;
        try
        {
            Olympiad.USerID = string.Empty;
            Olympiad.searchText = strText;
            Olympiad.Status = "99";
            Olympiad.ConnectionString = user.ConnectionString;
            dtDetails = objclsOlympiad.getStudentSignupRequest(Olympiad);

            grdStyled.DataSource = dtDetails;
            grdStyled.DataBind();

            btnSearch.Enabled = true;
            if (grdStyled.Rows.Count > 0)
            {
                grdStyled.Columns[0].Visible = false;
                grdStyled.Columns[1].Visible = true;
            }
            getRegistrationStatusCount();
            UpdatePanel2.Update();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objclsOlympiad = null;
            Olympiad = null;
            dtDetails = null;
        }
    }
    private void getDetailsActived(string strText)
    {
        clsOlympiad objclsOlympiad = new clsOlympiad();
        objOlympiad Olympiad = new objOlympiad();
        DataTable dtDetails;
        try
        {
            Olympiad.USerID = string.Empty;
            Olympiad.searchText = strText;
            Olympiad.Status = "0";
            Olympiad.ConnectionString = user.ConnectionString;
            dtDetails = objclsOlympiad.getStudentSignupRequest(Olympiad);

            grdStyled.DataSource = dtDetails;
            grdStyled.DataBind();

            btnSearch.Enabled = true;
            if (grdStyled.Rows.Count > 0)
            {
                grdStyled.Columns[0].Visible = false;
                grdStyled.Columns[1].Visible = false;
            }
            getRegistrationStatusCount();
            UpdatePanel2.Update();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objclsOlympiad = null;
            Olympiad = null;
            dtDetails = null;
        }
    }
    protected void grdStyled_PageIndexChanging(Object sender, GridViewPageEventArgs e)
    {
        grdStyled.PageIndex = e.NewPageIndex;
        getDetailsPending(txtSearch.Text.Trim());
    } 
    protected void grdStyled_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        clsCommon objclsCommon = new clsCommon();
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsOlympiad objclsOlympiad = new clsOlympiad();
        objOlympiad olympiad = new objOlympiad();
        DataTable dtResult;
        string _strAlertMessage = string.Empty;
        int SchoolId;

        try
        {
            lblMessage.Text = string.Empty;
            int.TryParse(grdStyled.DataKeys[e.RowIndex]["SIGNUP_ID"].ToString(), out SchoolId);
            if (SchoolId > 0)
            {
                olympiad.SignupId = SchoolId;
                olympiad.ConnectionString = user.ConnectionString;
                olympiad.USerID = user.UserId;

                dtResult = objclsOlympiad.ActivateDeActivatedSignupRequests(olympiad);
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

                //Refresh the grid
                getDetailsDeactivated(txtSearch.Text.Trim());
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
            if (_strAlertMessage != string.Empty)
            {
                lblMessage.Text = _strAlertMessage;
                objclsCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);
                UpdatePanel2.Update();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }
            objclsCommon = null;
            objclsErrorLogger = null;
            olympiad = null;
            objclsOlympiad = null;
        }

    }
    protected void grdStyled_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strActive = DataBinder.Eval(e.Row.DataItem, "STS").ToString().Trim();

            if (strActive == "0")
            {
                e.Row.BackColor = System.Drawing.Color.LightGreen;
            }
            else if (strActive == "1")
            {
                e.Row.BackColor = System.Drawing.Color.LightYellow;
            }
            else if (strActive == "99")
            {
                e.Row.BackColor = System.Drawing.Color.LightGray;
            }
        }
    }
    private void clearControls()
    {
        clsCommon objClsCommon = new clsCommon();
        try
        {
            txtSearch.Text = "";
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message ;
            objClsCommon.setLabelMessage(lblMessage, "Error");
            ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);
            UpdatePanel2.Update();
        }
        finally
        {
            objClsCommon = null;
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
            if(rdoPendindg.Checked ) getDetailsPending(txtSearch.Text.Trim());
            if (rdoDeactivated.Checked) getDetailsDeactivated(txtSearch.Text.Trim());
            if (rdoActive.Checked) getDetailsActived(txtSearch.Text.Trim());
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
    protected void grdStyled_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Confirm")
        {
            lblMessage.Text = "";
            int schoolID;
            string strPassword = string.Empty;

            clsCommon objClsCommon = new clsCommon();
            clsErrorLogger objclsErrorLogger = new clsErrorLogger();
            clsOlympiad objclsOlympiad = new clsOlympiad();
            objOlympiad olympiad = new objOlympiad();
            DataTable dtResult;
            try
            {

                GridViewRow oItem = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int RowIndex = oItem.RowIndex;
                int.TryParse(grdStyled.DataKeys[RowIndex]["SIGNUP_ID"].ToString(), out schoolID);
             
                strPassword = objClsCommon.getDynamicPassWord(user.ConnectionString);
                olympiad.SignupId = schoolID;
                olympiad.USerID = user.UserId;
                olympiad.Password = objClsCommon.EncryptData(strPassword);
                olympiad.ConnectionString = user.ConnectionString;
                dtResult = objclsOlympiad.ConfirmStudentSignupRequests(olympiad);

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

                //Send Activation Mail starts
                int smtpPort;
                string strEmail = grdStyled.Rows[RowIndex].Cells[7].Text.ToString();

                if (strEmail.Trim() != "")
                {
                    ObjSendEmail SendMail = new ObjSendEmail();
                    DataTable dtInformation = null;

                    dtInformation = objClsCommon.getMailBody("MailFormat", "MAIL ON USER CREATION", user.ConnectionString);

                    try
                    {
                        if (dtInformation != null & dtInformation.Rows.Count > 0)
                        {
                           
                            SendMail.From = user.SMTPUSerID;
                            SendMail.To =   strEmail;
                            SendMail.CC = objClsCommon.getConfigKeyValue("EMAIL IDS FOR CC OF MAIL MESSAGE FOR EACH OPERATION", "EMAIL RECEPIENTS GROUP", user.ConnectionString);
                            SendMail.SMTPHost = user.SMTPServer;
                            int.TryParse(user.SMTPPort, out smtpPort);
                            SendMail.SMTPPort = smtpPort;
                            SendMail.UserId = user.SMTPUSerID;
                            SendMail.SMTPSSL = user.SMTPSSL == "Y" ? true : false; ;

                            SendMail.Subject = user.Orig_Name + ": New User Creation Acknowledgement";

                            SendMail.Password = user.SMTPPassword;
                            SendMail.MailBody = dtInformation.Rows[0]["MAIL_BODY"].ToString();
                            SendMail.MailBody = SendMail.MailBody.Replace("#UserID#", strEmail);
                            SendMail.MailBody = SendMail.MailBody.Replace("#Name#", grdStyled.Rows[RowIndex].Cells[4].Text.ToString());
                            SendMail.MailBody = SendMail.MailBody.Replace("#UserName#", grdStyled.Rows[RowIndex].Cells[4].Text.ToString());
                            SendMail.MailBody = SendMail.MailBody.Replace("#Role#", "Student");
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
                //Send Activation Mail Ends
                //Refresh the grid
                getDetailsPending(txtSearch.Text.Trim());
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
                    UpdatePanel2.Update();

                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
                }
                objClsCommon = null;
                olympiad = null;
                objclsOlympiad = null;
                objclsErrorLogger = null;
                dtResult = null;
            } 
        }
    }
}
 