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
        
    }
    protected void Page_Load(object sender, EventArgs e) 
    {
       
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        
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
        //clsOlympiad objclsOlympiad = new clsOlympiad();
        DataTable dtInfo;
        try
        {
            dtInfo = null;// objclsOlympiad.getStateList( user.ConnectionString);

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
            //objclsOlympiad = null;
            master = null;
            objclsCommonUtility = null;

        }

    }
 
    private void getRegistrationRequest(string strText)
    {
        objOlympiad Olympiad = new objOlympiad();
       // clsOlympiad objclsOlympiad = new clsOlympiad();
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
            dtInfo = null;// objclsOlympiad.getStudentSignupRequest (Olympiad);

            if (dtInfo != null)
            {
                //grdStyled.DataSource = dtInfo;
                //grdStyled.DataBind();
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
           // objclsOlympiad = null;
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
 
    
}