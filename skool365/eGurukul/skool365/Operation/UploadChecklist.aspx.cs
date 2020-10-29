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

using System.Web.Script.Serialization;
 
    public partial class Operation_UploadChecklist : System.Web.UI.Page
    {

        string M_strcon = string.Empty;
        string M_UserId = string.Empty;
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

                if (IsPostBack == false)
                {
                    tblDetail.Visible = false;
                    rowThumbnail.Visible = false;

                    if (Request.QueryString.Keys.Count != 0)
                    {
                        txtRegNo.Text = Request.QueryString["RegNo"];
                        btnSubmit_Click(sender, e);
                    }

                }

                //btnUploadSubmit.Attributes.Add("OnClick", "javascript:Confirm('Do you want to continue the process?');");

            }
            catch (Exception EX)
            {
                lblMessage.Text = EX.Message;
            }
        }
        #region"Events"
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            clsCheckList objclsCheckList = new clsCheckList();
            string strAlertMessage = string.Empty;
            DataTable dtDetails;
            int RegistrationNo;

            try
            {
                int.TryParse(txtRegNo.Text, out RegistrationNo);
                objclsCheckList.RegNo = RegistrationNo;
                objclsCheckList.ConnectionString = M_strcon;

                dtDetails = objclsCheckList.getStudentDetails(objclsCheckList);

                if (dtDetails != null)
                {
                   
                    if (dtDetails.Rows.Count > 0)
                    {
                        tblDetail.Visible = true;
                        rowThumbnail.Visible = true;
                        //Student Information
                        lblRegNo.Text = dtDetails.Rows[0]["RegistrationNo"].ToString();
                        lblName.Text = dtDetails.Rows[0]["FullName"].ToString();
                        lblCourse.Text = dtDetails.Rows[0]["CourseName"].ToString();
                        lblBatch.Text = dtDetails.Rows[0]["BATCHNAME"].ToString();
                        lblSession.Text = dtDetails.Rows[0]["AcedemicYear"].ToString();
                        lblError.Text = "";

                        //Bind CheckList Header
                        bindCheckListHeader();

                        txtRegNo.Enabled = false;
                        rowupload.Visible = false;
                        rowUploadDtl.Visible = false;
                        btnUploadSubmit.Visible = false;
                      
                        if (ddlCheckList.Items.Count > 0)
                        {
                            btnUploadSubmit.Visible = true;
                            rowupload.Visible = true;
                            rowThumbnail.Visible = true;
                            rowUploadDtl.Visible = true;
                        }

                        //Get Upload Details
                        BindGrid();
                        return;
                    }
                    else
                    {
                        strAlertMessage = "No Information Found. Please check the Registration No";
                        lblError.Text = strAlertMessage;
                        lblRegNo.Text = "";
                        lblName.Text = "";
                        lblCourse.Text = "";
                        lblBatch.Text = "";
                        lblSession.Text = "";

                        tblDetail.Visible = false;
                        btnUploadSubmit.Visible = false;
                        rowupload.Visible = false;
                        rowUploadDtl.Visible = false;
                        rowThumbnail.Visible = false;
                        txtRegNo.Enabled = true;

                        //Get Upload Details
                        BindGrid();
                    }
                   
                }
                else
                {
                    tblDetail.Visible = false;
                    rowUploadDtl.Visible = false;
                    txtRegNo.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            finally
            {
                //Alert Message
                if (strAlertMessage != string.Empty && strAlertMessage.Trim() != "")
                {
                    lblMessage.Text = strAlertMessage;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + strAlertMessage + "');</script>", false);
                }
            }
        }
        protected void btnUploadSubmit_Click(object sender, EventArgs e)
        {
            clsCheckList objclsCheckList = new clsCheckList();
            clsCommon objclsCommon = new clsCommon();
            string strAlertMessage = string.Empty;
            DataTable dtDetails;
            string strExtn = string.Empty;
            string confirmValue = string.Empty;
            int RegistrationNo;
            int checkListID;
            int fileSizeDefined;

            try
            {

                lblError.Text = "";

                //uploade file validations
                strExtn = ddlCheckList.SelectedValue.ToString();
                int.TryParse(txtRegNo.Text, out RegistrationNo);
                int.TryParse(strExtn.Split('/')[0], out checkListID);
                int.TryParse(strExtn.Split('/')[2], out fileSizeDefined);

                strExtn = strExtn.Split('/')[1];
                string[] validFileTypes = strExtn.Split(';');


                if (FileUploadImage.HasFile ==false)
                {
                    lblError.ForeColor = System.Drawing.Color.Red;
                    strAlertMessage = "Please browse the file to upload.";
                    lblError.Text = strAlertMessage;
                    return;
                }

                string ext = System.IO.Path.GetExtension(FileUploadImage.PostedFile.FileName);
                bool isValidFile = false;

                for (int i = 0; i < validFileTypes.Length; i++)
                {
                    if (ext.ToUpper() == "." + validFileTypes[i].ToUpper())
                    {
                        isValidFile = true;
                        break;
                    }
                }

                if (!isValidFile)
                {
                    lblError.ForeColor = System.Drawing.Color.Red;
                    strAlertMessage = "Invalid File. Please upload a File with extension " +
                                   string.Join(",", validFileTypes);
                    lblError.Text = strAlertMessage;
                    return;
                }
 
                //File Size Validation
                int filesize = (FileUploadImage.PostedFile.ContentLength) / 1024;

                if (filesize > fileSizeDefined)
                {
                    lblError.ForeColor = System.Drawing.Color.Red;
                    strAlertMessage="The file size exceeds the allowed limit.";
                    lblError.Text = strAlertMessage;
                    return;
                }

                string strUniqueFileName = string.Empty;
                string Filepath = string.Empty;

                //if Student Photo/Signature is going to upload
                if (ddlCheckList.SelectedItem.Text.Contains("STUDENT PHOTO") == true)
                {
                    strUniqueFileName = "PH_" + lblRegNo.Text.ToString() + FileUploadImage.PostedFile.FileName.Substring(FileUploadImage.PostedFile.FileName.LastIndexOf("."));
                    Filepath = Request.MapPath(".", Request.ApplicationPath, true) + "\\Operation\\UploadedDocument\\OfflineAdmissionDoc\\Student\\Photo\\" + strUniqueFileName;
                    System.IO.File.WriteAllBytes(Filepath, FileUploadImage.FileBytes);
                }
                else if (ddlCheckList.SelectedItem.Text.Contains("STUDENT SIGNATURE") == true)
                {
                    strUniqueFileName = "SIG_" + lblRegNo.Text.ToString() + FileUploadImage.PostedFile.FileName.Substring(FileUploadImage.PostedFile.FileName.LastIndexOf("."));
                    Filepath = Request.MapPath(".", Request.ApplicationPath, true) + "\\Operation\\UploadedDocument\\OfflineAdmissionDoc\\Student\\Signature\\" + strUniqueFileName;
                    System.IO.File.WriteAllBytes(Filepath, FileUploadImage.FileBytes); 
                }
                else
                {
                    strUniqueFileName = lblRegNo.Text.ToString() + "_" + FileUploadImage.PostedFile.FileName;
                    Filepath = Server.MapPath(".") + "\\Documents\\" + strUniqueFileName;
                    System.IO.File.WriteAllBytes(Filepath, FileUploadImage.FileBytes);
                }

                //After Successful upload
                if (System.IO.File.Exists(Filepath))
                {
                    objclsCheckList.RegNo = RegistrationNo;
                    objclsCheckList.ConnectionString = M_strcon;
                    objclsCheckList.UpdateImage = strUniqueFileName;
                    objclsCheckList.CheckListID = checkListID;
                    objclsCheckList.SubmittedBy = M_UserId;
                    objclsCheckList.Mode = "I";
                    dtDetails = objclsCheckList.UploadCheckListDocument(objclsCheckList);

                    if (dtDetails != null)
                    {
                        if (dtDetails.Columns.Contains("ERROR"))
                        {
                            if (dtDetails.Rows[0]["ERROR"].ToString().Trim() != "")
                            {
                                strAlertMessage = dtDetails.Rows[0]["ERROR"].ToString().Trim();

                                //Delete Uploaded file
                                try
                                {
                                    if (System.IO.File.Exists(Filepath))
                                    {
                                        System.IO.File.Delete(Filepath);
                                    }
                                }
                                catch { }
                                return;
                            }
                        }
                        strAlertMessage = dtDetails.Rows[0]["SUCCESS"].ToString().Trim();

                        //Reresh Grid and Checklist Header
                        BindGrid();
                        bindCheckListHeader();

                        //if File in all checklist have been uoloaded
                        if (ddlCheckList.Items.Count == 0)
                        {
                            rowupload.Visible = false;
                            rowUploadDtl.Visible = false;
                            btnUploadSubmit.Visible = false; 
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            finally
            {
                objclsCheckList = null;
                objclsCommon = null;
                //Alert Message
                if (strAlertMessage != string.Empty && strAlertMessage.Trim() != "")
                {
                    lblMessage.Text = strAlertMessage;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + strAlertMessage + "');</script>", false);
                }
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearControls();
            lblMessage.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), " DisplayDiv();", true);
        }
        /// <summary>
        /// Delete CheckList uploaded Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdStyled_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }
        /// <summary>
        /// Delete/Show the Checklist Upload Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdStyled_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteRecord")
            {
                clsCheckList objclsCheckList = new clsCheckList();
                DataTable dtResult;
                string strAlertMessage = string.Empty;
                string strFileName = string.Empty;
                int checklistID;
                int regNo;
                int rowIndex;
                try
                {
                    GridViewRow oItem = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                    rowIndex = oItem.RowIndex;
 
                    int.TryParse(e.CommandArgument.ToString(), out checklistID);
                    int.TryParse(lblRegNo.Text, out regNo); 
                    strFileName = grdStyled.Rows[rowIndex].Cells[3].Text;

                    objclsCheckList.ConnectionString = M_strcon;
                    objclsCheckList.CheckListID = checklistID;
                    objclsCheckList.RegNo = regNo;
                    objclsCheckList.Mode = "D";
                    dtResult = objclsCheckList.UploadCheckListDocument(objclsCheckList);

                    if (dtResult != null)
                    {
                        if (dtResult.Columns.Contains("ERROR"))
                        {
                            if (dtResult.Rows[0]["ERROR"].ToString().Trim() != "")
                            {
                                strAlertMessage = dtResult.Rows[0]["ERROR"].ToString().Trim();
                                return;
                            }
                        }
                        strAlertMessage = dtResult.Rows[0]["SUCCESS"].ToString().Trim();
                    }

                    //Delete Uploaded File
                    try
                    {
                        string Filepath = string.Empty;

                        //if Student Photo/Signature is going to delete
                        if (grdStyled.Rows[rowIndex].Cells[1].Text.Trim()=="STUDENT PHOTO" )
                        {
                            Filepath = Request.MapPath(".", Request.ApplicationPath, true) + "\\Operation\\UploadedDocument\\OfflineAdmissionDoc\\Student\\Photo\\" + strFileName;
                        }
                        else if (grdStyled.Rows[rowIndex].Cells[1].Text.Trim()== "STUDENT SIGNATURE" )
                        {
                            Filepath = Request.MapPath(".", Request.ApplicationPath, true) + "\\Operation\\UploadedDocument\\OfflineAdmissionDoc\\Student\\Signature\\" + strFileName;
                        }
                        else
                        {
                            Filepath = Server.MapPath(".") + "\\Documents\\" + strFileName;
                        }

                        //Delete 
                        if (System.IO.File.Exists(Filepath))
                        {
                            System.IO.File.Delete(Filepath);
                        }
                    }
                    catch { }

                    //Reresh Grid and Checklist Header
                    BindGrid();
                    bindCheckListHeader();

                    //if File in all checklist have been uoloaded
                    if (ddlCheckList.Items.Count > 0)
                    {
                        rowupload.Visible = true;
                        rowUploadDtl.Visible = true;
                        btnUploadSubmit.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                }
                finally
                {
                    objclsCheckList = null;
                    dtResult = null;

                    //Alert Message
                    if (strAlertMessage != string.Empty && strAlertMessage.Trim() != "")
                    {
                        lblMessage.Text = strAlertMessage;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + strAlertMessage + "');</script>", false);
                    }

                }
            }
 
        }

        // This button click event is used to download files from gridview
        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            string  filePath =string.Empty ;
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
           // filePath = "http://localhost:1337/Checklist/Documents/"
            try
            {
                //if Student Photo/Signature is going to download
                if (grdStyled.Rows[gvrow.RowIndex].Cells[1].Text.Trim() == "STUDENT PHOTO")
                {
                    filePath = Request.MapPath(".", Request.ApplicationPath, true) + "\\Operation\\UploadedDocument\\OfflineAdmissionDoc\\Student\\Photo\\" + grdStyled.Rows[gvrow.RowIndex].Cells[3].Text;
                }
                else if (grdStyled.Rows[gvrow.RowIndex].Cells[1].Text.Trim() == "STUDENT SIGNATURE")
                {
                    filePath = Request.MapPath(".", Request.ApplicationPath, true) + "\\Operation\\UploadedDocument\\OfflineAdmissionDoc\\Student\\Signature\\" + grdStyled.Rows[gvrow.RowIndex].Cells[3].Text;
                }
                else
                {
                    filePath = Server.MapPath(".") + "\\Documents\\" + grdStyled.Rows[gvrow.RowIndex].Cells[3].Text;
                }

                if (System.IO.File.Exists(filePath) == false)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "File Not Exists", "<script>alert('File not found. Please upload it again.');</script>", false);
                    return;
                }

                //imgSrc = 'http://www.soaneemrana.com/BackSys/Checklist/Documents/' + imgSrc;
                Response.ContentType = "image/jpg";
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filePath + "\"");
                Response.TransmitFile(filePath);
                Response.End();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            finally
            { }
        }
        #endregion

        #region "Methods and Functions"
        /// <summary>
        /// Bind the Documents details uploaded against each segment
        /// </summary>
        private void BindGrid()
        {
            DataTable dtResult = new DataTable();
            DataTable dtUploads = new DataTable();
            clsCheckList objclsCheckList = new clsCheckList();
            int RegNo;
            try
            {
                lblMessage.Text = "";

                int.TryParse(txtRegNo.Text, out RegNo);
                objclsCheckList.Mode = "S";
                objclsCheckList.RegNo = RegNo;
                objclsCheckList.ConnectionString = M_strcon;

                dtResult = objclsCheckList.UploadCheckListDocument(objclsCheckList);
                dtUploads = dtResult;

                grdStyled.DataSource = dtResult;
                grdStyled.DataBind();

                dtResult = null;

                //Bind Image List Controls
                rptrUploads.DataSource = dtUploads;
                rptrUploads.DataBind();

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            finally
            {
                dtResult = null;
                objclsCheckList = null;
            }
        }
        /// <summary>
        /// Get Check List Header
        /// </summary>
        private void bindCheckListHeader()
        {
            DataTable dtResult = new DataTable();
            clsCheckList objclsCheckList = new clsCheckList();
            int RegNo;
            try
            {
                lblMessage.Text = "";
                int.TryParse(txtRegNo.Text, out RegNo);

                objclsCheckList.RegNo = RegNo;
                objclsCheckList.ConnectionString = M_strcon;
                dtResult = objclsCheckList.getCheckListName(objclsCheckList);

                ddlCheckList.DataSource = dtResult;
                ddlCheckList.DataTextField = "CHECKLIST_NAME_DESC";
                ddlCheckList.DataValueField = "ID_EXT";
                ddlCheckList.DataBind();


                //Get List of Opted Document
                dtResult = objclsCheckList.GetAdmissionDocumentProofFlag(objclsCheckList);

                ddlDocOpted.DataSource = dtResult;
                ddlDocOpted.DataTextField = "CHECKLIST_NAME";
                ddlDocOpted.DataValueField = "CHECKLIST_ID";
                ddlDocOpted.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            finally
            {
                objclsCheckList = null;
            }
        }
        /// <summary>
        /// Clear Form Controls
        /// </summary>
        private void ClearControls()
        {
            lblMessage.Text = "";
            txtRegNo.Text = "";
            lblError.Text = "";
            txtRegNo.Enabled = true;
            tblDetail.Visible = false;
            rowUploadDtl.Visible = false;
            btnSubmit.Enabled = true;

            //Refesh Grid
            BindGrid();
        }
        #endregion

        /// <summary>
        /// Quick Link
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Stag2Button_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/AdmissionSubjectCriteria.aspx?RegNo=" + lblRegNo.Text.Trim());
        }

        protected void Stag3Button_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Operation/AdmissionFeeDetails.aspx?RegNo=" + lblRegNo.Text.Trim());
        }

        protected void Stag4Button_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Operation/InstallmentDtl.aspx?RegNo=" + lblRegNo.Text.Trim());
        }

        protected void Stag5Button_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/StudentMessAllotment.aspx?RegNo=" + lblRegNo.Text.Trim());
        }

        protected void Stag6Button_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/StudentHostelAllotment.aspx?RegNo=" + lblRegNo.Text.Trim());
        }

    }
 