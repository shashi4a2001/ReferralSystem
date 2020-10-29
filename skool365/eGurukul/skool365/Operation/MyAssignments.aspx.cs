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

public partial class Operation_MyAssignments : System.Web.UI.Page
{
    string M_strAlertMessage = string.Empty;
    string _strAlertMessage = string.Empty;
    string _strMsgType = string.Empty;
    ObjPortalUser user;

    string M_strcon = string.Empty;
    string M_UserId = string.Empty;
    string M_AlertMessage = string.Empty;
 
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

               ltrlPageHeader.Text = "";
                string DocType = "a";

                if (Request.QueryString.Count>0 && Request.QueryString["t"] != null)
                {
                    DocType = Request.QueryString["t"].ToString();
                }

                if (DocType == "a")
                {
                    ltrlPageHeader.Text = "Assignments";
                    BindGrid("Assignment");
                }
                else if (DocType == "b")
                {
                    ltrlPageHeader.Text = "eBooks";
                    BindGrid("eBook");
                }
                else
                {
                    BindGrid("99999");
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


    private void BindGrid(string Type)
    {
        clsAssignments objclsAssignments = new clsAssignments();
        EC_Assignments objEC_Assignments = new EC_Assignments();
        DataTable dt;
        try
        {
            objEC_Assignments.ConnectionString = user.ConnectionString;
            objEC_Assignments.RegNO = user.UId;
            dt = objclsAssignments.ShowAssignmentsByType(Type, objEC_Assignments);

            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objEC_Assignments = null;
            dt = null;
            objclsAssignments = null;
        }

    }
    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            //Reference the Repeater Item.
            RepeaterItem item = e.Item;
           
            //Reference the Controls.
            string strStatus = (item.FindControl("lblStatus") as Label).Text;

            if (strStatus == "Pending")
            {
                (item.FindControl("lblStatus") as Label).Font.Bold = true;
                (item.FindControl("lblStatus") as Label).ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                (item.FindControl("lblStatus") as Label).Font.Bold = true;
                (item.FindControl("lblStatus") as Label).ForeColor = System.Drawing.Color.Green;
            }
        }
    }
}
 