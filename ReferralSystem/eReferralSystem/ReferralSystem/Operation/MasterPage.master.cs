using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLCMR;

public partial class Operation_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Validate User Authetication Starts
        if (Context.User.Identity.IsAuthenticated)
        {
            //Check User Session and Connection String 
            if (Session["PortalUserDtl"] != null)
            {
                ObjPortalUser user;
                user = (ObjPortalUser)Session["PortalUserDtl"];

                ltrlUserName.Text = user.UserName;

                if (user.UserRole == "100") //Super Admin
                {
                    ltrlMenu1.Text = @"<a class=""navbar-brand"" href=""RoleManagement.aspx"" style=""color:#f5f5f5;"">Role Management</a>&nbsp;&nbsp;";
                    ltrlMenu2.Text = @"<a class=""navbar-brand"" href=""ClientRegistration.aspx"" style=""color:#f5f5f5;"">User Registration</a>";
                }

            }
        }
        else
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            //Response.Redirect("login.aspx?Exp=Y", false);
            return;
        }
    }
}
