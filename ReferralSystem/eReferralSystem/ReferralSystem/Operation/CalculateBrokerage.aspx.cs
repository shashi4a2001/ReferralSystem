using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using BLCMR;
using DLCMR;
using System.IO;
using SocialExplorer.IO.FastDBF;


public partial class Operation_CalculateBrokerage : System.Web.UI.Page
{
    ObjPortalUser user;
    protected void Page_Load(object sender, EventArgs e)
    {
        UC_PageLabel.pagelabelProperty = "Manage Revenue";

        if (!Context.User.Identity.IsAuthenticated || Session["PortalUserDtl"] == null)
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("SessionExpired.aspx", false);
            return;
        }

        user = (ObjPortalUser)Session["PortalUserDtl"];
        if (user.UserRole != "100") //Not Super Admin
        {
            Response.Redirect("UnauthorizeAccess.aspx", false);
            return;
        }

        if (Page.IsPostBack==false)
        {
            BindDropDown();
        }


    }

    private void BindDropDown()
    {
        DataSet ds = new DataSet();
        DLClsGeneric objDLGeneric = new DLClsGeneric();
        SqlCommand cmd = new SqlCommand();
        ds = objDLGeneric.SpDataSet("usp_SelectNationalHeadList", cmd, user.ConnectionString);
        ddlShareTo.DataSource = ds.Tables[0];
        ddlShareTo.DataTextField = "ClientName";
        ddlShareTo.DataValueField = "ClientId";
        ddlShareTo.DataBind();

    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        try
        {


            string userId = user.LogId.ToString();
            DateTime DateFrom = DateTime.ParseExact(txtDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime DateTo = DateTime.ParseExact(txtDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string BrokerageType = "";
            string BrokerageAmount = "";
            BrokerageAmount = txtAmount.Text.ToString().Trim();

            if (ddlRevenueType.SelectedValue.ToString() == "1")
            {
                BrokerageType = "101";
            }
            else if (ddlRevenueType.SelectedValue.ToString() == "2")
            {
                BrokerageType = "102";
            }


            if (BrokerageType != "101" && BrokerageType != "102")
            {
                lblMsg.Text = "Please select Sharing Type";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }
            if (ddlShareTo.SelectedValue.ToString() == "0")
            {
                lblMsg.Text = "Please select Share To";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (BrokerageAmount == "")
            {
                lblMsg.Text = "Please enter Amount To Share";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }
            if (BrokerageAmount != "")
            {
                double n;
                bool isNumeric = int.TryParse("123", out n);

                if (isNumeric == false)
                {
                    lblMsg.Text = "Please enter valid Amount To Share";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    return;
                }

            }



            if (DateTo > DateFrom)
            {
                lblMsg.Text = "Invalid FromDate/ToDate";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }




            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@ClientId", ddlShareTo.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@DateFrom", DateFrom.ToString("dd/MMM/yyyy"));
            cmd.Parameters.AddWithValue("@DateTo", DateTo.ToString("dd/MMM/yyyy"));
            cmd.Parameters.AddWithValue("@BrokerageType", BrokerageType);
            cmd.Parameters.AddWithValue("@BrokerageAmount", BrokerageAmount);
            cmd.Parameters.AddWithValue("@UserId", userId);
            objDLGeneric.SpExecuteNonQuery("usp_CalculateBrokerage", cmd, user.ConnectionString);

            lblMsg.Text = "Amount Processed Successfully at :"+ DateTime.Now.ToString();
            lblMsg.ForeColor = System.Drawing.Color.Green;

            BindGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message.ToString();
            lblMsg.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void ddlRevenueType_SelectedIndexChanged(object sender, EventArgs e)
    {
        

    }
    private void BindGrid()
    {
        DataSet ds = new DataSet();

        if (ddlRevenueType.Text != "--Select--")
        {

            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@BrokerageType", ddlRevenueType.SelectedValue.ToString());
            ds = objDLGeneric.SpDataSet("usp_SelectBrokerageSummary", cmd, user.ConnectionString);

        }

        grdStyled.DataSource = ds.Tables[0];
        grdStyled.DataBind();
    }
}