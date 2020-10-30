using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Security;
using System.Security.Authentication;
using BLCMR;

public partial class Operation_LogOut : System.Web.UI.Page
{
    ObjPortalUser user;
    string _strAlertMessage = string.Empty;
    string _strMsgType = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Text = "You have successfully Logged Out.";
        Session.Clear();
        Session.RemoveAll();
        Session.Abandon();
        FormsAuthentication.SignOut();
    }
}