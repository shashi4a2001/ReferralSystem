using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Configuration;
using System.Web.UI.WebControls;
using BLCMR;

    public partial class Operation_Err : System.Web.UI.Page
    {
        ObjPortalUser user;

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Text = "We are very sorry for the inconvenience caused to you...";
        Session.Clear();
        Session.RemoveAll();
        Session.Abandon();
        System.Web.Security.FormsAuthentication.SignOut();

        return;
    }
    }
 
 