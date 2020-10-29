using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Services;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using BLCMR;
using System.Text.RegularExpressions;
using System.Security;
using System.Security.Authentication;


public partial class Operation_InstallmentDtl : System.Web.UI.Page
    {

        string M_strcon = string.Empty;
        string M_strAlertMessage = string.Empty;
        string M_UserId = string.Empty;
        string _strMsgType = string.Empty;
        ObjPortalUser user;
        protected void Page_Load(object sender, EventArgs e)
        {
            clsCommon objclsCommon = new clsCommon();
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

                    txtRegistrationNo.Attributes.Add("onkeypress", "javascript:return IsNumeric()");
                    txtInstallment.Attributes.Add("onkeypress", "javascript:return IsNumeric()");
                    if (Request.QueryString.Keys.Count != 0)
                    {
                        txtRegistrationNo.Text = Convert.ToString(Request.QueryString["RegNo"]);
                        txtRegistrationNo.Enabled = false;
                        btnGetDetails.Enabled = false;
                        btnCancel.Enabled = false;
                        Binddata();
                    }

                    if (txtInstallment.Text == string.Empty || grdInstallment.Rows.Count <= 0)
                    {
                        btnSubmit.Visible = false;
                    }
                    else
                    {
                        btnSubmit.Visible = true;

                        if (grdInstallment.Rows.Count > 0)
                        {
                            rowTotal.Visible = true;
                        }
                        else
                        {
                            rowTotal.Visible = false;
                        }
                    }

                    Page.Title = user.Orig_Name + ": Installment Plan Definition";
                }
            }
            catch (Exception ex)
            {
                M_strAlertMessage = ex.Message;
            }
            finally
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>SemesterInstallment();</script>", false);
                //Alert Message
                if (M_strAlertMessage != string.Empty && M_strAlertMessage.Trim() != "")
                {
                    lblMessage.Text = M_strAlertMessage;
                    objclsCommon.setLabelMessage(lblMessage, _strMsgType);
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + M_strAlertMessage + "');</script>", false);
                }

                objclsCommon = null;
            }
        }

        protected void btnGetDetails_Click(object sender, EventArgs e)
        {
            clsCommon objclsCommon = new clsCommon();
            try
            {
                if (txtRegistrationNo.Text.Trim() != "")
                {
                    //Bind Grid
                    Binddata();

                    if (lblMessage.Text.Trim() != "") return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Enter Registration No');</script>", false);
                    M_strAlertMessage = "Enter Registration No";
                    return;
                }
            }
            catch (Exception ex)
            {
                M_strAlertMessage = ex.Message;
            }
            finally
            {
                lblMessage.Text = M_strAlertMessage;
                objclsCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + M_strAlertMessage + "');</script>", false);
                objclsCommon = null;
            }
        }

        public void Binddata()
        {
            DataTable dtSemester = new DataTable();
            DataSet ds = new DataSet();
            double dblStudentFeeAmount = 0;
            double dblCollegeFeeAmount = 0;
            Int32 intIns = 0;
            int RegNo;

            clsFeeMaster objclasFeeMaster = new clsFeeMaster();
            ObjFeeMaster FeeMaster = new ObjFeeMaster();
            clsCommon objclsCommon = new clsCommon();
            try
            {
                int.TryParse(txtRegistrationNo.Text, out RegNo);
                FeeMaster.RegistrationNo = RegNo;
                ds = objclasFeeMaster.StudentSemesterInstallmentDetails(M_strcon, FeeMaster);

                lblStudentName.Text = "";
                lblAcedemicYear.Text = "";
                lblFinancialYear.Text = "";
                tblDetail.Visible = false;

                // Here data is bind to the control and show on page
                if (ds.Tables[0].Rows.Count > 0)
                {
                    tblDetail.Visible = true;
                    for (intIns = 0; intIns <= ds.Tables[0].Rows.Count - 1; intIns++)
                    {
                        dblCollegeFeeAmount = dblCollegeFeeAmount + Convert.ToDouble(ds.Tables[0].Rows[intIns]["AnnualFee"]);
                        dblStudentFeeAmount = dblStudentFeeAmount + Convert.ToDouble(ds.Tables[0].Rows[intIns]["FeeAmount"]);
                    }
                    lblStudentSemFee.Text = Convert.ToString(dblStudentFeeAmount);
                    lblCollegeSemesterFee.Text = Convert.ToString(dblCollegeFeeAmount);
 
                    //Student Name
                    lblStudentName.Text = ds.Tables[0].Rows[0]["StudentName"].ToString();
                    //Academic Year
                    lblAcedemicYear.Text = ds.Tables[0].Rows[0]["AcademicYear"].ToString() + " / " + ds.Tables[0].Rows[0]["AcademicCourse"].ToString();
                    //Financial Year
                    lblFinancialYear.Text = ds.Tables[0].Rows[0]["FinancialYear"].ToString() + " / " + ds.Tables[0].Rows[0]["FinancialCourse"].ToString();
                }

                btnShow.Enabled = true;

                if (ds.Tables[0].Rows.Count == 0)
                {
                    btnSubmit.Enabled = false;
                    btnShow.Enabled = false;
                    txtInstallment.Enabled = false;
                    lblMessage.Text = "No Information found for given Registration No.";
                    M_strAlertMessage = lblMessage.Text;
                    return;
                }
                else
                {
                    txtInstallment.Enabled = true;
                    txtRegistrationNo.Enabled = false;
                    btnGetDetails.Enabled = false;
                }

                if (ds.Tables[0].Rows[0]["InstallmentNo"].ToString() == "0") return;

                grdInstallment.DataSource = ds.Tables[0];
                txtInstallment.Text = ds.Tables[0].Rows.Count.ToString();

                btnSubmit.Enabled = true;

                if (ds.Tables[0].Rows[0]["IsReceiptIssued"].ToString() == "N")
                {
                    hdReceiptIssued.Value = "N";
                    btnShow.Enabled = true ;
                    txtInstallment.Enabled = true;

                    rowTotal.Visible = true;
                    btnSubmit.Visible = true;
                }
                else
                {
                    hdReceiptIssued.Value = "Y";
                    btnShow.Enabled = false;
                    txtInstallment.Enabled = false;
                }

                lblMessage.Text = "";
                // Here data is fetched from the database through stored procedure(insert_reg).
               grdInstallment.DataBind();

                lblCollegeSemesterFee.Text = ds.Tables[0].Rows[0]["AnnualFee"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                  dtSemester = null;
                  ds = null;
                  objclasFeeMaster = null;
                  FeeMaster = null;
                //Alert Message
                  lblMessage.Text = M_strAlertMessage;
                  objclsCommon.setLabelMessage(lblMessage, _strMsgType);
                  ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);

                if (M_strAlertMessage != string.Empty && M_strAlertMessage.Trim() != "")
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + M_strAlertMessage + "');</script>", false);
                }

                objclsCommon = null;
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder qryBuilder = new System.Text.StringBuilder();
            clsAdmission objclsAdmission = new clsAdmission();
            ObjFeeMaster feeMaster = new ObjFeeMaster();
            clsCommon objclsCommon = new clsCommon();
            int intRes;
            int intIntNo;
            double dblTotalAmt = 0;
            double dblStudentSemFee = 0;
            string strDueDate;
            Label objlblInstNo;
            TextBox objtxtDate;
            TextBox objtxtSemFeeAmount;
            try
            {

                //Validation
                lblMessage.Text = "";
                if (lblCollegeSemesterFee.Text.Trim() == "" || lblStudentSemFee.Text.Trim() == "")
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Enter Student Installment Amount";
                    M_strAlertMessage = lblMessage.Text;
                    return;
                }
                //semester fee


                if (hdReceiptIssued.Value == "N")
                {
                    qryBuilder.Append("DELETE FROM Student_Installment_Detail WHERE RegistrationNumber=" + txtRegistrationNo.Text + " AND SEGMENT='Semester' ");
                    qryBuilder.Append("\r\n");
                }

                foreach (GridViewRow rows in grdInstallment.Rows)
                {
                    if (rows.RowType == DataControlRowType.DataRow)
                    {

                        objlblInstNo = (Label)rows.FindControl("lblInstNo");
                        objtxtSemFeeAmount = (TextBox)rows.FindControl("txtSemAmount");
                        objtxtDate = (TextBox)rows.FindControl("txtDate");

                        objtxtDate.BackColor = System.Drawing.Color.White;
                        objtxtSemFeeAmount.BackColor = System.Drawing.Color.White;
                        
                        //<Start>Validation
                        if (objtxtDate.Text.Trim() == "")
                        {
                            lblMessage.Visible = true;
                            lblMessage.Text = "Enter Installment Due Date";
                            M_strAlertMessage = lblMessage.Text;
                            objtxtDate.BackColor = System.Drawing.Color.LightYellow ;
                            objtxtDate.Focus();
                            return;
                        }

                        if (objtxtDate.Text.Trim().Length != 10)
                        {
                            lblMessage.Visible = true;
                            lblMessage.Text = "Invalid Date Format. Format should be [DD/MM/YYYY]";
                            M_strAlertMessage = lblMessage.Text;
                            objtxtDate.BackColor = System.Drawing.Color.LightYellow;
                            objtxtDate.Focus();
                            return;
                        }

                        if (objtxtSemFeeAmount.Text.Trim() == "")
                        {
                            lblMessage.Visible = true;
                            lblMessage.Text = "Enter Installment Amount";
                            M_strAlertMessage = lblMessage.Text;
                            objtxtDate.BackColor = System.Drawing.Color.LightYellow;
                            objtxtSemFeeAmount.Focus();
                            return;
                        }
                        //<End>Validation

                        intIntNo = Convert.ToInt32(objlblInstNo.Text);
                        strDueDate =objclsCommon.ConvertDateForInsert( objtxtDate.Text);
                        dblStudentSemFee = Convert.ToDouble(objtxtSemFeeAmount.Text);
                        dblTotalAmt = dblStudentSemFee + dblTotalAmt;

                        qryBuilder.Append("\r\n");
                        qryBuilder.Append("IF EXISTS(SELECT TOP 1 1 FROM Student_Installment_Detail (NOLOCK) WHERE RegistrationNumber=" + txtRegistrationNo.Text + " and InstallmentNo=" + intIntNo + " AND SEGMENT='Semester'   ) ");
                        qryBuilder.Append("\r\n");
                        qryBuilder.Append("BEGIN ");
                        qryBuilder.Append("\r\n");
                        qryBuilder.Append("UPDATE Student_Installment_Detail SET [Amount]=" + dblStudentSemFee + ", [DueDate]='" + strDueDate + "' ,[ModifiedBy]='gunjesh',[ModifiedDate]  =dbo.FnGetIndianTime() ");
                        qryBuilder.Append("\r\n");
                        qryBuilder.Append("WHERE   RegistrationNumber=" + txtRegistrationNo.Text + " and InstallmentNo=" + intIntNo + "  AND SEGMENT='Semester' ");
                        qryBuilder.Append("\r\n");
                        qryBuilder.Append("END ");
                        qryBuilder.Append("\r\n");
                        qryBuilder.Append("ELSE ");
                        qryBuilder.Append("\r\n");
                        qryBuilder.Append("BEGIN ");
                        qryBuilder.Append("\r\n");
                        qryBuilder.Append("INSERT INTO Student_Installment_Detail ( ");
                        qryBuilder.Append("\r\n");
                        qryBuilder.Append("[RegistrationNumber]  ,[InstallmentNo]  ,[DueDate],[Amount] ,[CreatedBy]  ,[CreatedDate]  ) ");
                        qryBuilder.Append("\r\n");
                        qryBuilder.Append("Select " + txtRegistrationNo.Text + "," + intIntNo + ",'" + strDueDate + "' ," + dblStudentSemFee + ",'gunjesh',dbo.FnGetIndianTime()  ");
                        qryBuilder.Append("\r\n");
                        qryBuilder.Append("END ");
                        qryBuilder.Append("\r\n");

                    }
                }
                //Amount Validation
                lblMessage.Text = "";
                //Semester Fee
                if (dblTotalAmt != Convert.ToDouble(lblStudentSemFee.Text))
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Please check the total of Student Semester Fee";
                    M_strAlertMessage = lblMessage.Text;
                    return;
                }

                if (qryBuilder.ToString().Length > 0)
                {
                    qryBuilder.Append(" UPDATE ADMISSION SET ADMISSIONSTAGE ='STAGE5',IsConfirmRegistration=1  WHERE REGISTRATIONNUMBER=" + txtRegistrationNo.Text + "");
                    feeMaster.strInsertQuery = qryBuilder.ToString();
                    intRes = objclsAdmission.SaveAnnulFeeDetails(M_strcon, feeMaster);

                    if (intRes > 0)
                    {
                        lblMessage.Visible = true;
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        lblMessage.BackColor = System.Drawing.Color.Yellow;

                        lblMessage.Text = "Student Admission Procedure has been completed successfully now. Proceed to generate Receipt.";
                        M_strAlertMessage = lblMessage.Text;
                    }
                }
                UpdatePanel1.Update();
            }
            catch (Exception ex)
            {
                M_strAlertMessage = ex.Message;
            }
            finally
            {
                qryBuilder.Length = 0;
                objclsAdmission = null;
                feeMaster = null;

                objlblInstNo = null;
                objtxtDate = null;
                objtxtSemFeeAmount = null;

                lblMessage.Text = M_strAlertMessage;
                objclsCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);

                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>SemesterInstallment();</script>", false);
                //Alert Message
                if (M_strAlertMessage != string.Empty && M_strAlertMessage.Trim() != "")
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + M_strAlertMessage + "');</script>", false);
                }

                objclsCommon = null;
            }
        }
        //protected void dtSemeterFee_ItemDataBound(object sender, DataListItemEventArgs e)
        //{
        //    try
        //    {
        //        TextBox objtxtSemAmount;
        //        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //        {
        //            objtxtSemAmount = (TextBox)e.Row.FindControl("txtSemAmount");
        //            objtxtSemAmount.Attributes.Add("onkeypress", "javascript:return fnCommonAcceptNumberOnly(this)");
        //            objtxtSemAmount.Attributes.Add("onblur", "javascript:return SemesterInstallment()");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        //Alert Message
        //        if (M_strAlertMessage != string.Empty && M_strAlertMessage.Trim() != "")
        //        {
        //            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + M_strAlertMessage + "');</script>", false);
        //        }
        //    }
        //}



        protected void grdInstallment_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            Double dblSemesterFee = 0;
            TextBox objtxtSemAmount;

            clsErrorLogger objclsErrorLogger = new clsErrorLogger();
            clsCommon objclsCommon = new clsCommon();
            string _strAlertMessage = string.Empty;
            string strActive = string.Empty;
            try
            {
                if (txtInstallmentTotal.Text.Trim() != "") dblSemesterFee = Convert.ToDouble(txtInstallmentTotal.Text);

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    objtxtSemAmount = (TextBox)e.Row.FindControl("txtSemAmount");
                    objtxtSemAmount.Attributes.Add("onkeypress", "javascript:return fnCommonAcceptNumberOnly(this)");
                    objtxtSemAmount.Attributes.Add("onblur", "javascript:return SemesterInstallment()");

                    if (objtxtSemAmount.Text != null && objtxtSemAmount.Text != "")
                    {
                        dblSemesterFee = dblSemesterFee + Convert.ToDouble(objtxtSemAmount.Text);
                    }

                }
                txtInstallmentTotal.Text = Convert.ToString(dblSemesterFee);
                objtxtSemAmount = null;
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
                if (_strAlertMessage != string.Empty && _strAlertMessage.Trim() != "")
                {
                    lblMessage.Text = _strAlertMessage;
                    objclsCommon.setLabelMessage(lblMessage, _strMsgType);
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
                }
                objclsCommon = null;
                objclsErrorLogger = null;
            }
        }

        protected void grdInstallment_RowCreated(object sender, GridViewRowEventArgs e)
        {
            TextBox objtxtAdmnAmount;

            clsErrorLogger objclsErrorLogger = new clsErrorLogger();
            clsCommon objclsCommon = new clsCommon();
            string _strAlertMessage = string.Empty;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    objtxtAdmnAmount = (TextBox)e.Row.FindControl("txtSemAmount");
                    objtxtAdmnAmount.Attributes.Add("onkeypress", "javascript:return fnCommonAcceptNumberOnly()");
                    objtxtAdmnAmount.Attributes.Add("onblur", "javascript:return SemesterInstallment()");
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
                    if (_strAlertMessage != string.Empty && _strAlertMessage.Trim() != "")
                    {
                        lblMessage.Text = _strAlertMessage;
                        objclsCommon.setLabelMessage(lblMessage, _strMsgType);
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
                    }
                    objclsCommon = null;
                    objclsErrorLogger = null;
                }
            }
        }
    
        protected void btnShow_Click(object sender, EventArgs e)
        {
            DataTable dtSemester = new DataTable();
            DataSet ds = new DataSet();
            clsCommon objclsCommon = new clsCommon();
            double dblStudentFeeAmount = 0;
            double dblCollegeFeeAmount = 0;
            clsFeeMaster objclasFeeMaster = new clsFeeMaster();
            ObjFeeMaster FeeMaster = new ObjFeeMaster();
            DataRow dtRow;

            try
            {
                FeeMaster.RegistrationNo = Convert.ToInt32(txtRegistrationNo.Text);
                ds = objclasFeeMaster.StudentSemesterInstallmentDetails(M_strcon, FeeMaster);
                grdInstallment.DataSource = ds.Tables[0];

                // Here data is bind to the control and show on page
                btnSubmit.Enabled = true;
                lblMessage.Text = "";

                if (ds.Tables[0].Rows.Count == 0)
                {
                    btnSubmit.Enabled = false;
                    lblMessage.Text = "Student Semester Fess has not defined.";
                    M_strAlertMessage = lblMessage.Text;
                    return;
                }
                else
                {
                    for (Int32 intIns = 0; intIns <= ds.Tables[0].Rows.Count - 1; intIns++)
                    {
                        dblStudentFeeAmount = dblStudentFeeAmount + Convert.ToDouble(ds.Tables[0].Rows[intIns]["FeeAmount"]);
                        dblCollegeFeeAmount = Convert.ToDouble(ds.Tables[0].Rows[intIns]["AnnualFee"]);
                    }

                    lblStudentSemFee.Text = Convert.ToString(dblStudentFeeAmount);
                    lblCollegeSemesterFee.Text = Convert.ToString(dblCollegeFeeAmount);
                }

                if (txtInstallment.Text.Trim() == "")
                {
                    btnSubmit.Enabled = false;
                    lblMessage.Text = "Enter Installment No.";
                    M_strAlertMessage = lblMessage.Text;
                    return;
                }

                dtSemester.Columns.Add("InstallmentNo", typeof(Int32));
                dtSemester.Columns.Add("DueDate", typeof(string));
                dtSemester.Columns.Add("FeeAmount", typeof(string));

                for (Int32 intIns = 1; intIns <= Convert.ToInt32(txtInstallment.Text); intIns++)
                {
                    dtRow = dtSemester.NewRow();
                    dtRow["InstallmentNo"] = intIns;
                    dtRow["DueDate"] = "";
                    dtRow["FeeAmount"] = "";
                    dtSemester.Rows.Add(dtRow);
                }


                // Here data is fetched from the database through stored procedure(insert_reg).
                grdInstallment.DataSource = dtSemester;
                grdInstallment.DataBind();

                //Mailer objMailer = new Mailer();
                //objMailer.SendEmail_AdmissionComplete(txtRegistrationNo.Text);

                btnShow.Enabled = false;
                txtInstallment.Enabled = false;
                btnSubmit.Visible = true;
                UpdatePanel1.Update();

                if (grdInstallment.Rows.Count > 0)
                {
                    rowTotal.Visible = true;
                }
            }
            catch (Exception ex)
            {
                M_strAlertMessage = ex.Message;
            }
            finally
            {
                dtSemester = null;
                ds = null;
                objclasFeeMaster = null;
                FeeMaster = null;
                dtRow = null;

                lblMessage.Text = M_strAlertMessage;
                objclsCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);

                //Alert Message
                if (M_strAlertMessage != string.Empty && M_strAlertMessage.Trim() != "")
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + M_strAlertMessage + "');</script>", false);
                }

                objclsCommon = null;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (Request.QueryString.Keys.Count == 0)
            {
                tblDetail.Visible = false;
                lblMessage.Text = "";
                txtRegistrationNo.Text = "";
                txtRegistrationNo.Enabled = true;

                txtInstallment.Text = "";
                txtInstallment.Enabled = true;
                lblStudentName.Text = "";
                lblAcedemicYear.Text = "";
                lblFinancialYear.Text = "";

                grdInstallment.DataSource = null;
                grdInstallment.DataBind();

                btnGetDetails.Enabled = true;

                rowTotal.Visible = false;
                btnSubmit.Visible = false;

                UpdatePanel1.Update();
            }
        }
    }
 
