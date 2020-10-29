using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLCMR;

    public partial class Operation_CRMOperationArea : System.Web.UI.MasterPage
    {
        string M_strcon = string.Empty;
        string M_UserId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Check User Session and Connection String 
                if (Session["PortalUserDtl"] != null )//&& string.IsNullOrEmpty(Session["STPUserName"] as string))
                {
                    ObjPortalUser user;
                    user = (ObjPortalUser)Session["PortalUserDtl"];
                    lblUserRole.Text = user.UserRole;
                    ltrlUserName.Text = user.UserName;

                    //User Log In Information
                    lblLastLogIn.Text = " " + user.LastLogInTime;
                    lblLastPasswordChange.Text = " " + user.LastPasswordChangeTime;
                    lblWorkLocation.Text = user.Orig_Code;
                    lblOrganization.Text = user.Orig_Name;


                    if (user.ReqChangePassword == "N")
                        changePwd.HRef = "#";

                    if (user.ReqDashobard == "N")
                        dashBoard.HRef = "#";

                    //if (user.SelectUW == "N")
                    //    SelectUW.HRef = "#";

                }
                else
                {
                    Session.Abandon();
                    Response.Redirect("login.aspx");
                    return;
                }

            }
            catch { }
        }
    }
 