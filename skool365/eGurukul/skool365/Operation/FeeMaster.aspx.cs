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

public partial class Operation_FeeMaster : System.Web.UI.Page
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
                ddlSegment.Focus();
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        clsCommonUtility objclsCommonUtility = new clsCommonUtility();
        ObjMaster objmaster = new ObjMaster();
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objclsCommon = new clsCommon();
        System.Text.StringBuilder qryBuilder = new System.Text.StringBuilder();
 

        string _strAlertMessage = string.Empty;
        string strLoggedInUser = string.Empty;
        string confirmValue = string.Empty;
        double dblAmt;

        strLoggedInUser = user.UserId;
        if (strLoggedInUser == "" || strLoggedInUser == null) strLoggedInUser = "365skool";

        int intRes;
        try
        {
            //For Admission
            int intSectionID;

           if(grdAdmissionFee.Rows.Count>0) qryBuilder.Append("DECLARE @W_DATE DATETIME SELECT @W_DATE=GETDATE() ");

            foreach (GridViewRow rows in grdAdmissionFee.Rows)
            {
                if (rows.RowType == DataControlRowType.DataRow)
                {
                    TextBox foo = rows.FindControl("txtAdmnAmount") as TextBox;
                    dblAmt = 0;
                    dblAmt = Convert.ToDouble(((TextBox)rows.FindControl("txtAdmnAmount")).Text);
                    intSectionID = Convert.ToInt16(grdAdmissionFee.DataKeys[rows.RowIndex ]["ID"].ToString());

                    qryBuilder.Append(" IF EXISTS(SELECT 1 FROM Fee_Master WHERE Section_ID=" + intSectionID + " AND Session='" + ddlSegment.SelectedItem.Text + "' AND Course_ID=" + ddlCourse.SelectedValue + " AND STS=1 ) ");
                    qryBuilder.Append("\r\n");
                    qryBuilder.Append("BEGIN ");
                    qryBuilder.Append("\r\n");
                    qryBuilder.Append("UPDATE Fee_Master SET [Amount]=" + dblAmt + " ,[Modified_By]='" + strLoggedInUser + "',[Modified_Date]  =@W_DATE ");
                    qryBuilder.Append("\r\n");
                    qryBuilder.Append("WHERE  Section_ID=" + intSectionID + " AND Session='" + ddlSegment.SelectedItem.Text + "' AND Course_ID=" + ddlCourse.SelectedValue + "  AND sTS=1 ");
                    qryBuilder.Append("\r\n");
                    qryBuilder.Append("END ");
                    qryBuilder.Append("\r\n");
                    qryBuilder.Append("ELSE ");
                    qryBuilder.Append("\r\n");
                    qryBuilder.Append("BEGIN ");
                    qryBuilder.Append("\r\n");
                    qryBuilder.Append("INSERT INTO Fee_Master ( ");
                    qryBuilder.Append("\r\n");
                    qryBuilder.Append("[Session]  ,[Course_ID] ,[Section_ID] ,[Amount]  ,[sts]  ,[Created_By]  ,[Created_Date]  ) ");
                    qryBuilder.Append("\r\n");
                    qryBuilder.Append("Select '" + ddlSegment.SelectedItem.Text + "'," + ddlCourse.SelectedValue + "," + intSectionID + "," + dblAmt + ",1,'" + strLoggedInUser + "',@W_DATE ");
                    qryBuilder.Append("\r\n");
                    qryBuilder.Append("END ");
                    qryBuilder.Append("\r\n");

                }
            }

            //semester fee

            foreach (GridViewRow rows in grdSemesterFee.Rows)
            {
                if (rows.RowType == DataControlRowType.DataRow)
                {
                    dblAmt = 0;
                    dblAmt = Convert.ToDouble(((TextBox)rows.FindControl("txtSemAmount")).Text);

                    intSectionID = Convert.ToInt16(grdSemesterFee.DataKeys[rows.RowIndex]["ID"].ToString());

                    qryBuilder.Append("\r\n");
                    qryBuilder.Append("IF EXISTS(SELECT 1 FROM Fee_Master WHERE Section_ID=" + intSectionID + " AND Session='" + ddlSegment.SelectedItem.Text + "' AND Course_ID=" + ddlCourse.SelectedValue + " AND STS=1 ) ");
                    qryBuilder.Append("\r\n");
                    qryBuilder.Append("BEGIN ");
                    qryBuilder.Append("\r\n");
                    qryBuilder.Append("UPDATE Fee_Master SET [Amount]=" + dblAmt + " ,[Modified_By]='" + strLoggedInUser + "',[Modified_Date]  = @W_DATE ");
                    qryBuilder.Append("\r\n");
                    qryBuilder.Append("WHERE  Section_ID=" + intSectionID + " AND Session='" + ddlSegment.SelectedItem.Text + "' AND Course_ID=" + ddlCourse.SelectedValue + " AND STS=1  ");
                    qryBuilder.Append("\r\n");
                    qryBuilder.Append("END ");
                    qryBuilder.Append("\r\n");
                    qryBuilder.Append("ELSE ");
                    qryBuilder.Append("\r\n");
                    qryBuilder.Append("BEGIN ");
                    qryBuilder.Append("\r\n");
                    qryBuilder.Append("INSERT INTO Fee_Master ( ");
                    qryBuilder.Append("\r\n");
                    qryBuilder.Append("[Session]  ,[Course_ID] ,[Section_ID] ,[Amount]  ,[Sts]  ,[Created_By]  ,[Created_Date]  ) ");
                    qryBuilder.Append("\r\n");
                    qryBuilder.Append("Select '" + ddlSegment.SelectedItem.Text + "'," + ddlCourse.SelectedValue + "," + intSectionID + "," + dblAmt + ",1 ,'" + strLoggedInUser + "' , @W_DATE  ");
                    qryBuilder.Append("\r\n");
                    qryBuilder.Append("END ");
                    qryBuilder.Append("\r\n");

                }
            }

            //
            if (qryBuilder.ToString().Length > 0)
            {
                objmaster.ConnectionString = user.ConnectionString;
                objmaster.strQuery = qryBuilder.ToString();
                intRes = objclsCommonUtility.saveFeeMaster(objmaster);
                if (intRes > 0)
                {
                    _strMsgType = "Success";
                    _strAlertMessage = "Master Fee detail for Course [" + ddlCourse.SelectedItem.Text.Trim() + "] and Session [" + ddlSegment.SelectedItem.Text.Trim() + "] has been defined successfully.";

                    //Refresh Detail tab
                    getDetails("");
                }
            }


            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>AnnualFee();SemesterFee() ;</script>", false);

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

            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>AnnualFee();SemesterFee() ;</script>", false);
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
       
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clearControls();
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
            getDetails(txtSearch.Text.Trim());
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>AnnualFee();SemesterFee() ;</script>", false);
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

    protected void btnGetData_Click(object sender, EventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objclsCommon = new clsCommon();
        clsCommonUtility objclsCommonUtility = new clsCommonUtility();
        ObjMaster master = new ObjMaster();
        DataTable dtInfo = new DataTable();
        string _strAlertMessage = string.Empty;
        int intSectionID;
        DataView dvFeeHeader; 
        try
        {
            if (ddlCourse.SelectedValue == "0" || ddlSegment.SelectedValue == "0")
            {
                clearControls();
                return;
            }

            clearControls();
            lblMessage.Text = "";
            int.TryParse(ddlCourse.SelectedValue.ToString(), out intSectionID);
            master.SearchText = "";
            master.Mode = "D";
            master.Session = ddlSegment.SelectedValue;
            master.CourseId = intSectionID;
            master.ConnectionString = user.ConnectionString;
            dtInfo = objclsCommonUtility.getFeeDetails(master);

            //Bind grids
            //Admission
            dvFeeHeader = new DataView(dtInfo);
            dvFeeHeader.RowFilter = "Segment='Admission'";
            grdAdmissionFee.DataSource = dvFeeHeader.ToTable(true, "ID", "HEADER_NAME", "AMOUNT","STS");
            grdAdmissionFee.DataBind();

            //Semester
            dvFeeHeader = new DataView(dtInfo);
            dvFeeHeader.RowFilter = "Segment='Semester'";
            grdSemesterFee.DataSource = dvFeeHeader.ToTable(true,"ID", "HEADER_NAME", "AMOUNT","STS");
            grdSemesterFee.DataBind();

            rwAnnual.Visible = false; ;
            rwSemester.Visible = false;
            rwStatus.Visible = false;

            //Validation
            if (grdAdmissionFee.Rows.Count == 0 && grdSemesterFee.Rows.Count == 0)
            {
                _strMsgType = "Info";
                _strAlertMessage = "Fee Header is not defined for Admission & Semester Segment. Please define it.";
            }
            else if (grdAdmissionFee.Rows.Count == 0)
            {
                _strMsgType = "Info";
                _strAlertMessage = "Fee Header is not defined for Admission Segment. Please define it.";
                rwAnnual.Visible = false;
                rwSemester.Visible = true;
                rwStatus.Visible = true;
            }
            else if (grdSemesterFee.Rows.Count == 0)
            {
                _strMsgType = "Info";
                _strAlertMessage = "Fee Header is not defined for Semester Segment. Please define it.";
                rwAnnual.Visible = true;
                rwSemester.Visible = false; ;
                rwStatus.Visible = true;
            }
            else
            {
                rwAnnual.Visible = true;
                rwSemester.Visible = true;
                rwStatus.Visible = true;
                rwOperation.Visible = true;

                ddlSegment.Enabled = false;
                ddlCourse.Enabled = false;
                btnGetData.Enabled = false;

                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>AnnualFee();SemesterFee() ;</script>", false);

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
            dvFeeHeader = null;
            objclsCommon = null;
            objclsErrorLogger = null;
        }
    }

    #endregion

    #region "Methos"


    private void getDetails(string strText)
    {
        clsCommonUtility objclsCommonUtility = new clsCommonUtility();
        ObjMaster master = new ObjMaster();
        DataTable dt;
        try
        {
            master.SearchText = strText;
            master.ConnectionString = user.ConnectionString;
            dt = objclsCommonUtility.getFeeDetails(master);

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
                ddlSegment.DataSource = dtInfo;
                ddlSegment.DataValueField = "SESSION";
                ddlSegment.DataTextField = "SESSION";
                ddlSegment.DataBind();
            }

            if (ddlSegment.Items.Count > 0)
            {
                item = new ListItem();
                item.Value = "0";
                item.Text = "Select Session";
                ddlSegment.Items.Insert(0, item);
                ddlSegment.SelectedValue = "0";
            }
            else
            {
                btnGetData.Enabled = false;
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
                item.Text = "Select Course";
                ddlCourse.Items.Insert(0, item);
                ddlCourse.SelectedValue = "0";
            }
            else
            {
                btnGetData.Enabled = false;
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


    private void clearControls()
    {
        clsCommon objClsCommon = new clsCommon();
        try
        {
            lblAccountStatus.Text = "";
            rwAnnual.Visible = false; ;
            rwSemester.Visible = false;
            rwStatus.Visible = false;
            rwOperation.Visible = false;

            ddlSegment.Enabled = true;
            ddlCourse.Enabled = true;
            btnGetData.Enabled = true;

            hdValAdmission.Value = "0";
            hdValSemester.Value = "0";

            grdAdmissionFee.DataSource = null;
            grdAdmissionFee.DataBind();

            grdSemesterFee.DataSource = null;
            grdSemesterFee.DataBind();

            txtTotalAnnualFee.Text = string.Empty;
            txtTotalSemesterFee.Text = string.Empty;
            txtSearch.Text = string.Empty;

            btnSubmit.Text = "Define Fee Details" ;
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            objClsCommon.setLabelMessage(lblMessage, "Error");
            ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);
            UpdatePanel1.Update();
        }
        finally
        {
            objClsCommon = null;
        }
    }

    #endregion

    #region "Grid Event"

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
        int CourseID;

        try
        {
            clearControls();
            lblMessage.Text = string.Empty;

            int.TryParse(grdStyled.DataKeys[grdStyled.SelectedIndex]["COURSE_ID"].ToString(), out CourseID);

            if (CourseID > 0)
            {
                ddlCourse.SelectedValue = CourseID.ToString();
                ddlSegment.SelectedValue = Server.HtmlDecode(grdStyled.SelectedRow.Cells[1].Text.Trim());
                btnSubmit.Text =  "Update Fee Details" ;
                btnGetData_Click(btnGetData, e);
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
        int headerID;
        clsCommon objClsCommon = new clsCommon();
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommonUtility obclsCommonUtility = new clsCommonUtility();
        ObjMaster master = new ObjMaster();
        DataTable dtResult;

        try
        {
            int.TryParse(grdStyled.DataKeys[e.RowIndex]["id"].ToString(), out headerID);
            master.HeaderID = headerID;
            master.ModifiedBy = user.UserId;
            master.ConnectionString = user.ConnectionString;
            dtResult = obclsCommonUtility.DeleteHeader(master);

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
   
    protected void grdAdmissionFee_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        Double dblCollegeAdmnFee = 0;
        TextBox objtxtAdmnAmount;

        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objclsCommon = new clsCommon();
        string _strAlertMessage = string.Empty;
        string strActive = string.Empty;
        try
        {
            if (txtTotalAnnualFee.Text.Trim() != "") dblCollegeAdmnFee = Convert.ToDouble(txtTotalAnnualFee.Text);

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                objtxtAdmnAmount = (TextBox)e.Row.FindControl("txtAdmnAmount");
                objtxtAdmnAmount.Attributes.Add("onkeypress", "javascript:return fnCommonAcceptNumberOnly(this)");
                objtxtAdmnAmount.Attributes.Add("onblur", "javascript:return AnnualFee()");
                dblCollegeAdmnFee = dblCollegeAdmnFee + Convert.ToDouble(objtxtAdmnAmount.Text);

                strActive = grdAdmissionFee.DataKeys[e.Row.RowIndex]["STS"].ToString();
               
                if (strActive == "A")
                {
                    e.Row.BackColor = System.Drawing.Color.LightYellow;
                }
                else if (strActive == "U")
                {
                    e.Row.BackColor = System.Drawing.Color.LightGreen;
                    btnSubmit.Text = "Update Fee Details";
                }
                else if (strActive == "I")
                {
                    e.Row.BackColor = System.Drawing.Color.LightGray;
                }
            }
            txtTotalAnnualFee.Text = Convert.ToString(dblCollegeAdmnFee);
            objtxtAdmnAmount = null;
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

    protected void grdAdmissionFee_RowCreated(object sender, GridViewRowEventArgs e)
    {
        TextBox objtxtAdmnAmount;

        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objclsCommon = new clsCommon();
        string _strAlertMessage = string.Empty;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try{
                objtxtAdmnAmount = (TextBox)e.Row.FindControl("txtAdmnAmount");
                objtxtAdmnAmount.Attributes.Add("onkeypress", "javascript:return fnCommonAcceptNumberOnly()");
                objtxtAdmnAmount.Attributes.Add("onblur", "javascript:return AnnualFee()");
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

    protected void grdSemesterFee_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        Double dblSemesterFee = 0;
        TextBox objtxtSemAmount;

        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objclsCommon = new clsCommon();
        string _strAlertMessage = string.Empty;
        string strActive = string.Empty;
        try
        {
            if (txtTotalSemesterFee.Text.Trim() != "") dblSemesterFee = Convert.ToDouble(txtTotalSemesterFee.Text);

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                objtxtSemAmount = (TextBox)e.Row.FindControl("txtSemAmount");
                objtxtSemAmount.Attributes.Add("onkeypress", "javascript:return fnCommonAcceptNumberOnly(this)");
                objtxtSemAmount.Attributes.Add("onblur", "javascript:return SemesterFee()");
                dblSemesterFee = dblSemesterFee + Convert.ToDouble(objtxtSemAmount.Text);

                strActive = grdSemesterFee.DataKeys[e.Row.RowIndex]["STS"].ToString();  
                if (strActive == "A")
                {
                    e.Row.BackColor = System.Drawing.Color.LightYellow;
                }
                else if (strActive == "U")
                {
                    e.Row.BackColor = System.Drawing.Color.LightGreen;
                    btnSubmit.Text = "Update Fee Details";
                }
                else if (strActive == "I")
                {
                    e.Row.BackColor = System.Drawing.Color.LightGray;
                }

            }
            txtTotalAnnualFee.Text = Convert.ToString(dblSemesterFee);
            objtxtSemAmount = null;
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

    protected void grdSemesterFee_RowCreated(object sender, GridViewRowEventArgs e)
    {
        TextBox objtxtAdmnAmount;

        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objclsCommon = new clsCommon();
        string _strAlertMessage = string.Empty;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                objtxtAdmnAmount = (TextBox)e.Row.FindControl("txtSemAmount");
                objtxtAdmnAmount.Attributes.Add("onkeypress", "javascript:return fnCommonAcceptNumberOnly()");
                objtxtAdmnAmount.Attributes.Add("onblur", "javascript:return SemesterFee()");
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
    
    #endregion
}
 