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

public partial class Operation_FeeHeader : System.Web.UI.Page
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
                bindSegment();
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
        int headerID;

        strLoggedInUser = user.UserId;
        if (strLoggedInUser == "" || strLoggedInUser == null) strLoggedInUser = "365skool";

        try
        {
            lblMessage.Text = "";

            //Vlidation
            if (txtHeaderCode.Text.Trim() == "")
            {
                _strMsgType = "Info";
                _strAlertMessage = "Enter fee header code!";
                txtHeaderCode.Focus();
            }

            if (objclsCommon.isValidInput(user.RestrictedInputChar, txtHeaderCode.Text.Trim()) == false)
            {
                _strMsgType = "Info";
                _strAlertMessage = "Fee header code contains invalid characters";
                txtHeaderCode.Focus();
                return;
            }

            if (txtHeaderName.Text.Trim() == "")
            {
                _strMsgType = "Info";
                _strAlertMessage = "Enter fee header name!";
                txtHeaderName.Focus();
                return;
            }

            if (objclsCommon.isValidInput(user.RestrictedInputChar, txtHeaderName.Text.Trim()) == false)
            {
                _strAlertMessage = "Fee header name contains invalid characters";
                txtHeaderName.Focus();
                return;
            }

 
            objmaster.SegmentName = ddlSegment.SelectedItem.Text;
            objmaster.HeaderCode = txtHeaderCode.Text.Trim();
            objmaster.HeaderName = txtHeaderName.Text.Trim();
            objmaster.GSTApplicable = chkGSTApplicable.Checked ? 1 : 0;

            if (btnSubmit.Text == "Save")
            {
                objmaster.Mode = "C";
                objmaster.Status = 1;
            }
            else
            {
                objmaster.Mode = "U";
                int.TryParse(hdId.Value, out headerID);
                objmaster.HeaderID = headerID;
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
            dtResult = objclsCommonUtility.SaveFeeHeader(objmaster);

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
            dt = objclsCommonUtility.ShowHeader(master);

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
        int headerID ;

        try
        {
            clearControls();
            lblMessage.Text = string.Empty;

            int.TryParse(grdStyled.DataKeys[grdStyled.SelectedIndex]["id"].ToString(), out headerID);

            if (headerID > 0) 
            {
                hdId.Value = Convert.ToString(headerID);
                txtHeaderCode.Text = Server.HtmlDecode(grdStyled.SelectedRow.Cells[2].Text.Trim());
                txtHeaderName.Text = Server.HtmlDecode(grdStyled.SelectedRow.Cells[3].Text.Trim());
                chkGSTApplicable.Checked = Server.HtmlDecode(grdStyled.SelectedRow.Cells[4].Text.Trim())=="No"? false  :true;
                ddlSegment.SelectedValue = Server.HtmlDecode(grdStyled.SelectedRow.Cells[1].Text.Trim());
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
   
    private void clearControls()
    {
        clsCommon objClsCommon = new clsCommon();
        try
        {
            lblAccountStatus.Text = "";
            hdId.Value = "0";
            txtHeaderCode.Text = "";
            txtHeaderName.Text = "";
            txtSearch.Text = "";

            txtHeaderCode.Enabled = true;
            txtHeaderName.Enabled = true;
            chkActivated.Checked = true;
            chkActivated.Enabled = false;
            chkGSTApplicable.Checked = false;
            ddlSegment.Enabled = true;

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


    private void bindSegment()
    {
        clsCommonUtility objclsCommonUtility = new clsCommonUtility();
        ObjMaster master = new ObjMaster();
        DataTable dtInfo;
        try
        {
            master.Mode = "HEADER_SECTION";
            master.ConnectionString = user.ConnectionString;
            dtInfo = objclsCommonUtility.getCommonMaster(master);

            if (dtInfo != null)
            {
                ddlSegment.DataSource = dtInfo;
                ddlSegment.DataValueField = "SECTION";
                ddlSegment.DataTextField = "SECTION";
                ddlSegment.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            master = null;
            dtInfo = null;
            objclsCommonUtility = null;
        }
    }
  

}
 