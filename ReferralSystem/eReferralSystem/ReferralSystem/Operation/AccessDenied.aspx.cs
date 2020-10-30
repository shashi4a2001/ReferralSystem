using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Security;
using System.Security.Authentication;
using BLCMR;

public partial class Operation_AccessDenied : System.Web.UI.Page
{
    ObjPortalUser user;
    string _strAlertMessage = string.Empty;
  

    protected void Page_Load(object sender, EventArgs e)
    {
        //base.OnLoad(e);
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objclsCommon = new clsCommon();
        //STPclsLogin objSTPclsLogin = new STPclsLogin();
        string _strAlertMessage = string.Empty;
        DataTable dtInfo;
        try
        {
            //Check User Session and Connection String 
            if (Session["PortalUserDtl"] != null)
            {
                user = (ObjPortalUser)Session["PortalUserDtl"];
            }

            if (IsPostBack == false)
            {
                //if (user != null)
                //{
                //    dtInfo = objSTPclsLogin.LogOut(user.EmailId, user.LogId, user.ConnectionString);
                //}

                Session.Clear();
                Session.RemoveAll();
                Session.Abandon();
                FormsAuthentication.SignOut();
            }
        }
        catch (Exception ex)
        {
            _strAlertMessage = ex.Message;
        }
        finally
        {
            //Alert Message
            if (_strAlertMessage != string.Empty && _strAlertMessage.Trim() != "")
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }
            objclsErrorLogger = null;
            objclsCommon = null;
            //objSTPclsLogin = null;
            dtInfo = null;
        }
    }
}