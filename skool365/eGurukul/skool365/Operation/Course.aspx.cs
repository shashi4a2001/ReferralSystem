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

public partial class Operation_Course : System.Web.UI.Page
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
                bindCourseSegments();
                getDetails("");
                ddlCourseSegment.Focus();
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
        clsCommonUtility objclsCommonUtility = new clsCommonUtility();
        ObjMaster objmaster = new ObjMaster();
        DataTable dtResult;
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objclsCommon = new clsCommon();

        string _strAlertMessage = string.Empty;
        string strLoggedInUser = string.Empty;
        string confirmValue = string.Empty;
        string strPassword = string.Empty;
        int UID;
        int SegmentID;
        int SemesterCount;

        strLoggedInUser = user.UserId;
        if(strLoggedInUser=="" || strLoggedInUser==null) strLoggedInUser = "365skool";

        try
        {
            lblMessage.Text = "";

            //Vlidation
            if (txtCourseCode.Text.Trim() == "")
            {
                _strMsgType = "Info";
                _strAlertMessage = "Enter Course Code!";
                txtCourseCode.Focus();
            }

            if (objclsCommon.isValidInput(user.RestrictedInputChar, txtCourseCode.Text.Trim()) == false)
            {
                _strMsgType = "Info";
                _strAlertMessage = "Course Code contains Invalid Characters";
                txtCourseCode.Focus();
                return;
            }

            if (txtCourseName.Text.Trim() == "")
            {
                _strMsgType = "Info";
                _strAlertMessage = "Enter Course Name!";
                txtCourseName.Focus();
                return;
            }

            if (objclsCommon.isValidInput(user.RestrictedInputChar, txtCourseName.Text.Trim()) == false)
            {
                _strAlertMessage = "Course Name contains Invalid Characters";
                txtCourseName.Focus();
                return;
            }

            int.TryParse(ddlCourseSegment.SelectedValue, out SegmentID);
            int.TryParse(txtSemesterCount.Text, out SemesterCount);

            objmaster.SegmentId = SegmentID;
            objmaster.CourseCode = txtCourseCode.Text.Trim();
            objmaster.CourseName = txtCourseName.Text.Trim();
            objmaster.SemesterCount = SemesterCount;

            if (btnSubmit.Text == "Save")
            {
                objmaster.Mode = "C";
                objmaster.Status = 1;
            }
            else
            {
                objmaster.Mode = "U";
                int.TryParse(hdId.Value, out UID);
                objmaster.CourseId = UID;
            }

            if (objmaster.Mode == "U")
            {
                objmaster.Status = 1;
                //If status is going to make Active. Generate and Send Password
                if (chkActivated.Visible == true)
                {
                    if (chkActivated.Checked == false) objmaster.Status =0;
                    if (chkActivated.Checked == true) objmaster.Status = 1;
                }
            }

            objmaster.ModifiedBy = strLoggedInUser;
            objmaster.CreatedBy = strLoggedInUser;
            objmaster.ConnectionString = user.ConnectionString;
            dtResult = objclsCommonUtility.SaveCourse(objmaster);

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

            lblMessage.Text = _strAlertMessage;
            getDetails(txtSearch.Text.Trim());
            UpdatePanel2.Update();

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
            objmaster = null;
            dtResult = null;
            objclsCommon = null;
            objclsCommonUtility = null;
            objclsErrorLogger = null;
 
        }
    }
 
    private void getDetails(string strText)
    {
        clsCommonUtility objclsCommonUtility = new clsCommonUtility();
        ObjMaster master = new ObjMaster();
        DataTable dt;
        try
        {
            master.SearchText = strText;
            master.ConnectionString = user.ConnectionString;
            dt = objclsCommonUtility.ShowCourse(master);

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
            master = null;
            objclsCommonUtility = null;
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
        int CourseID, SegmentID;

        try
        {
            clearControls();
            lblMessage.Text = string.Empty;

            int.TryParse(grdStyled.DataKeys[grdStyled.SelectedIndex]["Course_ID"].ToString(), out CourseID);
            int.TryParse(grdStyled.DataKeys[grdStyled.SelectedIndex]["Segment_id"].ToString(), out SegmentID);
            if (CourseID > 0) 
            {
                hdId.Value = Convert.ToString(CourseID);
                txtCourseCode.Text = Server.HtmlDecode(grdStyled.SelectedRow.Cells[1].Text.Trim());
                txtCourseName.Text =  Server.HtmlDecode(grdStyled.SelectedRow.Cells[2].Text.Trim());
                txtSemesterCount.Text = grdStyled.SelectedRow.Cells[4].Text.Trim();
                ddlCourseSegment.SelectedValue = SegmentID.ToString();// grdStyled.SelectedRow.Cells[3].Text.Trim();
                btnSubmit.Text = "Update";
                rwStatus.Visible = true;

                chkActivated.Checked = false;
                chkActivated.Visible = false;

                lblAccountStatus.Text = "Status: Active";
                objclsCommon.setLabelMessage(lblAccountStatus, "Info");

                if (grdStyled.SelectedRow.Cells[5].Text == "I")
                {
                    lblAccountStatus.Text ="Status: Inactive";

                    objclsCommon.setLabelMessage(lblAccountStatus, "Error");
                    chkActivated.Enabled = true;
                    chkActivated.Checked = false;
                    chkActivated.Visible = true;
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
        int courseID;
        clsCommon objClsCommon = new clsCommon();
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommonUtility obclsCommonUtility = new clsCommonUtility();
        ObjMaster master = new ObjMaster();
        DataTable dtResult;

        try
        {
            int.TryParse(grdStyled.DataKeys[e.RowIndex]["Course_ID"].ToString(), out courseID);
            master.CourseId = courseID;
            master.ModifiedBy = user.UserId;
            master.ConnectionString = user.ConnectionString;
            dtResult = obclsCommonUtility.DeleteCourse(master);

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
            obclsCommonUtility = null;
            master = null;
            objclsErrorLogger = null;
            dtResult = null;
        }
    }
   
    protected void grdStyled_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strActive = DataBinder.Eval(e.Row.DataItem, "STS").ToString().Trim();
           
            if (strActive == "A")
            {
                e.Row.BackColor = System.Drawing.Color.LightGreen;
            }
            else if (strActive == "I")
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
            lblAccountStatus.Text = "";
            hdId.Value = "0";
            txtCourseCode.Text = "";
            txtCourseName.Text = "";
            txtSemesterCount.Text = "";
            txtSearch.Text = "";

            txtCourseName.Enabled = true;
            chkActivated.Checked = true;
            chkActivated.Enabled = false;
            ddlCourseSegment.Enabled = true;
            btnSubmit.Text = "Save";
            UpdatePanel1.Update();
            rwStatus.Visible = false ;
            chkActivated.Checked = false;
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

    private void bindCourseSegments()
    {
        clsCommonUtility objclsCommonUtility = new clsCommonUtility();
        DataTable dtInfo;
        try
        {
            dtInfo = objclsCommonUtility.ShowSegment(user.ConnectionString);

            if (dtInfo != null)
            {
                ddlCourseSegment.DataSource = dtInfo;
                ddlCourseSegment.DataValueField = "ID";
                ddlCourseSegment.DataTextField = "SEGMENT_NAME";
                ddlCourseSegment.DataBind();
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dtInfo = null;
            objclsCommonUtility = null;
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
 