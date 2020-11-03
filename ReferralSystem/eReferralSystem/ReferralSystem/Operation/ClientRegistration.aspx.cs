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

        DataRow dr = dt.NewRow();
        dr["ClientTypeLevel"] = "-1";
        dr["ClientTypeCode"] = "-1";
        dr["ClientTypeName"] = "--Select--";
        dt.Rows.InsertAt(dr, 0);


        ddlClientType.DataSource = dt;
        ddlClientType.DataTextField = "ClientTypeName";
        ddlClientType.DataValueField = "ClientTypeCode";
        ddlClientType.DataBind();

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string MobileNo = txtMobileNo.Text.Trim();
            string ClientType = ddlClientType.SelectedValue;
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

            if (MobileNo == "")
            {
                ShowErrorMsg("Please Enter Mobile No...");
                txtMobileNo.Focus();
                return;
            }
            if (ClientType == "")
            {
                ShowErrorMsg("Please Select Client Type..");
                ddlClientType.Focus();
                return;
            }
            if (ClientCode == "")
            {
                ShowErrorMsg("Please Enter Client Code..");
                txtClientCode.Focus();
                return;
            }
            if (ClientName == "")
            {
                ShowErrorMsg("Please Enter Client Name..");
                txtClientName.Focus();
                return;
            }
            if (ContactPerson  == "")
            {
                ShowErrorMsg("Please Enter Contact Person..");
                txtContactPerson.Focus();
                return;
            }
            if (EmailId == "")
            {
                ShowErrorMsg("Please Enter Email Id..");
                txtEmailId.Focus();
                return;
            }
            if (LandlineNo == "")
            {
                ShowErrorMsg("Please Enter Landline No..");
                txtLandlineNo.Focus();
                return;
            }
            if (Address == "")
            {
                ShowErrorMsg("Please Enter Address..");
                txtAddress.Focus();
                return;
            }
            if (LoginId == "")
            {
                ShowErrorMsg("Please Enter LoginId..");
                txtLoginId.Focus();
                return;
            }
            if (BankName == "")
            {
                ShowErrorMsg("Please Enter Bank Name..");
                txtBankName.Focus();
                return;
            }
            if (ClientNameAsPerBank == "")
            {
                ShowErrorMsg("Please Enter Client Name as Per Bank..");
                txtClientNameAsperBank.Focus();
                return;
            }
            if (AccountNo == "")
            {
                ShowErrorMsg("Please Enter Account No..");
                txtAccountNo.Focus();
                return;
            }
            if (IFSCCode == "")
            {
                ShowErrorMsg("Please Enter IFSC Code..");
                txtIFSCCode.Focus();
                return;
            }
            if (SharingPercnt == "")
            {
                ShowErrorMsg("Please Enter Sharing Percnt..");
                txtSharingPercnt.Focus();
                return;
            }
            if (ReferredReferralCode == "")
            {
                ShowErrorMsg("Please Enter Referred Referral Code..");
                txtReferredReferralCode.Focus();
                return;
            }

            clsCommon objclsCommon = new clsCommon();
            string LoginPassword = objclsCommon.EncryptData(txtMobileNo.Text.Trim()); ;

            clsErrorLogger objclsErrorLogger = new clsErrorLogger();



            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@MobileNo", MobileNo);
            cmd.Parameters.AddWithValue("@ClientTypeCode", ClientType);
            cmd.Parameters.AddWithValue("@ClientCode", ClientCode);
            cmd.Parameters.AddWithValue("@ClientName", ClientName);
            cmd.Parameters.AddWithValue("@ContactPerson", ContactPerson);
            cmd.Parameters.AddWithValue("@EmailId", EmailId);
            cmd.Parameters.AddWithValue("@LandlineNo", LandlineNo);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@LoginId", LoginId);
            cmd.Parameters.AddWithValue("@LoginPassword", LoginPassword);
            cmd.Parameters.AddWithValue("@BankName", BankName);
            cmd.Parameters.AddWithValue("@ClientNameAsPerBank", ClientNameAsPerBank);
            cmd.Parameters.AddWithValue("@AccountNo", AccountNo);
            cmd.Parameters.AddWithValue("@IFSCCode", IFSCCode);
            cmd.Parameters.AddWithValue("@SharingPercentage", SharingPercnt);
            cmd.Parameters.AddWithValue("@SelfReferralCode", "");
            cmd.Parameters.AddWithValue("@ReferredReferralCode", ReferredReferralCode);
            cmd.Parameters.AddWithValue("@UserId", user.LogId);

            DataTable dt = objDLGeneric.SpDataTable("usp_InsertClientMaster", cmd, user.ConnectionString);

            if (dt!=null  && dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == "Success")
                {
                    lblmsg.Text = "Client Created Successfully...";
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    ClearControls();
                }
                else
                {
                    lblmsg.Text = dt.Rows[0][0].ToString();
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                }

            }
            else
            {
                lblmsg.Text = "Oops! some problem in creating client..";
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
           
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message.ToString();
            lblmsg.ForeColor = System.Drawing.Color.Red;
        }
    }

    private void ShowErrorMsg(string msg)
    {
        lblmsg.Text = msg;
        lblmsg.ForeColor = System.Drawing.Color.Red;
    }
    private void ClearControls()
    {
        txtMobileNo.Text = "";
        txtLoginId.Text = "";
        ddlClientType.SelectedIndex = 0;
        txtClientCode.Text = "";
        txtClientName.Text = "";
        txtContactPerson.Text = "";
        txtEmailId.Text = "";
        txtAddress.Text = "";
        txtLandlineNo.Text = "";
        txtBankName.Text = "";
        txtClientNameAsperBank.Text = "";
        txtAccountNo.Text = "";
        txtIFSCCode.Text = "";
        txtSharingPercnt.Text = "";
        txtReferredReferralCode.Text = "";
    }

    protected void btnFetchDetail0_Click(object sender, EventArgs e)
    {
        if (txtLoginId.Text.Trim() == "")
        {
            lblmsgCheckAvailability.Text = "LoginId Required..";
            lblmsgCheckAvailability.ForeColor = System.Drawing.Color.Red;
            return;
        }
        DLClsGeneric objDLGeneric = new DLClsGeneric();
        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@LoginId", txtLoginId.Text.Trim());        

        string Result  = objDLGeneric.SpExecuteScalar("usp_IsExistsLoginId", cmd, user.ConnectionString);
        if (Result.StartsWith("Success"))
        {
            lblmsgCheckAvailability.Text = Result.Replace("Success - ", "");
            lblmsgCheckAvailability.ForeColor = System.Drawing.Color.Green;
        }
        else
        {
            lblmsgCheckAvailability.Text = Result;
            lblmsgCheckAvailability.ForeColor = System.Drawing.Color.Red;
            txtLoginId.Text = "";
        }

    }
}