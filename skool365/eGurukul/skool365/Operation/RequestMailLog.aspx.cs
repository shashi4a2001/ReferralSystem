using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLCMR;
using System.Configuration;
using System.Globalization;
using System.Text;
using OpenPop.Pop3;
using OpenPop.Mime;

public partial class Operation_RequestMailLog : System.Web.UI.Page
{

    ObjPortalUser user;
    string _strAlertMessage = string.Empty;
    string _strMsgType = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objCommon = new clsCommon();
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

            if (IsPostBack == false)
            {
                bindSubjectHeader();
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
            _strMsgType = "Error_DB";
            _strAlertMessage = ex.Message;
            objclsErrorLogger.WriteError(System.IO.Path.GetFileName(Request.Path), "", user == null ? "" : user.ConnectionString, ex);
            Response.Redirect("~/Operation/Err.aspx");
        }
        finally
        {
            objclsErrorLogger = null;
            objCommon = null;

            //Alert Message
            if (_strAlertMessage != string.Empty && _strAlertMessage.Trim() != "")
            {
                lblMessage.Text = _strAlertMessage;
                objCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);

                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }
        }
    }

    protected void btnReply_Click(object sender, EventArgs e)
    {
        ObjSendEmail SendMail = new ObjSendEmail();
        clsCommon objClsCommon = new clsCommon();
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();

        int smtpPort;
        string _strAlertMessage = string.Empty;
        string strHeaderAdnl = string.Empty;

        try
        {
            string strRequestID = string.Empty;

            if (hdReplyTo.Value != null & hdReplyTo.Value.ToString().Trim() != "")
            {
                SendMail.RequestID = hdRequestID.Value;
                SendMail.From = user.SMTPUSerID;
                SendMail.To = hdReplyTo.Value;
                SendMail.CC = hdReplyCC.Value;

                if (SendMail.CC == string.Empty)
                {
                    SendMail.CC = user.EmailId;
                }
                else
                {
                    if (SendMail.CC.Contains(user.EmailId) == false)
                    {
                        SendMail.CC = SendMail.CC + "," + user.EmailId;
                    }
                }

                SendMail.SMTPHost = user.SMTPServer;
                int.TryParse(user.SMTPPort, out smtpPort);
                SendMail.SMTPPort = smtpPort;
                SendMail.UserId = user.SMTPUSerID;
                SendMail.Password = user.SMTPPassword; ;
                SendMail.SMTPSSL = user.SMTPSSL == "Y" ? true : false; ;

                if (hdSubject.Value.StartsWith("Re:"))
                {
                    SendMail.Subject = hdSubject.Value;
                }
                else
                {
                    SendMail.Subject = "Re:" + hdSubject.Value;
                }

                SendMail.MailBody = txtReply.Text.Trim();

                strHeaderAdnl = txtReply.Text.Trim().Replace("\n", "<BR>") + " <br /><hr style='border-style: solid; background-color: #000066'  />";
                strHeaderAdnl = strHeaderAdnl + " <b>From:</b> " + hdReplyFrom.Value + " ";
                strHeaderAdnl = strHeaderAdnl + " <br />";
                strHeaderAdnl = strHeaderAdnl + " <b>Sent:</b> " + hdReplyDate.Value + "";
                strHeaderAdnl = strHeaderAdnl + " <br />";
                strHeaderAdnl = strHeaderAdnl + " <b>To:</b> " + hdReplyTo.Value + "";
                strHeaderAdnl = strHeaderAdnl + " <br />";
                strHeaderAdnl = strHeaderAdnl + " <b>Subject:</b> " + hdSubject.Value + "";
                strHeaderAdnl = strHeaderAdnl + " <br /><br />";
                strHeaderAdnl = strHeaderAdnl + hdMessage.Value;

                SendMail.MailBody = strHeaderAdnl;// System.Environment.NewLine + System.Environment.NewLine + hdMessage.Value;
                SendMail.ConnectionString = user.ConnectionString;
                objClsCommon.SendEmail(SendMail);

                //Refresh the page
                txtReply.Text = string.Empty;
                SyncMail();
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
                UpdatePanel1.Update();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }

            objClsCommon = null;
            objclsErrorLogger = null;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objclsCommon = new clsCommon();
        string confirmValue = string.Empty;

        try
        {
            SyncMail();
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
            objclsErrorLogger = null;
            objclsCommon = null;
        }
    }
    private void SyncMail()
    {
        objUnderWriter UnderWriter = new objUnderWriter();
        clsUnderWriter objclsUnderWriter = new clsUnderWriter();
        DataTable dtInfo;
        int requestID;
        try
        {
            hdReplyTo.Value = "";
            hdRequestID.Value = "";
            hdSubject.Value = "";

            UnderWriter.ConnectionString = user.ConnectionString;
            int.TryParse(ddlSubject.SelectedValue, out requestID);
            UnderWriter.RequetID = requestID;
            dtInfo = objclsUnderWriter.getUWMailLog(UnderWriter);

            if (dtInfo != null)
            {
                grdStyled.DataSource = dtInfo;
                grdStyled.DataBind();

                //Set Replying Information
                hdMessage.Value = dtInfo.Rows[0]["Message"].ToString();
                hdReplyTo.Value = dtInfo.Rows[0]["To"].ToString();
                hdRequestID.Value = dtInfo.Rows[0]["REQUEST_ID"].ToString();
                hdReplyCC.Value = dtInfo.Rows[0]["CC"].ToString();
                hdSubject.Value = dtInfo.Rows[0]["Subject"].ToString();
                hdReplyFrom.Value = dtInfo.Rows[0]["FROM"].ToString();
                hdReplyDate.Value = dtInfo.Rows[0]["LOG_DATE_DESCRIPTIVE"].ToString();
            }


        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dtInfo = null;
            objclsUnderWriter = null;
        }
    }
    private void bindSubjectHeader()
    {

        DLCMR.DLClsGeneric objDLGeneric = new DLCMR.DLClsGeneric();
        DataTable dtList = new DataTable();

        SqlCommand cmd = new SqlCommand();
        string strSql = string.Empty;

        try
        {

            strSql = "SELECT [MAIL_TYPE],[REQUEST_ID], [LOG_DATE], [SUBJECT] FROM STMP_MAIL_ERROR_LOG(NOLOCK) where [MAIL_TYPE]='I' AND COALESCE(REQUEST_ID,0)>0  ORDER BY [REQUEST_ID] DESC";
            cmd.CommandText = strSql;
            dtList = objDLGeneric.SpDataTableByCommandText(cmd, user.ConnectionString);

            ddlSubject.DataSource = dtList;
            ddlSubject.DataTextField = "SUBJECT";
            ddlSubject.DataValueField = "REQUEST_ID";
            ddlSubject.DataBind();

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDLGeneric = null;
            dtList = null;
            cmd = null;
        }
    }
}