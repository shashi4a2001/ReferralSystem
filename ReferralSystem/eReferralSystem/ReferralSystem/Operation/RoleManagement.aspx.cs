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

public partial class RoleManagement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UC_PageLabel.pagelabelProperty = "Role Management";

        if (!Context.User.Identity.IsAuthenticated || Session["PortalUserDtl"]==null)
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("SessionExpired.aspx", false);
            return;
        }

        ObjPortalUser user;
        user = (ObjPortalUser)Session["PortalUserDtl"];
        if (user.UserRole != "100") //Not Super Admin
        {
            Response.Redirect("UnauthorizeAccess.aspx", false);
            return;
        }

        if (Page.IsPostBack == false)
        {
            BindGrid();
        }

    }

    private void BindGrid()
    {
        DLClsGeneric objDLGeneric = new DLClsGeneric();
        SqlCommand cmd = new SqlCommand();

        ObjPortalUser user;
        user = (ObjPortalUser)Session["PortalUserDtl"];
        DataTable dt = objDLGeneric.SpDataTable("usp_SelectClientTypeMaster", cmd, user.ConnectionString);
        grdStyled.DataSource = dt;
        grdStyled.DataBind();
    }

    protected void UpdateData_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        try
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();


            ObjPortalUser user;
            user = (ObjPortalUser)Session["PortalUserDtl"];


            for (int i = 0; i < grdStyled.Rows.Count; i++)
            {
                string code = ((Label)grdStyled.Rows[i].FindControl("lblCode")).Text.Trim();
                Boolean blSuperAdmin = ((CheckBox)grdStyled.Rows[i].FindControl("chkSuperAdmin")).Checked;
                Boolean blNationalHead = ((CheckBox)grdStyled.Rows[i].FindControl("chkNationalHead")).Checked;
                Boolean blRegionalHead = ((CheckBox)grdStyled.Rows[i].FindControl("chkRegionalHead")).Checked;
                Boolean blStateFranchisee = ((CheckBox)grdStyled.Rows[i].FindControl("chkStateFranchisee")).Checked;
                Boolean blDistrictFranchisee = ((CheckBox)grdStyled.Rows[i].FindControl("chkDistrictFranchisee")).Checked;
                Boolean blIndividualAgent = ((CheckBox)grdStyled.Rows[i].FindControl("chkIndividualAgent")).Checked;
                Boolean blIndividual = ((CheckBox)grdStyled.Rows[i].FindControl("chkIndividual")).Checked;

                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@ClientTypeCode", code);
                cmd.Parameters.AddWithValue("@CanMakeSuperAdmin", blSuperAdmin);
                cmd.Parameters.AddWithValue("@CanMakeNationalHead", blNationalHead);
                cmd.Parameters.AddWithValue("@CanMakeRegionalHead", blRegionalHead);
                cmd.Parameters.AddWithValue("@CanMakeStateFranchisee", blStateFranchisee);
                cmd.Parameters.AddWithValue("@CanMakeDistrictFranchisee", blDistrictFranchisee);
                cmd.Parameters.AddWithValue("@CanMakeIndividualAgent", blIndividualAgent);
                cmd.Parameters.AddWithValue("@CanMakeIndividual", blIndividual);
                cmd.Parameters.AddWithValue("@UserId", user.UserId);

                objDLGeneric.SpExecuteNonQuery("usp_UpdateClientTypeMaster", cmd, user.ConnectionString);

            }

            BindGrid();

            lblmsg.Text = "Updated Successfully...";
            lblmsg.ForeColor = System.Drawing.Color.Green;
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message.ToString();
            lblmsg.ForeColor = System.Drawing.Color.Red;

        }
       

    }
}