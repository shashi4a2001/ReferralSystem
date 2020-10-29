using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using BLCMR;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading;
 
    public partial class Operation_AdmissionFeeDetails : System.Web.UI.Page
    {
        string M_strcon = string.Empty;
        string M_UserId = string.Empty;
        string M_strAlertMessage = string.Empty;
        ObjPortalUser user;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Check User Session and Connection String 
                if (Session["PortalUserDtl"] != null)
                {
                    user = (ObjPortalUser)Session["PortalUserDtl"];
                    M_UserId = user.UserId;
                    M_strcon = user.ConnectionString;
                }
                else
                {
                    Session.Abandon();
                    Response.Redirect("login.aspx?Exp=Y");
                }

                if (!IsPostBack)
                {
                    hdPrvSemFee.Value = "";

                    txtRegistrationNo.Enabled = true;
                    txtRegistrationNo.ReadOnly = false;
                    btnCancel.Enabled = true;
                    if (Request.QueryString.Keys.Count != 0)
                    {
                        txtRegistrationNo.Text = Convert.ToString(Request.QueryString["RegNo"]);
                        Binddata();
                        if (hdPrvSemFee.Value == "" || hdPrvSemFee.Value == "0")
                        {
                            hdPrvSemFee.Value = txtStudentSemFee.Text;
                        }
                        btnShow.Enabled = false;
                        btnCancel.Enabled = false;
                        txtRegistrationNo.Enabled = false;
                    }
                    txtRegistrationNo.Attributes.Add("onkeypress", "javascript:return IsNumeric()");
                    txtStudentSemFee.Attributes.Add("onkeypress", "javascript:return IsNumeric()");
                    txtStudentAdmnFee.Attributes.Add("onkeypress", "javascript:return IsNumeric()");
                }
            }
            catch (Exception ex)
            {
                M_strAlertMessage = ex.Message;
            }
            finally
            {
                //Alert Message
                if (M_strAlertMessage != string.Empty && M_strAlertMessage.Trim() != "")
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + M_strAlertMessage + "');</script>", false);
                }
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtRegistrationNo.Text = "";
            lblStudentName.Text = "";
            lblAcedemicYear.Text = "";
            lblFinancialYear.Text = "";


            dtAnnualFee.DataSource = null;
            dtAnnualFee.DataBind();

            dtSemeterFee.DataSource = null;
            dtSemeterFee.DataBind();

            txtCollegeAnnualFee.Text = "";
            txtCollegeSemesterFee.Text = "";
            txtStudentAdmnFee.Text = "";
            txtStudentSemFee.Text = "";
            txtTotalAnnualFee.Text = "";
            txtTotalSemesterFee.Text = "";
            tblDetail.Visible = false;
            btnShow.Enabled = true;
        }
        protected void btnShow_Click(object sender, EventArgs e)
        {
            if (txtRegistrationNo.Text.Trim() != "")
            {
                //Bind Grid
                Binddata();
                if (lblMessage.Text.Trim() != "") return;
                if (lblMessage.Text.Trim() != "") return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Enter Registration No');</script>", false);
                lblMessage.Text = "Enter Registration No";
                return;
            }
        }

        public void Binddata()
        {
            DataSet ds = new DataSet();
            clsFeeMaster objclasFeeMaster= new clsFeeMaster();
            ObjFeeMaster FeeMaster = new ObjFeeMaster();
            int RegNo;
            try
            {
                int.TryParse(txtRegistrationNo.Text, out RegNo);
                FeeMaster.RegistrationNo = RegNo;
                ds = objclasFeeMaster.StudentAdmissionFeeDetails(M_strcon, FeeMaster);
                dtAnnualFee.DataSource = ds.Tables[0];
                dtSemeterFee.DataSource = ds.Tables[1];

                //Student Name
                if (ds.Tables[3] != null)
                {
                    lblStudentName.Text = ds.Tables[3].Rows[0]["StudentName"].ToString();
                    //Academic Year
                    lblAcedemicYear.Text = ds.Tables[3].Rows[0]["AcademicYear"].ToString() + " / " + ds.Tables[3].Rows[0]["AcademicCourse"].ToString();
                    //Financial Year
                    lblFinancialYear.Text = ds.Tables[3].Rows[0]["FinancialYear"].ToString() + " / " + ds.Tables[3].Rows[0]["FinancialCourse"].ToString();

                }
                // Here data is fetched from the database through stored procedure(insert_reg).
                dtSemeterFee.DataBind();
                dtAnnualFee.DataBind();

                // Here data is bind to the control and show on page
                btnSubmit.Enabled = true;
                lblMessage.Text = "";

                tblDetail.Visible = false;
              
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[1].Rows.Count == 0)
                {
                    tblDetail.Visible = false;
                    btnSubmit.Enabled = false;
                    lblMessage.Text = "Annual Fee for Admission/Semester not defined.";
                    M_strAlertMessage = lblMessage.Text;
                    return;
                }
                tblDetail.Visible = true;

                //Disable Save button if receipt is issued for both Semester and Receipt
                if (ds.Tables[3].Rows[0]["AdmnFeeReceipt"].ToString() == "Y")
                {
                    dtAnnualFee.Enabled = false;
                    txtTotalAnnualFee.Enabled = false;
                    txtStudentAdmnFee.Enabled = false;
                }
                if (ds.Tables[3].Rows[0]["SemesterReceipt"].ToString() == "Y")
                {
                    dtSemeterFee.Enabled = false;
                    txtStudentSemFee.Enabled = false;
                    txtTotalSemesterFee.Enabled = false;
                }
                if (ds.Tables[3].Rows[0]["AdmnFeeReceipt"].ToString() == "Y" && ds.Tables[3].Rows[0]["SemesterReceipt"].ToString() == "Y")
                {
                    dtAnnualFee.Enabled = false;
                    dtSemeterFee.Enabled = false;
                    btnSubmit.Enabled = false;
                    txtTotalAnnualFee.Enabled = false;
                    txtStudentAdmnFee.Enabled = false;
                    txtStudentSemFee.Enabled = false;
                    txtTotalSemesterFee.Enabled = false;

                    lblMessage.Text = "Modification can not be done as the receipt has been issued for both Admission and Semester";
                    M_strAlertMessage = lblMessage.Text;
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //Alert Message
                if (M_strAlertMessage != string.Empty && M_strAlertMessage.Trim() != "")
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + M_strAlertMessage + "');</script>", false);
                }
            }

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder qryBuilder = new System.Text.StringBuilder();
            clsAdmission objclsAdmission = new clsAdmission();
            ObjFeeMaster feeMaster = new ObjFeeMaster();
            clsCheckList objclsCheckList = new clsCheckList();
            DataTable  dtCheckList ;

            int intRes;
            int RegNo;
            int intSectionID;
            double dblAmt;
            double dblAdmnFeeAmt = 0;
            double dblStudentAdmnFee = 0;
            double dblStudentSemFee = 0;
            //double dblStudentHostelFee = 0;
            string strloggedInUser = string.Empty;
            try
            {

                //Check for Student CheckList Upload
                //Check for Student CheckList Upload
                int.TryParse(txtRegistrationNo.Text, out RegNo);
                objclsCheckList.RegNo = RegNo;
                objclsCheckList.ConnectionString = M_strcon;
                dtCheckList = objclsCheckList.checkStudentCheckListUploadStatus(objclsCheckList);
                if (dtCheckList != null && dtCheckList.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + dtCheckList.Rows[0][0].ToString() + "');</script>", false);
                    Response.Redirect(dtCheckList.Rows[0][1].ToString());
                }

                //Validation
                lblMessage.Text = "";
                if (txtCollegeSemesterFee.Text.Trim() == "" || txtStudentSemFee.Text.Trim() == "")// || txtCollegeHostelFee.Text.Trim() == "")
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Enter Student Admission/Semester Fee";
                    M_strAlertMessage = lblMessage.Text;
                    return;
                }

                if (Session["userObject"] != null)
                {
                    ObjUser user;
                    user = (ObjUser)Session["userObject"];
                    strloggedInUser = user.UserId;
                }


                //For Admission
                if (dtAnnualFee.Enabled)
                {
                    foreach (DataListItem dli in dtAnnualFee.Items)
                    {
                        if (dli.ItemType == ListItemType.Item || dli.ItemType == ListItemType.AlternatingItem)
                        {
                            TextBox foo = dli.FindControl("txtAdmnAmount") as TextBox;
                            dblAdmnFeeAmt = Convert.ToDouble(((TextBox)dli.FindControl("txtAdmnFeeAmount")).Text);
                            dblAmt = Convert.ToDouble(((TextBox)dli.FindControl("txtAdmnAmount")).Text);
                            dblStudentAdmnFee = dblStudentAdmnFee + dblAdmnFeeAmt;
                            intSectionID = Convert.ToInt16(dtAnnualFee.DataKeys[dli.ItemIndex]);

                            qryBuilder.Append("IF EXISTS(SELECT 1 FROM dbo.Admission_Fee_Detail (NOLOCK) WHERE RegistrationNumber=" + txtRegistrationNo.Text + " and SectionID=" + intSectionID + "    ) ");
                            qryBuilder.Append("\r\n");
                            qryBuilder.Append("BEGIN ");
                            qryBuilder.Append("\r\n");
                            qryBuilder.Append("UPDATE Admission_Fee_Detail SET [Amount]=" + dblAdmnFeeAmt + " ,[ModifiedBy]='" + strloggedInUser + "',[ModifiedDate]  =dbo.FnGetIndianTime() ");
                            qryBuilder.Append("\r\n");
                            qryBuilder.Append("WHERE  RegistrationNumber=" + txtRegistrationNo.Text + " and SectionID=" + intSectionID + " ");
                            qryBuilder.Append("\r\n");
                            qryBuilder.Append("END ");
                            qryBuilder.Append("\r\n");
                            qryBuilder.Append("ELSE ");
                            qryBuilder.Append("\r\n");
                            qryBuilder.Append("BEGIN ");
                            qryBuilder.Append("\r\n");
                            qryBuilder.Append("INSERT INTO Admission_Fee_Detail ( ");
                            qryBuilder.Append("\r\n");
                            qryBuilder.Append("[RegistrationNumber]  ,[SectionID] ,[Amount] ,[CreatedBy]  ,[CreatedDate]  ) ");
                            qryBuilder.Append("\r\n");
                            qryBuilder.Append("Select " + txtRegistrationNo.Text + "," + intSectionID + "," + dblAdmnFeeAmt + ",'" + strloggedInUser + "',dbo.FnGetIndianTime() ");
                            qryBuilder.Append("\r\n");
                            qryBuilder.Append("END ");
                            qryBuilder.Append("\r\n");
                        }
                    }

                    lblMessage.Text = "";
                    if (dblStudentAdmnFee != Convert.ToDouble(txtStudentAdmnFee.Text))
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = "Please check the total of Student Annaul Fee";
                        M_strAlertMessage = lblMessage.Text;
                        return;
                    }
                }
                //semester fee
                if (dtSemeterFee.Enabled)
                {
                    if (hdPrvSemFee.Value != "")
                    {
                        if (Convert.ToInt32(hdPrvSemFee.Value) != Convert.ToInt32(txtStudentSemFee.Text))
                        {
                            qryBuilder.Append("\r\n");
                            qryBuilder.Append(" delete from Student_Installment_Detail where Segment='Semester' and registrationnumber=" + txtRegistrationNo.Text);
                            qryBuilder.Append("\r\n");
                        }
                    }

                    foreach (DataListItem dli in dtSemeterFee.Items)
                    {
                        if (dli.ItemType == ListItemType.Item || dli.ItemType == ListItemType.AlternatingItem)
                        {
                            TextBox foo = dli.FindControl("txtSemAmount") as TextBox;

                            dblAmt = Convert.ToDouble(((TextBox)dli.FindControl("txtSemAmount")).Text);
                            dblAdmnFeeAmt = Convert.ToDouble(((TextBox)dli.FindControl("txtSemFeeAmount")).Text);
                            dblStudentSemFee = dblStudentSemFee + dblAdmnFeeAmt;
                            intSectionID = Convert.ToInt16(dtSemeterFee.DataKeys[dli.ItemIndex]);

                            qryBuilder.Append("\r\n");
                            qryBuilder.Append("IF EXISTS(SELECT 1 FROM Admission_Fee_Detail(NOLOCK) WHERE RegistrationNumber=" + txtRegistrationNo.Text + " and SectionID=" + intSectionID + "    ) ");
                            qryBuilder.Append("\r\n");
                            qryBuilder.Append("BEGIN ");
                            qryBuilder.Append("\r\n");
                            qryBuilder.Append("UPDATE Admission_Fee_Detail SET [Amount]=" + dblAdmnFeeAmt + " ,[ModifiedBy]='" + strloggedInUser + "',[ModifiedDate]  =dbo.FnGetIndianTime() ");
                            qryBuilder.Append("\r\n");
                            qryBuilder.Append("WHERE RegistrationNumber=" + txtRegistrationNo.Text + " and SectionID=" + intSectionID + " ");
                            qryBuilder.Append("\r\n");
                            qryBuilder.Append("END ");
                            qryBuilder.Append("\r\n");
                            qryBuilder.Append("ELSE ");
                            qryBuilder.Append("\r\n");
                            qryBuilder.Append("BEGIN ");
                            qryBuilder.Append("\r\n");
                            qryBuilder.Append("INSERT INTO Admission_Fee_Detail ( ");
                            qryBuilder.Append("\r\n");
                            qryBuilder.Append("[RegistrationNumber]  ,[SectionID] ,[Amount] ,[CreatedBy]  ,[CreatedDate]  ) ");
                            qryBuilder.Append("\r\n");
                            qryBuilder.Append("Select " + txtRegistrationNo.Text + "," + intSectionID + "," + dblAdmnFeeAmt + ",'" + strloggedInUser + "',dbo.FnGetIndianTime()  ");
                            qryBuilder.Append("\r\n");
                            qryBuilder.Append("END ");
                            qryBuilder.Append("\r\n");

                        }
                    }
                    //Semester Fee
                    if (dblStudentSemFee != Convert.ToDouble(txtStudentSemFee.Text))
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = "Please check the total of Student Semester Fee";
                        M_strAlertMessage = lblMessage.Text;
                        return;
                    }
                }
                if (qryBuilder.ToString().Length > 0)
                {
                    qryBuilder.Append("update ADMISSION SET ADMISSIONSTAGE ='STAGE4'   WHERE REGISTRATIONNUMBER=" + txtRegistrationNo.Text + "");
                    feeMaster.strInsertQuery = qryBuilder.ToString();
                    intRes = objclsAdmission.SaveAnnulFeeDetails(M_strcon, feeMaster);

                    if (intRes > 0)
                    {
                        Response.Redirect("InstallmentDtl.aspx?RegNo=" + txtRegistrationNo.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                M_strAlertMessage = ex.Message;
            }
            finally
            {
                qryBuilder = null;
                objclsAdmission = null;
                feeMaster = null;
                objclsCheckList = null;
                dtCheckList  = null;

                //Alert Message
                if (M_strAlertMessage != string.Empty && M_strAlertMessage.Trim() != "")
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + M_strAlertMessage + "');</script>", false);
                }
            }
        }
        protected void dtAnnualFee_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            try
            {
                Double dblCollegeAdmnFee = 0;
                Double dblStudentAdmnFee = 0;

                if (txtCollegeAnnualFee.Text.Trim() != "") dblCollegeAdmnFee = Convert.ToDouble(txtCollegeAnnualFee.Text);
                if (txtStudentAdmnFee.Text.Trim() != "") dblStudentAdmnFee = Convert.ToDouble(txtStudentAdmnFee.Text);

                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    TextBox objtxtAdmnAmount = (TextBox)e.Item.FindControl("txtAdmnAmount");
                    objtxtAdmnAmount.Attributes.Add("onkeypress", "javascript:return IsNumeric('d')");

                    TextBox objtxtAdmnFeeAmount = (TextBox)e.Item.FindControl("txtAdmnFeeAmount");
                    objtxtAdmnFeeAmount.Attributes.Add("onkeypress", "javascript:return IsNumeric('d')");

                    objtxtAdmnFeeAmount.Attributes.Add("onblur", "javascript:return AdmissionFee(this)");
                    objtxtAdmnFeeAmount.Attributes.Add("onfocus", "javascript:return PrvAmount(this)");


                    dblCollegeAdmnFee = dblCollegeAdmnFee + Convert.ToDouble(objtxtAdmnAmount.Text);
                    dblStudentAdmnFee = dblStudentAdmnFee + Convert.ToDouble(objtxtAdmnFeeAmount.Text);
                }

                txtCollegeAnnualFee.Text = Convert.ToString(dblCollegeAdmnFee);
                txtStudentAdmnFee.Text = Convert.ToString(dblStudentAdmnFee);
                txtTotalAnnualFee.Text = Convert.ToString(dblStudentAdmnFee);
            }
            catch (Exception ex)
            {
                M_strAlertMessage = ex.Message;
            }
            finally
            {
                //Alert Message
                if (M_strAlertMessage != string.Empty && M_strAlertMessage.Trim() != "")
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + M_strAlertMessage + "');</script>", false);
                }
            }
        }
        protected void dtSemeterFee_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            try
            {
                Double dblCollegeSemFee = 0;
                Double dblStudentSemFee = 0;

                if (txtCollegeSemesterFee.Text.Trim() != "") dblCollegeSemFee = Convert.ToDouble(txtCollegeSemesterFee.Text);
                if (txtStudentSemFee.Text.Trim() != "") dblStudentSemFee = Convert.ToDouble(txtStudentSemFee.Text);

                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    TextBox objtxtSemAmount = (TextBox)e.Item.FindControl("txtSemAmount");
                    objtxtSemAmount.Attributes.Add("onkeypress", "javascript:return IsNumeric('d')");


                    TextBox objtxtSemFeeAmount = (TextBox)e.Item.FindControl("txtSemFeeAmount");
                    objtxtSemFeeAmount.Attributes.Add("onkeypress", "javascript:return IsNumeric('d')");
                    objtxtSemFeeAmount.Attributes.Add("onblur", "javascript:return SemesterFee(this)");
                    objtxtSemFeeAmount.Attributes.Add("onfocus", "javascript:return PrvAmount(this)");

                    dblCollegeSemFee = dblCollegeSemFee + Convert.ToDouble(objtxtSemAmount.Text);
                    dblStudentSemFee = dblStudentSemFee + Convert.ToDouble(objtxtSemFeeAmount.Text);
                }

                txtCollegeSemesterFee.Text = Convert.ToString(dblCollegeSemFee);
                txtStudentSemFee.Text = Convert.ToString(dblStudentSemFee);
                txtTotalSemesterFee.Text = Convert.ToString(dblStudentSemFee);
            }
            catch (Exception ex)
            {
                M_strAlertMessage = ex.Message;
            }
            finally
            {
                //Alert Message
                if (M_strAlertMessage != string.Empty && M_strAlertMessage.Trim() != "")
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + M_strAlertMessage + "');</script>", false);
                }
            }
        }
 
}
 