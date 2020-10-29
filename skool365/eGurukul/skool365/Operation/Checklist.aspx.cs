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

public partial class Operation_Checklist : System.Web.UI.Page
{
 

        string M_strcon = string.Empty;
        string M_UserId = string.Empty;
        ObjPortalUser user;
        clsCommon objCommon = new clsCommon();
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
                    //Display CheckList Master Details
                    BindGrid();
                }
                txtFileSize.Attributes.Add("onkeypress", "javascript:return IsNumeric( )");
                btnSubmit.Attributes.Add("OnClick", "javascript:Confirm('Do you want to continue the process?');");
            }
            catch (Exception ex)
            {
                lblMessage.Text  = ex.Message;
            }
        }

        #region "Events"

        /// <summary>
        /// Define Student CheckList Master
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            clsCheckList objclsCheckList = new clsCheckList();
            clsCommon objclsCommon = new clsCommon();
            int checklistID;
            int file_size;

            DataTable dtResult  ;
            string strAlertMessage = string.Empty;
            string confirmValue = string.Empty;

            try
            {
                //Confirmation Message
                confirmValue = objclsCommon.getConfirmVal(Request.Form["confirm_value"]);
                if (confirmValue != "Yes") return;

                int.TryParse(hdId.Value, out checklistID);
                objclsCheckList.ConnectionString = M_strcon;
                objclsCheckList.SubmittedBy = M_UserId;
                objclsCheckList.ReqForAdmission = chkReqForAdmn.Checked ==true ?"Y":"N";
                objclsCheckList.ModifiedBy = M_UserId;
                objclsCheckList.CheckListName = txtChecklistName.Text.ToUpper();
                objclsCheckList.CheckListID = checklistID;


                //Check List Extensions
                objclsCheckList.Extensions = "";
                for (int i = 0; i < ChecklistExtensions.Items.Count; i++)
                {
                    if (ChecklistExtensions.Items[i].Selected)
                    {
                        objclsCheckList.Extensions = objclsCheckList.Extensions == "" ? ChecklistExtensions.Items[i].Text.ToString() : objclsCheckList.Extensions + ";" + ChecklistExtensions.Items[i].Text.ToString();
                    }
                }

                //validation
                //Check List Name
                if (objclsCheckList.CheckListName.Trim() == "")
                {
                    strAlertMessage = "Please enter CheckList Name";
                    txtChecklistName.Focus();
                    return;
                }
                //Extensions
                if (objclsCheckList.Extensions.Trim() == "")
                {
                    strAlertMessage = "Please enter CheckList extension";
                    ChecklistExtensions.Focus();
                    return;
                }

                //File Size
                if (txtFileSize.Text.Trim() == "")
                {
                    strAlertMessage = "Please enter file size";
                    txtFileSize.Focus();
                    return;
                }
                int.TryParse(txtFileSize.Text, out file_size);

                if (file_size<=0)
                {
                    strAlertMessage = "File size should be either numeric or greater than 0";
                    txtFileSize.Focus();
                    return;
                }

                if (file_size >1024  )
                {
                    strAlertMessage = "Please enter file size should not be greater than 1024";
                    txtFileSize.Focus();
                    return;
                }

                objclsCheckList.FileSize = file_size;

                //Proceed for Operation
                if (btnSubmit.Text == "Save")
                {
                    objclsCheckList.Mode = "I";
                }
                else
                {
                    objclsCheckList.Mode = "E";
                }
                dtResult = objclsCheckList.DefineCheckList(objclsCheckList);

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

                //Refesh Page and Grid
                BindGrid();
                ClearControls();
                UpdatePanel2.Update();
            }
            catch (Exception ex)
            {
                strAlertMessage = ex.Message;
            }
            finally
            {
                objclsCheckList = null;
                dtResult = null;
                objclsCommon = null;

                //Alert Message
                if (strAlertMessage != string.Empty && strAlertMessage.Trim() != "")
                {
                    lblMessage.Text = strAlertMessage;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + strAlertMessage + "');</script>", false);
                }

            }
        }
        /// <summary>
        /// Reset data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReset_Click(object sender, EventArgs e)
        {
             ClearControls();
             lblMessage.Text = "";
        }
        /// <summary>
        /// Edit Check List Master data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdStyled_SelectedIndexChanged(object sender, EventArgs e)
        {
            int checklistID, rowIndex;
            string strExtensions = string.Empty;

            try
            {
                int.TryParse(grdStyled.SelectedValue.ToString(), out checklistID);
                hdId.Value = checklistID.ToString();
                rowIndex = grdStyled.SelectedRow.RowIndex;
                txtChecklistName.Text = grdStyled.Rows[rowIndex].Cells[1].Text;
                chkReqForAdmn.Checked = grdStyled.Rows[rowIndex].Cells[4].Text == "Y" ? true : false;
                txtFileSize.Text = grdStyled.Rows[rowIndex].Cells[3].Text;
                strExtensions = grdStyled.Rows[rowIndex].Cells[2].Text;

                txtChecklistName.ReadOnly = false;
                if (grdStyled.Rows[rowIndex].Cells[6].Text == "DO_NO_DELETE")
                {
                    txtChecklistName.ReadOnly = true;
                }


                //Extensions
                for (int i = 0; i < ChecklistExtensions.Items.Count; i++)
                {
                    if (strExtensions.Contains(ChecklistExtensions.Items[i].Text.ToString()))
                    {
                        ChecklistExtensions.Items[i].Selected = true;
                    }
                    else
                    {
                        ChecklistExtensions.Items[i].Selected = false;//Remove extension
                    }
                }

                btnSubmit.Text = "Update";

                lblMessage.Text = "";
                UpdatePanel1.Update();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "setFocusToEntryTab('EntryTab')", true);
        }
        /// <summary>
        /// Deete Master CheckList Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdStyled_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            clsCheckList objclsCheckList = new clsCheckList();
            DataTable dtResult;
            string strAlertMessage = string.Empty;
            int checklistID;
            try
            {
                int.TryParse(grdStyled.DataKeys[e.RowIndex].Value.ToString(), out checklistID); ;

                if (grdStyled.Rows[e.RowIndex].Cells[6].Text == "DO_NO_DELETE")
                {
                    strAlertMessage = "Master Checklist record(s) can not be deleted";
                    return;
                }

                objclsCheckList.ConnectionString = M_strcon;
                objclsCheckList.CheckListID = checklistID;
                objclsCheckList.Mode = "D";
                dtResult = objclsCheckList.DefineCheckList(objclsCheckList);

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

                //Refesh Page and Grid
                BindGrid();
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
        #endregion
        #region "Methods and Function"
        /// <summary>
        /// Clear Form
        /// </summary>
        private void ClearControls()
        {
            hdId.Value = "0";
            txtChecklistName.Text = "";
            txtChecklistName.Enabled = true;
            txtFileSize.Text = "";
            chkReqForAdmn.Checked = false;

            //Remove extension
            for (int i = 0; i < ChecklistExtensions.Items.Count; i++)
            {
                if (ChecklistExtensions.Items[i].Selected)
                {
                    ChecklistExtensions.Items[i].Selected = false;
                }
            }
            txtChecklistName.Focus();
            btnSubmit.Text = "Save";
        }
        /// <summary>
        /// Refresh Grid
        /// </summary>
        private void BindGrid()
        {
            clsCheckList objclsCheckList = new clsCheckList();
            DataTable dtResult;
            try
            {
                objclsCheckList.ConnectionString = M_strcon;
                objclsCheckList.Mode = "S";
                dtResult = objclsCheckList.DefineCheckList(objclsCheckList);
                grdStyled.DataSource = dtResult;
                grdStyled.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            finally
            {
                objclsCheckList = null;
                dtResult = null;
            }
        }
 
        protected void grdStyled_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[6].Text == "DO_NO_DELETE")
                {
                    e.Row.Cells[9].Enabled = false;
                    e.Row.BackColor = System.Drawing.Color.LightGray;
                }
            }

        }
        #endregion

    }
 