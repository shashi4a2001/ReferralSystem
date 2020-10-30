using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLCMR;
using System.Net;
using System.Web.Security;
using System.Security;
using System.Security.Authentication;
using System.Configuration;



public partial class Operation_CRMOperationArea : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            clsCommon objCommon = new clsCommon();
            clsErrorLogger objclsErrorLogger = new clsErrorLogger();
            try
            {
                lblWorkLocation.Text = objCommon.DecryptData(ConfigurationManager.AppSettings["WorkLocation"].ToString());
                lblOrganization.Text = objCommon.DecryptData(ConfigurationManager.AppSettings["Organization"].ToString());
                lblPhone.Text = objCommon.DecryptData(ConfigurationManager.AppSettings["Phone"].ToString());
                lblEmail.Text = objCommon.DecryptData(ConfigurationManager.AppSettings["Email"].ToString());
                Page.Title = "@All Right Reserved." + lblOrganization.Text;
            }
            catch (Exception ex)
            {
                objclsErrorLogger.WriteError(System.IO.Path.GetFileName(Request.Path), "", "", ex);
            }
            finally
            {
                objCommon = null;
                objclsErrorLogger = null;
            }
        }
    }
 