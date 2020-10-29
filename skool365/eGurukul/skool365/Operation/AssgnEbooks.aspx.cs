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

public partial class Operation_AssgnEbooks : System.Web.UI.Page
{
    string M_strAlertMessage = string.Empty;
    string _strAlertMessage = string.Empty;
    string _strMsgType = string.Empty;
    ObjPortalUser user;

    string M_strcon = string.Empty;
    string M_UserId = string.Empty;
 
    string[] Qry;

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

                bindCombos();
                //Display Center Details in the Grid
                BindGrid("");
                //Bind State Details in Combo box
                txtSubject.Focus();
                txtPostedDate.Text = "";// DateTime.Now.ToString("dd/MM/yyyy");

                if (rdoAssignment.Checked)
                {
                    rowStudent.Visible = true;
                }
 
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



    private void bindCombos()
    {
        clsCommonUtility objclsCommonUtility = new clsCommonUtility();
        ObjMaster master = new ObjMaster();
        DataTable dtInfo;
        ListItem item;
        try
        {
            master.Mode = "SESSION";
            master.ConnectionString = user.ConnectionString;
            dtInfo = objclsCommonUtility.getCommonMaster(master);

            if (dtInfo != null)
            {
                ddlSession.DataSource = dtInfo;
                ddlSession.DataValueField = "SESSION";
                ddlSession.DataTextField = "SESSION";
                ddlSession.DataBind();
            }

            if (ddlSession.Items.Count > 0)
            {
                item = new ListItem();
                item.Value = "0";
                item.Text = "All";
                ddlSession.Items.Insert(0, item);
                ddlSession.SelectedValue = "0";
            }

         
            //For Course
            dtInfo = null;
            master.SearchText = "";
            dtInfo = objclsCommonUtility.ShowCourse(master);

            ddlCourse.DataSource = dtInfo;
            ddlCourse.DataValueField = "Course_id";
            ddlCourse.DataTextField = "Course_Name";
            ddlCourse.DataBind();

            if (ddlCourse.Items.Count > 0)
            {
                item = new ListItem();
                item.Value = "0";
                item.Text = "All";
                ddlCourse.Items.Insert(0, item);
                ddlCourse.SelectedValue = "0";
            }
         

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            item = null;
            master = null;
            dtInfo = null;
            objclsCommonUtility = null;
        }
    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        clsCommon objCommon = new clsCommon();
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsAssignments objclsAssignments = new clsAssignments();
        EC_Assignments objEC_Assignments = new EC_Assignments();
        DataTable dtResult = new DataTable();
        string ActualFileName, FileNewPath;
        string _strAlertMessage = string.Empty;

        ActualFileName = string.Empty;
        FileNewPath = string.Empty;

        try
        {

            string NewFilename = "#";

            if (btnSubmit.Text == "Save" && FileUpload1.HasFile == false)
            {
                _strAlertMessage = "Please upload the document for Assignment/eBooks!";
                return;
            }

            NewFilename = "";
            if (FileUpload1.HasFile == true)
            {
                //Get Actual File Name
                ActualFileName = FileUpload1.FileName.ToString();
                //Get New File Name
                NewFilename = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss-FFFFF") + "-" + ActualFileName;
                //Set New Path to save
                FileNewPath = Server.MapPath("UploadedDocument\\Assignment") + "\\" + NewFilename;
                //Save File with New Name at specific folder
                FileUpload1.SaveAs(FileNewPath);
            }

            int SemesterId, CourseId, BatchId;
            string strStudentSrchInput = string.Empty;

            if (chkNotification.Checked && rdoAssignment.Checked)
            {
                string SessionCode = ddlSession.SelectedItem.Text;
                int.TryParse(ddlCourse.SelectedValue, out CourseId);
                strStudentSrchInput = SessionCode + "|" + CourseId.ToString()  ;
            }


            objEC_Assignments.AssignmentTitle = txtSubject.Text.Trim();
            objEC_Assignments.AssignmentSummary = txtDescription.Text.Trim();
            objEC_Assignments.AssignmentType = rdoAssignment.Checked ? rdoAssignment.Text : rdoEbook.Text; 
            objEC_Assignments.AttachmentPath = NewFilename;
            objEC_Assignments.PostedDate = "";// ConvertDateForInsert(txtPostedDate.Text);
            objEC_Assignments.CreatedBy = user.UserId;
            objEC_Assignments.Is_Submission_Required = chkResponseReq.Checked ? "Y" : "N";
            objEC_Assignments.Is_ForLifeTime = rdoAlwaysLive.Checked ? "Y" : "N";

            if (rdoAlwaysLive.Checked)
            {
                objEC_Assignments.Is_ForLifeTime = "Y";
                objEC_Assignments.AvailableTillDate = "";
                if (txtPostedDate.Text.Trim() != string.Empty)
                {
                    _strAlertMessage = "Activation Till Date is not appicable for this option, Make it blank!";
                    return;
                }
            }
            else
            {
                objEC_Assignments.Is_ForLifeTime = "N";
                if (txtPostedDate.Text.Trim() == string.Empty)
                {
                    _strAlertMessage = "Please enter the date Activation Till Date!";
                    return;
                }
                objEC_Assignments.AvailableTillDate =  ConvertDateForInsert(txtPostedDate.Text.Trim());
            }

            objEC_Assignments.ConnectionString = user.ConnectionString;

            objEC_Assignments.GrdiSrchInput = strStudentSrchInput;
            objEC_Assignments.ConnectionString = user.ConnectionString;

            objEC_Assignments.AssignmentId = 0;
            if (btnSubmit.Text != "Save")
            {           
                if (hdId.Value != null && hdId.Value != "")
                {
                    objEC_Assignments.AssignmentId = Convert.ToInt32(hdId.Value);
                }
            }

            dtResult = objclsAssignments.SaveAssignments(objEC_Assignments, true);

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
                lblUploadRemarks.Visible = false;
                _strAlertMessage = dtResult.Rows[0]["SUCCESS"].ToString().Trim();
            }

            //send Notification in case if Assignment upload
            if (chkNotification.Checked && rdoAssignment.Checked && grdDetails.Rows.Count > 0 && rowStudent.Visible == true)
            {
                //SendSMSAlert();
                SendMailAlert("uploaded", FileNewPath);
            }

            BindGrid("");
            ClearControls(false);
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
            objclsAssignments = null;
            objEC_Assignments = null;
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


    private void BindGrid(string strSearchText)
    {
        clsAssignments objclsAssignments = new clsAssignments();
        EC_Assignments objEC_Assignments = new EC_Assignments();
        DataTable dt;
        try
        {
            objEC_Assignments.ConnectionString = user.ConnectionString;
            objEC_Assignments.GrdiSrchInput = strSearchText;
            dt = objclsAssignments.ShowAssignments(objEC_Assignments);

            grdEvents.DataSource = dt;
            grdEvents.DataBind();

            if (grdEvents.Rows.Count > 0)
            {
                grdEvents.Columns[13].Visible = false;
                grdEvents.Columns[14].Visible = false;
                grdEvents.Columns[15].Visible = false;
                grdEvents.Columns[16].Visible = false;
            }

            lblUploadRemarks.Visible = false;
            hdId.Value = "";

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objclsAssignments = null;
            dt = null;
        }
    }
    private void ClearControls(Boolean blnclearStudentGrid)
    {
        txtComments.Text = "";
        txtSubject.Text = "";
        txtDescription.Text = "";
        txtPostedDate.Text = "";//DateTime.Now.ToString("dd/MM/yyyy");
        txtSubject.Focus();
        lblUploadRemarks.Visible = false;
        rdoLiveUpdate.Checked = true;
        txtPostedDate.Enabled = true;
        chkResponseReq.Checked = false;
        btnSubmit.Text = "Save";

        //for notification
        if (blnclearStudentGrid == true)
        {
            lblstatusMsg.Text = "";
            grdDetails.DataSource = null;
            grdDetails.DataBind();
        }

        UpdatePanel1.Update();
    }
    protected void grdStyled_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        clsCommon objCommon = new clsCommon();
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsAssignments objclsAssignments = new clsAssignments();
        EC_Assignments objEC_Assignments = new EC_Assignments();
        DataTable dtResult = new DataTable();
        string _strAlertMessage = string.Empty;
        string strFileName = string.Empty;

        try
        {
            int AssignmentId = Convert.ToInt32(grdEvents.DataKeys[Convert.ToInt32(e.RowIndex)]["AssignmentId"]);
            strFileName = "";
            objEC_Assignments.AssignmentId = AssignmentId;
            objEC_Assignments.CreatedBy = user.UserId;
            objEC_Assignments.ConnectionString = user.ConnectionString;
            dtResult = objclsAssignments.DeleteAssignments(objEC_Assignments);

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
                lblUploadRemarks.Visible = false;
                _strAlertMessage = dtResult.Rows[0]["SUCCESS"].ToString().Trim();
            }

            //Delete Uploaded Assignment/Ebooks
            try
            {
                //Set New Path to save
                string FileNewPath = Server.MapPath("UploadedDocument\\StudentPortalDoc") + "\\" + grdEvents.DataKeys[Convert.ToInt32(e.RowIndex)]["AttachmentPath"];
                if (System.IO.File.Exists(FileNewPath))
                {
                    System.IO.File.Delete(FileNewPath);
                }
            }
            catch { }

            lblMessage.Text = "";
            BindGrid("");
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

    protected void grdEvents_SelectedIndexChanged(object sender, EventArgs e)
    {
        clsCommon objCommon = new clsCommon();
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        string _strAlertMessage = string.Empty;
        int assignmentId;
        try
        {
            int.TryParse(grdEvents.DataKeys[grdEvents.SelectedIndex]["AssignmentId"].ToString(), out assignmentId);
            txtSubject.Text = grdEvents.Rows[grdEvents.SelectedIndex].Cells[2].Text.ToString();
            txtDescription.Text = grdEvents.Rows[grdEvents.SelectedIndex].Cells[3].Text.ToString();
 
            if (grdEvents.Rows[grdEvents.SelectedIndex].Cells[4].Text.ToString() == "Assignment")
            {
                rdoAssignment.Checked = true;
            }
            else
            {
                rdoEbook.Checked = true;
            }
            //Life Time Accessible
            if (grdEvents.Rows[grdEvents.SelectedIndex].Cells[5].Text.ToString() == "Y")
            {
                rdoAlwaysLive.Checked = true;
            }
            else
            {
                rdoLiveUpdate.Checked = true;
                try
                {
                    txtPostedDate.Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(grdEvents.Rows[grdEvents.SelectedIndex].Cells[6].Text.ToString()));
                }
                catch { }
            }
            //Submission Req?
            chkResponseReq.Checked = false;
            if (grdEvents.Rows[grdEvents.SelectedIndex].Cells[7].Text.ToString() == "Y")
            {
                chkResponseReq.Checked = true;
            }

            hdId.Value = assignmentId.ToString();
            //Set Student Grid Parameters
            string[] Qry;
            string SrchInput;
            int SemesterId, CourseId;

            //Grid_Qry
            SrchInput = (grdEvents.Rows[grdEvents.SelectedIndex].Cells[14].Text.ToString()).Trim();
            SrchInput = SrchInput.Replace("&nbsp;", "");
           
            if (SrchInput != "")
            {
                chkNotification.Checked = true;
                Qry = SrchInput.Split('|');

                ddlSession.SelectedItem.Text = Qry[0].ToString();
                ddlCourse.SelectedValue = Qry[1].ToString();
 
                //Bind Batch DropDown
                string SessionCode = ddlSession.SelectedItem.Text;
                int.TryParse(ddlCourse.SelectedValue, out CourseId);
  
                btnPopulate_Click(btnPopulate, e);
            }
            else
            {
                bindCombos();

                grdDetails.DataSource = null;
                grdDetails.DataBind();

                lblstatusMsg.Text = "Total Record(s) : 0";
            }
            
            btnSubmit.Text = "Update";
            lblUploadRemarks.Visible = true;
            lblMessage.Text = "";
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
                objCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }
            objclsErrorLogger = null;
            objCommon = null;
        }
    }

    protected void bt_Click(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "LoadNicEditor();", true);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls(true);
        lblMessage.Text = "";
    }
    private string ConvertDateForInsert(string str)
    {

        char[] splitchar = { '/' };
        string[] dt = str.Split(splitchar);

        int iDate = Convert.ToInt16(dt[0]);
        int iMonthNo = Convert.ToInt16(dt[1]);
        int iYear = Convert.ToInt16(dt[2]);

        DateTime dtnew = new DateTime(iYear, iMonthNo, iDate);

        return dtnew.ToString("dd-MMM-yyyy");

    }
    private string ConvertDateForSelect(DateTime dt)
    {
        return dt.ToString("dd/MM/yyyy");
    }

    
 
    //protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    clsDropDown objclsDropDown;
    //    try
    //    {
    //        int SemesterId, CourseId;
    //        objclsDropDown = new clsDropDown();

    //        Bind Batch DropDown
    //        string SessionCode = ddlSession.SelectedItem.Text;
    //        int.TryParse(ddlCourse.SelectedValue, out CourseId);
    //        int.TryParse(ddlSemester.SelectedValue, out SemesterId);

    //        objclsDropDown.BindBatchBySessionAndCourseIdAndSemesterId(ddlBatchCode, SessionCode, CourseId, SemesterId, true, M_strcon);
    //        if (ddlBatchCode.Items.Count > 0)
    //        {
    //            ddlBatchCode.Items.RemoveAt(0);
    //            ddlBatchCode.Items.Insert(0, new ListItem("All Batch", "0"));
    //        }

    //        grdDetails.DataSource = null;
    //        grdDetails.DataBind();

    //        lblstatusMsg.Text = "Total Record(s) : 0";
    //        UpdatePanel1.Update();

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    finally
    //    {
    //          con.Close();
    //    }
    //}
    //protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    clsDropDown objclsDropDown;
    //    try
    //    {
    //        int SemesterId, CourseId;
    //        objclsDropDown = new clsDropDown();
    //        //Bind Batch DropDown
    //        string SessionCode = ddlSession.SelectedItem.Text;
    //        int.TryParse(ddlCourse.SelectedValue, out CourseId);
    //        int.TryParse(ddlSemester.SelectedValue, out SemesterId);

    //        objclsDropDown.BindBatchBySessionAndCourseIdAndSemesterId(ddlBatchCode, SessionCode, CourseId, SemesterId, true, M_strcon);

    //        if (ddlBatchCode.Items.Count > 0)
    //        {
    //            ddlBatchCode.Items.RemoveAt(0);
    //            ddlBatchCode.Items.Insert(0, new ListItem("All Batch", "0"));
    //        }

    //        grdDetails.DataSource = null;
    //        grdDetails.DataBind();

    //        lblstatusMsg.Text = "Total Record(s) : 0";
    //        UpdatePanel1.Update();

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    finally
    //    {
    //        //con.Close();
    //    }
    //}


    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlSession.Enabled = true;
        ddlCourse.Enabled = true;
 
        grdDetails.DataSource = null;
        grdDetails.DataBind();
        lblstatusMsg.Text = "Total Record(s) : 0";

        UpdatePanel1.Update();
        UpdatePanel2.Update();
    }
    protected void btnPopulate_Click(object sender, EventArgs e)
    {
        clsCommon objCommon = new clsCommon();
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        ObjStudentRegistrationSearch objObjStudentRegistrationSearch = new ObjStudentRegistrationSearch();
        clsUserRegistration objclsUserRegistration = new clsUserRegistration();
        DataTable dtBatchStudent = new DataTable();
        string _strAlertMessage = string.Empty;

        try
        {

            int CourseId;

            string SessionCode = ddlSession.SelectedItem.Text;
            int.TryParse(ddlCourse.SelectedValue, out CourseId);

            dtBatchStudent = null;

            objObjStudentRegistrationSearch.ConnectionString = user.ConnectionString;
            objObjStudentRegistrationSearch.Session = SessionCode;
            objObjStudentRegistrationSearch.CourseID = CourseId;

            dtBatchStudent = objclsUserRegistration.getConfirmedSignupStudents(objObjStudentRegistrationSearch);

            grdDetails.DataSource = dtBatchStudent;
            grdDetails.DataBind();

            lblstatusMsg.Text = "Total Record(s) : " + grdDetails.Rows.Count.ToString();

            if (grdDetails.Rows.Count > 0)
            {
                ddlSession.Enabled = false;
                ddlCourse.Enabled = false;
            }
            else
            {
                ddlSession.Enabled = true;
                ddlCourse.Enabled = true;
            }

            UpdatePanel1.Update();
        }
        catch (Exception ex)
        {
            _strAlertMessage = ex.Message;
        }
        finally
        {
            if (_strAlertMessage != string.Empty && _strAlertMessage.Trim() != "")
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }

            objCommon = null;
            objclsErrorLogger = null;
            objObjStudentRegistrationSearch = null;
            objclsUserRegistration = null;
            dtBatchStudent = null;
        }
    }
    protected void rdoAssignment_CheckedChanged(object sender, EventArgs e)
    {
        if (rdoAssignment.Checked)
        {
            rowStudent.Visible = true;
        }
        else
        {
            rowStudent.Visible = false;
            chkNotification.Checked = false;
        }

        UpdatePanel1.Update();
        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "setFocusToEntryTab('EntryTab')", true);
    }

    private void SendMailAlert(string strAction, string strAssignmentPath)
    {
        clsCommon objClsCommon = new clsCommon();
        ObjSendEmail SendMail;
        DataTable dtInformation;
        Label objlblName;
        Label objlblEmailID;
        Label objlblMobile;
        string strRecepeintEmailIds = string.Empty;
        int smtpPort;

        try
        {

            lblMessage.Text = "";

            dtInformation = objClsCommon.getMailBody("COMMON", "MailFormat", "Mail On Assignment Upload(Student)", user.ConnectionString);
            if (dtInformation != null & dtInformation.Rows.Count > 0)
            {
                for (int i = 0; i <= grdDetails.Rows.Count - 1; i++)
                {
                    CheckBox chkAccessibility = (CheckBox)grdDetails.Rows[i].Cells[1].FindControl("chkAccessibility");
                    if ((chkAccessibility != null))
                    {
                        if (chkAccessibility.Checked == true)
                        {
                           // objlblRegistrationNumber = (Label)grdDetails.Rows[i].Cells[1].FindControl("lblRegistrationNumber");
                            objlblName = (Label)grdDetails.Rows[i].Cells[1].FindControl("lblName");
                            objlblEmailID = (Label)grdDetails.Rows[i].Cells[1].FindControl("lblEmailID");
                            objlblMobile = (Label)grdDetails.Rows[i].Cells[1].FindControl("lblMobile");

                            //Validation
                            if (objlblEmailID.Text == "")
                            {
                                grdDetails.Rows[i].Cells[0].BackColor = System.Drawing.Color.Red;
                                grdDetails.Rows[i].Cells[0].ToolTip = "Email ID not available!";
                                continue;
                            }

                            if (chkMailAsGroupMail.Checked)
                            {
                                strRecepeintEmailIds = strRecepeintEmailIds == "" ? objlblEmailID.Text.Trim() : strRecepeintEmailIds + "," + objlblEmailID.Text.Trim();
                            }
                            else
                            {

                                //SEND MAIL STARTS
                                SendMail = new ObjSendEmail();
                                try
                                {
                                    SendMail.From = dtInformation.Rows[0]["FROM"].ToString();
                                    SendMail.To = objlblEmailID.Text.Trim();
                                    SendMail.CC = string.Empty;
                                    SendMail.BCC = objClsCommon.getConfigKeyValue("EMAIL IDS FOR CC OF MAIL MESSAGE FOR EACH OPERATION", "EMAIL RECEPIENTS GROUP", user.ConnectionString); ;
                                    SendMail.SMTPHost = user.SMTPServer;
                                    int.TryParse(user.SMTPPort, out smtpPort);
                                    SendMail.SMTPPort = smtpPort;
                                    SendMail.UserId = user.SMTPUSerID;
                                    SendMail.Password = user.SMTPPassword; ;
                                    SendMail.SMTPSSL = user.SMTPSSL == "Y" ? true : false; ;
                                    SendMail.MailType = "I";
                                    SendMail.Subject = user.Orig_Name + ": Assignment Upload Acknowledgement";
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
                                    ////////////////////////////////////////

                                    if (SendMail.To.Trim() != "")
                                    {

                                        if (chkMailAsGroupMail.Checked)
                                        {
                                            strRecepeintEmailIds = strRecepeintEmailIds == "" ? SendMail.To.Trim() : strRecepeintEmailIds + "," + SendMail.To.Trim();
                                        }
                                        else
                                        {
                                            SendMail.MailBody = SendMail.MailBody.Replace("#Name#", objlblName.Text);
                                            SendMail.MailBody = SendMail.MailBody.Replace("#Subject#", txtSubject.Text.Trim());
                                            SendMail.MailBody = SendMail.MailBody.Replace("#Description#", txtDescription.Text.Trim());
                                            SendMail.MailBody = SendMail.MailBody.Replace("#action#", strAction);
                                            SendMail.MailBody = SendMail.MailBody.Replace("#Comments#", txtComments.Text.Trim());

                                            if (ChkAttachAssignment.Checked)
                                            {
                                                SendMail.Attachment = strAssignmentPath;
                                            }

                                            SendMail.ConnectionString = M_strcon;
                                            try
                                            {
                                                objClsCommon.SendEmail(SendMail);
                                                grdDetails.Rows[i].Cells[0].BackColor = System.Drawing.Color.LightGreen;
                                                grdDetails.Rows[i].Cells[0].ToolTip = grdDetails.Rows[i].Cells[0].ToolTip + " Email Notification Sent";
                                            }
                                            catch { }
                                            finally
                                            {
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    //lblMessage.Text = ex.Message;
                                }
                                finally
                                {
                                    SendMail = null;
                                }
                            }
                            //SEND MAIL ENDS
                        }
                    }
                }

                //Send mail by keeping recepients in single mail
                if (chkMailAsGroupMail.Checked && strRecepeintEmailIds != "")
                {

                    //SEND MAIL STARTS
                    SendMail = new ObjSendEmail();
                    try
                    {
                        SendMail.From = dtInformation.Rows[0]["FROM"].ToString();
                        SendMail.To = strRecepeintEmailIds;
                        SendMail.CC = string.Empty;
                        SendMail.BCC = objClsCommon.getConfigKeyValue("EMAIL IDS FOR CC OF MAIL MESSAGE FOR EACH OPERATION", "EMAIL RECEPIENTS GROUP", user.ConnectionString); ;
                        SendMail.SMTPHost = user.SMTPServer;
                        int.TryParse(user.SMTPPort, out smtpPort);
                        SendMail.SMTPPort = smtpPort;
                        SendMail.UserId = user.SMTPUSerID;
                        SendMail.Password = user.SMTPPassword; ;
                        SendMail.SMTPSSL = user.SMTPSSL == "Y" ? true : false; ;
                        SendMail.MailType = "I";

                        SendMail.Subject = user.Orig_Name + ": Assignent Upload Acknowledgement";
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

                        SendMail.MailBody = SendMail.MailBody.Replace("#Name#", "All");
                        SendMail.MailBody = SendMail.MailBody.Replace("#Subject#", txtSubject.Text.Trim());
                        SendMail.MailBody = SendMail.MailBody.Replace("#Description#", txtDescription.Text.Trim());
                        SendMail.MailBody = SendMail.MailBody.Replace("#action#", strAction);
                        SendMail.MailBody = SendMail.MailBody.Replace("#Comments#", txtComments.Text.Trim());

                        if (ChkAttachAssignment.Checked)
                        {
                            SendMail.Attachment = strAssignmentPath;
                        }

                        SendMail.ConnectionString = user.ConnectionString;
                        try
                        {
                            objClsCommon.SendEmail(SendMail);
                        }
                        catch { }
                        finally
                        {
                        }
                    }
                    catch (Exception ex)
                    {
                        //lblMessage.Text = ex.Message;
                    }
                    finally
                    {
                        SendMail = null;
                    }

                    //SEND MAIL ENDS
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objClsCommon = null;
            if (lblMessage.Text != "") ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + lblMessage.Text + "');</script>", false);
        }
    }
    private void SendSMSAlert()
    {

        clsCommon objClsCommon = new clsCommon();
        string strKey = string.Empty;
        string strLink = string.Empty;
        DataTable dtInformation;
        Label objlblRegistrationNumber;
        Label objlblName;
        Label objlblEmailID;
        Label objlblMobile;

        try
        {

            lblMessage.Text = "";

            strKey = "SMS On Assignment Upload";
            dtInformation = objClsCommon.getSMSBody("SMS", strKey, M_strcon);

            if (dtInformation != null & dtInformation.Rows.Count > 0)
            {

                for (int i = 0; i <= grdDetails.Rows.Count - 1; i++)
                {
                    CheckBox chkAccessibility = (CheckBox)grdDetails.Rows[i].Cells[1].FindControl("chkAccessibility");
                    if ((chkAccessibility != null))
                    {
                        if (chkAccessibility.Checked == true)
                        {
                            objlblRegistrationNumber = (Label)grdDetails.Rows[i].Cells[1].FindControl("lblRegistrationNumber");
                            objlblName = (Label)grdDetails.Rows[i].Cells[1].FindControl("lblName");
                            objlblEmailID = (Label)grdDetails.Rows[i].Cells[1].FindControl("lblEmailID");
                            objlblMobile = (Label)grdDetails.Rows[i].Cells[1].FindControl("lblMobile");

                            //Validation
                            if (objlblMobile.Text == "")
                            {
                                grdDetails.Rows[i].Cells[0].BackColor = System.Drawing.Color.Red;
                                grdDetails.Rows[i].Cells[0].ToolTip = "Mobile No not available!";
                                continue;
                            }

                            //Sending SMS -Start  
                            EC_SMS_SendMessage objEC_SMS_SendMessage = new EC_SMS_SendMessage();
                            clsEC_SMS_SendMessage objclsEC_SMS_SendMessage = new clsEC_SMS_SendMessage();
                            try
                            {
                                objEC_SMS_SendMessage.SMS_KEY = strKey;
                                objEC_SMS_SendMessage.SMS_Desc = "SMS";
                                objEC_SMS_SendMessage.SMS_MobileNo = objlblMobile.Text;
                                objEC_SMS_SendMessage.SMS_Msg = dtInformation.Rows[0]["SMS_MSG"].ToString();
                                objEC_SMS_SendMessage.SMS_Msg = objEC_SMS_SendMessage.SMS_Msg.Replace("#Name#", objlblName.Text.Trim());
                                objEC_SMS_SendMessage.SMS_Msg = objEC_SMS_SendMessage.SMS_Msg.Replace("#Assignment#", txtDescription.Text.Trim());
                                objEC_SMS_SendMessage.SMS_URLLink = dtInformation.Rows[0]["SMS_Link"].ToString();

                                try
                                {
                                    objclsEC_SMS_SendMessage.SendSMS(objEC_SMS_SendMessage);
                                    grdDetails.Rows[i].Cells[0].BackColor = System.Drawing.Color.LightGreen;
                                    grdDetails.Rows[i].Cells[0].ToolTip = grdDetails.Rows[i].Cells[0].ToolTip + " SMS Notification Sent";
                                }
                                catch { }
                                finally
                                {
                                    objEC_SMS_SendMessage = null;
                                    objclsEC_SMS_SendMessage = null;
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                            finally
                            {
                                objEC_SMS_SendMessage = null;
                                objclsEC_SMS_SendMessage = null;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objClsCommon = null;
            dtInformation = null;
            objlblRegistrationNumber = null;
            objlblName = null;
            objlblEmailID = null;
            objlblMobile = null;

            if (lblMessage.Text != "") ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + lblMessage.Text + "');</script>", false);
        }
    }
 


    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{
    //    clsCommonUtility objclsCommonUtility = new clsCommonUtility();
    //    ObjMaster objmaster = new ObjMaster();
    //    DataTable dtResult;
    //    clsErrorLogger objclsErrorLogger = new clsErrorLogger();
    //    clsCommon objclsCommon = new clsCommon();

    //    string _strAlertMessage = string.Empty;
    //    string strLoggedInUser = string.Empty;
    //    string confirmValue = string.Empty;
    //    string strPassword = string.Empty;
    //    int UID;
    //    int SegmentID;
    //    int SemesterCount;

    //    strLoggedInUser = user.UserId;
    //    if(strLoggedInUser=="" || strLoggedInUser==null) strLoggedInUser = "365skool";

    //    try
    //    {
    //        lblMessage.Text = "";

    //        //Vlidation
    //        if (txtCourseCode.Text.Trim() == "")
    //        {
    //            _strMsgType = "Info";
    //            _strAlertMessage = "Enter Course Code!";
    //            txtCourseCode.Focus();
    //        }

    //        if (objclsCommon.isValidInput(user.RestrictedInputChar, txtCourseCode.Text.Trim()) == false)
    //        {
    //            _strMsgType = "Info";
    //            _strAlertMessage = "Course Code contains Invalid Characters";
    //            txtCourseCode.Focus();
    //            return;
    //        }

    //        if (txtCourseName.Text.Trim() == "")
    //        {
    //            _strMsgType = "Info";
    //            _strAlertMessage = "Enter Course Name!";
    //            txtCourseName.Focus();
    //            return;
    //        }

    //        if (objclsCommon.isValidInput(user.RestrictedInputChar, txtCourseName.Text.Trim()) == false)
    //        {
    //            _strAlertMessage = "Course Name contains Invalid Characters";
    //            txtCourseName.Focus();
    //            return;
    //        }

    //        int.TryParse(ddlCourseSegment.SelectedValue, out SegmentID);
    //        int.TryParse(txtSemesterCount.Text, out SemesterCount);

    //        objmaster.SegmentId = SegmentID;
    //        objmaster.CourseCode = txtCourseCode.Text.Trim();
    //        objmaster.CourseName = txtCourseName.Text.Trim();
    //        objmaster.SemesterCount = SemesterCount;

    //        if (btnSubmit.Text == "Save")
    //        {
    //            objmaster.Mode = "C";
    //            objmaster.Status = 1;
    //        }
    //        else
    //        {
    //            objmaster.Mode = "U";
    //            int.TryParse(hdId.Value, out UID);
    //            objmaster.CourseId = UID;
    //        }

    //        if (objmaster.Mode == "U")
    //        {
    //            objmaster.Status = 1;
    //            //If status is going to make Active. Generate and Send Password
    //            if (chkActivated.Visible == true)
    //            {
    //                if (chkActivated.Checked == false) objmaster.Status =0;
    //                if (chkActivated.Checked == true) objmaster.Status = 1;
    //            }
    //        }

    //        objmaster.ModifiedBy = strLoggedInUser;
    //        objmaster.CreatedBy = strLoggedInUser;
    //        objmaster.ConnectionString = user.ConnectionString;
    //        dtResult = objclsCommonUtility.SaveCourse(objmaster);

    //        if (dtResult != null)
    //        {
    //            if (dtResult.Columns.Contains("ERROR"))
    //            {
    //                if (dtResult.Rows[0]["ERROR"].ToString().Trim() != "")
    //                {
    //                    _strMsgType = "Error_DB";
    //                    _strAlertMessage = dtResult.Rows[0]["ERROR"].ToString().Trim();
    //                    return;
    //                }
    //            }
    //            _strMsgType = "Success";
    //            _strAlertMessage = dtResult.Rows[0]["SUCCESS"].ToString().Trim();
    //        }

    //        lblMessage.Text = _strAlertMessage;
    //        getDetails(txtSearch.Text.Trim());
    //        UpdatePanel2.Update();

    //        clearControls();
    //        UpdatePanel1.Update();
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "setFocusToEntryTab('EntryTab')", true);

    //    }
    //    catch (Exception ex)
    //    {
    //        _strMsgType = "Error";
    //        _strAlertMessage = ex.Message;
    //        objclsErrorLogger.WriteError(System.IO.Path.GetFileName(Request.Path), "", user == null ? "" : user.ConnectionString, ex);
    //        Response.Redirect("~/Operation/Err.aspx");
    //    }
    //    finally
    //    {
    //        //Alert Message
    //        if (_strAlertMessage != string.Empty && _strAlertMessage.Trim() != "")
    //        {
    //            lblMessage.Text = _strAlertMessage;
    //            objclsCommon.setLabelMessage(lblMessage, _strMsgType);
    //            ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);
    //            UpdatePanel1.Update();

    //            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
    //        }

    //        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "setFocusToEntryTab('EntryTab')", true);
    //        objmaster = null;
    //        dtResult = null;
    //        objclsCommon = null;
    //        objclsCommonUtility = null;
    //        objclsErrorLogger = null;
 
    //    }
    //}
 
    
   
    //protected void btnCancel_Click(object sender, EventArgs e)
    //{
    //    clearControls();
    //    lblMessage.Text = "";
    //    UpdatePanel1.Update();

    //    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "setFocusToEntryTab('EntryTab')", true);
    //}

    //protected void grdStyled_PageIndexChanging(Object sender, GridViewPageEventArgs e)
    //{
    //    grdStyled.PageIndex = e.NewPageIndex;
    //    getDetails(txtSearch.Text.Trim());
    //} 

    //protected void grdStyled_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    clsCommon objclsCommon = new clsCommon();
    //    clsErrorLogger objclsErrorLogger = new clsErrorLogger();

    //    string _strAlertMessage=string.Empty ;
    //    string ImgPath = string.Empty;
    //    int CourseID, SegmentID;

    //    try
    //    {
    //        clearControls();
    //        lblMessage.Text = string.Empty;

    //        int.TryParse(grdStyled.DataKeys[grdStyled.SelectedIndex]["Course_ID"].ToString(), out CourseID);
    //        int.TryParse(grdStyled.DataKeys[grdStyled.SelectedIndex]["Segment_id"].ToString(), out SegmentID);
    //        if (CourseID > 0) 
    //        {
    //            hdId.Value = Convert.ToString(CourseID);
    //            txtCourseCode.Text = Server.HtmlDecode(grdStyled.SelectedRow.Cells[1].Text.Trim());
    //            txtCourseName.Text =  Server.HtmlDecode(grdStyled.SelectedRow.Cells[2].Text.Trim());
    //            txtSemesterCount.Text = grdStyled.SelectedRow.Cells[4].Text.Trim();
    //            ddlCourseSegment.SelectedValue = SegmentID.ToString();// grdStyled.SelectedRow.Cells[3].Text.Trim();
    //            btnSubmit.Text = "Update";
    //            rwStatus.Visible = true;

    //            chkActivated.Checked = false;
    //            chkActivated.Visible = false;

    //            lblAccountStatus.Text = "Status: Active";
    //            objclsCommon.setLabelMessage(lblAccountStatus, "Info");

    //            if (grdStyled.SelectedRow.Cells[5].Text == "I")
    //            {
    //                lblAccountStatus.Text ="Status: Inactive";

    //                objclsCommon.setLabelMessage(lblAccountStatus, "Error");
    //                chkActivated.Enabled = true;
    //                chkActivated.Checked = false;
    //                chkActivated.Visible = true;
    //            }
    //        }
    //        UpdatePanel1.Update();

    //        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "setFocusToEntryTab('EntryTab')", true);
    //    }
    //    catch (Exception ex)
    //    {
    //        _strMsgType = "Error";
    //        _strAlertMessage = ex.Message;
    //        objclsErrorLogger.WriteError(System.IO.Path.GetFileName(Request.Path), "", user == null ? "" : user.ConnectionString, ex);
    //        Response.Redirect("~/Operation/Err.aspx");
    //    }
    //    finally
    //    {
    //        if (_strAlertMessage != string.Empty)
    //        {
    //            lblMessage.Text = _strAlertMessage;
    //            objclsCommon.setLabelMessage(lblMessage, _strMsgType);
    //            ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);

    //            UpdatePanel1.Update();
    //            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
    //        }
    //        objclsCommon = null;
    //        objclsErrorLogger = null;
    //    }
    //}
   
    //protected void grdStyled_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    lblMessage.Text = "";
    //    int courseID;
    //    clsCommon objClsCommon = new clsCommon();
    //    clsErrorLogger objclsErrorLogger = new clsErrorLogger();
    //    clsCommonUtility obclsCommonUtility = new clsCommonUtility();
    //    ObjMaster master = new ObjMaster();
    //    DataTable dtResult;

    //    try
    //    {
    //        int.TryParse(grdStyled.DataKeys[e.RowIndex]["Course_ID"].ToString(), out courseID);
    //        master.CourseId = courseID;
    //        master.ModifiedBy = user.UserId;
    //        master.ConnectionString = user.ConnectionString;
    //        dtResult = obclsCommonUtility.DeleteCourse(master);

    //        if (dtResult != null)
    //        {
    //            if (dtResult.Columns.Contains("ERROR"))
    //            {
    //                if (dtResult.Rows[0]["ERROR"].ToString().Trim() != "")
    //                {
    //                    _strMsgType = "Error_DB";
    //                    _strAlertMessage = dtResult.Rows[0]["ERROR"].ToString().Trim();
    //                    return;
    //                }
    //            }
    //            _strMsgType = "Success";
    //            _strAlertMessage = dtResult.Rows[0]["SUCCESS"].ToString().Trim();
    //        }

    //        //Dind Details
    //        getDetails(txtSearch.Text.Trim());

    //        clearControls();
    //        UpdatePanel2.Update();
    //    }
    //    catch (Exception ex)
    //    {
    //        _strMsgType = "Error";
    //        _strAlertMessage = ex.Message;
    //        objclsErrorLogger.WriteError(System.IO.Path.GetFileName(Request.Path), "", user == null ? "" : user.ConnectionString, ex);
    //        Response.Redirect("~/Operation/Err.aspx");
    //    }
    //    finally
    //    {
    //        if (_strAlertMessage != string.Empty)
    //        {
    //            lblMessage.Text = _strAlertMessage;
    //            objClsCommon.setLabelMessage(lblMessage, _strMsgType);
    //            ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);

    //            UpdatePanel1.Update();
    //            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
    //        }

    //        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "setFocusToEntryTab('ListUser')", true);
    //        objClsCommon = null;
    //        obclsCommonUtility = null;
    //        master = null;
    //        objclsErrorLogger = null;
    //        dtResult = null;
    //    }
    //}

    protected void grdEvents_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strCellVal = string.Empty;
            int counter = 0;

            strCellVal = DataBinder.Eval(e.Row.DataItem, "IsAssigned").ToString().Trim();
            if (strCellVal == "Y")
            {
                counter=counter+1;
                e.Row.BackColor = System.Drawing.Color.LightGray;
                e.Row.ToolTip =  counter.ToString ()+ ".Modification is not allowed as it has been assigned to students!";
                e.Row.Enabled = false;
            }

            strCellVal = DataBinder.Eval(e.Row.DataItem, "Is_ForLifeTime").ToString().Trim();
            if (strCellVal == "Y")
            {
                e.Row.BackColor = System.Drawing.Color.LightYellow;
            }

            strCellVal = DataBinder.Eval(e.Row.DataItem, "IsExpuired").ToString().Trim();

            if (strCellVal == "Y")
            {
                counter = counter + 1;
                e.Row.BackColor = System.Drawing.Color.Red;
                e.Row.ToolTip =e.Row.ToolTip +counter.ToString ()+ ".Not applicable for as it has been expired!";
            }
        }
    }
   
    //private void clearControls()
    //{
    //    clsCommon objClsCommon = new clsCommon();
    //    try
    //    {
    //        lblAccountStatus.Text = "";
    //        hdId.Value = "0";
    //        txtCourseCode.Text = "";
    //        txtCourseName.Text = "";
    //        txtSemesterCount.Text = "";
    //        txtSearch.Text = "";

    //        txtCourseName.Enabled = true;
    //        chkActivated.Checked = true;
    //        chkActivated.Enabled = false;
    //        ddlCourseSegment.Enabled = true;
    //        btnSubmit.Text = "Save";
    //        UpdatePanel1.Update();
    //        rwStatus.Visible = false ;
    //        chkActivated.Checked = false;
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMessage.Text = ex.Message ;
    //        objClsCommon.setLabelMessage(lblMessage, "Error");
    //        ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);
    //        UpdatePanel1.Update();
    //    }
    //    finally
    //    {
    //        objClsCommon = null;
    //    }
    //}

    //private void bindCourseSegments()
    //{
    //    clsCommonUtility objclsCommonUtility = new clsCommonUtility();
    //    DataTable dtInfo;
    //    try
    //    {
    //        dtInfo = objclsCommonUtility.ShowSegment(user.ConnectionString);

    //        if (dtInfo != null)
    //        {
    //            ddlCourseSegment.DataSource = dtInfo;
    //            ddlCourseSegment.DataValueField = "ID";
    //            ddlCourseSegment.DataTextField = "SEGMENT_NAME";
    //            ddlCourseSegment.DataBind();
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    finally
    //    {
    //        dtInfo = null;
    //        objclsCommonUtility = null;
    //    }
    //}
  
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
            BindGrid(txtSearch.Text.Trim());
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
    
    //private void getDetails(string strText)
    //{
    //    clsCommonUtility objclsCommonUtility = new clsCommonUtility();
    //    ObjMaster master = new ObjMaster();
    //    DataTable dt;
    //    try
    //    {
    //        master.SearchText = strText;
    //        master.ConnectionString = user.ConnectionString;
    //        dt = objclsCommonUtility.ShowCourse(master);

    //        grdEvents.DataSource = dt;
    //        grdEvents.DataBind();

    //        if (grdEvents.Rows.Count > 0)
    //        {
    //            grdEvents.Columns[13].Visible = false;
    //            grdEvents.Columns[14].Visible = false;
    //            grdEvents.Columns[15].Visible = false;
    //            grdEvents.Columns[16].Visible = false;
    //        }

    //        btnSearch.Enabled = true;
    //        UpdatePanel2.Update();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    finally
    //    {
    //        master = null;
    //        objclsCommonUtility = null;
    //        dt = null;
    //    }
    //}
}
 