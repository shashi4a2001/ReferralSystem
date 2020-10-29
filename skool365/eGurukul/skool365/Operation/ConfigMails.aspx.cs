using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLCMR;
using System.Configuration;
using System.Data;

public partial class Operation_ConfigMails : System.Web.UI.Page
{
    ObjPortalUser user;
    string _strAlertMessage = string.Empty;
    string _strMsgType = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objClsCommon = new clsCommon();
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
                    if (objClsCommon.GetIPAddress() != user.IPAddress)
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
                //Bind combo box
                BindMailFormat();
                //Get Configuration Settings value
                getConfigurationSettings();
            }

            //Check Page Accessibility
            if (user != null)
            {
                if (objClsCommon.checkPageAccessiblity(user.EmailId, System.IO.Path.GetFileName(Request.Path), user.ConnectionString) != "Y")
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
                objClsCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);

                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }
            objclsErrorLogger = null;
            objClsCommon = null;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dtInfo;
        clsConfiguration objEC_Settings = new clsConfiguration();
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objClsCommon = new clsCommon();

        try
        {
            if (txtMailBody.Text.Trim() == "")
            {
                _strMsgType = "Info";
                _strAlertMessage = "Mail Format cannot be blank";
                return;
            }

            objEC_Settings.Value = txtMailBody.Text.Trim();
            objEC_Settings.ID = Convert.ToString(ddlMailFormat.SelectedValue);
            objEC_Settings.Mode = "U";
            objEC_Settings.LoggedInUser = user.UserId;
            objEC_Settings.ConnectionString = user.ConnectionString;
            dtInfo = objEC_Settings.InsertDeleteUpdateSettings(objEC_Settings);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    if ((dtInfo.Columns.Contains("Error") == true) && (dtInfo.Rows[0]["Error"].ToString().Trim() != ""))  //Return Either Error or Db Information
                    {
                        _strMsgType = "Error_DB";
                        _strAlertMessage = dtInfo.Rows[0]["Error"].ToString();
                        return;
                    }

                    _strMsgType = "Success";
                    _strAlertMessage = dtInfo.Rows[0]["MSG"].ToString();
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
            //Alert Message
            if (_strAlertMessage != string.Empty && _strAlertMessage.Trim() != "")
            {
                lblMessage.Text = _strAlertMessage;
                objClsCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);

                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }
            objclsErrorLogger = null;
            dtInfo = null;
            objEC_Settings = null;
            objClsCommon = null;
        }
    }
    private void getConfigurationSettings()
    {
        DataTable dtInfo;
        clsConfiguration objEC_Settings = new clsConfiguration();
        try
        {
            objEC_Settings.Mode = "M";
            objEC_Settings.ConnectionString = user.ConnectionString;
            dtInfo = objEC_Settings.getSettingsDetails(objEC_Settings);

            if (dtInfo != null)
            {
                for (int intLoop = 0; intLoop <= dtInfo.Rows.Count - 1; intLoop++)
                {
                    if (dtInfo.Rows[intLoop]["KEY"].ToString().ToUpper() == ddlMailFormat.SelectedItem.Text.ToUpper())
                    {
                        txtMailBody.Text = dtInfo.Rows[intLoop]["Value"].ToString(); 
                        break;
                    }
                }
                ddlMailFormat.Focus();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void BindMailFormat()
    {
        DataTable dt;
        clsConfiguration objEC_Settings = new clsConfiguration();
        try
        {
            objEC_Settings.Mode = "M";
            objEC_Settings.ConnectionString = user.ConnectionString;

            dt = objEC_Settings.getSettingsDetails(objEC_Settings);
            if (dt != null)
            {
                ddlMailFormat.DataSource = dt;
                ddlMailFormat.DataValueField = "ID";
                ddlMailFormat.DataTextField = "KEY";
                ddlMailFormat.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objEC_Settings = null;
            dt = null;
        }
    }
    protected void ddlMailFormat_SelectedIndexChanged(object sender, EventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objClsCommon = new clsCommon();
        string _strAlertMessage = string.Empty;

        try
        {
            lblMessage.Text = "";
            txtMailBody.Text = "";
            getConfigurationSettings();
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
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }
            objclsErrorLogger = null;
            objClsCommon = null;
        }
    }
}
 