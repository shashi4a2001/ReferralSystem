using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Threading;
using System.Web.UI.WebControls;
using BLCMR;

    public partial class Operation_CRMOperationArea2 : System.Web.UI.MasterPage
    {
        string M_strcon = string.Empty;
        string M_UserId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            clsCommon objCommon = new clsCommon();
            clsErrorLogger objclsErrorLogger = new clsErrorLogger();
            ObjPortalUser user=new ObjPortalUser ();
            try
            {

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

                        lblUserRole.Text = user.UserRole;
                        ltrlUserName.Text = user.UserName;
                        if (user.ReqChangePassword == "N")
                            changePwd.HRef = "#";

                        if (user.ReqDashobard == "N")
                            dashBoard.HRef = "#";
                        //if (user.SelectUW == "N")
                        //    SelectUW.HRef = "#";

                        if (user.FirstTimeLoggedIn == "Y")
                        {
                            dashBoard.HRef = "#";
                            //SelectUW.HRef = "#";
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
            }
            catch (ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                objclsErrorLogger.WriteError(System.IO.Path.GetFileName(Request.Path), "", user == null ? "" : user.ConnectionString, ex);
                Response.Redirect("~/Operation/Err.aspx");
            }
            finally
            {
                user = null;
                objCommon = null;
                objclsErrorLogger = null;
            }
        }
    }
 