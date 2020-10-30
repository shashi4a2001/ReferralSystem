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
        if (!Context.User.Identity.IsAuthenticated)
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("SessionExpired.aspx", false);
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
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void UpdateData_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        try
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();


            ObjPortalUser user;
            user = (ObjPortalUser)Session["PortalUserDtl"];


            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                string code = ((Label)GridView1.Rows[i].FindControl("lblCode")).Text.Trim();
                Boolean blSuperAdmin = ((CheckBox)GridView1.Rows[i].FindControl("chkSuperAdmin")).Checked;
                Boolean blMasterAgent = ((CheckBox)GridView1.Rows[i].FindControl("chkMasterAgent")).Checked;
                Boolean blSubAgent = ((CheckBox)GridView1.Rows[i].FindControl("chkSubAgent")).Checked;
                Boolean blFranchisee = ((CheckBox)GridView1.Rows[i].FindControl("chkFranchisee")).Checked;
                Boolean blFranchiseeAgent = ((CheckBox)GridView1.Rows[i].FindControl("chkFranchiseeAgent")).Checked;
                Boolean blIndividual = ((CheckBox)GridView1.Rows[i].FindControl("chkIndividual")).Checked;

                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@ClientTypeCode", code);
                cmd.Parameters.AddWithValue("@CanMakeSuperAdmin", blSuperAdmin);
                cmd.Parameters.AddWithValue("@CanMakeMasterAgent", blMasterAgent);
                cmd.Parameters.AddWithValue("@CanMakeSubAgent", blSubAgent);
                cmd.Parameters.AddWithValue("@CanMakeFranchisee", blFranchisee);
                cmd.Parameters.AddWithValue("@CanMakeFranchiseeAgent", blFranchiseeAgent);
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