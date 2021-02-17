using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLCMR;
using DLCMR;
using System.Data;
using System.Data.SqlClient;

public partial class Operation_ReportDrillDown : System.Web.UI.Page
{
    ObjPortalUser user;
    protected void Page_Load(object sender, EventArgs e)
    {
        UC_PageLabel.pagelabelProperty = "Drill Down Report";

        if (!Context.User.Identity.IsAuthenticated || Session["PortalUserDtl"] == null)
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
            //BindClientType();
            //txtDateFrom.Text = DateTime.Today.ToString("dd/MM/yyyy");
            //txtDateTo.Text = DateTime.Today.ToString("dd/MM/yyyy");
            ShowReport(user.LogId.ToString());
        }
    }

    //private void BindClientType()
    //{
    //    DLClsGeneric objDLGeneric = new DLClsGeneric();
    //    SqlCommand cmd = new SqlCommand();
    //    cmd.Parameters.AddWithValue("@ClientTypeCode", user.UserRole);
    //    DataTable dt = objDLGeneric.SpDataTable("usp_SelectClientTypePermissionList2", cmd, user.ConnectionString);

    //    DataRow dr = dt.NewRow();
    //    dr["ClientTypeLevel"] = "-1";
    //    dr["ClientTypeCode"] = "-1";
    //    dr["ClientTypeName"] = "All";
    //    dt.Rows.InsertAt(dr, 0);


    //    ddlClientType.DataSource = dt;
    //    ddlClientType.DataTextField = "ClientTypeName";
    //    ddlClientType.DataValueField = "ClientTypeCode";
    //    ddlClientType.DataBind();

    //}

    protected void btnGo_Click(object sender, EventArgs e)
    {
        
    }

    private void ShowReport(string DrillDownClientId)
    {
        string ClientId = user.LogId.ToString();
        //DateTime DateFrom = DateTime.ParseExact(txtDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //DateTime DateTo = DateTime.ParseExact(txtDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

        //ddlClientType
        DLClsGeneric objDLGeneric = new DLClsGeneric();
        SqlCommand cmd = new SqlCommand();
        //cmd.Parameters.AddWithValue("@ClientTypeCode", ddlClientType.SelectedValue);
        cmd.Parameters.AddWithValue("@ParentClientId", ClientId);
        cmd.Parameters.AddWithValue("@ClientId", DrillDownClientId);
        //cmd.Parameters.AddWithValue("@DateFrom", DateFrom.ToString("dd/MMM/yyyy"));
        //cmd.Parameters.AddWithValue("@DateTo", DateTo.ToString("dd/MMM/yyyy"));
        //if (rdbDetail.Checked)
        //{
        //    cmd.Parameters.AddWithValue("@ReportType", "Detail");
        //}
        //else
        //{
        //    cmd.Parameters.AddWithValue("@ReportType", "Summary");
        //}
        DataSet ds = objDLGeneric.SpDataSet("usp_AccountOpenReportDrillDown", cmd, user.ConnectionString);

        grdStyled2.DataSource = ds.Tables[0];
        grdStyled2.DataBind();

        grdStyled.DataSource = ds.Tables[1];
        grdStyled.DataBind();
    }


    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        ExportGridToExcel();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }
    private void ExportGridToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "AccountOpen" + DateTime.Now + ".xls";
        System.IO.StringWriter strwritter = new System.IO.StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        //GridView1.GridLines = GridLines.Both;
        //GridView1.HeaderStyle.Font.Bold = true;
        grdStyled.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();

    }

    protected void grdStyled_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "SelectData")
        {

            string ClientId = e.CommandArgument.ToString();
            ShowReport(ClientId);
            Literal1.Text= " <a href = 'javascript: history.back()'><< Go Back</a>";
        }
    }
}