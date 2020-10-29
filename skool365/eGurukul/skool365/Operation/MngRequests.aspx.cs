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
using System.Threading ;
using BLCMR;
using System.Text.RegularExpressions;
 
public partial class Operation_MngRequests : System.Web.UI.Page
{
 
    string M_strAlertMessage = string.Empty;
    string _strMsgType = string.Empty;
    string _strAlertMessage = string.Empty;
    ObjPortalUser user;
    
    public static string WM_UW { get; set; }
    public static string WM_Con_Str { get; set; }
    public static string WM_Due_Date { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objCommon=new clsCommon ();
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

            //Validate User Authetication Ends
            if (IsPostBack == false)
            {
             
                //Bind Year
                ddlYear.Items.Add("All");
                for (int Year = 2020; Year <= System.DateTime.Today.Year; Year++)
                {
                    ddlYear.Items.Add(Year.ToString());
                }

                ddlYear.SelectedValue = System.DateTime.Today.Year.ToString();
               
                //Get All Assignments for Studnet
                getAssignedAssignments(false);

                //Get All UW List
                getUWList();

                //Underwriter Request
                getUnderwriterRequest("");

                //Get All Remodel Reason
                getAllRemodelReason();

                ddlAssignments.Focus();
               
                //Get Pending Account Status
                getPendingAccountStatus();
                clearControls(false);

                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "setFocusToEntryTab('UWList')", true);
            }
            else
            {
                if(hdCalendarVal.Value !="")
                {
                    txtDueDate.Text = hdCalendarVal.Value;
                    hdCalendarVal.Value = ""; UpdatePanel1.Update();
                    //UpdatePanelDueDate.Update();
                    //updatePnlSOVName.Update();
                    //updatepnlddlUWList.Update();
                    //ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabelMsg();</script>", false);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "HideImage();HideLabelMsg();", true);
                }
            }

            fleUpdSOVName.ToolTip = "Supported File Type :" + user.SOVFileExtns;
            txtSOVName.ToolTip = "Supported File Type :" + user.SOVFileExtns;
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            int a = Request.Files.Count;

