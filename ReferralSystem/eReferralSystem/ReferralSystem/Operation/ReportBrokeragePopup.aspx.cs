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

public partial class ReportBrokeragePopup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        try
        {
            ObjPortalUser user;
            user = (ObjPortalUser)Session["PortalUserDtl"];

            string ClientId = Request.Params["p1"].ToString();
            string BrokerageType = Request.Params["p2"].ToString();

            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@ClientId", ClientId);
            cmd.Parameters.AddWithValue("@BrokerageType", BrokerageType);
            DataSet ds = objDLGeneric.SpDataSet("usp_BrokerageDetailPopuUp", cmd, user.ConnectionString);

            grdStyled.DataSource = ds.Tables[0];
            grdStyled.DataBind();

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error Occured.. " + ex.Message.ToString();
            lblMsg.ForeColor = System.Drawing.Color.Red;
        }
      
    }
}