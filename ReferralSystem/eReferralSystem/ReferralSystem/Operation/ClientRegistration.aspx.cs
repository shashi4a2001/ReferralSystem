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


public partial class Operation_ClientRegistration : System.Web.UI.Page
{
    ObjPortalUser user;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Context.User.Identity.IsAuthenticated)
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("SessionExpired.aspx", false);
            return;
        }

        user = (ObjPortalUser)Session["PortalUserDtl"];
        if (Page.IsPostBack == false)
        {
            BindClientType();
            txtMobileNo.Focus();
        }

       
    }

    private void BindClientType()
    {
        DLClsGeneric objDLGeneric = new DLClsGeneric();
        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@ClientTypeCode", user.UserRole);
        DataTable dt =objDLGeneric.SpDataTable("usp_SelectClientTypePermissionList", cmd, user.ConnectionString);
            
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string MobileNo = txtMobileNo.Text.Trim();
        string ClientType = ddlType.SelectedValue;
        string ClientCode = txtClientCode.Text.Trim();
        string ClientName = txtClientName.Text.Trim();
        string ContactPerson = txtContactPerson.Text.Trim();
        string EmailId = txtEmailId.Text.Trim();
        string LandlineNo = txtLandlineNo.Text.Trim();
        string Address = txtAddress.Text.Trim();
        string LoginId = txtLoginId.Text.Trim();
        string BankName = txtBankName.Text.Trim();
        string ClientNameAsPerBank = txtClientNameAsperBank.Text.Trim();
        string AccountNo = txtAccountNo.Text.Trim();
        string IFSCCode = txtIFSCCode.Text.Trim();
        string SharingPercnt = txtSharingPercnt.Text.Trim();
        string ReferredReferralCode = txtReferredReferralCode.Text.Trim();

        clsCommon objclsCommon = new clsCommon();
        string LoginPassword = objclsCommon.EncryptData(txtMobileNo.Text.Trim()); ;

        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        
       

        DLClsGeneric objDLGeneric = new DLClsGeneric();
        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@MobileNo",MobileNo  );
        cmd.Parameters.AddWithValue("@ClientTypeCode",ClientType  );
        cmd.Parameters.AddWithValue("@ClientCode",ClientCode  );
        cmd.Parameters.AddWithValue("@ClientName",ClientName  );
        cmd.Parameters.AddWithValue("@ContactPerson",ContactPerson  );
        cmd.Parameters.AddWithValue("@EmailId",EmailId  );
        cmd.Parameters.AddWithValue("@LandlineNo",LandlineNo  );
        cmd.Parameters.AddWithValue("@Address",Address  );
        cmd.Parameters.AddWithValue("@LoginId",LoginId  );
        cmd.Parameters.AddWithValue("@LoginPassword",LoginPassword );
        cmd.Parameters.AddWithValue("@BankName",BankName );
        cmd.Parameters.AddWithValue("@ClientNameAsPerBank",ClientNameAsPerBank  );
        cmd.Parameters.AddWithValue("@AccountNo",AccountNo );
        cmd.Parameters.AddWithValue("@IFSCCode",IFSCCode );
        cmd.Parameters.AddWithValue("@SharingPercentage",SharingPercnt );
        cmd.Parameters.AddWithValue("@SelfReferralCode","" );
        cmd.Parameters.AddWithValue("@ReferredReferralCode",txtReferredReferralCode  );
        cmd.Parameters.AddWithValue("@UserId",user.UserId );

        DataTable  dt = objDLGeneric.SpDataTable("usp_InsertClientMaster", cmd, user.ConnectionString);

    }
}