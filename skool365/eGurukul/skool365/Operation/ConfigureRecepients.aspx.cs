using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLCMR;
using System.Configuration;
using System.Data;
 
public partial class Operation_ConfigureRecepients : System.Web.UI.Page
{

    ObjPortalUser user;
    string _strAlertMessage = string.Empty;
    string _strMsgType = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objCommon = new clsCommon();
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
                        Session.Abandon();
                        Response.Redirect("login.aspx?Exp=Y", false);
                    }
                }
                else
                {
                    Session.Abandon();
                    Response.Redirect("login.aspx?Exp=Y", false);
                    return;
                }
            }
            else
            {
                Session.Abandon();
                Response.Redirect("login.aspx?Exp=Y", false);
                return;
            }
            //Validate User Authetication Ends

            if (IsPostBack == false)
            {
                //Bind combo box
                BindEMailRecepient();
                //Get Configuration Settings value
                getConfigurationSettings();
            }


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

            objclsErrorLogger = null;
            objCommon = null;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dtInfo;
        clsConfiguration objEC_Settings = new clsConfiguration();
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objclsCommon = new clsCommon();

        try
        {
            if (txtMailGroup.Text.Trim() == "")
            {
                _strMsgType = "Info";
                _strAlertMessage = "Recepient group email ids cannot be blank";
                txtMailGroup.Focus();
                return;
            }

            objEC_Settings.Value = txtMailGroup.Text.Trim();
            objEC_Settings.ID = Convert.ToString(ddlMailGroup.SelectedValue);
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
                objclsCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);

                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }

            dtInfo = null;
            objEC_Settings = null;
            objclsErrorLogger = null;
            objclsCommon = null;
        }
    }

    private void getConfigurationSettings()
    {
        DataTable dtInfo;
        clsConfiguration objEC_Settings = new clsConfiguration();
        string strMessage = string.Empty;

        try
        {
            objEC_Settings.Mode = "EMAIL RECEPIENTS GROUP";
            objEC_Settings.ConnectionString = user.ConnectionString;
            dtInfo = objEC_Settings.getSettingsDetails(objEC_Settings);

            if (dtInfo != null)
            {
                for (int intLoop = 0; intLoop <= dtInfo.Rows.Count - 1; intLoop++)
                {
                    if (dtInfo.Rows[intLoop]["KEY"].ToString().ToUpper() == ddlMailGroup.SelectedItem.Text.ToUpper())
                    {
                        strMessage = dtInfo.Rows[intLoop]["Value"].ToString();
                        txtMailGroup.Text = strMessage;
                        break;
                    }
                }
                ddlMailGroup.Focus();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    private void BindEMailRecepient()
    {
        DataTable dtInfo;
        clsConfiguration objEC_Settings = new clsConfiguration();
        try
        {
            objEC_Settings.Mode = "EMAIL RECEPIENTS GROUP";
            objEC_Settings.ConnectionString = user.ConnectionString;
            dtInfo = objEC_Settings.getSettingsDetails(objEC_Settings);

            if (dtInfo != null)
            {
                ddlMailGroup.DataSource = dtInfo;
                ddlMailGroup.DataValueField = "ID";
                ddlMailGroup.DataTextField = "KEY";
                ddlMailGroup.DataBind();
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objEC_Settings = null;
            dtInfo = null;
        }
    }

    protected void ddlMailGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objclsCommon = new clsCommon();

        try
        {
            lblMessage.Text = "";
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
                objclsCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }

            objclsErrorLogger = null;
            objclsCommon = null;
        }
    }
}