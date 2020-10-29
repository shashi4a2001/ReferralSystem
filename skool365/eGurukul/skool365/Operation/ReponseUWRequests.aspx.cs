using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using BLCMR;
using System.Configuration;
using System.Data;
using System.Text;
using System.Net;
using System.Net.Mail;

public partial class Operation_ReponseUWRequests : System.Web.UI.Page
{
    string _strAlertMessage = string.Empty;
    string _strMsgType = string.Empty;
    string M_strAlertMessage = string.Empty;
    ObjPortalUser user;

    protected void Page_Load(object sender, EventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objCommon = new clsCommon();
 
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
                //Bind Year
                for (int Year = 1990; Year <= System.DateTime.Today.Year; Year++)
                {
                    ddlYear.Items.Add(Year.ToString());
                }

                ddlYear.SelectedValue = System.DateTime.Today.Year.ToString();
                //Underwriter Request
                getUnderwriterRequest();

                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "setFocusToEntryTab('UWList')", true);
                UpdatePanel2.Update();
            }

            Page.Form.Attributes.Add("enctype", "multipart/form-data");

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

            objCommon = null;
            objclsErrorLogger = null;

        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        clsUserMaintainance objclsUser = new clsUserMaintainance();
        ObjUser objuser = new ObjUser();
    
        clsCommon objclsCommon = new clsCommon();

        DataTable dtResult;
        DataTable dtMailThreadResult;
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsUnderWriter objclsUnderWriter = new clsUnderWriter();
        objUnderWriter Underwriter = new objUnderWriter();
   
        string confirmValue = string.Empty;
        string strSOVName = string.Empty;
        string strSubjectName = string.Empty;
        string strMailThread = string.Empty;
        string strMailHeader = string.Empty;
        int smtpPort, UWID, requestID;

        try
        {
            lblMessage.Text = "";

            if(  lblReqID.Text.Trim() == string.Empty || lblUnderwriter.Text.Trim() == string.Empty )
            {
                _strAlertMessage = "Info";
                _strAlertMessage = "Please select Request from List.";
                return;
            }
            if(txtRequest.Text.Trim() == string.Empty)
            {
                _strAlertMessage = "Info";
                _strAlertMessage = "Response mail body is missing.";
                return;
            }

            Underwriter.Comment = txtRequest.Text ;
            Underwriter.ConnectionString =  user.ConnectionString;
            int.TryParse(hdId.Value, out requestID);
            Underwriter.RequetID = requestID; 
            int.TryParse(hdUWId.Value, out UWID);

            ObjSendEmail SendMail = new ObjSendEmail();
            DataTable dtInformation;
            clsCommon objClsCommon = new clsCommon();
            string strRequestID = string.Empty;

            //Get Sent Mail Body
            dtMailThreadResult = getRequestPrvMailBody(Underwriter.RequetID, UWID.ToString());

            if (dtMailThreadResult != null)
            {
                strMailThread = dtMailThreadResult.Rows[0]["Message"].ToString();

                strMailHeader = txtRequest.Text.Trim().Replace("\n", "<BR>")  +" <br /><hr style='border-style: solid; background-color: #000066'  />";
                strMailHeader = strMailHeader + " <b>From:</b> " + dtMailThreadResult.Rows[0]["From"].ToString() + " ";
                strMailHeader = strMailHeader + " <br />";
                strMailHeader = strMailHeader + " <b>Sent:</b> " + dtMailThreadResult.Rows[0]["LOG_DATE_DESCRIPTIVE"].ToString() + "";
                strMailHeader = strMailHeader + " <br />";
                strMailHeader = strMailHeader + " <b>To:</b> " + dtMailThreadResult.Rows[0]["TO"].ToString() + "";
                strMailHeader = strMailHeader + " <br />";
                strMailHeader = strMailHeader + " <b>CC:</b> " + dtMailThreadResult.Rows[0]["CC"].ToString() + "";
                strMailHeader = strMailHeader + " <br />";
                strMailHeader = strMailHeader + " <b>Subject:</b> " + dtMailThreadResult.Rows[0]["Subject"].ToString() + "";
                strMailHeader = strMailHeader + " <br /><br />" + strMailThread;
            }

            //Send Mail to Underwriter on New/Modifiy Request
            dtInformation = objClsCommon.getMailBody("MailFormat", "MAIL ON ANALYST UW REQUEST QUERY",  user.ConnectionString);

            try
            {
                
                if (dtInformation != null & dtInformation.Rows.Count > 0)
                {
                    int.TryParse(hdId.Value.ToString(), out requestID);
                    Underwriter.RequetID = requestID ;

                    SendMail.From = user.SMTPUSerID;
                    SendMail.To = lblEmailId.Text;// objClsCommon.getConfigKeyValue("RECEPIENTS EMAIL ID FOR ON EACH UNDERWRITER REQUEST", "EMAIL RECEPIENTS GROUP",  user.ConnectionString); ;

                    if (txtCC.Text.Trim() != "")
                    {
                        SendMail.CC = txtCC.Text.Trim();
                    }

                    SendMail.SMTPHost = user.SMTPServer;
                    int.TryParse(user.SMTPPort, out smtpPort);
                    SendMail.SMTPPort = smtpPort;
                    SendMail.UserId = user.SMTPUSerID;
                    SendMail.Password = user.SMTPPassword;
                    SendMail.SMTPSSL = user.SMTPSSL == "Y" ? true : false; ;
                    SendMail.CC = txtCC.Text.Trim(); 

                    strSubjectName = "Query_" + lblAccountName.Text.Trim() + "_" + lblReqID.Text.Trim() + "_" + ddlQueryTypes.SelectedItem.Text;
                    SendMail.Subject = "[" + strSubjectName + "]" + "Institute of Paramedical Science & Management: Analyst Query";

                    if (strMailHeader == string.Empty)
                    {
                        SendMail.MailBody = dtInformation.Rows[0]["MAIL_BODY"].ToString();
                        SendMail.MailBody = SendMail.MailBody.Replace("#RequestID#", lblReqID.Text);
                        SendMail.MailBody = SendMail.MailBody.Replace("#UW#", lblUnderwriter.Text);
                        SendMail.MailBody = SendMail.MailBody.Replace("#Query#", txtRequest.Text.Trim());
                    }
                    else
                    {
                        SendMail.MailBody = strMailHeader;
                    }

                    SendMail.IsUnderwriter = "Y";
                    SendMail.RequestID = lblReqID.Text.Trim();
                    SendMail.ConnectionString =  user.ConnectionString;

                    SendingMail(SendMail);
 
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {

            }
            //Send Mail Ends
            //Change Query Status

            //Clear Controls
            clearControls();

            //Get Underwriter Request
            getUnderwriterRequest();
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "setFocusToEntryTab('UWList')", true);
            UpdatePanel2.Update();

            lblMessage.Text = _strAlertMessage;
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

            objclsUser = null;
            dtResult = null;
            objclsCommon = null;
            objuser = null;
            objclsErrorLogger = null;
        }
    }


    protected void grdStyled_SelectedIndexChanged(object sender, EventArgs e)
    {
        clsCommon objclsCommon = new clsCommon();
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        string _strAlertMessage = string.Empty;

        int UniqueID,UnderWriter_ID ;
        string ImgPath = string.Empty;
        try
        {
            clearControls();
            lblMessage.Text = string.Empty;

            int.TryParse(grdStyled.DataKeys[grdStyled.SelectedIndex]["UniqueID"].ToString(), out UniqueID);
            int.TryParse(grdStyled.DataKeys[grdStyled.SelectedIndex]["UnderWriter_ID"].ToString(), out UnderWriter_ID);

            if (UniqueID > 0)
            {
                hdId.Value = Convert.ToString(UniqueID);
                hdUWId.Value = Convert.ToString(UnderWriter_ID);
                lblUnderwriter.Text = grdStyled.Rows[grdStyled.SelectedIndex].Cells[13].Text;// user.ActingUW;
                txtCC.Text = user.EmailId;
                lblAccountName.Text = grdStyled.Rows[grdStyled.SelectedIndex].Cells[5].Text;
                lblEmailId.Text = grdStyled.Rows[grdStyled.SelectedIndex].Cells[17].Text;
                lblReqID.Text = hdId.Value;
               
                btnSubmit.Enabled = true;
                txtRequest.Text = string.Empty;

                if (lblEmailId.Text.Trim() == string.Empty)
                {
                    btnSubmit.Enabled = false;
                    _strMsgType = "Info";
                    _strAlertMessage = "UW Email ID is missing";
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

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "setFocusToEntryTab('EntryTab')", true);
            objclsErrorLogger = null;
        }
    }

    protected void grdStyled_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strStatus = string.Empty;
            strStatus = e.Row.Cells[14].Text.ToString().Trim();

            if (strStatus == "Cleansing In Progress")
            {
                e.Row.Cells[14].BackColor = System.Drawing.Color.Yellow;
                e.Row.Cells[14].ForeColor = System.Drawing.Color.Black;
            }
            else if (strStatus == "Modeling In Progress")
            {
                e.Row.Cells[14].BackColor = System.Drawing.Color.AliceBlue;
                e.Row.Cells[14].ForeColor = System.Drawing.Color.Black;
            }

            else if (strStatus == "Report In Progress")
            {
                e.Row.Cells[14].BackColor = System.Drawing.Color.LightCyan;
                e.Row.Cells[14].ForeColor = System.Drawing.Color.Black;
            }

            else if (strStatus == "Cleansing QC In Progress")
            {
                e.Row.Cells[14].BackColor = System.Drawing.Color.LemonChiffon;
                e.Row.Cells[14].ForeColor = System.Drawing.Color.Black;
            }
            else if (strStatus == "Modeling QC In Progress")
            {
                e.Row.Cells[14].BackColor = System.Drawing.Color.LightSkyBlue;
                e.Row.Cells[14].ForeColor = System.Drawing.Color.Black;
            }

            else if (strStatus == "Report QC In Progress")
            {
                e.Row.Cells[14].BackColor = System.Drawing.Color.LightBlue;
                e.Row.Cells[14].ForeColor = System.Drawing.Color.Black;
            }

            else if (strStatus == "Reassigned For Cleansing")
            {
                e.Row.Cells[14].BackColor = System.Drawing.Color.LightSalmon;
                e.Row.Cells[14].ForeColor = System.Drawing.Color.Black;
            }


            else if (strStatus == "Reassigned For Modeling")
            {
                e.Row.Cells[14].BackColor = System.Drawing.Color.Khaki;
                e.Row.Cells[14].ForeColor = System.Drawing.Color.Black;
            }

            else if (strStatus == "Reassigned For Report")
            {
                e.Row.Cells[14].BackColor = System.Drawing.Color.LavenderBlush;
                e.Row.Cells[14].ForeColor = System.Drawing.Color.Black;
            }

            else if (strStatus == "Cleansing QC Complete")
            {
                e.Row.Cells[14].BackColor = System.Drawing.Color.LightSeaGreen;
                e.Row.Cells[14].ForeColor = System.Drawing.Color.Black;
            }

            else if (strStatus == "Modeling QC Complete")
            {
                e.Row.Cells[14].BackColor = System.Drawing.Color.LightGoldenrodYellow;
                e.Row.Cells[14].ForeColor = System.Drawing.Color.Black;
            }
            else if (strStatus == "Report QC Complete")
            {
                e.Row.Cells[14].BackColor = System.Drawing.Color.BurlyWood;
                e.Row.Cells[14].ForeColor = System.Drawing.Color.Black;
            }
            else if (strStatus == "Assigned")
            {
                e.Row.Cells[14].BackColor = System.Drawing.Color.Aquamarine;
                e.Row.Cells[14].ForeColor = System.Drawing.Color.Black;
            }
            else if (strStatus == "Completed")
            {
                e.Row.Cells[14].BackColor = System.Drawing.Color.LawnGreen;
                e.Row.Cells[14].ForeColor = System.Drawing.Color.Black;
            }

            else if (strStatus == "Cancelled")
            {
                e.Row.Cells[14].BackColor = System.Drawing.Color.LightPink;
                e.Row.Cells[14].ForeColor = System.Drawing.Color.Black;
            }

            else if (strStatus == "Submitted For Cleansing QC")
            {
                e.Row.Cells[14].BackColor = System.Drawing.Color.LightSteelBlue;
                e.Row.Cells[14].ForeColor = System.Drawing.Color.Black;
            }

            else if (strStatus == "Submitted For Modeling QC")
            {
                e.Row.Cells[14].BackColor = System.Drawing.Color.Linen;
                e.Row.Cells[14].ForeColor = System.Drawing.Color.Black;
            }

            else if (strStatus == "Submitted For Report QC")
            {
                e.Row.Cells[14].BackColor = System.Drawing.Color.Beige;
                e.Row.Cells[14].ForeColor = System.Drawing.Color.Black;
            }

            else if (strStatus == "Pending")
            {
                e.Row.Cells[14].BackColor = System.Drawing.Color.GhostWhite;
                e.Row.Cells[14].ForeColor = System.Drawing.Color.Black;
            }
            else if (strStatus == "In Process")
            {
                e.Row.Cells[14].BackColor = System.Drawing.Color.Crimson;
                e.Row.Cells[14].ForeColor = System.Drawing.Color.Black;
            }
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string _strAlertMessage = string.Empty;
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objclsCommon = new clsCommon();

        try
        {
            clearControls();
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

            objclsCommon = null;
            objclsErrorLogger = null;
        }
    }
    private void clearControls()
    {
        try
        {
            txtCC.Text  = string.Empty;
            hdId.Value    = string.Empty;
            hdUWId.Value = string.Empty;
            lblReqID.Text = string.Empty;
            lblEmailId.Text = string.Empty;
            txtRequest.Text = string.Empty;
            lblUnderwriter.Text = string.Empty;
            lblAccountName.Text = string.Empty;

            ddlYear.SelectedValue = System.DateTime.Today.Year.ToString();
            btnSubmit.Enabled = false;
            UpdatePanel1.Update();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
 
    private void getUnderwriterRequest()
    {
        objUnderWriter UnderWriter = new objUnderWriter();
        clsUnderWriter objclsUnderWriter = new clsUnderWriter();
        DataTable dtInfo;
        int Year;
        try
        {
            UnderWriter.ConnectionString =  user.ConnectionString;

            //Set Acting UW
            if (user.IsUW != "Y")
            {
                 
            }
            else
            {
                rdoMyAccount.Checked = true;
                rdoMyAccount.Enabled = false;

                rdoAllAccount.Checked = false;
                rdoAllAccount.Enabled = false;
            }

            if (rdoMyAccount.Checked)
            {
                UnderWriter.UnderWriter = user.ActingUW;
                UnderWriter.UnderWriterID = user.ActingUW_ID.ToString();
            }
            else
            {
                UnderWriter.UnderWriterID = "0";
                UnderWriter.UnderWriter = "All";
            }
            UnderWriter.searchText = "";

            int.TryParse (ddlYear.SelectedItem.Text, out Year);
            UnderWriter.Year =Year ;
            dtInfo = objclsUnderWriter.getUnderwriterRequests(UnderWriter);

            if (dtInfo != null)
            {
                grdStyled.DataSource = dtInfo;
                grdStyled.DataBind();
                UpdatePanel2.Update();
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

    private DataTable  getRequestPrvMailBody(int ReqID, string UW)
    {
        objUnderWriter UnderWriter = new objUnderWriter();
        clsUnderWriter objclsUnderWriter = new clsUnderWriter();
        DataTable dtInfo;
        int Year;
        try
        {
            UnderWriter.ConnectionString =  user.ConnectionString;
            //UnderWriter.UnderWriter = UW;
            UnderWriter.UnderWriterID = UW.ToString();
            UnderWriter.searchText = "";
            int.TryParse (ddlYear.SelectedItem.Text, out Year);
            UnderWriter.Year =Year ;
            UnderWriter.RequetID =ReqID;

            dtInfo = objclsUnderWriter.getUnderwriterRequests(UnderWriter);
            return dtInfo;
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

    protected void btnGetDetails_Click(object sender, ImageClickEventArgs e)
    {
        
    }

    public void SendingMail(ObjSendEmail SendMail )
    {
        string strStatus = string.Empty;
        string strErrorMsg = string.Empty;
        string strQuery = string.Empty;
        clsCommon objCommon = new clsCommon();
        MailMessage mail = new MailMessage();

        try
        {
        
            MailMessage message;
            SmtpClient mySmtpClient;

            message = new MailMessage(SendMail.From, SendMail.To, SendMail.Subject, SendMail.MailBody);
            mySmtpClient = new SmtpClient(SendMail.SMTPHost, SendMail.SMTPPort);

            try
            {

                mySmtpClient.Credentials = new System.Net.NetworkCredential(SendMail.UserId, SendMail.Password);
                message.IsBodyHtml = true;
                message.Priority = MailPriority.High;
                mySmtpClient.EnableSsl = SendMail.SMTPSSL;

                // Check for CC addresses
                if (SendMail.CC != null && SendMail.CC != string.Empty)
                {
                    string[] emailCC = SendMail.CC.Trim().Split(',');
                    foreach (string addressCC in emailCC)
                    {
                        if (addressCC.Contains(SendMail.To) == false)
                        {
                            message.CC.Add(addressCC);
                        }
                    }
                }

                // CHECK IF USER HAS SELECTED ANY FILE.
                if (fleUpd.HasFile)
                {
                    HttpFileCollection hfc = Request.Files;

                    // LOOP THROUGH EACH SELECTED FILE.
                    for (int i = 0; i <= hfc.Count - 1; i++)
                    {
                        HttpPostedFile hpf = hfc[i];

                        // CREATE A FILE ATTACHMENT.
                        Attachment objAttachements =
                            new Attachment(hpf.InputStream,
                                hpf.FileName);

                        // ADD FILE ATTACHMENT TO THE EMAIL.
                        message.Attachments.Add(objAttachements);
                    }
                }

                mySmtpClient.Timeout = 1000000;
                mySmtpClient.Send(message);
                strStatus = "Success";

            }
            catch (FormatException ex)
            {
                strErrorMsg = ex.Message;
                strStatus = "Failed";
            }
            catch (SmtpException ex)
            {
                strErrorMsg = ex.Message;
                strStatus = "Failed";
            }
            catch (Exception ex)
            {
                strErrorMsg = ex.Message;
                strStatus = "Failed";
            }
            finally
            {
                //SMTP LOG
                if (SendMail.ConnectionString != "" && SendMail.ConnectionString != "")
                {
                    if (SendMail.RequestID == null)
                    {
                        SendMail.RequestID = "";
                    }
                    strQuery = "UPDATE [dbo].[Underwriterinput] SET ProcessingStatus='InQuery' where UniqueID= " + SendMail.RequestID;
                    strQuery = strQuery + " INSERT INTO dbo.STMP_MAIL_ERROR_LOG([TO],[CC],[FROM],[SUBJECT],[MAIL_BODY],[STATUS],[ERROR],[LOG_DATE],[request_id])";
                    strQuery = strQuery + " SELECT '" + SendMail.To + "', '" + SendMail.CC + "', '" + SendMail.From + "', '" + SendMail.Subject + "', '" + SendMail.MailBody.Replace("'", "''") + "','" + strStatus + "','" + strErrorMsg + "',getdate()," + SendMail.RequestID + " ";
                    objCommon.executeQuery(SendMail.ConnectionString, strQuery);
                }
                message.Dispose();
                mySmtpClient = null;
            }
        }
        catch (Exception ex)
        {
            strErrorMsg = ex.Message;
            strStatus = "Failed";
        }
    }
 
    protected void btnGo_Click(object sender, EventArgs e)
    {
        //Underwriter Request
        getUnderwriterRequest();

        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "setFocusToEntryTab('UWList')", true);
    }

    protected void btnReply_Click(object sender, EventArgs e)
    {
        ObjSendEmail SendMail = new ObjSendEmail();
        clsCommon objClsCommon = new clsCommon();
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        DataTable dtMessage = new DataTable();
        string _strAlertMessage = string.Empty;
        string strHeaderAdnl = string.Empty;

        try
        {
            if (txtReply.Text.Trim() == string.Empty) return;

            string strRequestID = string.Empty;

            if (hdReplyTo.Value != null & hdReplyTo.Value.ToString().Trim() != "")
            {
                int SMTPPort, ReqID;
                int.TryParse(hdRequestID.Value, out ReqID);
                dtMessage = getRequestPrvMailBody(ReqID, hdMessage.Value);

                hdMessage.Value = dtMessage.Rows[0]["Message"].ToString();
                SendMail.RequestID = hdRequestID.Value;
                SendMail.From = hdReplyFrom.Value;
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

                int.TryParse(user.SMTPPort, out SMTPPort);
                SendMail.SMTPHost = user.SMTPServer;
                SendMail.SMTPPort = SMTPPort;
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
                strHeaderAdnl = strHeaderAdnl + hdMessage.Value.Trim();

                SendMail.MailBody = strHeaderAdnl;
                SendMail.ConnectionString = user.ConnectionString;
                objClsCommon.SendEmail(SendMail);

                txtReply.Text = string.Empty;
 
                //Underwriter Request
                getUnderwriterRequest();

                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "setFocusToEntryTab('UWList')", true);
                UpdatePanel2.Update();

            }
        }
        catch (Exception ex)
        {
            _strMsgType = "Erorr";
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

            objclsErrorLogger = null;
            objClsCommon = null;
        }
    }

    protected void imgbtn_Click(object sender, ImageClickEventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objCommon = new clsCommon();
        ImageButton btndetails;
        GridViewRow gvrow;

        string _strAccountName = string.Empty;
        string _strRequestID = string.Empty;
        try
        {
            btndetails = sender as ImageButton;
            gvrow = (GridViewRow)btndetails.NamingContainer;
            _strAccountName = gvrow.Cells[5].Text;
            _strRequestID = gvrow.Cells[2].Text;

            lblAccName.Text = "For Account: " + _strAccountName;

            //Display SOV Files
            trSOV.Nodes.Clear();

            lblSOVError.Text = string.Empty;
            if (System.IO.Directory.Exists(Server.MapPath("~/Operation/Uploads/ManageRequests/" + _strAccountName)) == false)
            {
                lblSOVError.Text = "File Server path not found.";
            }

            DirectoryInfo rootInfo = new DirectoryInfo(Server.MapPath("~/Operation/Uploads/ManageRequests/" + _strAccountName + "/"));
            this.PopulateTreeView(rootInfo, null);
            this.ModalPopupExtender1.Show();
        }
        catch (Exception ex)
        {
            lblSOVError.Text = "File Server path not found.";
            _strMsgType = "Error";
            _strAlertMessage = ex.Message;
            objclsErrorLogger.WriteError(System.IO.Path.GetFileName(Request.Path), "", user == null ? "" : user.ConnectionString, ex);
        }
        finally
        {
            btndetails = null;
            gvrow = null;

            //Alert Message
            if (_strAlertMessage != string.Empty && _strAlertMessage.Trim() != "")
            {
                lblMessage.Text = _strAlertMessage;
                objCommon.setLabelMessage(lblMessage, _strMsgType);
            }
            objclsErrorLogger = null;
            objCommon = null;
        }
    }

    private void PopulateTreeView(DirectoryInfo dirInfo, TreeNode treeNode)
    {
        try
        {
            foreach (DirectoryInfo directory in dirInfo.GetDirectories())
            {
                TreeNode directoryNode = new TreeNode
                {
                    Text = directory.Name,
                    Value = directory.FullName
                };

                if (treeNode == null)
                {
                    //If Root Node, add to TreeView.
                    trSOV.Nodes.Add(directoryNode);
                }
                else
                {
                    //If Child Node, add to Parent Node.
                    treeNode.ChildNodes.Add(directoryNode);
                }

                //Get all files in the Directory.
                string strNavigateURL = string.Empty;
                foreach (FileInfo file in directory.GetFiles())
                {
                    //Add each file as Child Node.
                    TreeNode fileNode = new TreeNode
                    {
                        Text = file.Name,
                        Value = file.FullName,
                        Target = "_blank",
                        NavigateUrl = (new Uri(Server.MapPath("~/Operation/"))).MakeRelativeUri(new Uri(file.FullName)).ToString(),
                    };

                    directoryNode.ChildNodes.Add(fileNode);
                }
                PopulateTreeView(directory, directoryNode);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


}