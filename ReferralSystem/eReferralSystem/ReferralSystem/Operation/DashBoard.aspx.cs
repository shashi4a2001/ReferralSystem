using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLCMR;
using DLCMR;


public partial class Operation_DashBoard : System.Web.UI.Page
{
    ObjPortalUser user;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Context.User.Identity.IsAuthenticated || Session["PortalUserDtl"] == null)
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("SessionExpired.aspx", false);
            return;
        }

        user = (ObjPortalUser)Session["PortalUserDtl"];
        string ClientId = user.LogId.ToString();

        DLClsGeneric objDLGeneric = new DLClsGeneric();
        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@ClientId", ClientId);
        DataSet ds = objDLGeneric.SpDataSet("usp_Dashboard", cmd, user.ConnectionString);

        DataTable dtAccountInfo = ds.Tables[0];
        grdSummary.DataSource = ds.Tables[1];
        grdSummary.DataBind();

        if (dtAccountInfo.Rows.Count > 0)
        {
            DataRow dr = dtAccountInfo.Rows[0];
            ltrlMobileNo.Text = dr["MobileNo"].ToString();
            ltrlLoginId.Text = dr["LoginId"].ToString();
            ltrlClientType.Text = dr["ClientType"].ToString();
            ltrlClientCode.Text = dr["ClientCode"].ToString();
            ltrlClientName.Text = dr["ClientName"].ToString();
            ltrlContactPerson.Text = dr["ContactPerson"].ToString();
            ltrlEmailId.Text = dr["EmailId"].ToString();
            ltrlAddress.Text = dr["Address"].ToString();
            ltrlLandlineNo.Text = dr["LandlineNo"].ToString();
            ltrlBankName.Text = dr["BankName"].ToString();
            ltrlClientNameAsperBank.Text = dr["ClientNameAsPerBank"].ToString();
            ltrlAccountNo .Text = dr["AccountNo"].ToString();
            ltrlIFSCCode.Text = dr["IFSCCode"].ToString();
            ltrlSharingPercnt.Text = dr["SharingPercentage"].ToString();
            ltrlReferredReferralCode.Text = dr["ReferredReferralCode"].ToString();

        }

    }
}