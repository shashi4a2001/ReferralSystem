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
 
using System.Security;
using System.Security.Authentication;

public partial class Operation_SignupSt : System.Web.UI.Page
{
 
    string M_strAlertMessage = string.Empty;
    string _strMsgType = string.Empty;
    string _strAlertMessage = string.Empty;
    string M_strWebRegistration = string.Empty;
    ObjPortalUser user;
    
    public static string WM_UW { get; set; }
    public static string WM_Con_Str { get; set; }
    public static string WM_Due_Date { get; set; }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objCommon = new clsCommon();
        try
        {
            if (Session["PortalUserDtl"] != null)//ByPass when redirected from login page
            {
                M_strWebRegistration = "Y";
                user = (ObjPortalUser)Session["PortalUserDtl"];
                user.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["CMRConnection"]);

                if (user.UserName == null || user.UserName == "Guest")
                {
                    M_strWebRegistration = "N";
                    this.Page.MasterPageFile = "~/Operation/CRMUsers.master";
                }
                else
                {
                    this.Page.MasterPageFile = "~/Operation/CRMOperationArea2.master";
                }
            }
            else
            {
                M_strWebRegistration = "N";
                //Session["PortalUserDtl"] = null;
                user = new ObjPortalUser();
                user.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["CMRConnection"]);
                this.Page.MasterPageFile = "~/Operation/CRMUsers.master";
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
                objCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);

                UpdatePanel1.Update();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }

            objclsErrorLogger = null;
            objCommon = null;
        }
    }
    protected void Page_Load(object sender, EventArgs e) 
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objCommon=new clsCommon ();
        try
        { 
            //Check If Page is directly called from website
            if (hdCalledFromPortal.Value == "Y" || M_strWebRegistration=="Y") //if (Session["PortalUserDtl"] != null)//ByPass when redirected from login page
            {
                hdCalledFromPortal.Value = "Y";
                M_strWebRegistration="Y";
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

                user.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["CMRConnection"]);
                hdCalledFromPortal.Value = "Y";
                M_strWebRegistration = "Y";
                if (objCommon.checkPageAccessiblity(user.EmailId, System.IO.Path.GetFileName(Request.Path), user.ConnectionString) != "Y")
                    Response.Redirect("~/Operation/AccessDenied.aspx", false);
            }
            

            //Validate User Authetication Ends
            if (IsPostBack == false)
            {
                if (hdCalledFromPortal.Value != "Y" ||  M_strWebRegistration !="Y")
                {
                    getDefaultLogin();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "DisplayEntryListOnly();", true);
                }

                //Get All State List
                getComboData();
                txtApplicantName.Focus();

                clearControls();
            }

            //Check Page Accessibility
            if (user != null && M_strWebRegistration == string.Empty && hdCalledFromPortal.Value == "Y")
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
        clsOlympiad objclsOlympiad = new clsOlympiad();
        objOlympiad Olympiad = new objOlympiad();

        string confirmValue = string.Empty;
        string strSOVName = string.Empty;
        string strFolderName = string.Empty;
        string strSOVNameListHdr = string.Empty;
        string strSOVNameListDtl = string.Empty;
        int stateID,PIN,schoolID, courseID;

        try
        {
            lblMessage.Text = "";
            user = (ObjPortalUser)Session["PortalUserDtl"];

            //Applicant Name
            if (txtApplicantName.Text.Trim() != "")
            {
                if (objclsCommon.isValidInput(user.RestrictedInputChar, txtApplicantName.Text.Trim()) == false)
                {
                    _strMsgType = "Info";
                    _strAlertMessage = "Invalid Applicant Name. Contains invalid characters.";
                    return;
                }
            }

            //father Name
            if (txtFatherName.Text.Trim() != "")
            {
                if (objclsCommon.isValidInput(user.RestrictedInputChar, txtFatherName.Text.Trim()) == false)
                {
                    _strMsgType = "Info";
                    _strAlertMessage = "Invalid Father Name. Contains invalid characters.";
                    return;
                }
            }

            //mother Name
            if (txtMotherName.Text.Trim() != "")
            {
                if (objclsCommon.isValidInput(user.RestrictedInputChar, txtFatherName.Text.Trim()) == false)
                {
                    _strMsgType = "Info";
                    _strAlertMessage = "Invalid Mother Name. Contains invalid characters.";
                    return;
                }
            }

            //Nationality
            if (txtNationality.Text.Trim() != "")
            {
                if (objclsCommon.isValidInput(user.RestrictedInputChar, txtNationality.Text.Trim()) == false)
                {
                    _strMsgType = "Info";
                    _strAlertMessage = "Invalid Nationality Name. Contains invalid characters.";
                    return;
                }
            }

            int.TryParse(ddlState.SelectedValue, out stateID);
            int.TryParse(ddlCourse.SelectedValue, out courseID);
            int.TryParse(txtPinCode.Text, out PIN);

            Olympiad.ApplicantName = txtApplicantName.Text.Trim().ToUpper();
            Olympiad.Address  = txtAddress.Text.Trim();
            Olympiad.PinCode = txtPinCode.Text.Trim();
            Olympiad.StateID = stateID ;
            Olympiad.Occupation = txtOccupation.Text.Trim();
            Olympiad.MobileNo = txtMobile.Text.Trim(); ;
            Olympiad.EmailID = txtEmail.Text.Trim();
            Olympiad.FatherName = txtFatherName.Text.Trim(); ;
            Olympiad.MotherName = txtMotherName.Text;
            Olympiad.DOB = objclsCommon.ConvertDateForInsert   (  txtDob.Text.Trim());
            Olympiad.MartialStatus = rdoMarried.Checked ? "M" : "U";
            Olympiad.CourseID = courseID;
            Olympiad.USerID = txtApplicantName.Text.Trim();
            Olympiad.Nationality = txtNationality.Text.Trim();
            Olympiad.Status = "1";
            Olympiad.ConnectionString = user.ConnectionString;
            Olympiad.MachineIP  = objclsCommon.GetIPAddress() ;
 
 
            if (hdId.Value == "" || hdId.Value == "0")
            {
                Olympiad.Mode = "I"  ;
                dtResult = objclsOlympiad.SignUpStudent(Olympiad);
            }
            else
            {
                Olympiad.Mode = "U";
                int.TryParse(hdId.Value, out schoolID );
                Olympiad.SchoolID = schoolID ;
                dtResult = objclsOlympiad.CreateSchoolRegistrationRequests(Olympiad);
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

                if (Olympiad.Mode == "I")
                {
                    if (dtResult.Rows.Count > 0)
                    {

                        ObjSendEmail SendMail = new ObjSendEmail();
                        DataTable dtInformation = new DataTable();
                        clsCommon objClsCommon = new clsCommon();
                        string strRequestID = string.Empty;
                        int smtpPort;
                        SendMail.CC = "";

                        //Send Mail to Student on Registration Submit
                        if (hdId.Value == "" || hdId.Value == "0")
                        {
                            dtInformation = objClsCommon.getMailBody("MailFormat", "MAIL ON ONLINE REGISTRATION REQUEST", user.ConnectionString);
                        }

                        try
                        {
                            if (dtInformation != null & dtInformation.Rows.Count > 0)
                            {
                                SendMail.From = user.SMTPUSerID;
                                SendMail.To = Olympiad.EmailID.Trim();
                                SendMail.CC = string.Empty;
                                SendMail.BCC = "";// objClsCommon.getConfigKeyValue("EMAIL IDS FOR CC OF MAIL MESSAGE FOR EACH OPERATION", "EMAIL RECEPIENTS GROUP", user.ConnectionString); ;
                                SendMail.SMTPHost = user.SMTPServer;
                                SendMail.SMTPHost = user.SMTPServer;
                                int.TryParse(user.SMTPPort, out smtpPort);
                                SendMail.SMTPPort = smtpPort;
                                SendMail.UserId = user.SMTPUSerID;
                                SendMail.Password = user.SMTPPassword; ;
                                SendMail.SMTPSSL = user.SMTPSSL == "Y" ? true : false; ;
                                SendMail.MailType = "I";
                                SendMail.Subject = user.Orig_Name + ": Student Signup Acknowledgement";
                                SendMail.Password = user.SMTPPassword;
                                SendMail.MailBody = dtInformation.Rows[0]["MAIL_BODY"].ToString();
                                SendMail.RequestID = "";
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
                        //Send Mail Ends

                        if (hdId.Value == "" || hdId.Value == "0")
                        {
                            dtInformation = objClsCommon.getMailBody("MailFormat", "MAIL ON ONLINE REGISTRATION REQUEST(ADMIN)", user.ConnectionString);
                        }

                        //Send mail to Admin against each Request
                        try
                        {

                            if (dtInformation != null & dtInformation.Rows.Count > 0)
                            {
                                SendMail.BCC = "";
                                SendMail.From = user.SMTPUSerID;
                                SendMail.To = objClsCommon.getConfigKeyValue("EMAIL IDS FOR CC OF MAIL MESSAGE FOR EACH OPERATION", "EMAIL RECEPIENTS GROUP", user.ConnectionString);

                                if (SendMail.To.Contains(user.EmailId) == false)
                                    SendMail.CC = user.EmailId;

                                SendMail.SMTPHost = user.SMTPServer;
                                int.TryParse(user.SMTPPort, out smtpPort);
                                SendMail.SMTPPort = smtpPort;
                                SendMail.UserId = user.SMTPUSerID;
                                SendMail.Password = user.SMTPPassword;
                                SendMail.SMTPSSL = user.SMTPSSL == "Y" ? true : false; ;
                                SendMail.MailType = "N";//NO REPLY
                                SendMail.Subject = user.Orig_Name + ": Student Signup Registration Request Received";

                                SendMail.MailBody = dtInformation.Rows[0]["MAIL_BODY"].ToString();
                                SendMail.MailBody = SendMail.MailBody.Replace("#ApplicantName#", Olympiad.ApplicantName.ToString().ToUpper());
                                SendMail.MailBody = SendMail.MailBody.Replace("#Address#", Olympiad.Address);
                                SendMail.MailBody = SendMail.MailBody.Replace("#MobileNo#", Olympiad.MobileNo);
                                SendMail.MailBody = SendMail.MailBody.Replace("#EmailID#", Olympiad.EmailID);
                                SendMail.MailBody = SendMail.MailBody.Replace("#Course#", ddlCourse.SelectedItem.Text);
                                SendMail.RequestID = "";

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

                }
                //Clear Controls
                clearControls();

                //Get Underwriter Request
                //getRegistrationRequest(txtSearch.Text.Trim());

                lblMessage.Text = _strAlertMessage;

                if (hdCalledFromPortal.Value != "N")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "setFocusToEntryTab('UWList')", true);

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
            if (hdCalledFromPortal.Value != "N")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "setFocusToEntryTab('EntryTab')", true);
            }

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
 
        DataTable dtResult;
        clsCommon objclsCommon = new clsCommon();
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsOlympiad objclsOlympiad = new clsOlympiad();
        objOlympiad Olympiad = new objOlympiad();

        string strLoggedInUser = string.Empty;
        string confirmValue = string.Empty;
        string strSOVName = string.Empty;

        int schoolID ;
        try
        {
            lblMessage.Text = "";

            Olympiad.ConnectionString = user.ConnectionString;
            Olympiad.USerID = grdStyled.Rows[e.RowIndex].Cells[14].Text.Trim();

            int.TryParse(grdStyled.DataKeys[e.RowIndex]["SCHOOL_ID"].ToString(), out schoolID);

            Olympiad.SchoolID = schoolID;
            dtResult = objclsOlympiad.DeleteOlympiadSchoolRegistrationRequests(Olympiad);

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

                //Clear Controls
                clearControls();

                //Get Underwriter Request
                getRegistrationRequest(txtSearch.Text.Trim());
                UpdatePanel2.Update();
                lblMessage.Text = _strAlertMessage;

                if (hdCalledFromPortal.Value != "N")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "setFocusToEntryTab('UWList')", true);
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

            objclsOlympiad = null;
            dtResult = null;
            objclsCommon = null;
            Olympiad = null;
            objclsErrorLogger = null;
        }
    }

    protected void grdStyled_PageIndexChanging(Object sender, GridViewPageEventArgs e)
    {
        grdStyled.PageIndex = e.NewPageIndex;
        getRegistrationRequest(txtSearch.Text.Trim());
    }

    protected void grdStyled_SelectedIndexChanged(object sender, EventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objclsCommon = new clsCommon();

        int SchoolID;
        int stateID;
        string ImgPath = string.Empty;

        try
        {
            clearControls();
            lblMessage.Text = string.Empty;

            int.TryParse(grdStyled.DataKeys[grdStyled.SelectedIndex]["SCHOOL_ID"].ToString(), out SchoolID);
            int.TryParse(grdStyled.DataKeys[grdStyled.SelectedIndex]["ADDRESS_STATE"].ToString(), out stateID);

            if (SchoolID > 0)
            {
                hdId.Value = Convert.ToString(SchoolID);

                ddlCourse.SelectedValue = stateID.ToString();

                //txtPricipalName.Text = grdStyled.Rows[grdStyled.SelectedIndex].Cells[5].Text.Trim();
                //txtSchoolName.Text = Server.HtmlDecode(grdStyled.Rows[grdStyled.SelectedIndex].Cells[4].Text).Trim();
                //txtBoard.Text = grdStyled.Rows[grdStyled.SelectedIndex].Cells[9].Text.Trim();
                //txtAddress.Text = Server.HtmlDecode(grdStyled.Rows[grdStyled.SelectedIndex].Cells[7].Text).Trim();
                //txtBoard.Text = grdStyled.Rows[grdStyled.SelectedIndex].Cells[6].Text.Trim();
                //txtPIN.Text = grdStyled.Rows[grdStyled.SelectedIndex].Cells[11].Text;
                //txtCity.Text = grdStyled.Rows[grdStyled.SelectedIndex].Cells[10].Text.Trim();
                //txtDistrict.Text = grdStyled.Rows[grdStyled.SelectedIndex].Cells[8].Text.Trim();
                //txtMobile.Text = grdStyled.Rows[grdStyled.SelectedIndex].Cells[13].Text;
                //txtLandLine.Text = grdStyled.Rows[grdStyled.SelectedIndex].Cells[12].Text;
                //txtEmail.Text = grdStyled.Rows[grdStyled.SelectedIndex].Cells[14].Text.Trim();
                //txtWebSite.Text = Server.HtmlDecode(grdStyled.Rows[grdStyled.SelectedIndex].Cells[15].Text).Trim();

                btnSubmit.Text = "Update";
            }

            UpdatePanel1.Update();

            if (hdCalledFromPortal.Value != "N")
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

            if (hdCalledFromPortal.Value != "N")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "setFocusToEntryTab('EntryTab')", true);
            }

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
            clearControls();

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
 
    private void clearControls()
    {
        try
        {
            hdId.Value = string.Empty;
            txtApplicantName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtPinCode.Text = string.Empty;
            txtOccupation.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtDob.Text = string.Empty;
            txtFatherName.Text = string.Empty;
            txtMotherName.Text = string.Empty;
            txtSearch.Text = string.Empty;
            ddlCourse.SelectedIndex = 0;
            ddlState.SelectedIndex = 0;
            hdAlertMessage.Value = "Do you wish to submit the request for the selected account?";
            btnSubmit.Text = "Save";
            txtApplicantName.Focus();
            UpdatePanel1.Update();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void getComboData()
    {

        clsCommonUtility objclsCommonUtility = new clsCommonUtility();
        ObjMaster master = new ObjMaster();
        clsOlympiad objclsOlympiad = new clsOlympiad();
        DataTable dtInfo;
        try
        {
            dtInfo = objclsOlympiad.getStateList( user.ConnectionString);

            if (dtInfo != null)
            {
                ddlState.DataSource = dtInfo;
                ddlState.DataValueField = "STATE_ID";
                ddlState.DataTextField = "STATE_NAME";
                ddlState.DataBind();
            }

            //For Course
            dtInfo = null;
            master.SearchText = "";
            master.ConnectionString = user.ConnectionString;
            dtInfo = objclsCommonUtility.ShowCourse(master);

            ddlCourse.DataSource = dtInfo;
            ddlCourse.DataValueField = "Course_id";
            ddlCourse.DataTextField = "Course_Name";
            ddlCourse.DataBind();

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dtInfo = null;
            objclsOlympiad = null;
            master = null;
            objclsCommonUtility = null;

        }

    }
 
    private void getRegistrationRequest(string strText)
    {
        objOlympiad Olympiad = new objOlympiad();
        clsOlympiad objclsOlympiad = new clsOlympiad();
        DataTable dtInfo;
        try
        {
            Olympiad.ConnectionString =  user.ConnectionString;

            if (rdoPending.Checked)
            {
                Olympiad.Status = "1";
            }
            else
            {
                Olympiad.Status = "-1";
            }

            Olympiad.searchText = strText.Trim();
            dtInfo = objclsOlympiad.getStudentSignupRequest (Olympiad);

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
            objclsOlympiad = null;
        }
    }


    protected void btnGetDetails_Click(object sender, ImageClickEventArgs e)
    {
        //Registration Request
        getRegistrationRequest(txtSearch.Text.Trim());

        if (hdCalledFromPortal.Value != "N")
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "setFocusToEntryTab('UWList')", true);
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        //Registration Request
        getRegistrationRequest(txtSearch.Text.Trim());

        if (hdCalledFromPortal.Value != "N")
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "setFocusToEntryTab('UWList')", true);
    }
 
    protected void grdStyled_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strStatus = string.Empty;
            strStatus = e.Row.Cells[0].Text.ToString().Trim();

            if (strStatus == "ACTIVE")
            {
                e.Row.Cells[0].BackColor = System.Drawing.Color.Green ;
                e.Row.Cells[0].ForeColor = System.Drawing.Color.White ;
            }
            else if (strStatus == "PENDING")
            {
                e.Row.Cells[0].BackColor = System.Drawing.Color.Yellow ;
                e.Row.Cells[0].ForeColor = System.Drawing.Color.Red;
            }

            else if (strStatus == "DEACTIVATED")
            {
                e.Row.Cells[0].BackColor = System.Drawing.Color.Gray;
                e.Row.Cells[0].ForeColor = System.Drawing.Color.White;
                e.Row.Enabled = false;
            }
 
        }
                
    }
 
    protected void getDefaultLogin()
    {
        ObjPortalUser user = new ObjPortalUser();
        STPclsLogin objSTPclsLogin = new STPclsLogin();
        clsCommon objclsCommon = new clsCommon();
        DataTable dtInfo;

        string _strUserID = string.Empty;
        string _strEmailID = string.Empty;
        string _strPassword = string.Empty;
        string _strAlertMessage = string.Empty;
        int uID, schoolID;

        try
        {

            lblMessage.Text = string.Empty;
            user.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["CMRConnection"]);
            user.IPAddress = objclsCommon.EncryptData(objclsCommon.GetIPAddress());
            dtInfo = objSTPclsLogin.GetDefaultLogin(user.IPAddress, user.ConnectionString);

            if (dtInfo != null && dtInfo.Rows.Count > 0)
            {
                int.TryParse(dtInfo.Rows[0]["ID"].ToString(), out uID);
                user.UId = uID;
                user.UserId = dtInfo.Rows[0]["USER_ID"].ToString();
                user.UserName = dtInfo.Rows[0]["USER_NAME"].ToString();
                user.UserRole = "Student";// dtInfo.Rows[0]["ROLE_NAME"].ToString();
                user.EmailId = _strEmailID;

                //smtp/po3
                user.SMTPPort = dtInfo.Rows[0]["SMTP_PORT"].ToString();
                user.SMTPUSerID = dtInfo.Rows[0]["SMTP_USER"].ToString();
                user.SMTPPassword = string.Empty;
                user.SMTPPassword = dtInfo.Rows[0]["SMTP_PASSWORD"].ToString();
                user.SMTPServer = dtInfo.Rows[0]["SMTP_SERVER"].ToString();
                user.SMTPSSL = dtInfo.Rows[0]["SMTP_SSL"].ToString();

                user.POPSSL = dtInfo.Rows[0]["POP3_SSL"].ToString();
                user.POPPort = dtInfo.Rows[0]["POP3_PORT"].ToString();
                user.POPUSerID = dtInfo.Rows[0]["POP3_USER"].ToString(); ;
                user.POPPassword = string.Empty;
                user.POPPassword = dtInfo.Rows[0]["POP3_PASSWORD"].ToString();
                user.POPServer = dtInfo.Rows[0]["POP3_SERVER"].ToString();
                user.POPByDefaultPort = dtInfo.Rows[0]["POPByDefaultPort"].ToString();
                user.POPDefaultPort = dtInfo.Rows[0]["POPDefaultPort"].ToString();

                user.IsUW = dtInfo.Rows[0]["IS_UW"].ToString();
                int.TryParse(dtInfo.Rows[0]["SCHOOL_ID"].ToString(), out schoolID);
                user.SchoolID = schoolID;
                user.ActingUW = dtInfo.Rows[0]["USER_NAME"].ToString();
                user.ActingUW_ID = user.UId;

                user.ReqDashobard = dtInfo.Rows[0]["REQ_DASHBOARD"].ToString();
                user.ReqChangePassword = dtInfo.Rows[0]["REQ_CHANGE_PASSWORD"].ToString();
                user.LandingPage = dtInfo.Rows[0]["LANDING_PAGE"].ToString();
                user.SelectUW = dtInfo.Rows[0]["SELECT_UW"].ToString();
                user.FirstTimeLoggedIn = dtInfo.Rows[0]["FirstTimeLoggedIn"].ToString();
                user.SOVFileExtns = dtInfo.Rows[0]["SOVFileExtns"].ToString();
                user.RestrictedInputChar = dtInfo.Rows[0]["Restricted_Input_Char"].ToString();


                //Organizational details
                user.Orig_Code = Convert.ToString(dtInfo.Rows[0]["Orig_Code"].ToString());
                user.Orig_Name = Convert.ToString(dtInfo.Rows[0]["Orig_Name"].ToString());
                user.Orig_Address1 = Convert.ToString(dtInfo.Rows[0]["Orig_Address1"].ToString());
                user.Orig_Address2 = Convert.ToString(dtInfo.Rows[0]["Orig_Address2"].ToString());
                user.Orig_Address3 = Convert.ToString(dtInfo.Rows[0]["Orig_Address3"].ToString());
                user.Orig_Phone1 = Convert.ToString(dtInfo.Rows[0]["Orig_Phone1"].ToString());
                user.Orig_Phone2 = Convert.ToString(dtInfo.Rows[0]["Orig_Phone2"].ToString());
                user.Orig_Url = Convert.ToString(dtInfo.Rows[0]["Orig_Url"].ToString());
                user.Orig_Email1 = Convert.ToString(dtInfo.Rows[0]["Orig_Email1"].ToString());
                user.Orig_Email2 = Convert.ToString(dtInfo.Rows[0]["Orig_Email2"].ToString());
                user.Orig_Logo1 = Convert.ToString(dtInfo.Rows[0]["Orig_Logo1"].ToString());
                user.Orig_Logo2 = Convert.ToString(dtInfo.Rows[0]["Orig_Logo2"].ToString());
                user.Orig_Year_Reserved = Convert.ToString(dtInfo.Rows[0]["Orig_Year_Reserved"].ToString());


                int SOVFileSize;
                int.TryParse(dtInfo.Rows[0]["SOVFileSize"].ToString(), out SOVFileSize);
                user.SOVFileSize = SOVFileSize;

                int LogId;
                int.TryParse(dtInfo.Rows[0]["LOG_ID"].ToString(), out LogId);
                user.LogId = LogId;

                //Store in Session Object
                Session["PortalUserDtl"] = user;
            }
        }
        catch (ThreadAbortException)
        {
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
        }
    }
}