using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using BLCMR;

    public partial class Operation_Err : System.Web.UI.Page
    {
        ObjPortalUser user;
 
        protected void Page_Load(object sender, EventArgs e)
        {
            //base.OnLoad(e);
            clsErrorLogger objclsErrorLogger = new clsErrorLogger();
            clsCommon objclsCommon = new clsCommon();
            STPclsLogin objSTPclsLogin = new STPclsLogin();
            string _strAlertMessage = string.Empty;
            DataTable  dtInfo;
            try
            {

                //Validate User Authetication Starts
                if (Context.User.Identity.IsAuthenticated)
                {
                    //Check User Session and Connection String 
                    if (Session["PortalUserDtl"] != null)
                    {
                        user = (ObjPortalUser)Session["PortalUserDtl"];
                    }
                }

                //Clear Session
                Session.Clear();
                Session.Contents.Clear();
                Session.RemoveAll();
                Session.Abandon();
                //Validate User Authetication Ends

                if (IsPostBack == false)
                {
                    if (user != null)
                    {
                        dtInfo = objSTPclsLogin.LogOut(user.EmailId, user.LogId, user.ConnectionString);
                    }
                    lblMessage.Text = " <b>We are very sorry for the inconvenience caused to you...<br> </b>";
                }

            }
            catch (Exception ex)
            {
                _strAlertMessage = ex.Message;
                objclsErrorLogger.WriteError(System.IO.Path.GetFileName(Request.Path), "", user==null?"" : user.ConnectionString,   ex);
            }
            finally
            {
                objclsErrorLogger = null;
                objclsCommon = null;
                objSTPclsLogin = null;
                dtInfo = null;
            }
        }
    }
 