            //Check Page Accessibility
            if (user != null)
            {
                if (objCommon.checkPageAccessiblity(user.EmailId, System.IO.Path.GetFileName(Request.Path), user.ConnectionString) != "Y")
                    Response.Redirect("~/Operation/AccessDenied.aspx", false);
            }

        }
        catch (ThreadAbortException)
        {
        }
        catch (Exception ex)
        {
            _strMsgType = "Error";
            _strAlertMessage = ex.Message;
            objclsErrorLogger.WriteError(System.IO.Path.GetFileName(Request.Path), "",  user==null?"" : user.ConnectionString,  ex);
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

                UpdatePanel1.Update();
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
        clsCommon objclsCommon = new clsCommon();
        DataTable dtResult;
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsUnderWriter objclsUnderWriter = new clsUnderWriter();
        objUnderWriter Underwriter = new objUnderWriter();

        string confirmValue = string.Empty;
        string strSOVName = string.Empty;
        string strFolderName = string.Empty;
        string strSOVNameListHdr = string.Empty;
        string strSOVNameListDtl = string.Empty;
        int requestID, assignmentID;

        try
        {
            lblMessage.Text = "";

            int.TryParse(ddlAssignments.SelectedValue , out assignmentID);
            Underwriter.AssignmentID = assignmentID;
            Underwriter.Type = "Assignment";
            Underwriter.AccountName = txAssigmentName.Text.Trim();
            Underwriter.AccountDesc = txtDescription.Text.Trim();
            Underwriter.RequestType = rdoTypeNew.Checked ? rdoTypeNew.Text : rdoTypeRemodel.Text;
            Underwriter.MachineIP = user.IPAddress;
            Underwriter.Comment = txtResponse.Text.Trim();

            strSOVName = string.Empty;
            strSOVName = txtSOVName.Text;
            Underwriter.DueDate = txtDueDate.Text;
            Underwriter.ConnectionString =  user.ConnectionString;
            Underwriter.UnderWriterID = ddlUWList.SelectedValue;
            Underwriter.UnderWriter = ddlUWList.SelectedItem.Text;
            Underwriter.ReGnO = user.UId;

            // //Validate Account Name ( New Folder Will be created by Account Name)
            // Regex regex = new Regex("^([^*/><?\"|:%~]*)$");
            //if (!regex.IsMatch(Underwriter.AccountName))
            //{
            //    _strMsgType = "Info";
            //    _strAlertMessage = "Invalid Account Name. Contains special charactesrs(/\"*:?<>|~%)";
            //    txAssigmentName.Focus();
            //    return;
            //}

            //Validation for Input Characters
            if (objclsCommon.isValidInput(user.RestrictedInputChar, Underwriter.AccountName.Trim()) == false)
            {
                _strMsgType = "Info";
                _strAlertMessage = "Invalid Account Name. Contains invalid characters.";
                txAssigmentName.Focus();
                return;
            }

            //Validation for Input Characters
            if (objclsCommon.isValidInput(user.RestrictedInputChar, Underwriter.Comment.Trim()) == false)
            {
                _strMsgType = "Info";
                _strAlertMessage = "Comments contains invalid characters.";
                txtResponse.Focus();
                return;
            }

            //if (objclsCommon.isValidInput(user.RestrictedInputChar, Underwriter.TIV.Trim()) == false)
            //{
            //    _strMsgType = "Info";
            //    _strAlertMessage = "Invalid TIV. Contains invalid characters.";
            //    txtTIV.Focus();
            //    return;
            //}

            //if (rdoTypeRemodel.Checked == true && txtRemodelReason.Text.Trim() == string.Empty)
            //{
            //    _strMsgType = "Info";
            //    _strAlertMessage = "Please enter Remodel reason";
            //    txtRemodelReason.Focus();
            //    return;
            //}

            //if (rdoTypeRemodel.Checked == true && objclsCommon.isValidInput(user.RestrictedInputChar, txtRemodelReason.Text.Trim()) == false)
            //{
            //    _strMsgType = "Info";
            //    _strAlertMessage = "Remodel reason contains invalid characters.";
            //    txtRemodelReason.Focus();
            //    return;
            //}

            string[] validFileTypes = user.SOVFileExtns.Split(';');

            string ext = string.Empty;
            bool isValidFile = false;

            for (int i = 0; i < Request.Files.Count; i++)
            {
                if (strSOVName != string.Empty) strSOVName = strSOVName + ",";
                HttpPostedFile PostedFile = Request.Files[i];
                strSOVName = strSOVName + PostedFile.FileName;

                ext = System.IO.Path.GetExtension(PostedFile.FileName);
             
                for (int icnt = 0; icnt < validFileTypes.Length; icnt++)
                {
                    if (ext.ToUpper() == "." + validFileTypes[icnt].ToUpper())
                    {
                        isValidFile = true;
                        break;
                    }
                }

                if (!isValidFile)
                {
                    _strMsgType = "Info";
                    _strAlertMessage = "Invalid File. Please upload a File with extension " +
                     String.Join(",", validFileTypes);
                    return;
                }

                //File Size Validation
                int filesize = (PostedFile.ContentLength) / 1024;

                if (filesize > user.SOVFileSize)
                {
                    _strMsgType = "Info";
                    _strAlertMessage = "The file size exceeds the allowed limit.";
                    return;
                }
            }

            Underwriter.SOVName = strSOVName;
            if (hdId.Value != "" && hdId.Value != "0")
            {
                Underwriter.RemodelReason =  getRemodelReason();
                Underwriter.RequestType = "ReModel";
                int.TryParse(hdId.Value, out requestID);
                Underwriter.RequetID = requestID  ;
                dtResult = objclsUnderWriter.CreateUnderWriterRequest(Underwriter);
            }
            else
            {
                Underwriter.RequetID = 0;
                dtResult = objclsUnderWriter.CreateUnderWriterRequest(Underwriter);
            }

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

                if (dtResult.Rows.Count > 0)
                {

                    //FOR FILE SERVER ACTIVITY STARTS
                    try
                    {
                        strFolderName = dtResult.Rows[0]["RequestID"].ToString();
                        int.TryParse(strFolderName, out requestID);
                        Underwriter.RequetID = requestID;
                        Underwriter.UWEmailId = dtResult.Rows[0]["Approver_EmailID"].ToString();
                        Underwriter.SubmitDate = dtResult.Rows[0]["SUBMIT_DATE"].ToString();

                        if (fleUpdSOVName.HasFile == true)
                        {

                            if (hdId.Value != "" && hdId.Value != "0")
                            {
                                strFolderName = System.IO.Path.Combine(Server.MapPath("."), "Uploads/ManageRequests", Underwriter.RequetID.ToString(), "ReModel", strFolderName);
                            }
                            else
                            {
                                strFolderName = System.IO.Path.Combine(Server.MapPath("."), "Uploads/ManageRequests", Underwriter.RequetID.ToString(), "New", strFolderName);
                            }

                            if (System.IO.Directory.Exists(strFolderName)==false)
                            {
                                System.IO.Directory.CreateDirectory(strFolderName);
                            }

                            strSOVNameListHdr = "<ul style = 'list-style-type:circle'>";
                            if (System.IO.Directory.Exists(strFolderName))
                            {
                                for (int i = 0; i < Request.Files.Count; i++)
                                {
                                    HttpPostedFile PostedFile = Request.Files[i];
                                    strSOVNameListDtl = strSOVNameListDtl + "<li>" + System.IO.Path.GetFileName(PostedFile.FileName) +"</li>";

                                    if (PostedFile.ContentLength > 0)
                                    {
                                        PostedFile.SaveAs(strFolderName+"\\"+ System.IO.Path.GetFileName(PostedFile.FileName));
                                    }
                                }
                            }
                            strSOVName = strSOVNameListHdr + strSOVNameListDtl + "</ul>";
                        }
                    }
                    catch { }
                    //FOR FILE SERVER ACTIVITY ENDS


                    ObjSendEmail SendMail = new ObjSendEmail();
                    DataTable dtInformation;
                    clsCommon objClsCommon = new clsCommon();
                    string strRequestID = string.Empty;
                    int smtpPort;
                    SendMail.CC = "";

                    //Send Mail to requester on New/Modifiy Request Starts
                    if (hdId.Value != "" && hdId.Value != "0")
                    {
                        dtInformation = objClsCommon.getMailBody("MailFormat", "MAIL ON NEW REQUEST (STUDENTS)", user.ConnectionString);
                    }
                    else
                    {
                        dtInformation = objClsCommon.getMailBody("MailFormat", "MAIL ON NEW REQUEST (STUDENTS)", user.ConnectionString);
                    }

                    try
                    {
                        int.TryParse(dtResult.Rows[0]["RequestID"].ToString(), out requestID);
                        Underwriter.RequetID = requestID;

                        if (dtInformation != null & dtInformation.Rows.Count > 0)
                        {
                            SendMail.From = user.SMTPUSerID;
                            SendMail.To = user.EmailId;
                            SendMail.BCC = "";
                            SendMail.CC = "";
                            SendMail.SMTPHost = user.SMTPServer;
                            int.TryParse(user.SMTPPort, out smtpPort);
                            SendMail.SMTPPort = smtpPort;
                            SendMail.UserId = user.SMTPUSerID;
                            SendMail.SMTPSSL = user.SMTPSSL == "Y" ? true : false; ;
                            SendMail.Password = user.SMTPPassword;
                            SendMail.MailType = "I";

                            if (hdId.Value != "" && hdId.Value != "0")
                            {
                                SendMail.Subject = "Assignment Request ID [" + Underwriter.RequetID.ToString() + "] " + user.Orig_Name + ": Submission Acknowledgement for modification";
                            }
                            else
                            {
                                SendMail.Subject = "Assignment Request ID [" + Underwriter.RequetID.ToString() + "] " + user.Orig_Name + ": Submission Acknowledgement";
                            }

                            SendMail.MailBody = dtInformation.Rows[0]["MAIL_BODY"].ToString();
                            SendMail.MailBody = SendMail.MailBody.Replace("#RequestID#", Underwriter.RequetID.ToString());
                            SendMail.MailBody = SendMail.MailBody.Replace("#AccountName#", Underwriter.AccountName);
                            SendMail.MailBody = SendMail.MailBody.Replace("#RequestType#", Underwriter.RequestType);
                            SendMail.MailBody = SendMail.MailBody.Replace("#Description#", Underwriter.AccountDesc);
                            SendMail.MailBody = SendMail.MailBody.Replace("#SOVName#", strSOVName);
                            SendMail.MailBody = SendMail.MailBody.Replace("#SubmitDate#", Underwriter.SubmitDate);
                            SendMail.MailBody = SendMail.MailBody.Replace("#Response#", Underwriter.Comment);
                            SendMail.MailBody = SendMail.MailBody.Replace("#Underwriter#", Underwriter.UnderWriter);
                            SendMail.RequestID = Underwriter.RequetID.ToString();

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

                    }
                    //Send Mail to requester on New/Modifiy Request Ends

                    if (hdId.Value != "" && hdId.Value != "0")
                    {
                        dtInformation = objClsCommon.getMailBody("MailFormat", "MAIL ON NEW REQUEST (ADMIN)", user.ConnectionString);
                    }
                    else
                    {
                        dtInformation = objClsCommon.getMailBody("MailFormat", "MAIL ON NEW REQUEST (ADMIN)", user.ConnectionString);
                    }

                    //Send mail to Admin against each Request Starts
                    try
                    {

                        if (dtInformation != null & dtInformation.Rows.Count > 0)
                        {
                            SendMail.From = user.SMTPUSerID;
                            SendMail.To = Underwriter.UWEmailId;
                            SendMail.BCC = "";
                            SendMail.CC = objClsCommon.getConfigKeyValue("EMAIL IDS FOR CC OF MAIL MESSAGE FOR EACH OPERATION", "EMAIL RECEPIENTS GROUP", user.ConnectionString);
                            SendMail.SMTPHost = user.SMTPServer;
                            int.TryParse(user.SMTPPort, out smtpPort);
                            SendMail.SMTPPort = smtpPort;
                            SendMail.UserId = user.SMTPUSerID;
                            SendMail.SMTPSSL = user.SMTPSSL == "Y" ? true : false; ;
                            SendMail.Password = user.SMTPPassword;
                            SendMail.MailType = "I";

                            if (hdId.Value != "" && hdId.Value != "0")
                            {
                                SendMail.Subject = "Assignment Request ID [" + Underwriter.RequetID.ToString() + "] " + user.Orig_Name + ": Submission Acknowledgement for modification";
                            }
                            else
                            {
                                SendMail.Subject = "Assignment Request ID [" + Underwriter.RequetID.ToString() + "] " + user.Orig_Name + ": Submission Acknowledgement";
                            }

                            SendMail.MailBody = dtInformation.Rows[0]["MAIL_BODY"].ToString();
                            SendMail.MailBody = SendMail.MailBody.Replace("#RequestID#", Underwriter.RequetID.ToString());
                            SendMail.MailBody = SendMail.MailBody.Replace("#AccountName#", Underwriter.AccountName);
                            SendMail.MailBody = SendMail.MailBody.Replace("#RequestType#", Underwriter.RequestType);
                            SendMail.MailBody = SendMail.MailBody.Replace("#Description#", Underwriter.AccountDesc);
                            SendMail.MailBody = SendMail.MailBody.Replace("#LastDate#", Underwriter.DueDate);
                            SendMail.MailBody = SendMail.MailBody.Replace("#SOVName#", strSOVName); 
                            SendMail.MailBody = SendMail.MailBody.Replace("#SubmitDate#", Underwriter.SubmitDate);
                            SendMail.MailBody = SendMail.MailBody.Replace("#Response#", Underwriter.Comment);
                            SendMail.MailBody = SendMail.MailBody.Replace("#Underwriter#", Underwriter.UnderWriter);
                            SendMail.RequestID = Underwriter.RequetID.ToString();

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
                    //Send mail to Admin against each Request Ends
                }

                //Clear Controls
                clearControls(true);
  
                //Get Underwriter Request
                getUnderwriterRequest(txtSearch.Text.Trim());
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "setFocusToEntryTab('UWList')", true);
                lblMessage.Text = _strAlertMessage;
            }
        }
        catch (Exception ex)
        {
            _strMsgType = "Error";
            _strAlertMessage = ex.Message;
            objclsErrorLogger.WriteError(System.IO.Path.GetFileName(Request.Path), "", user==null?"" : user.ConnectionString, ex);
            Response.Redirect("~/Operation/Err.aspx");
        }
        finally
        {
            //Alert Message
            if (_strAlertMessage != string.Empty && _strAlertMessage.Trim() != "")
            {
                lblMessage.Text = _strAlertMessage;
                objclsCommon.setLabelMessage(lblMessage, _strMsgType  );
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

    protected void grdStyled_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        clsUserMaintainance objclsUser = new clsUserMaintainance();
        ObjUser objuser = new ObjUser();
        clsCommon objclsCommon = new clsCommon();
        DataTable dtResult;
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsUnderWriter objclsUnderWriter = new clsUnderWriter();
        objUnderWriter Underwriter = new objUnderWriter();
        DataTable dtMailThreadResult;

        string _strAlertMessage = string.Empty;
        string confirmValue = string.Empty;
        string strSOVName = string.Empty;

        int UID, UW_ID;
        try
        {
            lblMessage.Text = "";

            Underwriter.ConnectionString =  user.ConnectionString;
            Underwriter.UnderWriter = grdStyled.Rows[e.RowIndex].Cells[14].Text.Trim();

            int.TryParse(grdStyled.DataKeys[e.RowIndex]["UnderWriter_ID"].ToString(), out UW_ID);
            int.TryParse(grdStyled.DataKeys[e.RowIndex]["UniqueID"].ToString(), out UID);
            Underwriter.RequetID = UID;
            Underwriter.UnderWriterID = UW_ID.ToString();
           
            //Get Sent Mail Body
            dtMailThreadResult = getRequestPrvMailBody(Underwriter.RequetID, Underwriter.UnderWriterID);
            dtResult = objclsUnderWriter.DeleteUnderWriterRequest(Underwriter);
         
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

                //Send Mail on Request Delete Starts
                ObjSendEmail SendMail = new ObjSendEmail();
                clsCommon objClsCommon = new clsCommon();

                int smtpPort;
                string strRequestID = string.Empty;
                string strMailThread = string.Empty;
                string strMailHeader = string.Empty;

                if (dtMailThreadResult != null)
                {
                    strMailThread = dtMailThreadResult.Rows[0]["Message"].ToString();
                    strMailHeader = "Dear All, <br />This Underwriting Request has been deleted by me. <br /><br /> Regards,<br />" + user.UserName + " <br /><hr style='border-style: solid; background-color: #000066'  />";
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
                try
                {
                    SendMail.From = user.SMTPUSerID;
                    SendMail.To = objClsCommon.getConfigKeyValue("RECEPIENTS EMAIL ID FOR ON EACH UNDERWRITER REQUEST", "EMAIL RECEPIENTS GROUP",  user.ConnectionString); ;
                    SendMail.CC = "";

                    if (dtMailThreadResult.Rows[0]["CC"].ToString() != "")
                    {
                        SendMail.CC = dtMailThreadResult.Rows[0]["CC"].ToString();
                    }

                    if (SendMail.CC != "")
                    {
                        SendMail.CC = SendMail.CC + "," + user.EmailId;
                    }
                    else
                    {
                        SendMail.CC = user.EmailId;
                    }

                    SendMail.SMTPHost = user.SMTPServer;
                    int.TryParse(user.SMTPPort, out smtpPort);
                    SendMail.SMTPPort = smtpPort;
                    SendMail.UserId = user.SMTPUSerID;
                    SendMail.Password = user.SMTPPassword;
                    SendMail.SMTPSSL = user.SMTPSSL == "Y" ? true : false; ;

                    SendMail.Subject = dtMailThreadResult.Rows[0]["Subject"].ToString();
                    SendMail.MailBody = strMailHeader;
                    SendMail.ConnectionString =  user.ConnectionString;
                    SendMail.RequestID = Underwriter.RequetID.ToString();
                    objClsCommon.SendEmail(SendMail);
                }
                catch (Exception ex)
                {
                    objclsErrorLogger.WriteError(System.IO.Path.GetFileName(Request.Path), "",  user==null?"" : user.ConnectionString,  ex);
                    Response.Redirect("~/Operation/Err.aspx");
                }
                finally
                {

                }
                //Send Mail on Request Delete Ends
                
                //Clear Controls
                clearControls(true);

                //Get Underwriter Request
                getUnderwriterRequest(txtSearch.Text.Trim());
                UpdatePanel2.Update();

                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "setFocusToEntryTab('UWList')", true);
                lblMessage.Text = _strAlertMessage;
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
 
    protected void grdStyled_PageIndexChanging(Object sender, GridViewPageEventArgs e)
    {
        grdStyled.PageIndex = e.NewPageIndex;
        getUnderwriterRequest(txtSearch.Text.Trim());
    }

    protected void grdStyled_SelectedIndexChanged(object sender, EventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objclsCommon = new clsCommon();

        int UniqueID;
        int UnderWriter_ID;
        int assignmentID;
        string ImgPath = string.Empty;

        try
        {
            clearControls(false);

            getAssignedAssignments(true);
            lblMessage.Text = string.Empty;
            int.TryParse(grdStyled.DataKeys[grdStyled.SelectedIndex]["UniqueID"].ToString(), out UniqueID);
            int.TryParse(grdStyled.DataKeys[grdStyled.SelectedIndex]["UnderWriter_ID"].ToString(), out UnderWriter_ID);
            int.TryParse(grdStyled.DataKeys[grdStyled.SelectedIndex]["ID"].ToString(), out assignmentID);
            
            if (UniqueID > 0)
            {
                hdId.Value = Convert.ToString(UniqueID);

                ddlAssignments.SelectedValue = assignmentID.ToString();
                ddlAssignments.Enabled = false;

                txAssigmentName.Text = grdStyled.Rows[grdStyled.SelectedIndex].Cells[6].Text;
                txAssigmentName.Enabled = false;

                rdoTypeRemodel.Checked = true;
                rdoTypeRemodel.Visible = true;
                rdoTypeNew.Visible = false;
                rdoTypeNew.Enabled = false;

                txtDueDate.Text = grdStyled.Rows[grdStyled.SelectedIndex].Cells[9].Text;
                ddlUWList.SelectedValue = UnderWriter_ID.ToString();

                //Account Update
                getPendingAccountStatus();
                getSubmittedAccountInDueDate(assignmentID);
                btnSubmit.Text = "Update";
            }

            UpdatePanel1.Update();
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "setFocusToEntryTab('EntryTab')", true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "openReasonWindow();", true);
        }
        catch (Exception ex)
        {
            _strMsgType = "Error";
            _strAlertMessage = ex.Message;
            objclsErrorLogger.WriteError(System.IO.Path.GetFileName(Request.Path), "", user==null?"" : user.ConnectionString,  ex);
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
            objclsCommon = null;
        }
    }
 
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string _strAlertMessage = string.Empty;
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objclsCommon = new clsCommon();
        try
        {
            clearControls(true);

            lblMessage.Text = string.Empty;
        }
        catch (Exception ex)
        {
            _strMsgType = "Error";
            _strAlertMessage = ex.Message;
            objclsErrorLogger.WriteError(System.IO.Path.GetFileName(Request.Path), "", user==null?"" : user.ConnectionString,  ex);
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

            objclsErrorLogger = null;
            objclsCommon = null;
        }
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

                //Get Underwriter Request
                getUnderwriterRequest(txtSearch.Text.Trim());
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

            objclsErrorLogger = null;
            objClsCommon = null;
        }
    }

    private void clearControls(Boolean blnbtnCancelClicked)
    {
        try
        {
            hdCalendarVal.Value = "";
            hdId.Value = "";
            txAssigmentName.Text = ""; ;
            txAssigmentName.Enabled = true;
            txtDescription.Text = ""; ;
            txtDescription.Enabled = true;
            txtDueDate.Text = "";
            txtSOVName.Text = "";
            txtSearch.Text = "";
            txtResponse.Text = "";
            txtReply.Text = "";
            rdoTypeNew.Checked = true;
            rdoTypeNew.Visible = true;
            rdoTypeRemodel.Visible = false;
            ddlAssignments.SelectedIndex = 0;
            ddlAssignments.Enabled = true;
            ddlAssignments.Enabled = true;
            hdAlertMessage.Value = "Do you wish to submit the request for the selected account?";
            
            //Bind Year
            if (ddlYear.Items.Count == 0)
            {
                ddlYear.Items.Add("All");
                for (int Year = 2020; Year <= System.DateTime.Today.Year; Year++)
                {
                    ddlYear.Items.Add(Year.ToString());
                }
                ddlYear.SelectedValue = System.DateTime.Today.Year.ToString();
            }

            //Get All Assignments for Studnet
            if (blnbtnCancelClicked)
            {
                getAssignedAssignments(false);
            }

            //Set Acting UW
            if (user.IsUW != "Y")
            {
                //ddlUWList.SelectedValue = user.ActingUW_ID.ToString();
            }
            else
            {
                ddlUWList.SelectedIndex = -1 ;
                ddlUWList.Enabled = false;
                rdoMyAccount.Checked = true;
                rdoAllAccount.Enabled = false;
            }

            imgAssignment.Visible = false;
            hrefAssignment.HRef = ""; 

            btnSubmit.Text = "Save";
            UpdatePanel1.Update();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void getUWList()
    {
        clsUnderWriter objclsUnderWriter = new clsUnderWriter();
        DataTable dtInfo;
        try
        {
            dtInfo = objclsUnderWriter.getUWList( user.ConnectionString);

            if (dtInfo != null)
            {
                ddlUWList.DataSource = dtInfo;
                ddlUWList.DataValueField = "UID";
                ddlUWList.DataTextField = "USER_NAME";
                ddlUWList.DataBind();

                //Set Acting UW
                if (user.IsUW != "Y")
                {
                    rdoAllAccount.Visible = true;
                    ddlUWList.SelectedValue  = user.UId.ToString();
                }
                else
                {
                    rdoAllAccount.Visible = false;
                    ddlUWList.SelectedValue  = user.ActingUW_ID.ToString();
                    
                    ddlUWList.Enabled = false;
                    rdoMyAccount.Checked = true;
                    rdoAllAccount.Enabled = false;
                }

                UpdatePanel1.Update();
                UpdatePanel2.Update();

                hdUW.Value = user.ActingUW;
                hdConStr.Value = user.ConnectionString ;

                WM_UW = user.ActingUW;
                WM_Con_Str =  user.ConnectionString;
                WM_Due_Date = txtDueDate.Text.Trim() ;
                UpdatePanel1.Update();
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

    private void getAssignedAssignments(Boolean blnAll)
    {
        clsUnderWriter objclsUnderWriter = new clsUnderWriter();
        objUnderWriter Underwriter = new objUnderWriter();
        DataTable dtInfo;
        DataView dvList;
        try
        {
            Session["Assignment"] = null;

            Underwriter.ConnectionString = user.ConnectionString;
            Underwriter.UnderWriterID = user.UId.ToString () ;
            dtInfo = objclsUnderWriter.getAssignedAssignment(Underwriter);
            Session["Assignment"] = dtInfo;

            dvList=dtInfo.DefaultView;

            if (blnAll == true)
            {
                dvList.RowFilter = "status=status";
            }
            else
            {
                dvList.RowFilter = "status='Pending'";
            }

            if (dtInfo != null)
            {
                ddlAssignments.DataSource = dvList;
                ddlAssignments.DataValueField = "AssignmentId";
                ddlAssignments.DataTextField = "Tit_Desc";
                ddlAssignments.DataBind();

                ddlAssignments.Items.Insert(0, new ListItem("Select", "0"));
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

    private void getAllRemodelReason()
    {
        clsUnderWriter objclsUnderWriter = new clsUnderWriter();
        DataTable dtInfo;
        try
        {
            dtInfo = objclsUnderWriter.getRemodelReason(user.ConnectionString);

            if (dtInfo != null)
            {
                grdRemodeReason.DataSource = dtInfo;
                grdRemodeReason.DataBind();
                UpdatePanel1.Update();
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

    private void getUnderwriterRequest(string strText)
    {
        objUnderWriter UnderWriter = new objUnderWriter();
        clsUnderWriter objclsUnderWriter = new clsUnderWriter();
        DataTable dtInfo;
        int year;
        
        try
        {
            UnderWriter.ConnectionString =  user.ConnectionString;
           
            if (rdoMyAccount.Checked)
            {
                UnderWriter.UnderWriterID = user.UId.ToString();
            }
            else
            {
                UnderWriter.UnderWriterID = "0";
            }

            int.TryParse(ddlYear.SelectedItem.Text, out year);
            UnderWriter.Year = year ;
            UnderWriter.searchText = strText.Trim();
            dtInfo = objclsUnderWriter.getUnderwriterRequests(UnderWriter);

            if (dtInfo != null)
            {
                grdStyled.DataSource = dtInfo;
                grdStyled.DataBind();
                UpdatePanel2.Update();
            }

            //Get Pending Account Status
            getPendingAccountStatus();

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

    private void getPendingAccountStatus()  
    {
        objUnderWriter UnderWriter = new objUnderWriter();
        clsUnderWriter objclsUnderWriter = new clsUnderWriter();
        DataTable dtInfo;
        int year;

        try
        {
            UnderWriter.ConnectionString = user.ConnectionString;
            UnderWriter.UnderWriterID = user.UId.ToString(); // ddlUWList.SelectedValue;

            int.TryParse(ddlYear.SelectedItem.Text, out year);
            UnderWriter.Year = year;
            dtInfo = objclsUnderWriter.getPendingAccountStatus(UnderWriter);

            lblPendingUnderUWCnt.Text = "0";
            lblTotalPendingUnderAllCnt.Text = "0";
            lblTotalApprovedInYearCnt.Text = "0";

            if (dtInfo != null)
            {
                lblPendingUnderUWCnt.Text  = dtInfo.Rows[0]["UW_PENDING_COUNT"].ToString();
                lblTotalPendingUnderAllCnt.Text = dtInfo.Rows[0]["ALL_PENDING_COUNT"].ToString();
                lblTotalApprovedInYearCnt.Text = dtInfo.Rows[0]["ALL_APPROVE_COUNT"].ToString();
            }
                updatepnlddlUWList.Update();
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
        //Underwriter Request
        getUnderwriterRequest(txtSearch.Text.Trim());

        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "setFocusToEntryTab('UWList')", true);
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        //Underwriter Request
        getUnderwriterRequest(txtSearch.Text.Trim());

        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "setFocusToEntryTab('UWList')", true);
    }

    private DataTable getRequestPrvMailBody(int ReqID, string UW_ID)
    {
        objUnderWriter UnderWriter = new objUnderWriter();
        clsUnderWriter objclsUnderWriter = new clsUnderWriter();
        DataTable dtInfo;
        try
        {
            int Year;
            UnderWriter.ConnectionString =  user.ConnectionString;

            UnderWriter.UnderWriterID = UW_ID;
            UnderWriter.searchText = "";
            int.TryParse(ddlYear.SelectedItem.Text, out Year);
            UnderWriter.Year =Year  ;
            UnderWriter.RequetID = ReqID;
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

    private string validateUWEmailID(string strUWVal)
    {
        string[] arrUWDtl;
        if (strUWVal.Trim() == "")
            return "";

        strUWVal = strUWVal.Replace("(", "~");
        strUWVal = strUWVal.Replace(")", "~");

        arrUWDtl = strUWVal.Split('~');

        if (arrUWDtl[1] != "")
            return arrUWDtl[1];
        else
            return "";
    }

    private string getRemodelReason()
    {
        CheckBox objchkSelect = new CheckBox();
        TextBox objtxtOthers = new TextBox();
        string strCode = string.Empty;
        int reasonID=0;
        try
        {
            string strRemodelReasonCode = string.Empty;

            foreach (GridViewRow row in  grdRemodeReason.Rows )
            {
                objchkSelect = ((CheckBox)(row.Cells[0].FindControl("chckSelect")));

                if (objchkSelect.Checked)
                {
                    if (strCode != string.Empty) strCode = strCode + "~";

                    int.TryParse(grdRemodeReason.DataKeys[row.RowIndex]["Reason_ID"].ToString(), out reasonID);
                    objtxtOthers = ((TextBox)(row.Cells[1].FindControl("txtOthers")));

                    if (reasonID == 0)
                    {
                        strCode = strCode + objtxtOthers.Text;
                    }
                    else
                    {
                        strCode = strCode + reasonID.ToString();
                    }

                    objchkSelect.Checked = false;
                    objtxtOthers.Text = "";
                }

            }
            return strCode;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objtxtOthers = null;
            objchkSelect = null;
        }
    }

    protected void ddlUWList_SelectedIndexChanged(object sender, EventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objclsCommon = new clsCommon();
        try
        {
            getPendingAccountStatus();
            updatepnlddlUWList.Update();
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
            objclsCommon = null;
            objclsErrorLogger = null;
        }
    }

    protected void ddlAssignments_SelectedIndexChanged(object sender, EventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objclsCommon = new clsCommon();
        DataRow[] dRow; 
        int assignmentId;
        
        try
        {
            if (Session["Assignment"] != null)
            {
                int.TryParse(ddlAssignments.SelectedValue, out assignmentId);

                if (assignmentId == 0)
                {
                    imgAssignment.Visible = false;
                    hrefAssignment.HRef = "";

                    txAssigmentName.Text = "";
                    txtDescription.Text = "";
                }
                else
                {
                    dRow = ((DataTable)Session["Assignment"]).Select("AssignmentId=" + assignmentId.ToString());

                    foreach (DataRow row in dRow)
                    {
                        txAssigmentName.Text = row["AssignmentTitle"].ToString();
                        txtDescription.Text = row["AssignmentSummary"].ToString();
                        imgAssignment.Visible=true;
                        hrefAssignment.HRef ="UploadedDocument/Assignment/"+ row["AttachmentPath"].ToString()  ; 
                    }
                }

            }
            getPendingAccountStatus();
            updatepnlddlUWList.Update();
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
            objclsCommon = null;
            objclsErrorLogger = null;
        }
    }


    protected void grdStyled_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strStatus = string.Empty;
            strStatus = e.Row.Cells[7].Text.ToString().Trim();

            if (strStatus == "Cleansing In Progress")
            {
                e.Row.Cells[7].BackColor = System.Drawing.Color.Yellow;
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
            }
            else if (strStatus == "Modeling In Progress")
            {
                e.Row.Cells[7].BackColor = System.Drawing.Color.AliceBlue;
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
            }

            else if (strStatus == "Report In Progress")
            {
                e.Row.Cells[7].BackColor = System.Drawing.Color.LightCyan;
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
            }

            else if (strStatus == "Cleansing QC In Progress")
            {
                e.Row.Cells[7].BackColor = System.Drawing.Color.LemonChiffon;
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
            }
            else if (strStatus == "Modeling QC In Progress")
            {
                e.Row.Cells[7].BackColor = System.Drawing.Color.LightSkyBlue;
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
            }

            else if (strStatus == "Report QC In Progress")
            {
                e.Row.Cells[7].BackColor = System.Drawing.Color.LightBlue;
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
            }

            else if (strStatus == "Reassigned For Cleansing")
            {
                e.Row.Cells[7].BackColor = System.Drawing.Color.LightSalmon;
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
            }


            else if (strStatus == "Reassigned For Modeling")
            {
                e.Row.Cells[7].BackColor = System.Drawing.Color.Khaki;
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
            }

            else if (strStatus == "Reassigned For Report")
            {
                e.Row.Cells[7].BackColor = System.Drawing.Color.LavenderBlush;
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
            }

            else if (strStatus == "Cleansing QC Complete")
            {
                e.Row.Cells[7].BackColor = System.Drawing.Color.LightSeaGreen;
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
            }

            else if (strStatus == "Modeling QC Complete")
            {
                e.Row.Cells[7].BackColor = System.Drawing.Color.LightGoldenrodYellow;
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
            }
            else if (strStatus == "Report QC Complete")
            {
                e.Row.Cells[7].BackColor = System.Drawing.Color.BurlyWood;
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
            }
            else if (strStatus == "Assigned")
            {
                e.Row.Cells[7].BackColor = System.Drawing.Color.Aquamarine;
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
            }
            else if (strStatus == "Completed")
            {
                e.Row.Cells[7].BackColor = System.Drawing.Color.LawnGreen;
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
            }

            else if (strStatus == "Cancelled")
            {
                e.Row.Cells[7].BackColor = System.Drawing.Color.LightPink;
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
            }

            else if (strStatus == "Submitted For Cleansing QC")
            {
                e.Row.Cells[7].BackColor = System.Drawing.Color.LightSteelBlue;
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
            }

            else if (strStatus == "Submitted For Modeling QC")
            {
                e.Row.Cells[7].BackColor = System.Drawing.Color.Linen;
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
            }

            else if (strStatus == "Submitted For Report QC")
            {
                e.Row.Cells[7].BackColor = System.Drawing.Color.Beige;
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
            }

            else if (strStatus == "Pending")
            {
                e.Row.Cells[7].BackColor = System.Drawing.Color.GhostWhite;
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
            }
            else if (strStatus == "In Process")
            {
                e.Row.Cells[7].BackColor = System.Drawing.Color.Crimson;
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
            }
        }
                
    } 

    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int reasonID;
            TextBox objtxtOthers = new TextBox();
            int.TryParse(grdRemodeReason.DataKeys[e.Row.RowIndex]["Reason_ID"].ToString(), out reasonID);
            objtxtOthers = ((TextBox)(e.Row.Cells[1].FindControl("txtOthers")));
            if (reasonID == 0)
            {
                objtxtOthers.Visible = true;
                objtxtOthers.Text = string.Empty;
            }
            else
            {
                objtxtOthers.Visible = false;
            }
        }
    }


    [WebMethod]
    public static List<string> getSuggestAccountName(string prefixText, int count)//string hdUW, string SrchAcc, string ConStr)
    {
        //string UW, string SrchAcc, string ConStr
        List<string> AccResult = new List<string>();
        using (SqlConnection con = new SqlConnection(WM_Con_Str))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "SELECT distinct ACCOUNTNAME FROM dbo.ACCOUNTNAME_SUGGESTION_LIST(NOLOCK)   where UNDERWRITER = ''+@UW+''  and  ACCOUNTNAME LIKE ''+@AccName+'%' order by ACCOUNTNAME ";
                cmd.Connection = con;
                con.Open();

                cmd.Parameters.AddWithValue("@UW", WM_UW);
                cmd.Parameters.AddWithValue("@AccName", prefixText);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    AccResult.Add(dr["ACCOUNTNAME"].ToString());
                }
                con.Close();
                return AccResult;
            }
        }
    }

    private void getSubmittedAccountInDueDate(int assignmentID) 
    {

        objUnderWriter UnderWriter = new objUnderWriter();
        clsUnderWriter objclsUnderWriter = new clsUnderWriter();
        DataTable dtInfo;
        string strResult = string.Empty;
        try
        {

            strResult = "Do you wish to submit the request for the selected account?";
            UnderWriter.ConnectionString = user.ConnectionString;
            UnderWriter.AssignmentID = assignmentID;
             
            dtInfo = objclsUnderWriter.getAccountLimitInADueDate(UnderWriter);

            if (dtInfo != null)
            {
                if (dtInfo.Rows[0]["ALERT_STS"].ToString() == "Y")
                {
                    strResult = "Already have " + dtInfo.Rows[0]["REQUEST_SUBMITTED"].ToString() + " accounts on selected date, want to select the next date?";
                }
            }

            hdAlertMessage.Value = strResult;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dtInfo = null;
        }
    }


    protected void dummybtn_Click(object sender, EventArgs e)
    {
        //string selectedDate = hdCalendarVal.Value;
        //UpdatePanelDueDate.Update();
    }
 
}