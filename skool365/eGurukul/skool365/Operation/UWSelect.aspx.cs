/**
 *  @2019 RESERVED PALLAV SOLUTIONS
 *  PURPOSE : ALLOW VALID USER TO LOGIN INTO THE SYSTEM
 *  CHANGE HISTORY :
 * 
 **/


using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using BLCMR;


public partial class Operation_UWSelect : System.Web.UI.Page
{

    ObjPortalUser user;
    string _strAlertMessage = string.Empty;
    string _strMsgType = string.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objCommon = new clsCommon();

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

                    ltrlUserName.Text = user.UserName;
                    lblUserRole.Text = user.UserRole;

                    if (user.ReqChangePassword == "N")
                        changePwd.HRef = "#";

                    if (user.ReqDashobard == "N")
                        dashBoard.HRef = "#";
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
                //Get All UW List
                getUWList();

                ddlUWList.Focus();

                if (ddlUWList.Items.Count == 0) btnLogin.Enabled = false;
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

            objCommon = null;
            objclsErrorLogger = null;
        }
    }

    private void getUWList()
    {
        clsUnderWriter objclsUnderWriter = new clsUnderWriter();
        DataTable dtInfo;
        try
        {
            dtInfo = objclsUnderWriter.getUWList(user.ConnectionString);

            if (dtInfo != null)
            {
                ddlUWList.DataSource = dtInfo;
                ddlUWList.DataValueField = "USER_ID";
                ddlUWList.DataTextField = "UNDERWRITER";
                ddlUWList.DataBind();
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
            objclsUnderWriter = null;
        }

    }


    protected void btnLogin_Click(object sender, EventArgs e)
    {
        clsCommon objCommon = new clsCommon();
        ObjPortalUser user = new ObjPortalUser();
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        int ActingUW_ID;

        try
        {
            UpdatePanel2.Update();
            lblMessage.Text = string.Empty;
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

            int.TryParse(hdUW_ID.Value, out ActingUW_ID);
            user.IsUW = "Y";
            //user.ActingUW = hdUW.Value ;// ddlUWList.SelectedItem.Text.Trim();
            //user.ActingUW_ID =ActingUW_ID;

            int.TryParse(ddlUWList.SelectedValue, out ActingUW_ID);
            user.ActingUW = ddlUWList.SelectedItem.Text.Trim();
            user.ActingUW_ID = ActingUW_ID;

            Session["PortalUserDtl"] = user;

            if (user.UserRole == "Analyst")
            {
                Response.Redirect("ReponseUWRequests.aspx", false);
            }
            else
            {
                Response.Redirect("MngRequests.aspx", false);
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

            user = null;
            objclsErrorLogger = null;
            objCommon = null;
        }
    }
}
 