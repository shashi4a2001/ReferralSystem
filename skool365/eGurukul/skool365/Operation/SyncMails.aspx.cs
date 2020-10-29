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
using System.Data;
using System.Globalization;
using System.Text;
using OpenPop.Pop3;
using OpenPop.Mime;
 

public partial class Operation_SyncMails : System.Web.UI.Page
    {
 
        ObjPortalUser user;
    string _strAlertMessage = string.Empty;
    string _strMsgType = string.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {
        clsCommon objCommon = new clsCommon();
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();

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

        clsCommon objCommon = new clsCommon();
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();

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
                objCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);

                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }
            objclsErrorLogger = null;
            objCommon = null;
        }
    }

    private void SyncMail()
    {
        DLCMR.DLClsGeneric objDLGeneric = new DLCMR.DLClsGeneric();
        DataTable dtList = new DataTable();
        DataTable dtListIN = new DataTable();
        SqlCommand cmd = new SqlCommand();
        string strSql = string.Empty;
        DataRow[] drRow;
        string strReqID = string.Empty;
        string Mail_MSGID = string.Empty; ;
        string strMailBody = string.Empty;

        StringBuilder strQR = new StringBuilder();
        try
        {
            DataTable dtMessages = new DataTable();
            dtMessages.Columns.Add("MessageNumber");
            dtMessages.Columns.Add("Message");
            dtMessages.Columns.Add("From");
            dtMessages.Columns.Add("Subject");
            dtMessages.Columns.Add("DateSent");


            strReqID = ddlSubject.SelectedValue;

            //get POP3 Details
            Pop3Client pop3Client;

            pop3Client = new Pop3Client();

            if (user.POPByDefaultPort == "Y")
            {
                pop3Client.Connect(user.POPServer, int.Parse(user.POPDefaultPort), false);
            }
            else
            {
                pop3Client.Connect(user.POPServer, int.Parse(user.POPPort), true);
            }

            pop3Client.Authenticate(user.POPUSerID, user.POPPassword);

            int count = pop3Client.GetMessageCount();

            strSql = "SELECT [MAIL_TYPE],[REQUEST_ID], [LOG_DATE], [SUBJECT] FROM STMP_MAIL_ERROR_LOG(NOLOCK) WHERE [REQUEST_ID]=" + strReqID + " AND COALESCE([MAIL_TYPE],'') IN('I','O') ";
            cmd.CommandText = strSql;
            dtList = objDLGeneric.SpDataTableByCommandText(cmd, user.ConnectionString);

            //List of All Request IDs
            DataView view = new DataView(dtList);
            view.RowFilter = "MAIL_TYPE='I'";
            dtListIN = view.ToTable(true, "REQUEST_ID");

            Message message;
            MessagePart messagePart;
            //Mail Sync Process Started
            for (int inCnt = 0; inCnt <= dtListIN.Rows.Count - 1; inCnt++)
            {
                strReqID = dtListIN.Rows[inCnt]["REQUEST_ID"].ToString();

                drRow = dtList.Select("[REQUEST_ID]='" + strReqID + "' AND MAIL_TYPE='O'");

                // Download all messages incompletely
                for (int i = count; i >= 1; i--)
                {
                    message = pop3Client.GetMessage(i);
                    if (message.Headers.Subject.Contains("[" + strReqID + "]") || message.Headers.Subject.Contains("_" + strReqID + "_"))
                    {
                        //messagePart = message.MessagePart.MessageParts[0];
                        messagePart = message.MessagePart;
                        dtMessages.Rows.Add();

                        Mail_MSGID = message.Headers.MessageId;

                        if (Mail_MSGID == null) Mail_MSGID = "";
                        dtMessages.Rows[0]["MessageNumber"] = i;
                        dtMessages.Rows[0]["From"] = message.Headers.From.Address;
                        dtMessages.Rows[0]["Subject"] = message.Headers.Subject;
                        dtMessages.Rows[0]["DateSent"] = message.Headers.DateSent;

                        MessagePart body = message.FindFirstHtmlVersion();
                        if (body != null)
                        {
                            strMailBody = body.GetBodyAsText();
                        }
                        else
                        {
                            body = message.FindFirstPlainTextVersion();
                            if (body != null)
                            {
                                strMailBody = body.GetBodyAsText();
                            }
                        }
                        dtMessages.Rows[0]["Message"] = strMailBody.Replace("'", "''");

                        strSql = "IF NOT EXISTS(SELECT 1 FROM dbo.STMP_MAIL_ERROR_LOG (NOLOCK) WHERE COALESCE(MAIL_MSG_ID,'')='" + Mail_MSGID.ToString() + "')  BEGIN ";
                        strQR.Append(strSql);
                        strSql = "INSERT INTO dbo.STMP_MAIL_ERROR_LOG([TO],[FROM],[SUBJECT],[MAIL_BODY],[STATUS],[ERROR],[LOG_DATE],REQUEST_ID,MAIL_TYPE,MAIL_MSG_ID) ";
                        strQR.Append(strSql);
                        strSql = " SELECT '" + message.Headers.To[0].ToString() + "', '" + message.Headers.From.Address + "', '" + message.Headers.Subject + "', '" + strMailBody.Replace("'", "''") + "','','',getdate()," + strReqID + ",'O','" + Mail_MSGID.ToString() + "'  END ";
                        strQR.Append(strSql);
                        break;
                    }
                }
            }

            //db iNTERACTION
            if (strQR.Length > 0)
            {
                int rowAffected;
                string _strAlertMessage;
                rowAffected = objDLGeneric.ExecuteQuery(strQR.ToString(), user.ConnectionString);

                if (rowAffected < 0) rowAffected = 0;

                 _strAlertMessage = "[" + rowAffected + "] Mails received succeessfully";

                //Bind in Grid
                grdStyled.DataSource = dtMessages;
                grdStyled.DataBind();


                //Alert Message
                if (_strAlertMessage != string.Empty && _strAlertMessage.Trim() != "")
                {
                    lblMessage.Text = _strAlertMessage;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
                }
            }
            else
            {
                _strAlertMessage = "No any mail archieved.";
                lblMessage.Text = _strAlertMessage;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDLGeneric = null;
            dtList = null;
            dtListIN = null;
            cmd = null;
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

            strSql = "SELECT [MAIL_TYPE],[REQUEST_ID], [LOG_DATE], [SUBJECT] FROM STMP_MAIL_ERROR_LOG(NOLOCK) where [MAIL_TYPE]='I' AND COALESCE(REQUEST_ID,0)>0 ORDER BY [REQUEST_ID] DESC ";
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
 