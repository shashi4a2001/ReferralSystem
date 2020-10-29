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

public partial class Operation_AssMapping : System.Web.UI.Page
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
                bindCombos();
                getDetails("");

                txtReading.Attributes.Add("onkeypress", "javascript:return IsNumeric( )");
                txtWriting1.Attributes.Add("onkeypress", "javascript:return IsNumeric( )");
                txtWriting2.Attributes.Add("onkeypress", "javascript:return IsNumeric( )");
                txtListening.Attributes.Add("onkeypress", "javascript:return IsNumeric( )");
                txtSpeaking.Attributes.Add("onkeypress", "javascript:return IsNumeric( )");
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

    #region "Control Events"
    protected void btnGetData_Click(object sender, EventArgs e)
    {
        clsCommon objCommon = new clsCommon();
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        ObjStudentRegistrationSearch objObjStudentRegistrationSearch = new ObjStudentRegistrationSearch();
        clsUserRegistration objclsUserRegistration = new clsUserRegistration();
        DataTable dtBatchStudent = new DataTable();
        string _strAlertMessage = string.Empty;
        int ReGNo;
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

            if (dtBatchStudent != null)
            {
                ddlStudents.DataSource = dtBatchStudent;
                ddlStudents.DataValueField = "UID";
                ddlStudents.DataTextField = "COMBO_VAL";
                ddlStudents.DataBind();

                int.TryParse(ddlStudents.SelectedValue, out ReGNo);
                bindAssignmentDetails(ReGNo);
            }

            if (ddlStudents.Items.Count > 0)
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        UpdatePanel1.Update();
        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "setFocusToEntryTab('EntryTab')", true);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objclsCommon = new clsCommon();
        string _strAlertMessage = string.Empty;

        try
        {
            lblMessage.Text = string.Empty;
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
    protected void btnREset_Click(object sender, EventArgs e)
    {

        ddlCourse.Enabled = true;
        ddlSession.Enabled = true;
        btnGetData.Enabled = true;

        ddlStudents.Items.Clear();
        ddlStudents.DataSource = null;
        ddlStudents.DataBind();

        txtAvailableSearch.Text = string.Empty;
        grdAvailable.DataSource = null;
        grdAvailable.DataBind();

        txtAssignedSearch.Text = string.Empty;
        grdAssigned.DataSource = null;
        grdAssigned.DataBind();

        btnActivateAssignment.Enabled = false;
        btnMapAssignment.Enabled = false;

        tblAssignment.Visible = false;

        lblAssigned.Text = string.Empty;
        lblAvailable.Text = string.Empty;

        lblMessage.Text = string.Empty;
        UpdatePanel1.Update();
    }
    protected void btnAvailableSearch_Click(object sender, EventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objclsCommon = new clsCommon();
        clsAssignments objclsAssignments = new clsAssignments();
        EC_Assignments objEC_Assignments = new EC_Assignments();
        string _strAlertMessage = string.Empty;
        DataSet dsResult;
        int ReGNo;

        try
        {
            lblMessage.Text = string.Empty;
            int.TryParse(ddlStudents.SelectedValue, out ReGNo);
            objEC_Assignments.ConnectionString = user.ConnectionString;
            objEC_Assignments.RegNO = ReGNo;
            objEC_Assignments.Mode = "L";
            objEC_Assignments.GrdiSrchInput = txtAvailableSearch.Text.Trim();
            dsResult = objclsAssignments.Assignment_Mapping_Details(objEC_Assignments);

            grdAvailable.DataSource = dsResult.Tables[0];
            grdAvailable.DataBind();

            lblAvailable.Text = " Records : " + dsResult.Tables[0].Rows.Count.ToString();

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
            dsResult = null;

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
    protected void btnAssignedSearch_Click(object sender, EventArgs e)
    {
        clsAssignments objclsAssignments = new clsAssignments();
        EC_Assignments objEC_Assignments = new EC_Assignments();
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        string _strAlertMessage = string.Empty;
        clsCommon objclsCommon = new clsCommon();
        DataSet dsResult;
        int ReGNo;
        try
        {
            lblMessage.Text = string.Empty;
            int.TryParse(ddlStudents.SelectedValue, out ReGNo);
            objEC_Assignments.ConnectionString = user.ConnectionString;
            objEC_Assignments.RegNO = ReGNo;
            objEC_Assignments.Mode = "R";
            objEC_Assignments.GrdiSrchInput = txtAssignedSearch.Text.Trim();
            dsResult = objclsAssignments.Assignment_Mapping_Details(objEC_Assignments);

            grdAssigned.DataSource = dsResult.Tables[0];
            grdAssigned.DataBind();

            lblAssigned.Text = " Records : " + dsResult.Tables[0].Rows.Count.ToString();
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
            dsResult = null;
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
    protected void ddlStudents_SelectedIndexChanged(object sender, EventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objclsCommon = new clsCommon();
        string _strAlertMessage = string.Empty;
        int ReGNo;
        try
        {
            lblMessage.Text = string.Empty;
            int.TryParse(ddlStudents.SelectedValue, out ReGNo);
            txtAssignedSearch.Text = string.Empty;
            txtAvailableSearch.Text = string.Empty;
            bindAssignmentDetails(ReGNo);
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
            objclsCommon = null;
            objclsErrorLogger = null;
        }
    }
    protected void btnMapAssignment_Click(object sender, EventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objclsCommon = new clsCommon();
        clsAssignments objclsAssignments = new clsAssignments();
        EC_Assignments objEC_Assignments = new EC_Assignments();
        DataTable dtResult;
        int assignmentId;
        string strAssignmentIds;
        int ReGNo;
        Boolean isChecked = false;

        try
        {

            lblMessage.Text = string.Empty;
            CheckBox chkAccessibility;
            strAssignmentIds = string.Empty;
            for (int i = 0; i <= grdAvailable.Rows.Count - 1; i++)
            {

                chkAccessibility = (CheckBox)grdAvailable.Rows[i].Cells[1].FindControl("chkAccessibility");
                if ((chkAccessibility != null))
                {
                    if (chkAccessibility.Checked == true)
                    {
                        int.TryParse(grdAvailable.DataKeys[i]["AssignmentId"].ToString(), out assignmentId);
                        isChecked = true;
                        if (strAssignmentIds == string.Empty)
                        {
                            strAssignmentIds = assignmentId.ToString();
                        }
                        else
                        {
                            strAssignmentIds = strAssignmentIds + "," + assignmentId.ToString();
                        }
                    }
                }
            }

            if (isChecked == false)
            {
                _strAlertMessage = "Please select the assignment!";
                return;
            }

            int.TryParse(ddlStudents.SelectedValue, out ReGNo);
            objEC_Assignments.ConnectionString = user.ConnectionString;
            objEC_Assignments.RegNO = ReGNo;
            objEC_Assignments.Mode = "M";
            objEC_Assignments.AssgnmentIds = strAssignmentIds;
            objEC_Assignments.CreatedBy = user.UserId;
            dtResult = objclsAssignments.Map_UnMap_Assignments(objEC_Assignments);

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

                if (chkAcknowledgement.Checked)
                {
                    //Send Mail Acknowledgement
                    SendMailAlert("Assigned", grdAvailable, dtResult.Rows[0]["Student_Name"].ToString(), dtResult.Rows[0]["Email_ID"].ToString());
                    //Send sms Acknowledgement
                    //SendSMSAlert("Assign", grdAvailable, dtResult.Rows[0]["Student_Name"].ToString(), dtResult.Rows[0]["MOBILE_NO"].ToString());
                }
                bindAssignmentDetails(ReGNo);
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
            if (_strAlertMessage != string.Empty && _strAlertMessage.Trim() != "")
            {
                lblMessage.Text = _strAlertMessage;
                objclsCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }

            objclsCommon = null;
        }
    }
    protected void btnActivateAssignment_Click(object sender, EventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objclsCommon = new clsCommon();
        clsAssignments objclsAssignments = new clsAssignments();
        EC_Assignments objEC_Assignments = new EC_Assignments();
        DataTable dtResult;
        int assignmentId;
        string strAssignmentIds;
        int ReGNo;
        Boolean isChecked = false;

        try
        {

            lblMessage.Text = string.Empty;
            CheckBox chkAccessibility;
            strAssignmentIds = string.Empty;
            for (int i = 0; i <= grdAssigned.Rows.Count - 1; i++)
            {

                chkAccessibility = (CheckBox)grdAssigned.Rows[i].Cells[1].FindControl("chkAccessibility");
                if ((chkAccessibility != null))
                {
                    if (chkAccessibility.Checked == true)
                    {
                        int.TryParse(grdAssigned.DataKeys[i]["AssignmentId"].ToString(), out assignmentId);
                        isChecked = true;
                        if (strAssignmentIds == string.Empty)
                        {
                            strAssignmentIds = assignmentId.ToString();
                        }
                        else
                        {
                            strAssignmentIds = strAssignmentIds + "," + assignmentId.ToString();
                        }
                    }
                }
            }

            if (isChecked == false)
            {
                _strAlertMessage = "Please select the assignment!";
                return;
            }

            int.TryParse(ddlStudents.SelectedValue, out ReGNo);
            objEC_Assignments.ConnectionString = user.ConnectionString;
            objEC_Assignments.RegNO = ReGNo;
            objEC_Assignments.Mode = "A";
            objEC_Assignments.AssgnmentIds = strAssignmentIds;
            objEC_Assignments.CreatedBy = user.UserId;
            dtResult = objclsAssignments.Map_UnMap_Assignments(objEC_Assignments);

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

                if (chkAcknowledgement.Checked)
                {
                    //Send Mail Acknowledgement
                    SendMailAlert("Activated", grdAssigned, dtResult.Rows[0]["Student_Name"].ToString(), dtResult.Rows[0]["Email_ID"].ToString());
                    //Send sms Acknowledgement
                    //SendSMSAlert("Activate", grdAvailable, dtResult.Rows[0]["Student_Name"].ToString(), dtResult.Rows[0]["MOBILE_NO"].ToString());
                }
                bindAssignmentDetails(ReGNo);
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
            if (_strAlertMessage != string.Empty && _strAlertMessage.Trim() != "")
            {
                lblMessage.Text = _strAlertMessage;
                objclsCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }
            objclsCommon = null;
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objclsCommon = new clsCommon();
        clsAssignments objclsAssignments = new clsAssignments();
        EC_Assignments objEC_Assignments = new EC_Assignments();
        DataTable dtResult;
        int assignmentId;
        int ReGNo;
        try
        {
            int.TryParse(hdAssignmentID.Value.ToString(), out assignmentId);
            if (assignmentId > 0)
            {

                int.TryParse(ddlStudents.SelectedValue, out ReGNo);
                objEC_Assignments.ConnectionString = user.ConnectionString;
                objEC_Assignments.RegNO = ReGNo;
                objEC_Assignments.Mode = "I";
                objEC_Assignments.AssignmentId = assignmentId;
                objEC_Assignments.CreatedBy = user.UserId;
                objEC_Assignments.Reading = txtReading.Text.Trim();
                objEC_Assignments.Listening = txtListening.Text.Trim();
                objEC_Assignments.Writing1 = txtWriting1.Text.Trim();
                objEC_Assignments.Writing2 = txtWriting2.Text.Trim();
                objEC_Assignments.Speaking = txtSpeaking.Text.Trim();
                objEC_Assignments.NoteRemarks = txtRemarks.Text.Trim();

                dtResult = objclsAssignments.Add_Remove_Assignments_Marks(objEC_Assignments);

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
                   
                    bindAssignmentDetails(ReGNo);
                }
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
            if (_strAlertMessage != string.Empty && _strAlertMessage.Trim() != "")
            {
                lblMessage.Text = _strAlertMessage;
                objclsCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);

               if(_strMsgType == "Error_DB")
                   ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "displayMarksEntryPanel('0');", true);
            }
            objclsCommon = null;
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objclsCommon = new clsCommon();
        clsAssignments objclsAssignments = new clsAssignments();
        EC_Assignments objEC_Assignments = new EC_Assignments();
        DataTable dtResult;
        int assignmentId;
        int ReGNo;
        try
        {
            int.TryParse(hdAssignmentID.Value.ToString(), out assignmentId);
            if (assignmentId > 0)
            {

                int.TryParse(ddlStudents.SelectedValue, out ReGNo);
                objEC_Assignments.ConnectionString = user.ConnectionString;
                objEC_Assignments.RegNO = ReGNo;
                objEC_Assignments.Mode = "U";
                objEC_Assignments.AssignmentId = assignmentId;
                objEC_Assignments.CreatedBy = user.UserId;
                objEC_Assignments.Reading = txtReading.Text.Trim();
                objEC_Assignments.Listening = txtListening.Text.Trim();
                objEC_Assignments.Writing1 = txtWriting1.Text.Trim();
                objEC_Assignments.Writing2 = txtWriting2.Text.Trim();
                objEC_Assignments.Speaking = txtSpeaking.Text.Trim();
                objEC_Assignments.NoteRemarks = txtRemarks.Text.Trim();

                dtResult = objclsAssignments.Add_Remove_Assignments_Marks(objEC_Assignments);

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

                    bindAssignmentDetails(ReGNo);
                }
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
            if (_strAlertMessage != string.Empty && _strAlertMessage.Trim() != "")
            {
                lblMessage.Text = _strAlertMessage;
                objclsCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);

                if (_strMsgType == "Error_DB")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "displayMarksEntryPanel('0');", true);
            }
            objclsCommon = null;
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objclsCommon = new clsCommon();
        clsAssignments objclsAssignments = new clsAssignments();
        EC_Assignments objEC_Assignments = new EC_Assignments();
        DataTable dtResult;
        int assignmentId;
        int ReGNo;
        try
        {
            int.TryParse(hdAssignmentID.Value.ToString(), out assignmentId);
            if (assignmentId > 0)
            {

                int.TryParse(ddlStudents.SelectedValue, out ReGNo);
                objEC_Assignments.ConnectionString = user.ConnectionString;
                objEC_Assignments.RegNO = ReGNo;
                objEC_Assignments.Mode = "D";
                objEC_Assignments.AssignmentId = assignmentId;
                objEC_Assignments.CreatedBy = user.UserId;
                objEC_Assignments.Reading = txtReading.Text.Trim();
                objEC_Assignments.Listening = txtListening.Text.Trim();
                objEC_Assignments.Writing1 = txtWriting1.Text.Trim();
                objEC_Assignments.Writing2 = txtWriting2.Text.Trim();
                objEC_Assignments.Speaking = txtSpeaking.Text.Trim();
                objEC_Assignments.NoteRemarks = txtRemarks.Text.Trim();

                dtResult = objclsAssignments.Add_Remove_Assignments_Marks(objEC_Assignments);

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

                    bindAssignmentDetails(ReGNo);
                }
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
            if (_strAlertMessage != string.Empty && _strAlertMessage.Trim() != "")
            {
                lblMessage.Text = _strAlertMessage;
                objclsCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);

                if (_strMsgType == "Error_DB")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "displayMarksEntryPanel('0');", true);
            }
            objclsCommon = null;
        }
    }
    
    #endregion

    #region "Grid Events"
    protected void grdStyled_PageIndexChanging(Object sender, GridViewPageEventArgs e)
    {
        grdStyled.PageIndex = e.NewPageIndex;
        getDetails(txtSearch.Text.Trim());
    }
    protected void grdStyled_SelectedIndexChanged(object sender, EventArgs e)
    {
        clsCommon objclsCommon = new clsCommon();
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();

        string _strAlertMessage = string.Empty;
        string ImgPath = string.Empty;
        int UID, RoleID;

        try
        {

            lblMessage.Text = string.Empty;

            int.TryParse(grdStyled.DataKeys[grdStyled.SelectedIndex]["UID"].ToString(), out UID);
            int.TryParse(grdStyled.DataKeys[grdStyled.SelectedIndex]["RoleId"].ToString(), out RoleID);
            if (UID > 0)
            {

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
    protected void grdAssigned_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        CheckBox chkAccessibility;
        Button btnSaveMarks;
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strActive = DataBinder.Eval(e.Row.DataItem, "GRD_STATUS").ToString().Trim();
                btnSaveMarks = (Button )e.Row.Cells[5].FindControl("buttonSaveMarks");

                if (strActive == "Assigned")
                {
                    e.Row.BackColor = System.Drawing.Color.LightYellow;
                    btnSaveMarks.Visible = false;
                }
                else if (strActive == "Activated")
                {
                    e.Row.BackColor = System.Drawing.Color.LightGreen;
                    chkAccessibility = (CheckBox)e.Row.Cells[1].FindControl("chkAccessibility");
                    chkAccessibility.Visible = false;
                    btnSaveMarks.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            chkAccessibility = null;
            btnSaveMarks = null;
        }
    }
    #endregion
 
    #region "Methods"
    private void SendMailAlert(string strAction, GridView grdDetails, string strStudentName, string strEmailID)
    {
        clsCommon objClsCommon = new clsCommon();
        ObjSendEmail SendMail;
        DataTable dtInformation;

        string strAssignment = string.Empty;
        string strTitle = string.Empty;
        string strOLList = string.Empty;
        string strSubject = string.Empty;
        int smtpPort;

        try
        {
            lblMessage.Text = "";
            if (strAction == "Assigned")
            {
                strSubject = "Assignment allotment acknowledgement";
            }
            else
            {
                strSubject = "Assignment activation acknowledgement";
            }

            dtInformation = objClsCommon.getMailBody("COMMON", "MailFormat", "Mail On Assignment Upload(Student)", user.ConnectionString);
            
            if (dtInformation != null & dtInformation.Rows.Count > 0)
            {
                for (int i = 0; i <= grdDetails.Rows.Count - 1; i++)
                {
                    CheckBox chkAccessibility = (CheckBox)grdDetails.Rows[i].Cells[0].FindControl("chkAccessibility");
                    if ((chkAccessibility != null))
                    {
                        if (chkAccessibility.Checked == true)
                        {
                            strAssignment = grdDetails.Rows[i].Cells[2].Text.ToString();
                            strTitle = grdDetails.Rows[i].Cells[3].Text.ToString();

                            if (strOLList == string.Empty)
                            {
                                strOLList = "<li class=''auto-style1''><strong>" + strAssignment + "/" + strTitle + "</strong></li>";
                            }
                            else
                            {
                                strOLList = strOLList + "<li class=''auto-style1''><strong>" + strAssignment + "/" + strTitle + "</strong></li>";
                            } 
                        }
                    }
                }

                //SEND MAIL STARTS 
                if (strOLList !=string.Empty )
                {

                    SendMail = new ObjSendEmail();
                    try
                    {
                        SendMail.From = dtInformation.Rows[0]["FROM"].ToString();
                        SendMail.To = strEmailID;
                        SendMail.CC = string.Empty;
                        SendMail.BCC = objClsCommon.getConfigKeyValue("EMAIL IDS FOR CC OF MAIL MESSAGE FOR EACH OPERATION", "EMAIL RECEPIENTS GROUP", user.ConnectionString); ;
                        SendMail.SMTPHost = user.SMTPServer;
                        int.TryParse(user.SMTPPort, out smtpPort);
                        SendMail.SMTPPort = smtpPort;
                        SendMail.UserId = user.SMTPUSerID;
                        SendMail.Password = user.SMTPPassword; ;
                        SendMail.SMTPSSL = user.SMTPSSL == "Y" ? true : false; ;
                        SendMail.MailType = "I";
                        SendMail.Subject = user.Orig_Name + ": " + strSubject;
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

                            SendMail.MailBody = SendMail.MailBody.Replace("#Name#", strStudentName);
                            SendMail.MailBody = SendMail.MailBody.Replace("#OL#", strOLList);
                            SendMail.MailBody = SendMail.MailBody.Replace("#action#", strAction);
                            SendMail.MailBody = SendMail.MailBody.Replace("#Subject#", strSubject);
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
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        SendMail = null;
                    }
                }
                //SEND MAIL ENDS
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (lblMessage.Text != "") ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + lblMessage.Text + "');</script>", false);
            objClsCommon = null;
            SendMail = null;
            dtInformation = null;
        }
    }
    private void SendSMSAlert(string strAction, GridView grdDetails, string strStudentName, string strMobileNo)
    {
       clsCommon objClsCommon = new clsCommon();
        string strKey = string.Empty;
        string strLink = string.Empty;
        DataTable dtInformation;     
        try
        {

            lblMessage.Text = "";

            if (strAction == "Assign")
            {
                strKey = "SMS On Assignment Upload";
            }
            else
            {
                strKey = "SMS On Assignment Activate";
            }

 
            dtInformation = objClsCommon.getSMSBody("SMS", strKey, user.ConnectionString );

            if (dtInformation != null & dtInformation.Rows.Count > 0)
            {

                for (int i = 0; i <= grdDetails.Rows.Count - 1; i++)
                {
                    CheckBox chkAccessibility = (CheckBox)grdDetails.Rows[i].Cells[1].FindControl("chkAccessibility");
                    if ((chkAccessibility != null))
                    {
                        if (chkAccessibility.Checked == true)
                        {
                         
                            //Sending SMS -Start  
                            EC_SMS_SendMessage objEC_SMS_SendMessage = new EC_SMS_SendMessage();
                            clsEC_SMS_SendMessage objclsEC_SMS_SendMessage = new clsEC_SMS_SendMessage();
                            try
                            {
                                objEC_SMS_SendMessage.SMS_KEY = strKey;
                                objEC_SMS_SendMessage.SMS_Desc = "SMS";
                                objEC_SMS_SendMessage.SMS_MobileNo = strMobileNo  ;
                                objEC_SMS_SendMessage.SMS_Msg = dtInformation.Rows[0]["SMS_MSG"].ToString();
                                objEC_SMS_SendMessage.SMS_Msg = objEC_SMS_SendMessage.SMS_Msg.Replace("#Name#", strStudentName );
                                objEC_SMS_SendMessage.SMS_Msg = objEC_SMS_SendMessage.SMS_Msg.Replace("#Assignment#", grdDetails.Rows[i].Cells[3].Text.Trim());
                                objEC_SMS_SendMessage.SMS_URLLink = dtInformation.Rows[0]["SMS_Link"].ToString();

                                try
                                {
                                    objclsEC_SMS_SendMessage.SendSMS(objEC_SMS_SendMessage);
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
            if (lblMessage.Text != "") ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + lblMessage.Text + "');</script>", false);
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
    private void bindAssignmentDetails(int RegNo)
    {
        clsAssignments objclsAssignments = new clsAssignments();
        EC_Assignments objEC_Assignments = new EC_Assignments();
        clsCommon objclsCommon = new clsCommon();
        DataSet dsResult;
        string _strAlertMessage = string.Empty;
        try
        {
            lblAssigned.Text = string.Empty;
            lblAvailable.Text = string.Empty;

            //CLEAR GRIDS STARTS
            tblAssignment.Visible = false;

            grdAvailable.DataSource = null;
            grdAvailable.DataBind();
            grdAssigned.DataSource = null;
            grdAssigned.DataBind();

            btnActivateAssignment.Visible = false;
            btnMapAssignment.Visible = false;
            //CLEAR GRIDS ENDS

            objEC_Assignments.ConnectionString = user.ConnectionString;
            objEC_Assignments.RegNO = RegNo;
            objEC_Assignments.Mode = "B";
            objEC_Assignments.GrdiSrchInput = "";
            dsResult = objclsAssignments.Assignment_Mapping_Details(objEC_Assignments);

            if (dsResult != null && dsResult.Tables.Count > 0)
            {
                btnActivateAssignment.Enabled = false;
                btnMapAssignment.Enabled = false;

                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    grdAvailable.DataSource = dsResult.Tables[0];
                    grdAvailable.DataBind();

                    btnMapAssignment.Visible  = true;
                    btnMapAssignment.Enabled = true;
                    tblAssignment.Visible = true;
                    lblAvailable.Text = " Records : " + dsResult.Tables[0].Rows.Count.ToString();
                }

                if (dsResult.Tables[1].Rows.Count > 0)
                {
                    grdAssigned.DataSource = dsResult.Tables[1];
                    grdAssigned.DataBind();

                    tblAssignment.Visible = true;
                    btnActivateAssignment.Enabled = true;
                    btnActivateAssignment.Visible = Visible;
                    lblAssigned.Text = " Records : " + dsResult.Tables[1].Rows.Count.ToString();
                }
            }
            else
            {
                tblAssignment.Visible = false;
                grdAvailable.DataSource = null;
                grdAvailable.DataBind();

                grdAssigned.DataSource = null;
                grdAssigned.DataBind();

                btnActivateAssignment.Enabled = false;
                btnMapAssignment.Enabled = false;

                _strAlertMessage = "No assignment details found!";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (_strAlertMessage != string.Empty && _strAlertMessage.Trim() != "")
            {
                objclsCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }
            objclsAssignments = null;
            dsResult = null;
            objclsCommon = null;
        }
    }
    #endregion
}
 