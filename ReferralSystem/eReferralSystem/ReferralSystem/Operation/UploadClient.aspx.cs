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
using ExcelDataReader;

public partial class Operation_UploadClient : System.Web.UI.Page
{
    ObjPortalUser user;
    protected void Page_Load(object sender, EventArgs e)
    {
        UC_PageLabel.pagelabelProperty = "Upload Client";

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

         
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        ViewState["uploadclientdata"] = null;
        grdStyled.DataSource = null;
        grdStyled.DataBind();
        lblMsg.Text = "";

        if (FileUpload1.HasFile)
        {
            string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            string newfileName = DateTime.Now.ToString("MMddyyyyhhmmssfff") + fileName;
            string fileLocation = Server.MapPath("Uploads/" + newfileName);
            FileUpload1.SaveAs(fileLocation);
            DataTable dt = ReadFile(fileLocation);

            DataTable dtCloned = dt.Clone();
            dtCloned.Columns[0].DataType = typeof(System.String);
            foreach (DataRow row in dt.Rows)
            {
                dtCloned.ImportRow(row);
            }

            grdStyled.DataSource = dtCloned;

            grdStyled.DataBind();
            ViewState["uploadclientdata"] = dtCloned;
        }
    }

    private DataTable ReadFile(string FilePath)
    {
        DataTable dt1 = new DataTable();
        DBFToTable db1 = new DBFToTable();
        if (Path.GetExtension(FilePath).ToLower() == ".xls" || Path.GetExtension(FilePath).ToLower() == ".xlsx" || Path.GetExtension(FilePath).ToLower() == ".csv" )
        {
            dt1 = GetDataTableFromExcel(FilePath, "ClientMaster");
        }
        
              
        return dt1;
    }

    public DataTable GetDataTableFromExcel(string filename, string SheetName)
    {
        
            string fileExtension = Path.GetExtension(filename).ToLower();
            var dt = new DataTable();
            DataSet result;
            using (var stream = File.Open(filename, FileMode.Open, FileAccess.Read))
            {
                IExcelDataReader excelReader;
                if (fileExtension == ".xlsx")
                {
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }
                else if (fileExtension == ".csv")
                {
                    excelReader = ExcelReaderFactory.CreateCsvReader(stream);
                }
                else
                {
                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                }

                result = excelReader.AsDataSet(new ExcelDataSetConfiguration() { ConfigureDataTable = __ => new ExcelDataTableConfiguration() { UseHeaderRow = true } });
            }

            if (!string.IsNullOrEmpty(SheetName))
            {
                dt = result.Tables[SheetName];
            }
            else
            {
                foreach (DataTable t in result.Tables)
                {
                    if (dt.Rows.Count == 0)
                    {
                        dt = t.Copy();
                    }
                    else
                    {
                        dt.Merge(t, true, MissingSchemaAction.Ignore);
                    }
                }
            }

            return dt;
        
    }


    protected void btnUpload_Click(object sender, EventArgs e)
    {
        DataTable dt =(DataTable)ViewState["uploadclientdata"];
        if (dt == null)
        {
            lblMsg.Text = "No Record to Upload..";
            lblMsg.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            if (!dt.Columns.Contains("UploadedRemarks"))
            {
                dt.Columns.Add(new DataColumn("UploadedRemarks"));
            }
            foreach (DataRow dr in dt.Rows )
            {
                dr["UploadedRemarks"] = "";
                try
                {
                    DLClsGeneric objDLGeneric = new DLClsGeneric();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@MobileNo", dr["MobileNo"].ToString());
                    cmd.Parameters.AddWithValue("@ClientTypeCode", dr["ClientTypeCode"].ToString());
                    cmd.Parameters.AddWithValue("@ClientCode", dr["ClientCode"].ToString());
                    cmd.Parameters.AddWithValue("@ClientName", dr["ClientName"].ToString());
                    cmd.Parameters.AddWithValue("@ContactPerson", dr["ContactPerson"].ToString());
                    cmd.Parameters.AddWithValue("@EmailId", dr["EmailId"].ToString());
                    cmd.Parameters.AddWithValue("@LandlineNo", dr["LandlineNo"].ToString());
                    cmd.Parameters.AddWithValue("@Address", dr["Address"].ToString());
                    cmd.Parameters.AddWithValue("@LoginId", dr["LoginId"].ToString());
                    cmd.Parameters.AddWithValue("@BankName", dr["BankName"].ToString());
                    cmd.Parameters.AddWithValue("@ClientNameAsPerBank", dr["ClientNameAsPerBank"].ToString());
                    cmd.Parameters.AddWithValue("@AccountNo", dr["AccountNo"].ToString());
                    cmd.Parameters.AddWithValue("@IFSCCode", dr["IFSCCode"].ToString());
                    if (!(dr["ReferralSharingPercentage"].ToString() == "NA" || dr["ReferralSharingPercentage"].ToString() == ""))
                    {
                        cmd.Parameters.AddWithValue("@ReferralSharingPercentage", dr["ReferralSharingPercentage"].ToString());
                    }

                    cmd.Parameters.AddWithValue("@SelfReferralCode", dr["SelfReferralCode"].ToString());
                    cmd.Parameters.AddWithValue("@ReferredReferralCode", dr["ReferredReferralCode"].ToString());

                    if (!(dr["RevenueSharingPercentage"].ToString() == "NA" || dr["RevenueSharingPercentage"].ToString() == ""))
                    {
                        cmd.Parameters.AddWithValue("@RevenueSharingPercentage", dr["RevenueSharingPercentage"].ToString());
                    }

                    cmd.Parameters.AddWithValue("@UserId", user.LogId);
                    cmd.Parameters.AddWithValue("@CreatedBy", dr["CreatedBy"].ToString());
                    cmd.Parameters.AddWithValue("@CreatedDate", dr["CreatedDate"].ToString());

                    DataTable dtResult = objDLGeneric.SpDataTable("usp_UploadClientMaster", cmd, user.ConnectionString);
                    if (dtResult == null)
                    {
                        dr["UploadedRemarks"] = "Problem In Uploading this record";
                    }
                    else
                    {
                        dr["UploadedRemarks"] = dtResult.Rows[0]["Result"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    dr["UploadedRemarks"] = "Error :" + ex.Message.ToString();
                }
            }

        }

        grdStyled.DataSource = dt;
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
        string FileName = "UploadClient" + DateTime.Now + ".xls";
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
}