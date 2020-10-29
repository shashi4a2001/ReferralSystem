using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using BLCMR;
using System.Security;
using System.Security.Authentication;
using System.Web.Security;

public partial class Operation_PreviewChartDetails : System.Web.UI.Page
{
 
    string _strMsgType = string.Empty;
    ObjPortalUser user;
    protected void Page_Load(object sender, System.EventArgs e)
    {
        clsCommon objCommon = new clsCommon();
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        string _strAlertMessage = string.Empty;
        try
        {
            //Validate User Authetication Starts
            if (Context.User.Identity.IsAuthenticated)
            {
                //Check User Session and Connection String 
                if (Session["PortalUserDtl"] != null)
                {
                    user = (ObjPortalUser)Session["PortalUserDtl"];

                    //Check Browser Address
                    if (objCommon.GetIPAddress() != user.IPAddress)
                    {
                        Session.Clear();
                        Session.RemoveAll();
                        Session.Abandon();
                        FormsAuthentication.SignOut();
                        Response.Redirect("login.aspx?Exp=Y", false);
                    }
                }
                else
                {
                    Session.Clear();
                    Session.RemoveAll();
                    Session.Abandon();
                    FormsAuthentication.SignOut();
                    Response.Redirect("login.aspx?Exp=Y", false);
                    return;
                }
            }
            else
            {
                Session.Clear();
                Session.RemoveAll();
                Session.Abandon();
                Response.Redirect("login.aspx?Exp=Y", false);
                return;
            }

            if (IsPostBack == false)
            {
                string region = "";
                if (this.Page.Request["region"] != null && this.Page.Request["BrUW"] == null)
                {
                    region = objCommon.DecryptData((string)this.Page.Request["region"]);
                    TATChart.Titles[0].Text = "TAT Chart for " + region;
                    bindTATChart(region);
                    return;
                }

                if (this.Page.Request["region"] != null && this.Page.Request["BrUW"] != null)
                {
                    region = objCommon.DecryptData((string)this.Page.Request["region"]);
                    TATChart.Titles[0].Text = "Underwriter Account Chart for Region " +  region;
                    bindRegionUWChart(region);
                    return;
                }
            }
 
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }
        catch (Exception ex)
        {
            _strMsgType = "Error";
            _strAlertMessage = ex.Message;
            objclsErrorLogger.WriteError(System.IO.Path.GetFileName(Request.Path), "", user == null ? "" : user.ConnectionString, ex);
            Response.Redirect("~/Operation/Err.aspx");
        }
        finally
        {
            objclsErrorLogger = null;
            objCommon = null;
        }
    }


    private void bindTATChart(string strRegion)
    {
        try
        {
            // load the chart with values
            // Here we create a DataTable with two columns.
            DataTable dtDetails = new DataTable();
            DataRow dRow;
            dtDetails.Columns.Add("WeekID", typeof(string));
            dtDetails.Columns.Add("WeekValue", typeof(int));

            int tatValue;
            DataSet dsInfo = new DataSet();
            dsInfo = (DataSet)Session["MIS"];

            dsInfo.Tables[0].DefaultView.RowFilter = "WEEKID='" + strRegion + "'";

            int.TryParse(dsInfo.Tables[0].DefaultView[0]["TATMET"].ToString(), out tatValue);
            dRow = dtDetails.NewRow();
            dRow["WeekID"] = "TATMET";
            dRow["WeekValue"] = tatValue;
            dtDetails.Rows.Add(dRow);

            dRow = dtDetails.NewRow();
            int.TryParse(dsInfo.Tables[0].DefaultView[0]["TATMISSED"].ToString(), out tatValue);
            dRow["WeekID"] = "TATMISSED";
            dRow["WeekValue"] = tatValue;
            dtDetails.Rows.Add(dRow);

            //Set DataTable as data source to Chart
            this.TATChart.DataSource = dtDetails;

            //Mapping a field with x-value of chart
            this.TATChart.Series[0].XValueMember = "WeekID";

            //Mapping a field with y-value of Chart
            this.TATChart.Series[0].YValueMembers = "WeekValue";

            //Bind the DataTable with Chart
            this.TATChart.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
        }
    }

    private void bindRegionUWChart(string strRegion)
    {
        DataTable dtRegionUW = new DataTable();
        DataSet dsInfo = new DataSet();
        try
        {

            dsInfo = (DataSet)Session["MIS"];
            DataTable dtDetails = new DataTable();
            DataRow[] dRow;
            DataRow dRowNew;
            int AccCount;

            dtDetails.Columns.Add("Underwriter", typeof(string));
            dtDetails.Columns.Add("Count", typeof(int));
            dRow = dsInfo.Tables[4].Select("Region='" + strRegion + "'", "Underwriter");

            foreach (DataRow Row in dRow)
            {
                dRowNew = dtDetails.NewRow();
                dRowNew["Underwriter"] = Row["Underwriter"].ToString();
                int.TryParse(Row["Count"].ToString(), out AccCount);
                dRowNew["Count"] = AccCount;
                dtDetails.Rows.Add(dRowNew);
            }

            //Set DataTable as data source to Chart
            this.TATChart.DataSource = dtDetails;

            //Mapping a field with x-value of chart
            this.TATChart.Series[0].XValueMember = "Underwriter";

            //Mapping a field with y-value of Chart
            this.TATChart.Series[0].YValueMembers = "Count";

            //Bind the DataTable with Chart
            this.TATChart.DataBind();


            this.TATChart.ChartAreas[0].IsSameFontSizeForAllAxes = true;
            this.TATChart.ChartAreas[0].AxisX.LabelStyle.Angle = -90;
            this.TATChart.ChartAreas[0].AxisX.LabelStyle.Interval = 1;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dtRegionUW = null;
        }
    }


    #region Web Form Designer generated code
    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: This call is required by the ASP.NET Web Form Designer.
        //
        InitializeComponent();
        base.OnInit(e);
    }

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {

    }
    #endregion
}
 
