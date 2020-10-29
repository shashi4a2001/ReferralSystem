using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using BLCMR;
using System.Data.OleDb;
using System.Drawing;


public partial class Operation_MIS : System.Web.UI.Page
{
    ObjPortalUser user;
    string _strAlertMessage = string.Empty;
    string _strMsgType = string.Empty;
    public static string WM_UW { get; set; }
    public static string WM_Con_Str { get; set; }
    int _mSelectedChart;

    protected void Page_Load(object sender, EventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objCommon = new clsCommon();
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
                        Response.Redirect("login.aspx?Exp=Y", false);
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
                _mSelectedChart = 0;
                //Get All UW List
                getUWList();
            }

            //Bind Charts
            getMISData(0);

            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            //Check Page Accessibility
            if (user != null)
            {
                if (objCommon.checkPageAccessiblity(user.EmailId, System.IO.Path.GetFileName(Request.Path), user.ConnectionString) != "Y")
                    Response.Redirect("~/Operation/AccessDenied.aspx", false);
            }
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
            //Alert Message
            if (_strAlertMessage != string.Empty && _strAlertMessage.Trim() != "")
            {
                lblMessage.Text = _strAlertMessage;
                objCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }
            objclsErrorLogger = null;
            objCommon = null;
        }
    }

    private void getUWList()
    {
        clsUnderWriter objclsUnderWriter = new clsUnderWriter();
        DataTable dtInfo;
        try
        {
            dtInfo = objclsUnderWriter.getUWList(user.ConnectionString);

            if (dtInfo != null)
            {
                ddlUWList.DataSource = dtInfo;
                ddlUWList.DataValueField = "USER_ID";
                ddlUWList.DataTextField = "UNDERWRITER";
                ddlUWList.DataBind();

                ddlUWList.Items.Insert(0, "All");

                //Set Acting UW
                if (user.IsUW != "Y")
                {
                    ddlUWList.SelectedValue = user.UId.ToString();
                }
                else
                {
                    ddlUWList.SelectedValue = user.ActingUW_ID.ToString();
                    ddlUWList.Enabled = false;
                }


                WM_UW = user.ActingUW;
                WM_Con_Str = user.ConnectionString;

                UpdatePanel2.Update();
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            UpdatePanel2.Update();
            dtInfo = null;
            objclsUnderWriter = null;
        }

    }

    private void getMISData(int chartType)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        objUnderWriter UnderWriter = new objUnderWriter();
        clsUnderWriter objclsUnderWriter = new clsUnderWriter();
        clsCommon objclsCommon = new clsCommon();
        DataSet dsInfo;
        chartType = 0;
        string _strAlertMessage = string.Empty;
        try
        {
            UnderWriter.ConnectionString = user.ConnectionString;

            if (ddlUWList.SelectedValue == "All")
            {
                UnderWriter.UnderWriterID = "0";
            }
            else
            {
                UnderWriter.UnderWriterID = ddlUWList.SelectedValue;
            }
            dsInfo = objclsUnderWriter.getMISReportDetails(UnderWriter);
            Session["MIS"] = dsInfo;

            //Load Chart Data
            loadCharts(chartType);
            //UpdatePanel2.Update();
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
            //Alert Message
            if (_strAlertMessage != string.Empty && _strAlertMessage.Trim() != "")
            {
                lblMessage.Text = _strAlertMessage;
                objclsCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);

                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }
            objclsErrorLogger = null;
            objclsCommon = null;
        }
    }

    protected void ddlUWList_SelectedIndexChanged(object sender, EventArgs e)
    {
        //getMISData(0);
    }


    #region "Charts"
    private void loadCharts(int chartType)
    {
        DataSet dsInfo = new DataSet();
        DataTable dtStacked = new DataTable();
        try
        {
            ////Clear Charts
            //OverallSummaryChart.DataSource=null;
            //SummaryByProcessingChart.DataSource = null;
            //SummaryByTATChart.DataSource = null;
            //TATChart.DataSource = null;
            //BranchUWChart.DataSource = null;


            dsInfo = (DataSet)Session["MIS"];
            if (dsInfo != null)
            {

                if ((chartType == 0 || chartType == 2 ) && dsInfo.Tables[1].Rows.Count >0)
                {
                    //"OVERALL SUMMARY" STARTS
                    // Add series to the chart
                    Series series = OverallSummaryChart.Series.Add("My series");

                    // Set series visual attributes
                    series.ChartType = SeriesChartType.Pie;
                    series.ShadowOffset = 2;
                    series.Font = new Font("Times New Format", 7f);
                    series.BorderColor = Color.DarkGray;
                    //series.CustomAttributes = "LabelStyle=Outside";

                    // Set series and legend tooltips
                    series.ToolTip = "#LEGENDTEXT: #VAL ";
                    series.LegendToolTip = "#PERCENT";
                    series.PostBackValue = "#INDEX";
                    series.LegendPostBackValue = "#INDEX";
                    OverallSummaryChart.Titles[0].Text = "OVERALL SUMMARY";

                    // Populate series data
                    int yValue;
                    int[] yValues = new int[dsInfo.Tables[1].Rows.Count];
                    string[] xValues = new string[dsInfo.Tables[1].Rows.Count];

                    for (int i = 0; i < dsInfo.Tables[1].Rows.Count; i++)
                    {
                        int.TryParse(dsInfo.Tables[1].Rows[i][1].ToString(), out yValue);
                        yValues[i] = yValue;
                        xValues[i] = dsInfo.Tables[1].Rows[i][0].ToString();
                    }
                    series.Points.DataBindY(yValues);

                    for (int pointIndex = 0; pointIndex < series.Points.Count; pointIndex++)
                    {
                        series.Points[pointIndex].LegendText = xValues[pointIndex];
                    }

                    if (!this.IsPostBack)
                    {
                        series.Points[0].CustomProperties += "Exploded=true";
                    }
                }
                //"OVERALL SUMMARY" ENDS

                //"SUMMARY BY PROCESSING STATUST" STARTS
                if ((chartType == 0 || chartType == 3) && dsInfo.Tables[2].Rows.Count > 0)
                {
                    // Add series to the chart
                    Series seriesSummaryByProcessingChart = SummaryByProcessingChart.Series.Add("SUMMARY BY PROCESSING STATUS");

                    // Set series visual attributes
                    seriesSummaryByProcessingChart.ChartType = SeriesChartType.Pie;
                    seriesSummaryByProcessingChart.ShadowOffset = 2;
                    seriesSummaryByProcessingChart.Font = new Font("Times New Format", 7f);
                    seriesSummaryByProcessingChart.BorderColor = Color.DarkGray;
                    //series.CustomAttributes = "LabelStyle=Outside";

                    // Set series and legend tooltips
                    seriesSummaryByProcessingChart.ToolTip = "#LEGENDTEXT: #VAL ";
                    seriesSummaryByProcessingChart.LegendToolTip = "#PERCENT";
                    seriesSummaryByProcessingChart.PostBackValue = "#INDEX";
                    seriesSummaryByProcessingChart.LegendPostBackValue = "#INDEX";
                    SummaryByProcessingChart.Titles[0].Text = "SUMMARY BY PROCESSING STATUS";

                    int yValueTATChart;
                    int[] yValuesTATChart = new int[dsInfo.Tables[2].Rows.Count];
                    string[] xValuesTATChart = new string[dsInfo.Tables[2].Rows.Count];

                    for (int i = 0; i < dsInfo.Tables[2].Rows.Count; i++)
                    {
                        int.TryParse(dsInfo.Tables[2].Rows[i][1].ToString(), out yValueTATChart);
                        yValuesTATChart[i] = yValueTATChart;
                        xValuesTATChart[i] = dsInfo.Tables[2].Rows[i][0].ToString();
                    }

                    seriesSummaryByProcessingChart.Points.DataBindY(yValuesTATChart);

                    for (int pointIndex = 0; pointIndex < seriesSummaryByProcessingChart.Points.Count; pointIndex++)
                    {
                        seriesSummaryByProcessingChart.Points[pointIndex].LegendText = xValuesTATChart[pointIndex];
                    }

                    if (!this.IsPostBack)
                    {
                        seriesSummaryByProcessingChart.Points[0].CustomProperties += "Exploded=true";
                    }
                }
                //"SUMMARY BY PROCESSING STATUS" ENDS


                //"SUMMARY BY TAT" STARTS
                if ((chartType == 0 || chartType == 4) && dsInfo.Tables[3].Rows.Count > 0)
                {
                    // Add series to the chart
                    Series seriesSummaryByTATChart = SummaryByTATChart.Series.Add("SUMMARY BY TAT");

                    // Set series visual attributes
                    seriesSummaryByTATChart.ChartType = SeriesChartType.Pie;
                    seriesSummaryByTATChart.ShadowOffset = 2;
                    seriesSummaryByTATChart.Font = new Font("Times New Format", 7f);
                    seriesSummaryByTATChart.BorderColor = Color.DarkGray;
 
                    // Set series and legend tooltips
                    seriesSummaryByTATChart.ToolTip = "#LEGENDTEXT: #VAL ";
                    seriesSummaryByTATChart.LegendToolTip = "#PERCENT";
                    seriesSummaryByTATChart.PostBackValue = "#INDEX";
                    seriesSummaryByTATChart.LegendPostBackValue = "#INDEX";
                    SummaryByTATChart.Titles[0].Text = "SUMMARY BY TAT";

                    int yValueSummaryByTATChart;
                    int[] yValuesSummaryByTATChart = new int[dsInfo.Tables[3].Rows.Count];
                    string[] xValuesSummaryByTATChart = new string[dsInfo.Tables[3].Rows.Count];

                    for (int i = 0; i < dsInfo.Tables[3].Rows.Count; i++)
                    {
                        int.TryParse(dsInfo.Tables[3].Rows[i][1].ToString(), out yValueSummaryByTATChart);
                        yValuesSummaryByTATChart[i] = yValueSummaryByTATChart;
                        xValuesSummaryByTATChart[i] = dsInfo.Tables[3].Rows[i][0].ToString();
                    }

                    seriesSummaryByTATChart.Points.DataBindY(yValuesSummaryByTATChart);

                    for (int pointIndex = 0; pointIndex < seriesSummaryByTATChart.Points.Count; pointIndex++)
                    {
                        seriesSummaryByTATChart.Points[pointIndex].LegendText = xValuesSummaryByTATChart[pointIndex];
                    }

                    if (!this.IsPostBack)
                    {
                        seriesSummaryByTATChart.Points[0].CustomProperties += "Exploded=true";
                    }
                }
                //"SUMMARY BY TAT" ENDS

                //CHART 2
                if ((chartType == 0 || chartType == 1) && dsInfo.Tables[0].Rows.Count > 0)
                {
                    // load the chart with values

                    //Set DataTable as data source to Chart
                    this.TATChart.DataSource = dsInfo.Tables[0];
                    //Mapping a field with x-value of chart
                    this.TATChart.Series[0].XValueMember = "WEEKID";
                    //Mapping a field with y-value of Chart
                    this.TATChart.Series[0].YValueMembers = "ACCTSPROCESSED";
                    //Bind the DataTable with Chart
                    this.TATChart.DataBind();
                    UpdateAttrib();

                    // Specify the position of the legend.
                    TATChart.ChartAreas[0].AxisX.Enabled = AxisEnabled.True;
                }

                //Branch Underwriter Chart
                if ((chartType == 0 || chartType == 5) && dsInfo.Tables[5].Rows.Count > 0)
                {

                    //Set DataTable as data source to Chart
                    this.BranchUWChart.DataSource = dsInfo.Tables[5];
                    //Mapping a field with x-value of chart
                    this.BranchUWChart.Series[0].XValueMember = "REGION";
                    //Mapping a field with y-value of Chart
                    this.BranchUWChart.Series[0].YValueMembers = "COUNT";
                    //Bind the DataTable with Chart
                    this.BranchUWChart.DataBind();

                    this.BranchUWChart.ChartAreas[0].IsSameFontSizeForAllAxes = true;
                    this.BranchUWChart.ChartAreas[0].AxisX.LabelStyle.Angle = -90;
                    this.BranchUWChart.ChartAreas[0].AxisX.LabelStyle.Interval = 1;
                    UpdateAttribBranchUWChart();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dtStacked = null;
            dsInfo = null;
        }
    }

    protected void OverallSummaryChart_Click(object sender, ImageMapEventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objCommon = new clsCommon();
        try
        {
           // getMISData(2);

            int pointIndex = int.Parse(e.PostBackValue);
            Series series = OverallSummaryChart.Series["My series"];
            if (pointIndex >= 0 && pointIndex < series.Points.Count)
            {
                series.Points[pointIndex].CustomProperties += "Exploded=true";
            }
        }
        catch (Exception ex)
        {
            _strMsgType = "Error";
            _strAlertMessage = ex.Message;
            objclsErrorLogger.WriteError(System.IO.Path.GetFileName(Request.Path), "", user == null ? "" : user.ConnectionString, ex);
        }
        finally
        {
            objclsErrorLogger = null;
            objCommon = null;
        }
    }

    protected void SummaryByProcessingChart_Click(object sender, ImageMapEventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objCommon = new clsCommon();
        try
        {
          //  getMISData(3);
            int pointIndex = int.Parse(e.PostBackValue);
            Series series = SummaryByProcessingChart.Series["SUMMARY BY PROCESSING STATUS"];
            if (pointIndex >= 0 && pointIndex < series.Points.Count)
            {
                series.Points[pointIndex].CustomProperties += "Exploded=true";
            }
        }
        catch (Exception ex)
        {
            _strMsgType = "Error";
            _strAlertMessage = ex.Message;
            objclsErrorLogger.WriteError(System.IO.Path.GetFileName(Request.Path), "", user == null ? "" : user.ConnectionString, ex);
        }
        finally
        {
            objclsErrorLogger = null;
            objCommon = null;
        }
    }

    protected void SummaryByTATChart_Click(object sender, ImageMapEventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objCommon = new clsCommon();
        try
        {
            //getMISData(4);
            int pointIndex = int.Parse(e.PostBackValue);
            Series series = SummaryByTATChart.Series["SUMMARY BY TAT"];
            if (pointIndex >= 0 && pointIndex < series.Points.Count)
            {
                series.Points[pointIndex].CustomProperties += "Exploded=true";
            }
        }
        catch (Exception ex)
        {
            _strMsgType = "Error";
            _strAlertMessage = ex.Message;
            objclsErrorLogger.WriteError(System.IO.Path.GetFileName(Request.Path), "", user == null ? "" : user.ConnectionString, ex);
        }
        finally
        {
            objclsErrorLogger = null;
            objCommon = null;
        }
    }

    public void UpdateAttrib()
    {
            clsCommon objclsCommon = new clsCommon();
            try
            {
                // Set series tooltips
                foreach (Series series in TATChart.Series)
                {
                    for (int pointIndex = 0; pointIndex < series.Points.Count; pointIndex++)
                    {
                        string toolTip = "";
                        toolTip = @"<img src=PreviewChart.aspx?region=" + objclsCommon.EncryptData(series.Points[pointIndex].AxisLabel) + " />";
                        series.Points[pointIndex].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip + "');\" onmouseout=\"DisplayTooltip('');\"";
                        series.Points[pointIndex].Url = "PreviewChartDetails.aspx?region=" + objclsCommon.EncryptData(series.Points[pointIndex].AxisLabel);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objclsCommon = null;
            }
    }

    public void UpdateAttribBranchUWChart()
    {
            clsCommon objclsCommon = new clsCommon();
            try
            {
                // Set series tooltips
                foreach (Series series in BranchUWChart.Series)
                {
                    for (int pointIndex = 0; pointIndex < series.Points.Count; pointIndex++)
                    {
                        string toolTip = "";
                        toolTip = @"<img src=PreviewChart.aspx?BrUW=1&region=" + objclsCommon.EncryptData(series.Points[pointIndex].AxisLabel) + " />";
                        series.Points[pointIndex].MapAreaAttributes = "onmouseover=\"DisplayTooltipBrUW('" + toolTip + "');\" onmouseout=\"DisplayTooltipBrUW('');\"";
                        series.Points[pointIndex].Url = "PreviewChartDetails.aspx?BrUW=1&region=" + objclsCommon.EncryptData(series.Points[pointIndex].AxisLabel);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objclsCommon = null;
            }
    }
    

    #endregion
}