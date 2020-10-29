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
using System.IO;
using System.Xml;
using System.Text;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Threading;
 
  public partial class Operation_Invoice : System.Web.UI.Page 
    {
      private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        string M_strcon = string.Empty;
        string M_UserId = string.Empty;
        string M_strWorkingLocation = string.Empty;
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
                    M_strWorkingLocation = user.WorkingLocation;
                }
                else
                {
                    Session.Abandon();
                    Response.Redirect("login.aspx?Exp=Y");
                }

                if (!IsPostBack)
                {
                    txtAmount.Attributes.Add("onkeypress", "javascript:return IsNumeric( )");
                    clearValues();

                    Page.Title = user.Orig_Name + ": Admission Fee Payment";
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

        public void Binddata()
        {
            DataSet ds = new DataSet();
            clsFeeMaster objclasFeeMaster = new clsFeeMaster();
            ObjFeeMaster FeeMaster = new ObjFeeMaster();
            try
            {

                FeeMaster.RegistrationNo = Convert.ToInt32(txtRegistrationNo.Text);
                FeeMaster.Mode = "Receipt";
                ds = objclasFeeMaster.StudentAdmissionFeeDetails(M_strcon, FeeMaster);
               
                dtAnnualFee.DataSource = ds.Tables[0];
                dtAnnualFee.DataBind();

                // Here data is bind to the control and show on page
                btnSubmit.Enabled = true;
                lblMessage.Text = "";

                if (ds.Tables[0].Rows.Count == 0)
                {
                    btnSubmit.Enabled = false;
                    lblTotalReceived.Text = "";
                    lblTotalDues.Text = "";
                    lblTotalFee.Text = "";
                    txtTotalPayingAmount.Text = "";
                    lblMessage.Text = "Annual Fee is not defined. Please contact to Accounts to define it.";
                    M_strAlertMessage = lblMessage.Text;
                    return;
                }

                if (ds.Tables[1] != null)
                {
                    lblName.Text = ds.Tables[1].Rows[0]["StudentName"].ToString();
                    //Academic Year
                    lblAcedemicYear.Text = ds.Tables[1].Rows[0]["AcademicYear"].ToString() + " / " + ds.Tables[1].Rows[0]["AcademicCourse"].ToString();
                    //Financial Year
                    lblFinancialYear.Text = ds.Tables[1].Rows[0]["FinancialYear"].ToString() + " / " + ds.Tables[1].Rows[0]["FinancialCourse"].ToString();
                }

                txtAmount.Visible = true;
                lblTotalDues.Visible = true;
                lblTotalReceived.Visible = true;
                lblTotalFee.Visible = true;
                txtTotalPayingAmount.Visible = true;
                lblTotalFee.Visible = true;
                txtTotalPayingAmount.Visible = true;
                btnSubmit.Visible = true;

                dvPaymentOption.Visible = true;
                divOfflinePaymentOption.Visible = true;
            }
            catch (Exception ex)
            {
                M_strAlertMessage = ex.Message;
            }
            finally
            {
                ds = null;
                objclasFeeMaster = null;
                FeeMaster = null;
                //Alert Message
                if (M_strAlertMessage != string.Empty && M_strAlertMessage.Trim() != "")
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + M_strAlertMessage + "');</script>", false);
                }
            }
        }

        public void BindMiscdata()
        {
            DataSet ds = new DataSet();

            clsFeeMaster objclasFeeMaster = new clsFeeMaster();
            ObjFeeMaster FeeMaster = new ObjFeeMaster();
            string strAlertMessage = string.Empty;
            try
            {
                FeeMaster.RegistrationNo = Convert.ToInt32(txtRegistrationNo.Text);
                FeeMaster.CreatedBy = M_UserId;
                ds = objclasFeeMaster.StudentMiscellaneousFeePaymentDetails(M_strcon, FeeMaster);

                if (ds != null && ds.Tables.Count >= 2)
                {
                    if (ds.Tables[1].Columns.Contains("FINE_MSG"))
                    {
                        if (ds.Tables[1].Rows[0]["FINE_MSG"].ToString().Trim() != "")
                        {
                            lblMessage.Text = ds.Tables[1].Rows[0]["FINE_MSG"].ToString().Trim();
                            lblMessage.Visible = true;
                            strAlertMessage = lblMessage.Text;
                            btnSubmit.Enabled = false;
                            UpdatePanel1.Update();
                            return;
                        }
                    }
                }

                dtPayingList.DataSource = ds.Tables[0];
                dtPayingList.DataBind();

                // Here data is bind to the control and show on page
                lblMessage.Text = "";
                if (ds.Tables[0].Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Section(s) are not defined for Miscellaneous Segment');</script>", false);
                    lblMessage.Text = "Section(s) are not defined for Miscellaneous Segment";

                    txtTotalPayingAmount.Text = "";
                    dtPayingList.DataSource = null;
                    dtPayingList.DataBind();
                    dtPayingList.Visible = false;

                    btnSubmit.Enabled = false;
                    txtTotalPayingAmount.Visible = false;
                    dvPaymentOption.Visible = false;
                    divOfflinePaymentOption.Visible = false;

                    return;
                }

                //Student Name
                if (ds.Tables[1] != null)
                {
                    lblName.Text = ds.Tables[1].Rows[0]["StudentName"].ToString();
                    //Academic Year
                    lblAcedemicYear.Text = ds.Tables[1].Rows[0]["AcademicYear"].ToString() + " / " + ds.Tables[1].Rows[0]["AcademicCourse"].ToString();
                    //Financial Year
                    lblFinancialYear.Text = ds.Tables[1].Rows[0]["FinancialYear"].ToString() + " / " + ds.Tables[1].Rows[0]["FinancialCourse"].ToString();
                }

                btnSubmit.Visible = true;
                dtPayingList.Visible = true;
                btnSubmit.Enabled = true;
                txtTotalPayingAmount.Visible = true;
                dvPaymentOption.Visible = true;
                divOfflinePaymentOption.Visible = true;

                lblTotalDues.Text = "NA";
                lblTotalReceived.Text = "NA";
                lblTotalFee.Text = "NA";

                lblTotalDues.ToolTip = "Not Applicable for Misc. Segment";
                lblTotalReceived.ToolTip = "Not Applicable for Misc. Segment";
                lblTotalFee.ToolTip = "Not Applicable for Misc. Segment";

                lblTotalDues.Visible = true;
                lblTotalReceived.Visible = true;
                lblTotalFee.Visible = true;

            }
            catch (Exception ex)
            {
                strAlertMessage = ex.Message;
            }
            finally
            {
                //Alert Message
                if (strAlertMessage != string.Empty && strAlertMessage.Trim() != "")
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + strAlertMessage + "');</script>", false);
                }
            }
        }


        public void BindInstallmentdata()
        {
            DataSet ds = new DataSet();
            clsFeeMaster objclasFeeMaster = new clsFeeMaster();
            ObjFeeMaster FeeMaster = new ObjFeeMaster();
            try
            {

                FeeMaster.RegistrationNo = Convert.ToInt32(txtRegistrationNo.Text);
                FeeMaster.CreatedBy = M_UserId;
                FeeMaster.Mode = "Receipt";
                ds = objclasFeeMaster.StudentSemesterFeePaymentDetails(M_strcon, FeeMaster);
               
                dtSemesterFee.DataSource = ds.Tables[0];
                dtSemesterFee.DataBind();

                dtPayingList.DataSource = ds.Tables[1];
                dtPayingList.DataBind();

                // Here data is bind to the control and show on page
                btnSubmit.Enabled = true;
                lblMessage.Text = "";

                if (ds.Tables[0].Rows.Count == 0)
                {
                    btnSubmit.Enabled = false;
                    lblTotalReceived.Text = "";
                    lblTotalDues.Text = "";
                    lblTotalFee.Text = "";
                    txtTotalPayingAmount.Text = "";
                    lblMessage.Text = "Annual Fee is not defined. Please contact to Accounts to define it.";
                    M_strAlertMessage = lblMessage.Text;
                    return;
                }

                if (ds.Tables[2] != null)
                {
                    lblName.Text = ds.Tables[2].Rows[0]["StudentName"].ToString();
                    //Academic Year
                    lblAcedemicYear.Text = ds.Tables[2].Rows[0]["AcademicYear"].ToString() + " / " + ds.Tables[2].Rows[0]["AcademicCourse"].ToString();
                    //Financial Year
                    lblFinancialYear.Text = ds.Tables[2].Rows[0]["FinancialYear"].ToString() + " / " + ds.Tables[2].Rows[0]["FinancialCourse"].ToString();
                }

                txtAmount.Visible = true;
                lblTotalDues.Visible = true;
                lblTotalReceived.Visible = true;
                lblTotalFee.Visible = true;
                txtTotalPayingAmount.Visible = true;
                lblTotalFee.Visible = true;
                txtTotalPayingAmount.Visible = true;
                btnSubmit.Visible = true;

                dvPaymentOption.Visible = true;
                divOfflinePaymentOption.Visible = true;
            }
            catch (Exception ex)
            {
                M_strAlertMessage = ex.Message;
            }
            finally
            {
                ds = null;
                objclasFeeMaster = null;
                FeeMaster = null;
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
            System.Text.StringBuilder qryReceiptDtl = new System.Text.StringBuilder();
            clsFeeMaster objclsFeeMaster = new clsFeeMaster();
            clsAdmission objclsAdmission = new clsAdmission();
            ObjFeeMaster feeMaster = new ObjFeeMaster();
            clsCommon objclsCommon = new clsCommon();
            DataTable dtReceipt = new DataTable();

            int intRes=0;
            int intSectionID;
            double dblAdmnAmt = 0;
            double dblPaidAmt=0;
            double dblDueAmt=0;
            double dblTotalPayingAmt = 0;

            int loopCounter = 0;

            //string strLoggedInUser = "";
            string strAlertMessage = "";

            //GST Sgment
            double dblCGST = 0;
            double dblSGST = 0;
            double dblIGST = 0;
            string strGSTNO = string.Empty;


          

            try
            {

                //if (Session["userObject"] != null)
                //{
                //    ObjUser user;
                //    user = (ObjUser)Session["userObject"];
                //    strLoggedInUser = user.UserId;
                //}

                if (txtTotalPayingAmount.Text.Trim() == "" || Convert.ToInt32(txtTotalPayingAmount.Text) == 0)
                {
                    return;
                }

                //Receipt Generation For Offline Payment
                if (btnSubmit.Text == "Print Receipt")
                {
                  //  //System.IO.Path.Combine(Server.MapPath("."), "Uploads/Receipt/", "Invoice_" + hdReceiptNo.Value.ToString())
                  //  //open another window
                  //  //string strPopUp = "window.open('http://www.soaneemrana.com/SoaReports/PrintReceipt.aspx?Center="+ M_strWorkingLocation + "&ReceiptNo=" + hdReceiptNo.Value + "','Receipt','status=yes,toolbar=no,menubar=no,location=no,resizable=yes');";
                  //  //ScriptManager.RegisterStartupScript(this, this.GetType(), "Receipt", strPopUp, true);
                  //  //return;

                  //  string pathAdd = ResolveClientUrl( "Uploads/Receipt/"+"Invoice_" + hdReceiptNo.Value.ToString() +".xls");
                  ////  string encodedPath = System.Web.HttpUtility.JavaScriptStringEncode(pathAdd, true);

                  ////  ClientScript.RegisterStartupScript(this.GetType(), "open", "window.open(" + encodedPath + ", '_blank');", true);


                  //  pathAdd = System.IO.Path.Combine(Server.MapPath("."), "Uploads/Receipt/", "Invoice_" + hdReceiptNo.Value.ToString());

                  //  ClientScript.RegisterStartupScript(this.GetType(), "open", "window.open('" + pathAdd + "','_blank' );", true);


                    string filePath = string.Empty;
 
                    // filePath = "http://localhost:1337/Checklist/Documents/"
                    try
                    {
                        //if Student Photo/Signature is going to download

                        filePath = System.IO.Path.Combine(Server.MapPath("."), "Uploads/Receipt/", "Invoice_" + hdReceiptNo.Value.ToString()) +".xls";
                        
                        if (System.IO.File.Exists(filePath) == false)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "File Not Exists", "<script>alert('File not found. Please upload it again.');</script>", false);
                            return;
                        }

                        //imgSrc = 'http://www.soaneemrana.com/BackSys/Checklist/Documents/' + imgSrc;
                        Response.ContentType = "application/vnd.ms-excel";
                        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filePath + "\"");
                        Response.TransmitFile(filePath);
                        Response.End();
                        return;
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = ex.Message;
                    }
                    finally
                    { }

                    return;
                }

                //Validation
                lblMessage.Text = "";
                if (rdoPaymentOption.SelectedIndex == 0)
                {
                    feeMaster.PaymentMode = "Cash";
                }
                else if (rdoPaymentOption.SelectedIndex == 1 || rdoPaymentOption.SelectedIndex ==2)
                {
                    if (txtBankName.Text.Trim() == "")
                    {
                        strAlertMessage = "Please Enter Bank Name";
                        lblMessage.Text = strAlertMessage;
                        txtBankName.Focus();
                        return;
                    }
                    if (txtDraftDate.Text.Trim() == "")
                    {
                        strAlertMessage = "Please Enter Draft/NEFT Date";
                        lblMessage.Text = strAlertMessage;
                        txtDraftDate.Focus();
                        return;
                    }
                    if (txtDraftNumber.Text.Trim() == "")
                    {
                        strAlertMessage = "Please Enter Draft/NEFT Number";
                        lblMessage.Text = strAlertMessage;
                        txtDraftNumber.Focus();
                        return;
                    }
                    if (txtAmount.Text.Trim() == "")
                    {
                        strAlertMessage = "Please Enter Amount";
                        lblMessage.Text = strAlertMessage;
                        txtAmount.Focus();
                        return;
                    }

                    if (Convert.ToInt32(txtAmount.Text) != Convert.ToInt32(txtTotalPayingAmount.Text))
                    {
                        strAlertMessage = "Total Paying Amount must be equal to Cheque/DD Amount";
                        lblMessage.Text = strAlertMessage;
                        txtAmount.Focus();
                        return;
                    }

                    feeMaster.PaymentMode = "Cheque";

                    if (rdoPaymentOption.SelectedIndex == 2)
                    {
                        feeMaster.PaymentMode = "NEFT";
                    }

                    feeMaster.IssuedBank = txtBankName.Text.Trim();
                    feeMaster.ChequeDDNo = txtDraftNumber.Text.Trim();
                    feeMaster.ChequeDate = objclsCommon.ConvertDateForInsert(txtDraftDate.Text.Trim());
                    feeMaster.ChequeDDReceiptAmount = Convert.ToDouble(txtAmount.Text);
                }
                else if (rdoPaymentOption.SelectedIndex == 3)
                {
                    feeMaster.OnlineTranId = objclsCommon.getTransactionID(HttpContext.Current.Session.SessionID.ToString());
                    feeMaster.PaymentMode = "Online";
                }

                //For Admission
                  dblCGST = user.CGSTPer ;
                  dblSGST = user.SGSTPer;
                  dblIGST = user.IGSTPer;
                  strGSTNO = user.Orig_GSTNO;

                if (rdoAdmission.Checked )
                {
                    foreach (DataListItem dli in dtAnnualFee.Items)
                    {
                        if (dli.ItemType == ListItemType.Item || dli.ItemType == ListItemType.AlternatingItem)
                        {
                            dblAdmnAmt = 0;
                            dblPaidAmt = 0;
                            dblDueAmt = 0;

                            if (((TextBox)dli.FindControl("txtAdmnAmount")).Text.Trim() != "") dblAdmnAmt = Convert.ToDouble(((TextBox)dli.FindControl("txtAdmnAmount")).Text);
                            if (((Label)dli.FindControl("lblPaidAmount")).Text.Trim() != "") dblPaidAmt = Convert.ToDouble(((Label)dli.FindControl("lblPaidAmount")).Text);
                            if (((TextBox)dli.FindControl("txtPayingAmount")).Text.Trim() != "") dblDueAmt = Convert.ToDouble(((TextBox)dli.FindControl("txtPayingAmount")).Text);

                            if (dblDueAmt > (dblAdmnAmt - dblPaidAmt))
                            {
                                lblMessage.Visible = true;
                                lblMessage.Text = "Please check the Due Amount";
                                ((TextBox)dli.FindControl("txtPayingAmount")).Focus();
                                return;
                            }

                            dblTotalPayingAmt = dblTotalPayingAmt + dblDueAmt;
                            intSectionID = Convert.ToInt16(dtAnnualFee.DataKeys[dli.ItemIndex]);

                            if (rdoPaymentOption.SelectedIndex == 3)
                            {
                                qryBuilder.Append("INSERT INTO OnlineTransaction_ReceiptDetails ( ");
                            }
                            else
                            {
                                qryBuilder.Append("INSERT INTO ReceiptDetails ( ");
                            }

                            loopCounter = loopCounter + 1;
                            if (((Label)dli.FindControl("lblGST_Applicable")).Text == "*")
                            {
                                qryBuilder.Append("[ReceiptNo],[SectionID],[Amount],[CGST_Per],[CGST_Amount],[SGST_Per],[SGST_Amount],[IGST_Per],[IGST_Amount]  ) ");
                                qryBuilder.Append("Select  R$E$C$E$I$P$T ," + intSectionID + "," + dblDueAmt + "," + dblCGST + " ," + dblDueAmt * dblCGST + " ," + dblSGST + " ," + dblDueAmt * dblSGST + " ," + dblIGST + " ," + dblDueAmt * dblIGST + "   ");
                                qryBuilder.Append(" GO ");

                                 
                                qryReceiptDtl.Append("<Row ss:AutoFitHeight=\"0\" ss:Height=\"15.45\">");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s62\"/>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s164\"><Data ss:Type=\"Number\">"+ loopCounter.ToString () +"</Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:MergeAcross=\"1\" ss:StyleID=\"m1716013900688\"><Data ss:Type=\"String\">"+ ((Label)dli.FindControl("lblAdmnSection")).Text.Trim () +"</Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s164\"><Data ss:Type=\"Number\">"+ dblDueAmt.ToString () +"</Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s171\"/>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s164\" ss:Formula=\"=IF(TRIM(RC[-2])=&quot;&quot;,&quot;&quot;,RC[-2]-RC[-1])\"><Data ss:Type=\"String\"></Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s172\"><Data ss:Type=\"Number\">" + dblCGST.ToString() + "</Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s164\" ss:Formula=\"=IF(TRIM(RC[-2])=&quot;&quot;,&quot;&quot;,RC[-1]*RC[-2])\"><Data ss:Type=\"String\"></Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s172\"><Data ss:Type=\"Number\">" + dblSGST.ToString() + "</Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s164\" ss:Formula=\"=IF(RC[-4]=&quot;&quot;,&quot;&quot;,  RC[-1]*RC[-4])\"><Data  ss:Type=\"String\"></Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s172\"><Data ss:Type=\"Number\">" + dblIGST.ToString() + "</Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s164\" ss:Formula=\"=IF(TRIM(RC[-6])=&quot;&quot;,&quot;&quot;,RC[-6]*RC[-1])\"><Data ss:Type=\"String\"></Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s62\"/>");
                                qryReceiptDtl.Append("</Row>");

                            }
                            else
                            {
                                qryBuilder.Append("[ReceiptNo],[SectionID],[Amount],[CGST_Per],[CGST_Amount],[SGST_Per],[SGST_Amount],[IGST_Per],[IGST_Amount]  ) ");
                                qryBuilder.Append("Select  R$E$C$E$I$P$T ," + intSectionID + "," + dblDueAmt + " ,0,0,0,0,0,0  ");
                                qryBuilder.Append(" GO ");

                                qryReceiptDtl.Append("<Row ss:AutoFitHeight=\"0\" ss:Height=\"15.45\">");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s62\"/>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s164\"><Data ss:Type=\"Number\">" + loopCounter.ToString() + "</Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:MergeAcross=\"1\" ss:StyleID=\"m1716013900688\"><Data ss:Type=\"String\">" + ((Label)dli.FindControl("lblAdmnSection")).Text.Trim() + "</Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s164\"><Data ss:Type=\"Number\">" + dblDueAmt.ToString() + "</Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s171\"/>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s164\" ss:Formula=\"=IF(TRIM(RC[-2])=&quot;&quot;,&quot;&quot;,RC[-2]-RC[-1])\"><Data ss:Type=\"String\"></Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s172\"><Data ss:Type=\"Number\">0</Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s164\" ss:Formula=\"=IF(TRIM(RC[-2])=&quot;&quot;,&quot;&quot;,RC[-1]*RC[-2])\"><Data ss:Type=\"String\"></Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s172\"><Data ss:Type=\"Number\">0</Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s164\" ss:Formula=\"=IF(RC[-4]=&quot;&quot;,&quot;&quot;,  RC[-1]*RC[-4])\"><Data  ss:Type=\"String\"></Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s172\"><Data ss:Type=\"Number\">0</Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s164\" ss:Formula=\"=IF(TRIM(RC[-6])=&quot;&quot;,&quot;&quot;,RC[-6]*RC[-1])\"><Data ss:Type=\"String\"></Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s62\"/>");
                                qryReceiptDtl.Append("</Row>");

                            }

                            //qryBuilder.Append("[ReceiptNo]  ,[SectionID] ,[Amount]  ) ");
                            //qryBuilder.Append("Select  R$E$C$E$I$P$T ," + intSectionID + "," + dblDueAmt + "  ");
                            //qryBuilder.Append(" GO ");

                        }
                    }
                }

                //For Misc. Segment
                if (rdoMisc.Checked || rdoSemester.Checked)
                {
                    dblTotalPayingAmt = 0;

                    foreach (DataListItem dli in dtPayingList.Items)
                    {
                        if (dli.ItemType == ListItemType.Item || dli.ItemType == ListItemType.AlternatingItem)
                        {
                            dblPaidAmt = 0;

                            if (((TextBox)dli.FindControl("txtPayingAmount")).Text.Trim() != "")
                            {
                                double.TryParse(((TextBox)dli.FindControl("txtPayingAmount")).Text, out dblPaidAmt);
                            }
                           
                            dblTotalPayingAmt = dblTotalPayingAmt + dblPaidAmt;
                            intSectionID = Convert.ToInt16(dtPayingList.DataKeys[dli.ItemIndex]);

                            if (rdoPaymentOption.SelectedIndex == 3)
                            {
                                qryBuilder.Append("INSERT INTO OnlineTransaction_ReceiptDetails ( ");
                            }
                            else
                            {
                                qryBuilder.Append("INSERT INTO ReceiptDetails ( ");
                            }

                            loopCounter = loopCounter + 1;
                            if (((Label)dli.FindControl("lblGST_Applicable")).Text == "*")
                            {
                                qryBuilder.Append("[ReceiptNo],[SectionID],[Amount],[CGST_Per],[CGST_Amount],[SGST_Per],[SGST_Amount],[IGST_Per],[IGST_Amount]  ) ");
                                qryBuilder.Append("Select  R$E$C$E$I$P$T ," + intSectionID + "," + dblPaidAmt + "," + dblCGST + " ," + dblPaidAmt * dblCGST + " ," + dblSGST + " ," + dblPaidAmt * dblSGST + " ," + dblIGST + " ," + dblPaidAmt * dblIGST + "   ");
                                qryBuilder.Append(" GO ");

                                qryReceiptDtl.Append("<Row ss:AutoFitHeight=\"0\" ss:Height=\"15.45\">");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s62\"/>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s164\"><Data ss:Type=\"Number\">" + loopCounter.ToString() + "</Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:MergeAcross=\"1\" ss:StyleID=\"m1716013900688\"><Data ss:Type=\"String\">" + ((Label)dli.FindControl("lblAdmnSection")).Text.Trim() + "</Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s164\"><Data ss:Type=\"Number\">" + dblPaidAmt.ToString() + "</Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s171\"/>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s164\" ss:Formula=\"=IF(TRIM(RC[-2])=&quot;&quot;,&quot;&quot;,RC[-2]-RC[-1])\"><Data ss:Type=\"String\"></Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s172\"><Data ss:Type=\"Number\">" + dblCGST.ToString() + "</Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s164\" ss:Formula=\"=IF(TRIM(RC[-2])=&quot;&quot;,&quot;&quot;,RC[-1]*RC[-2])\"><Data ss:Type=\"String\"></Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s172\"><Data ss:Type=\"Number\">" + dblSGST.ToString() + "</Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s164\" ss:Formula=\"=IF(RC[-4]=&quot;&quot;,&quot;&quot;,  RC[-1]*RC[-4])\"><Data  ss:Type=\"String\"></Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s172\"><Data ss:Type=\"Number\">" + dblIGST.ToString() + "</Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s164\" ss:Formula=\"=IF(TRIM(RC[-6])=&quot;&quot;,&quot;&quot;,RC[-6]*RC[-1])\"><Data ss:Type=\"String\"></Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s62\"/>");
                                qryReceiptDtl.Append("</Row>");

                            }
                            else
                            {
                                qryBuilder.Append("[ReceiptNo],[SectionID],[Amount],[CGST_Per],[CGST_Amount],[SGST_Per],[SGST_Amount],[IGST_Per],[IGST_Amount]  ) ");
                                qryBuilder.Append("Select  R$E$C$E$I$P$T ," + intSectionID + "," + dblPaidAmt + " ,0,0,0,0,0,0  ");
                                qryBuilder.Append(" GO ");

                                qryReceiptDtl.Append("<Row ss:AutoFitHeight=\"0\" ss:Height=\"15.45\">");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s62\"/>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s164\"><Data ss:Type=\"Number\">" + loopCounter.ToString() + "</Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:MergeAcross=\"1\" ss:StyleID=\"m1716013900688\"><Data ss:Type=\"String\">" + ((Label)dli.FindControl("lblAdmnSection")).Text.Trim() + "</Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s164\"><Data ss:Type=\"Number\">" + dblPaidAmt.ToString() + "</Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s171\"/>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s164\" ss:Formula=\"=IF(TRIM(RC[-2])=&quot;&quot;,&quot;&quot;,RC[-2]-RC[-1])\"><Data ss:Type=\"String\"></Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s172\"><Data ss:Type=\"Number\">0</Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s164\" ss:Formula=\"=IF(TRIM(RC[-2])=&quot;&quot;,&quot;&quot;,RC[-1]*RC[-2])\"><Data ss:Type=\"String\"></Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s172\"><Data ss:Type=\"Number\">0</Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s164\" ss:Formula=\"=IF(RC[-4]=&quot;&quot;,&quot;&quot;,  RC[-1]*RC[-4])\"><Data  ss:Type=\"String\"></Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s172\"><Data ss:Type=\"Number\">0</Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s164\" ss:Formula=\"=IF(TRIM(RC[-6])=&quot;&quot;,&quot;&quot;,RC[-6]*RC[-1])\"><Data ss:Type=\"String\"></Data></Cell>");
                                qryReceiptDtl.Append("<Cell ss:StyleID=\"s62\"/>");
                                qryReceiptDtl.Append("</Row>");

                            }
                        }
                    }
                }

                //Amount Validation
                lblMessage.Text = "";

                if (qryBuilder.ToString().Length > 0)
                {
                    hdReceiptNo.Value = "";
                    feeMaster.RegistrationNo = Convert.ToInt32(txtRegistrationNo.Text.Trim());
                    feeMaster.Amount = dblTotalPayingAmt;
                    feeMaster.CreatedBy = M_UserId;
                    feeMaster.Concession = 0;
                    feeMaster.strInsertQuery = qryBuilder.ToString();
                    feeMaster.Remarks = txtRemarks.Text.Trim();

                    //Organization information
                    feeMaster.ConnectionString = M_strcon;
                    feeMaster.Orig_Name = user.Orig_Name;
                    feeMaster.Orig_Address1 = user.Orig_Address1;
                    feeMaster.Orig_Address2 = user.Orig_Address2;
                    feeMaster.Orig_Phone1 = user.Orig_Phone1;
                    feeMaster.Orig_Phone2 = user.Orig_Phone2;
                    feeMaster.Orig_Email1 = user.Orig_Email1;
                    feeMaster.Orig_Email2 = user.Orig_Email2;
                    feeMaster.Orig_Url = user.Orig_Url;

                    Session["Receipt"] = feeMaster;

                    if (rdoPaymentOption.SelectedIndex ==3)//online Payment
                    {
                        string strQryString;
                        string strQryString1;
                        string strQryString2;
                        string strQryString3;

                        //strQryString = Server.UrlEncode(objclsCommon.Encrypt(txtRegistrationNo.Text));
                        //strQryString1 = Server.UrlEncode(objclsCommon.Encrypt("Admission Fee"));
                        //strQryString2 = Server.UrlEncode(objclsCommon.Encrypt(txtTotalPayingAmount.Text));
                        //strQryString3 = Server.UrlEncode((objclsCommon.Encrypt(feeMaster.OnlineTranId)));
                        //Response.Redirect("OnlinePayment.aspx?RegNo=" + strQryString + "&Mode=" + strQryString1 + "&Amount=" + strQryString2 + "&TranID=" + strQryString3);

                        strQryString = Server.UrlEncode(objclsCommon.Encrypt(txtRegistrationNo.Text));
                        strQryString1 = Server.UrlEncode(objclsCommon.Encrypt("Admission Fee"));
                        strQryString2 = Server.UrlEncode(objclsCommon.Encrypt(txtTotalPayingAmount.Text));
                        strQryString3 = Server.UrlEncode((objclsCommon.Encrypt(feeMaster.OnlineTranId)));
                        Response.Redirect("OnlinePayment.aspx?T1=" + strQryString + "&T2=" + strQryString1 + "&T3=" + strQryString2 + "&T4=" + strQryString3);

                    }
                    else//Offline payment
                    {
                        dtReceipt = objclsAdmission.generateReceipt(M_strcon, feeMaster);
                    }

                    if (dtReceipt != null && dtReceipt.Rows.Count > 0)
                    {

                        int.TryParse(dtReceipt.Rows[0]["RECEIPT_NO"].ToString(), out intRes);
                        hdReceiptNo.Value = Convert.ToString(intRes);

                        strAlertMessage = "Payment has been made successfully.Please note the Receipt No- " + Convert.ToString(intRes);
                        lblMessage.Text = strAlertMessage;
                        btnSubmit.Text = "Print Receipt";
                        rdoPaymentOption.Enabled = false;
                        btnSubmit.Attributes.Add("OnClick", "javascript:Confirm('Do you want to Print Receipt?');");

                        //Send Mail and Message Details Starts
                        //Send Mail/Message
                        clsEC_SMS_SendMessage objclsEC_SMS_SendMessage = new clsEC_SMS_SendMessage();
                        try
                        {
                            objclsEC_SMS_SendMessage.SendSMSWithEmail(dtReceipt, intRes,feeMaster);
                        }
                        catch (Exception ex)
                        {
                            lblMessage.Text = ex.Message;
                        }
                        finally
                        {
                            objclsEC_SMS_SendMessage = null;
                        }
                        //Send Mail and Message Details Ends

                        //Sending Mail for NEFT Trans Information
                        if (rdoPaymentOption.SelectedIndex == 2)
                        {
                            ObjSendEmail SendMail = new ObjSendEmail();
                            clsCommon objClsCommon = new clsCommon();
                            DataTable dtInformation;
                            string strMailContent = string.Empty;
                            try
                            {
                                dtInformation = objClsCommon.getMailBody("COMMON", "MailFormat", "MAIL FOR RECEIPT AGAINST CASH/DRAFT/NEFT PAYMENT TO ADMIN", M_strcon);

                                if (dtInformation != null & dtInformation.Rows.Count > 0)
                                {
                                    //Change Mail Body Context
                                    strMailContent = dtInformation.Rows[0]["MAIL_BODY"].ToString();
                                    strMailContent = strMailContent.Replace("#RegNo#", txtRegistrationNo.Text);
                                    strMailContent = strMailContent.Replace("#Name#", lblName.Text);
                                    strMailContent = strMailContent.Replace("#Section#", "Admission Fee Payment");
                                    strMailContent = strMailContent.Replace("#Remarks#", txtRemarks.Text);
                                    strMailContent = strMailContent.Replace("#NEFTNo#", txtDraftNumber.Text);
                                    strMailContent = strMailContent.Replace("#NEFTDate#", txtDraftDate.Text);
                                    strMailContent = strMailContent.Replace("#ReceiptNo#", Convert.ToString(intRes));
                                    strMailContent = strMailContent.Replace("#Amount#", txtTotalPayingAmount.Text);
                                    strMailContent = strMailContent.Replace("#IssuedBy#", M_UserId);

                                    //Organization Info
                                    strMailContent = strMailContent.Replace("#Organization#", user.Orig_Name);
                                    strMailContent = strMailContent.Replace("#Orig_Address1#", user.Orig_Address1);
                                    strMailContent = strMailContent.Replace("#Orig_Address2#", user.Orig_Address2);
                                    strMailContent = strMailContent.Replace("#Orig_Phone1#", user.Orig_Phone1);
                                    strMailContent = strMailContent.Replace("#Orig_Phone2#", user.Orig_Phone2);
                                    strMailContent = strMailContent.Replace("#Orig_Email1#", user.Orig_Email1);
                                    strMailContent = strMailContent.Replace("#Orig_Email2#", user.Orig_Email2);
                                    strMailContent = strMailContent.Replace("#Orig_Url#", user.Orig_Url);

                                    //Mail Body Information
                                    SendMail.From = dtInformation.Rows[0]["FROM"].ToString();
                                    SendMail.To = objClsCommon.getConfigKeyValue("EMAIL IDS FOR RECEIPT AGAINST CASH/DRAFT/NEFT PAYMENT TO ADMIN", "EMAIL RECEPIENTS GROUP", M_strcon);
                                    SendMail.CC = objClsCommon.getConfigKeyValue("EMAIL IDS FOR CC OF MAIL MESSAGE FOR EACH OPERATION", "EMAIL RECEPIENTS GROUP", M_strcon);
                                    SendMail.SMTPHost = dtInformation.Rows[0]["HOST"].ToString();
                                    SendMail.SMTPPort = Convert.ToInt32(dtInformation.Rows[0]["PORT"].ToString());
                                    SendMail.UserId = dtInformation.Rows[0]["USER_ID"].ToString();
                                    SendMail.Subject = "Receipt Issued: Payment Made by NEFT Transaction";
                                    SendMail.Password =objclsCommon.DecryptData( dtInformation.Rows[0]["PASSWORD"].ToString());
                                    SendMail.MailBody = strMailContent;
                                    SendMail.ConnectionString = M_strcon;

                                    if (SendMail.To != string.Empty)
                                    {
                                        objClsCommon.SendEmail(SendMail);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                            finally
                            {
                                SendMail = null;
                                objClsCommon = null;
                                dtInformation = null;
                            }
                        }
                        //Sending SMS -End

                        //create Invoice
                        fnGenerateInvoice(user, qryReceiptDtl.ToString(), loopCounter, intRes, M_UserId,"" );
                        
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                //dispose objects
                qryBuilder = null;
                objclsFeeMaster = null;
                objclsAdmission = null;
                feeMaster = null;
                objclsCommon = null;
                //Alert Message
                if (strAlertMessage != string.Empty && strAlertMessage.Trim() != "")
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + strAlertMessage + "');</script>", false);
                }
            }
        }

        protected void dtPayingList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            Double dblPayingAmount = 0;
            Double dblTotalAmount = 0;
 
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TextBox objtxtPayingAmount = (TextBox)e.Item.FindControl("txtPayingAmount");
                objtxtPayingAmount.Attributes.Add("onkeypress", "javascript:return IsNumeric('d')");
                objtxtPayingAmount.Attributes.Add("onblur", "javascript:return AdmissionFee(this)");
                objtxtPayingAmount.Attributes.Add("onfocus", "javascript:return  PrvAmount(this)");

                double.TryParse(objtxtPayingAmount.Text , out dblPayingAmount);
                double.TryParse(txtTotalPayingAmount.Text, out dblTotalAmount);

                dblTotalAmount = dblTotalAmount + dblPayingAmount;
            }

            txtTotalPayingAmount.Text = dblPayingAmount.ToString();
        }

        protected void dtAnnualFee_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            Double dblStudentPaidFee=0;
            Double dblStudentAdmnFee=0;
            Double dblStudentDueAdmnFee = 0;

            if (lblTotalFee.Text.Trim() != "") dblStudentAdmnFee = Convert.ToDouble(lblTotalFee.Text);
            if (lblTotalReceived.Text.Trim() != "") dblStudentPaidFee = Convert.ToDouble(lblTotalReceived.Text);
            if (lblTotalDues.Text.Trim() != "") dblStudentDueAdmnFee = Convert.ToDouble(lblTotalDues.Text);

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TextBox objtxtAdmnAmount = (TextBox)e.Item.FindControl("txtAdmnAmount");
                TextBox objtxtPayingAmount = (TextBox)e.Item.FindControl("txtPayingAmount");
  
                Label objlblPaidAmount = (Label)e.Item.FindControl("lblPaidAmount");
                Label objlblDueAmount = (Label)e.Item.FindControl("lblDueAmount");

                objtxtPayingAmount.Attributes.Add("onkeypress", "javascript:return IsNumeric('d')");

                objtxtPayingAmount.Attributes.Add("onblur", "javascript:return AdmissionFee(this)");
                objtxtPayingAmount.Attributes.Add("onfocus", "javascript:return PrvAmount(this)");

                dblStudentPaidFee = dblStudentPaidFee + Convert.ToDouble(objlblPaidAmount.Text);
                dblStudentAdmnFee = dblStudentAdmnFee + Convert.ToDouble(objtxtAdmnAmount.Text);
                dblStudentDueAdmnFee = dblStudentDueAdmnFee + (Convert.ToDouble(objtxtAdmnAmount.Text) - Convert.ToDouble(objlblPaidAmount.Text));

                objlblDueAmount.Text = Convert.ToString(Convert.ToDouble(objtxtAdmnAmount.Text) - Convert.ToDouble(objlblPaidAmount.Text));

                if (Convert.ToDouble(objtxtAdmnAmount.Text) == Convert.ToDouble(objlblPaidAmount.Text))
                {
                    objtxtPayingAmount.Enabled = false;
                    objtxtPayingAmount.BackColor = System.Drawing.Color.DimGray;
                }
                else
                {
                    objtxtPayingAmount.BackColor = System.Drawing.Color.LightYellow;
                }
 
            }

            if (rdoAdmission.Checked)
            {
                lblTotalFee.Text = Convert.ToString(dblStudentAdmnFee);
                lblTotalReceived.Text = Convert.ToString(dblStudentPaidFee);
                txtTotalPayingAmount.Text = "0";
                lblTotalDues.Text = Convert.ToString(dblStudentDueAdmnFee);
            }

        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            if (txtRegistrationNo.Text.Trim() != "")
            {
                clearValues();

                if (rdoAdmission.Checked )
                    Binddata();
                if (rdoMisc.Checked)
                    BindMiscdata();
                if (rdoSemester.Checked)
                    BindInstallmentdata();

                btnSubmit.Enabled = true;
                rdoPaymentOption.Enabled = true;
                txtRemarks.Enabled = true; 
                btnSubmit.Text = "Save";

                if (txtTotalPayingAmount.Text == "") txtTotalPayingAmount.Text = "0";
                if (lblTotalDues.Text == "") lblTotalDues.Text = "0";

                if (rdoMisc.Checked == false)
                {
                    if (Convert.ToInt32(lblTotalDues.Text) == 0)
                    {
                        btnSubmit.Enabled = false;
                        rdoPaymentOption.Enabled = false;
                        txtRemarks.Enabled = false;
                    }
                }
            }
            else
            {
                lblMessage.Text = "Enter Registration No";
                return;
            }
        }

        protected void rdoPaymentOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSubmit.Text = "Save";
            if (rdoPaymentOption.SelectedIndex ==0)
            {
                txtBankName.Enabled = false;
                txtDraftDate.Enabled = false;
                txtDraftNumber.Enabled = false;
                txtAmount.Enabled = false;
            }
            else if (rdoPaymentOption.SelectedIndex == 1 || rdoPaymentOption.SelectedIndex == 2)
            {
                txtBankName.Enabled = true;
                txtDraftDate.Enabled = true;
                txtDraftNumber.Enabled = true;
                txtAmount.Enabled = true;
            }
            else
            {
                txtBankName.Enabled = false;
                txtDraftDate.Enabled = false;
                txtDraftNumber.Enabled = false;
                txtAmount.Enabled = false;
                btnSubmit.Text = "Proceed For Online Payment";
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            clearValues();
            txtRegistrationNo.Text = "";
            btnSubmit.Enabled = false;
            btnShow.Enabled = true;
            rdoPaymentOption.Enabled = true;
            rdoPaymentOption.SelectedIndex = 0;
            btnSubmit.Text = "Save"; 
        }

        private void clearValues()
        {
            txtBankName.Text = "";
            txtDraftDate.Text = "";
            txtDraftNumber.Text = "";
            txtAmount.Text = "";
            txtRemarks.Text = "";
            lblAcedemicYear.Text = "";
            lblFinancialYear.Text = "";
            lblName.Text = "";
            lblMessage.Text = "";

            dtAnnualFee.DataSource = null;
            dtAnnualFee.DataBind();

            dtPayingList.DataSource = null;
            dtPayingList.DataBind();

            txtTotalPayingAmount.Text = "";
            lblTotalDues.Text = "";
            lblTotalReceived.Text = "";
            lblTotalFee.Text = "";

            lblTotalDues.ToolTip = "";
            lblTotalReceived.ToolTip = "";
            lblTotalFee.ToolTip = "";

            rdoPaymentOption.SelectedIndex = 0;

            txtBankName.Enabled = false;
            txtDraftDate.Enabled = false;
            txtDraftNumber.Enabled = false;
            txtAmount.Enabled = false;
            dvPaymentOption.Visible = false;
            divOfflinePaymentOption.Visible = false;

            lblTotalDues.Visible = false;
            lblTotalReceived.Visible = false;
            lblTotalFee.Visible = false;
            txtTotalPayingAmount.Visible = false;

            dtAnnualFee.DataSource = null;
            dtAnnualFee.DataBind();

            dtSemesterFee.DataSource = null;
            dtSemesterFee.DataBind();
 
            btnSubmit.Visible = false;
            UpdatePanel1.Update();
        }
       
        protected void dtSemesterFee_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            Double dblStudentPaidFee = 0;
            Double dblStudentAdmnFee = 0;
            Double dblStudentDueAdmnFee = 0;

            if (lblTotalFee.Text.Trim() != "") dblStudentAdmnFee = Convert.ToDouble(lblTotalFee.Text);
            if (lblTotalReceived.Text.Trim() != "") dblStudentPaidFee = Convert.ToDouble(lblTotalReceived.Text);
            if (lblTotalDues.Text.Trim() != "") dblStudentDueAdmnFee = Convert.ToDouble(lblTotalDues.Text);
 
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label objlblAdmnAmount = (Label)e.Item.FindControl("lblInstallmentAmount");
                Label objlblPaidAmount = (Label)e.Item.FindControl("lblPaidAmount");

                Label objlblDueAmount = (Label)e.Item.FindControl("lblDueAmount");

                dblStudentPaidFee = dblStudentPaidFee + Convert.ToDouble(objlblPaidAmount.Text);
                dblStudentAdmnFee = dblStudentAdmnFee + Convert.ToDouble(objlblAdmnAmount.Text);
                dblStudentDueAdmnFee = dblStudentDueAdmnFee + (Convert.ToDouble(objlblAdmnAmount.Text) - Convert.ToDouble(objlblPaidAmount.Text));

                objlblDueAmount.Text = Convert.ToString(Convert.ToDouble(objlblAdmnAmount.Text) - Convert.ToDouble(objlblPaidAmount.Text));
            }

            lblTotalFee.Text = Convert.ToString(dblStudentAdmnFee);
            lblTotalReceived.Text = Convert.ToString(dblStudentPaidFee);
            txtTotalPayingAmount.Text = "0";
            lblTotalDues.Text = Convert.ToString(dblStudentDueAdmnFee);
        }
  

      // **************************** Invoice file creation at server using xml template file
        public void fnGenerateInvoiceExcel( ObjPortalUser user,string strDetail, int loopCounter, int ReceiptNo, string AccountUser, string RecptDate)
        {
            try
            {
                string strMessage = "";
                string XMLResult = "";    // USED TO GET THE HEADER TEMPLATE AND REPLACE THE HEADER FROM DB

                string TEMPLATE_FILE_INVOICE_1 =System.IO.Path.Combine(Server.MapPath("."), "Template/","Invoice_1.txt");
                string TEMPLATE_FILE_INVOICE_2 = System.IO.Path.Combine(Server.MapPath("."), "Template/", "Invoice_2.txt");
                
                string strTXT_FILE_NAME = "Invoice_" + ReceiptNo .ToString();
                string strXML_FILE_NAME = strTXT_FILE_NAME + ".xls";
                strTXT_FILE_NAME = strTXT_FILE_NAME + ".txt";

                StreamWriter SW_MAIN;

                XMLResult = Convert.ToString(System.IO.File.OpenText(TEMPLATE_FILE_INVOICE_1).ReadToEnd());
                RecptDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).ToString();
                // ****** HEADER STARTS
                XMLResult = XMLResult.Replace("##Header##", user.Orig_Name );
                XMLResult = XMLResult.Replace("##Address#", user.Orig_Address1 );
                XMLResult = XMLResult.Replace("##GST##",  user.Orig_GSTNO);
                XMLResult = XMLResult.Replace("##Name##", lblName.Text );
                XMLResult = XMLResult.Replace("##Cust_Address1##", "");
                XMLResult = XMLResult.Replace("##Location##","##User##");
                XMLResult = XMLResult.Replace("##User##", AccountUser);
                XMLResult = XMLResult.Replace("##Cust_Address2##", user.Orig_Address2);
                XMLResult = XMLResult.Replace("##Date##", RecptDate);
                XMLResult = XMLResult.Replace("##Invoice##", ReceiptNo.ToString());

 
                // ''***** HEADER ENDS
                SW_MAIN = System.IO.File.AppendText(System.IO.Path.Combine(Server.MapPath("."), "Template/", strTXT_FILE_NAME)   );
                SW_MAIN.WriteLine(XMLResult);

                SW_MAIN.WriteLine(strDetail);
                //for (int cnt = 0; cnt <= 2; cnt++)
                //{
                //    SW_MAIN.WriteLine("<Row ss:AutoFitHeight=\"0\" ss:Height=\"15.45\">");
                //    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s62\"/>");
                //    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s164\"><Data ss:Type=\"Number\">1</Data></Cell>");
                //    SW_MAIN.WriteLine("<Cell ss:MergeAcross=\"1\" ss:StyleID=\"m1716013900688\"><Data ss:Type=\"String\">course fee</Data></Cell>");
                //    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s164\"><Data ss:Type=\"Number\">4000</Data></Cell>");
                //    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s171\"/>");
                //    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s164\" ss:Formula=\"=IF(TRIM(RC[-2])=&quot;&quot;,&quot;&quot;,RC[-2]-RC[-1])\"><Data ss:Type=\"String\"></Data></Cell>");
                //    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s172\"><Data ss:Type=\"Number\">0.06</Data></Cell>");
                //    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s164\" ss:Formula=\"=IF(TRIM(RC[-2])=&quot;&quot;,&quot;&quot;,RC[-1]*RC[-2])\"><Data ss:Type=\"String\"></Data></Cell>");
                //    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s172\"><Data ss:Type=\"Number\">0.06</Data></Cell>");
                //    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s164\" ss:Formula=\"=IF(RC[-4]=&quot;&quot;,&quot;&quot;,  RC[-1]*RC[-4])\"><Data  ss:Type=\"String\"></Data></Cell>");
                //    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s172\"><Data ss:Type=\"Number\">0</Data></Cell>");
                //    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s164\" ss:Formula=\"=IF(TRIM(RC[-6])=&quot;&quot;,&quot;&quot;,RC[-6]*RC[-1])\"><Data ss:Type=\"String\"></Data></Cell>");
                //    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s62\"/>");
                //    SW_MAIN.WriteLine("</Row>");
                //}

                for (int cnt = loopCounter ; cnt <= 32; cnt++)
                {
                    SW_MAIN.WriteLine("<Row ss:AutoFitHeight=\"0\" ss:Height=\"15.45\">");
                    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s62\"/>");
                    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s164\"/>");
                    SW_MAIN.WriteLine("<Cell ss:MergeAcross=\"1\" ss:StyleID=\"m1716013900688\"/>");
                    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s164\"/>");
                    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s171\"/>");
                    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s164\" ss:Formula=\"=IF(TRIM(RC[-2])=&quot;&quot;,&quot;&quot;,RC[-2]-RC[-1])\"><Data ss:Type=\"String\"></Data></Cell>");
                    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s172\"/>");
                    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s164\" ss:Formula=\"=IF(TRIM(RC[-2])=&quot;&quot;,&quot;&quot;,RC[-1]*RC[-2])\"><Data ss:Type=\"String\"></Data></Cell>");
                    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s172\"/>");
                    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s164\" ss:Formula=\"=IF(RC[-4]=&quot;&quot;,&quot;&quot;,  RC[-1]*RC[-4])\"><Data  ss:Type=\"String\"></Data></Cell>");
                    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s172\"/>");
                    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s164\" ss:Formula=\"=IF(TRIM(RC[-6])=&quot;&quot;,&quot;&quot;,RC[-6]*RC[-1])\"><Data ss:Type=\"String\"></Data></Cell>");
                    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s62\"/>");
                    SW_MAIN.WriteLine("</Row>");
                }

                SW_MAIN.WriteLine(Convert.ToString(System.IO.File.OpenText(TEMPLATE_FILE_INVOICE_2).ReadToEnd()));
                SW_MAIN.Close();

                //convert File and delete template file
                System.IO.File.Move(System.IO.Path.Combine(Server.MapPath("."), "Template/", strTXT_FILE_NAME), System.IO.Path.Combine(Server.MapPath("."), "Uploads/Receipt/", strXML_FILE_NAME));


                if (System.IO.File.Exists(System.IO.Path.Combine(Server.MapPath("."), "Template/", strXML_FILE_NAME)))
                    System.IO.Path.Combine(Server.MapPath("."), "Template/", strTXT_FILE_NAME);
            }
            catch (Exception ex)
            {
              
            }
            finally
            {
            }
        }


        public void fnGenerateInvoice(ObjPortalUser user, string strDetail, int loopCounter, int ReceiptNo, string AccountUser, string RecptDate)
        {
            try
            {

                string stringInvoice = Convert.ToString(System.IO.File.OpenText(System.IO.Path.Combine(Server.MapPath("."), "Template/", "Invoice_Header.txt"))); 

                //Header Information
                if (stringInvoice != null && stringInvoice.Trim() != "")
                {
                    stringInvoice = stringInvoice.Replace("#ORIG_NAME#", user.Orig_Name);
                    stringInvoice = stringInvoice.Replace("#ORIG_ADDRESS1#", user.Orig_Address1);
                    stringInvoice = stringInvoice.Replace("#ORIG_ADDRESS2#", user.Orig_Address2 );
                    stringInvoice = stringInvoice.Replace("#ORIG_PHONE1#", user.Orig_Phone1 );
                    stringInvoice = stringInvoice.Replace("#ORIG_PHONE2# ", user.Orig_Phone2);
                    stringInvoice = stringInvoice.Replace("#ORIG_EMAIL1#", user.Orig_Email1);
                    stringInvoice = stringInvoice.Replace("#ORIG_URL#", user.Orig_Url);
                    stringInvoice = stringInvoice.Replace("#GST_NO#", user.Orig_GSTNO );
                    
                    stringInvoice = stringInvoice.Replace("#STUSENT_NAME#", "Nyssa Aviation Training Academy (NATA)");
                    stringInvoice = stringInvoice.Replace("#ENROLLMENT_NO#", "Nyssa Aviation Training Academy (NATA)");
                    stringInvoice = stringInvoice.Replace("#PAYMENT_REMARKS#", "Nyssa Aviation Training Academy (NATA)");
                    stringInvoice = stringInvoice.Replace("#RECEIPT_NO#", "Nyssa Aviation Training Academy (NATA)");
                    stringInvoice = stringInvoice.Replace("#STUSENT_NAME#", "Nyssa Aviation Training Academy (NATA)");
                    stringInvoice = stringInvoice.Replace("#STUSENT_NAME#", "Nyssa Aviation Training Academy (NATA)");
                }
              //  StringReader sr = new StringReader(stringInvoice);//Convert.ToString(System.IO.File.OpenText(@"C:\Users\ranjan\Desktop\desktop file\WebSite1\Template\b.html").ReadToEnd()));//sb.ToString());
                //StringReader sr = new StringReader( sb.ToString());

                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                    {
                        StringBuilder sb = new StringBuilder();
                        string stringInvoicesTR = Convert.ToString(System.IO.File.OpenText(@"C:\Users\ranjan\Desktop\desktop file\WebSite1\Template\b.html").ReadToEnd());
                        stringInvoice = stringInvoice.Replace("#ORIG_NAME#", "Nyssa Aviation Training Academy (NATA)");
                        StringReader sr = new StringReader(stringInvoicesTR);//Convert.ToString(System.IO.File.OpenText(@"C:\Users\ranjan\Desktop\desktop file\WebSite1\Template\b.html").ReadToEnd()));//sb.ToString());
                        //StringReader sr = new StringReader( sb.ToString());


                        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
                        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);

                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                        pdfDoc.Open();
                        htmlparser.Parse(sr);
                        pdfDoc.Close();
                        Response.ContentType = "application/octectstream"; ;// "application/pdf";
                        Response.AddHeader("content-disposition", "attachment;filename=Invoice_" + "10001" + ".pdf");

                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.Write(pdfDoc);
                        Response.End();
                    }
                }



                // XMLResult = XMLResult.Replace("##Header##", user.Orig_Name);
                //XMLResult = XMLResult.Replace("##Address#", user.Orig_Address1);
                //XMLResult = XMLResult.Replace("##GST##", user.Orig_GSTNO);
                //XMLResult = XMLResult.Replace("##Name##", lblName.Text);
                //XMLResult = XMLResult.Replace("##Cust_Address1##", "");
                //XMLResult = XMLResult.Replace("##Location##", "##User##");
                //XMLResult = XMLResult.Replace("##User##", AccountUser);
                //XMLResult = XMLResult.Replace("##Cust_Address2##", user.Orig_Address2);
                //XMLResult = XMLResult.Replace("##Date##", RecptDate);
                //XMLResult = XMLResult.Replace("##Invoice##", ReceiptNo.ToString());


                //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
                //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);

                //PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                //pdfDoc.Open();
                //htmlparser.Parse(sr);
                //pdfDoc.Close();
                //Response.ContentType = "application/octectstream"; ;// "application/pdf";
                //Response.AddHeader("content-disposition", "attachment;filename=Invoice_" + orderNo + ".pdf");

                //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //Response.Write(pdfDoc);
                //Response.End();


                //string strMessage = "";
                //string XMLResult = "";    // USED TO GET THE HEADER TEMPLATE AND REPLACE THE HEADER FROM DB

                //string TEMPLATE_FILE_INVOICE_1 = System.IO.Path.Combine(Server.MapPath("."), "Template/", "Invoice_1.txt");
                //string TEMPLATE_FILE_INVOICE_2 = System.IO.Path.Combine(Server.MapPath("."), "Template/", "Invoice_2.txt");

                //string strTXT_FILE_NAME = "Invoice_" + ReceiptNo.ToString();
                //string strXML_FILE_NAME = strTXT_FILE_NAME + ".xls";
                //strTXT_FILE_NAME = strTXT_FILE_NAME + ".txt";

                //StreamWriter SW_MAIN;

                //XMLResult = Convert.ToString(System.IO.File.OpenText(TEMPLATE_FILE_INVOICE_1).ReadToEnd());
                //RecptDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).ToString();
                //// ****** HEADER STARTS
                //XMLResult = XMLResult.Replace("##Header##", user.Orig_Name);
                //XMLResult = XMLResult.Replace("##Address#", user.Orig_Address1);
                //XMLResult = XMLResult.Replace("##GST##", user.Orig_GSTNO);
                //XMLResult = XMLResult.Replace("##Name##", lblName.Text);
                //XMLResult = XMLResult.Replace("##Cust_Address1##", "");
                //XMLResult = XMLResult.Replace("##Location##", "##User##");
                //XMLResult = XMLResult.Replace("##User##", AccountUser);
                //XMLResult = XMLResult.Replace("##Cust_Address2##", user.Orig_Address2);
                //XMLResult = XMLResult.Replace("##Date##", RecptDate);
                //XMLResult = XMLResult.Replace("##Invoice##", ReceiptNo.ToString());


                //// ''***** HEADER ENDS
                //SW_MAIN = System.IO.File.AppendText(System.IO.Path.Combine(Server.MapPath("."), "Template/", strTXT_FILE_NAME));
                //SW_MAIN.WriteLine(XMLResult);

                //SW_MAIN.WriteLine(strDetail);
                //for (int cnt = 0; cnt <= 2; cnt++)
                //{
                //    SW_MAIN.WriteLine("<Row ss:AutoFitHeight=\"0\" ss:Height=\"15.45\">");
                //    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s62\"/>");
                //    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s164\"><Data ss:Type=\"Number\">1</Data></Cell>");
                //    SW_MAIN.WriteLine("<Cell ss:MergeAcross=\"1\" ss:StyleID=\"m1716013900688\"><Data ss:Type=\"String\">course fee</Data></Cell>");
                //    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s164\"><Data ss:Type=\"Number\">4000</Data></Cell>");
                //    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s171\"/>");
                //    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s164\" ss:Formula=\"=IF(TRIM(RC[-2])=&quot;&quot;,&quot;&quot;,RC[-2]-RC[-1])\"><Data ss:Type=\"String\"></Data></Cell>");
                //    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s172\"><Data ss:Type=\"Number\">0.06</Data></Cell>");
                //    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s164\" ss:Formula=\"=IF(TRIM(RC[-2])=&quot;&quot;,&quot;&quot;,RC[-1]*RC[-2])\"><Data ss:Type=\"String\"></Data></Cell>");
                //    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s172\"><Data ss:Type=\"Number\">0.06</Data></Cell>");
                //    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s164\" ss:Formula=\"=IF(RC[-4]=&quot;&quot;,&quot;&quot;,  RC[-1]*RC[-4])\"><Data  ss:Type=\"String\"></Data></Cell>");
                //    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s172\"><Data ss:Type=\"Number\">0</Data></Cell>");
                //    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s164\" ss:Formula=\"=IF(TRIM(RC[-6])=&quot;&quot;,&quot;&quot;,RC[-6]*RC[-1])\"><Data ss:Type=\"String\"></Data></Cell>");
                //    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s62\"/>");
                //    SW_MAIN.WriteLine("</Row>");
                //}

                ////for (int cnt = loopCounter; cnt <= 32; cnt++)
                ////{
                ////    SW_MAIN.WriteLine("<Row ss:AutoFitHeight=\"0\" ss:Height=\"15.45\">");
                ////    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s62\"/>");
                ////    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s164\"/>");
                ////    SW_MAIN.WriteLine("<Cell ss:MergeAcross=\"1\" ss:StyleID=\"m1716013900688\"/>");
                ////    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s164\"/>");
                ////    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s171\"/>");
                ////    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s164\" ss:Formula=\"=IF(TRIM(RC[-2])=&quot;&quot;,&quot;&quot;,RC[-2]-RC[-1])\"><Data ss:Type=\"String\"></Data></Cell>");
                ////    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s172\"/>");
                ////    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s164\" ss:Formula=\"=IF(TRIM(RC[-2])=&quot;&quot;,&quot;&quot;,RC[-1]*RC[-2])\"><Data ss:Type=\"String\"></Data></Cell>");
                ////    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s172\"/>");
                ////    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s164\" ss:Formula=\"=IF(RC[-4]=&quot;&quot;,&quot;&quot;,  RC[-1]*RC[-4])\"><Data  ss:Type=\"String\"></Data></Cell>");
                ////    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s172\"/>");
                ////    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s164\" ss:Formula=\"=IF(TRIM(RC[-6])=&quot;&quot;,&quot;&quot;,RC[-6]*RC[-1])\"><Data ss:Type=\"String\"></Data></Cell>");
                ////    SW_MAIN.WriteLine("<Cell ss:StyleID=\"s62\"/>");
                ////    SW_MAIN.WriteLine("</Row>");
                ////}

                ////SW_MAIN.WriteLine(Convert.ToString(System.IO.File.OpenText(TEMPLATE_FILE_INVOICE_2).ReadToEnd()));
                ////SW_MAIN.Close();

                //////convert File and delete template file
                ////System.IO.File.Move(System.IO.Path.Combine(Server.MapPath("."), "Template/", strTXT_FILE_NAME), System.IO.Path.Combine(Server.MapPath("."), "Uploads/Receipt/", strXML_FILE_NAME));


                //if (System.IO.File.Exists(System.IO.Path.Combine(Server.MapPath("."), "Template/", strXML_FILE_NAME)))
                //    System.IO.Path.Combine(Server.MapPath("."), "Template/", strTXT_FILE_NAME);
            }
            catch (Exception ex)
            {

            }
            finally
            {
            }
        }
  
  
  }

 