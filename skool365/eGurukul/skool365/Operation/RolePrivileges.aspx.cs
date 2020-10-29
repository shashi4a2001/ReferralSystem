using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLCMR;
using System.Configuration;
using System.Globalization;
using System.Text;

public partial class Operation_RolePrivileges : System.Web.UI.Page
{

    ObjPortalUser user;
    string _strAlertMessage = string.Empty;
    string _strMsgType = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        clsCommon objCommon = new clsCommon();
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
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
                //Get Bind User Roles
                bindRoles();
                //Generate All Menus
                InsertParentNodes();

                btnSubmit.Attributes.Add("OnClick", "javascript:Confirm('Do you really want to continue the process?');");
            }

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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        StringBuilder strQueryBuilderAcc = new StringBuilder();
        StringBuilder strQueryBuilder = new StringBuilder();
        clsUserMenuPrevilege objclsUserMenuPrevilege = new clsUserMenuPrevilege();
        ObjUserMenuPrevilege UserMenuPrevilege = new ObjUserMenuPrevilege();
        clsCommon objClsCommon = new clsCommon();
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();

        string confirmValue = string.Empty;
        int res, roleID, menuID;
        string P_Accessible = string.Empty;
        try
        {
            //Confirmation Message
            confirmValue = objClsCommon.getConfirmVal(Request.Form["confirm_value"]);
            if (confirmValue != "Yes") return;

            int.TryParse(ddlUserRole.SelectedValue, out roleID);
            UserMenuPrevilege.RoleId = roleID;
            UserMenuPrevilege.CreatedBy = user.UserId;
            UserMenuPrevilege.DeleteQuery = "";

            foreach (TreeNode item in this.trvMenu.CheckedNodes)
            {
                if (item.ChildNodes.Count == 0 && item.Checked == true)
                {
                    if (UserMenuPrevilege.DeleteQuery.Trim() == "") UserMenuPrevilege.DeleteQuery = " DELETE FROM Role_Menu_Privilege WHERE RoleID=" + UserMenuPrevilege.RoleId;
                    if (strQueryBuilder.Length > 0) strQueryBuilder.Append(" UNION ALL ");

                    P_Accessible = "1";
                    int.TryParse(item.Value, out menuID);
                    UserMenuPrevilege.MenuId = menuID ;
                    strQueryBuilder.Append("SELECT " + UserMenuPrevilege.RoleId + "," + UserMenuPrevilege.MenuId + ",");
                    strQueryBuilder.Append("  '" + P_Accessible + "', '" + UserMenuPrevilege.CreatedBy + "',  GETDATE() ");
                }
            }
            if (strQueryBuilder.Length > 0)
            {

                //For Landing Page Configuration
                strQueryBuilderAcc.Append(" UPDATE ROLE_MASTER SET LANDING_PG_NAME='" + ddlLandingPage.SelectedValue.Trim() + "' ");
                strQueryBuilderAcc.Append(" ,ACCESS_DASHBAORD = " + (chkAccessDashboard.Checked==true? "'Y'":"'N'") );
                strQueryBuilderAcc.Append(" ,CHANGE_PASSWORD = " + (chkChangePassword.Checked == true ? "'Y'" : "'N'") );
                strQueryBuilderAcc.Append(" ,SELECT_UW = " + (chkSelectUW.Checked == true ? "'Y'" : "'N'"));
                strQueryBuilderAcc.Append(" ,MODIFIED_BY ='" + UserMenuPrevilege.CreatedBy + "' ,MODIFIED_date=GETDATE() " );
                strQueryBuilderAcc.Append("  WHERE ROLEID=" + UserMenuPrevilege.RoleId);
                UserMenuPrevilege.DeleteQuery= UserMenuPrevilege.DeleteQuery + "; " + strQueryBuilderAcc.ToString();

                UserMenuPrevilege.InsertQuery = strQueryBuilder.ToString();
                res = objclsUserMenuPrevilege.SaveRoleMenuPrivelege(user.ConnectionString, UserMenuPrevilege);

                if (res > 0)
                {
                    _strMsgType = "Success";
                    _strAlertMessage = "Menu(s) is successfully assigned with the Role [" + ddlUserRole.SelectedItem.Text + "]";
                }
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
            if (_strAlertMessage != string.Empty && _strAlertMessage.Trim() != "")
            {
                lblMessage.Text = _strAlertMessage;
                objClsCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);

                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }
            strQueryBuilder.Length = 0;
            strQueryBuilderAcc.Length = 0;
            objclsUserMenuPrevilege = null;
            UserMenuPrevilege = null;
            objClsCommon = null;
            objclsErrorLogger = null;
        }
    }
    #region"Methods & Function"
    private void bindRoles()
    {
        clsUserMenuPrevilege objclsUserMenuPrevilege = new clsUserMenuPrevilege();
        DataTable dtInfo;
        int RoleID;
        try
        {
            dtInfo = objclsUserMenuPrevilege.ShowRole(user.ConnectionString);

            if (dtInfo != null)
            {
                ddlUserRole.DataSource = dtInfo;
                ddlUserRole.DataValueField = "RoleID";
                ddlUserRole.DataTextField = "RoleName";
                ddlUserRole.DataBind();



                int.TryParse(ddlUserRole.SelectedValue, out RoleID);
                dtInfo = objclsUserMenuPrevilege.ShowRole(user.ConnectionString, RoleID);

                if (dtInfo != null)
                {
                    ddlLandingPage.SelectedValue = dtInfo.Rows[0]["LANDING_PG_NAME"].ToString();
                    chkAccessDashboard.Checked = dtInfo.Rows[0]["ACCESS_DASHBAORD"].ToString() == "Y" ? true : false;
                    chkChangePassword.Checked = dtInfo.Rows[0]["CHANGE_PASSWORD"].ToString() == "Y" ? true : false;
                    chkSelectUW.Checked = dtInfo.Rows[0]["SELECT_UW"].ToString() == "Y" ? true : false;
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dtInfo = null;
            objclsUserMenuPrevilege = null;
        }
    }

    private void InsertParentNodes()
    {
        DLCMR.DLClsGeneric objDLGeneric = new DLCMR.DLClsGeneric();
        DataTable dtMenuList = new DataTable();
        SqlCommand cmd = new SqlCommand();
        int menuID;
        try
        {
            //DataTable table = new DataTable();
            //string strCon =  user.ConnectionString;
            //System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(strCon);

            string sql = "select menu_id, menu_name, menu_parent_id, menu_url from MENU_MASTER  (nolock) ";
            sql = sql + "where( coalesce( menu_parent_id,'') ='' or coalesce( menu_url,'') ='#' )  And Status=0 Order by menu_id ";

            cmd.CommandText = sql;
            dtMenuList = objDLGeneric.SpDataTableByCommandText(cmd, user.ConnectionString);

            DataView view = new DataView(dtMenuList);
            view.RowFilter = "menu_parent_id is NULL";

            // create root node
            TreeNode root = new TreeNode(user.Orig_Name);
            foreach (DataRowView row in view)
            {
                TreeNode tRoot = new TreeNode(row["menu_name"].ToString(), row["menu_id"].ToString());
                root.ChildNodes.Add(tRoot);

                int.TryParse(row["menu_id"].ToString(), out menuID);
                InsertChildNodes(tRoot, menuID);
            }

            trvMenu.Nodes.Add(root);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDLGeneric = null;
            dtMenuList = null;
            cmd = null;
        }
    }
    private void InsertChildNodes(TreeNode n, int module_id)
    {
        DLCMR.DLClsGeneric objDLGeneric = new DLCMR.DLClsGeneric();
        DataTable dtMenuList = new DataTable();
        SqlCommand cmd = new SqlCommand();
        int moduleID;
        try
        {
            string sql = " select distinct menu_id, menu_name, menu_parent_id, menu_url,coalesce(Privilege.Accessible,0) as 'Accessible' ";
            sql = sql + " from MENU_MASTER  M (nolock) left Join   ";
            sql = sql + " Role_Menu_Privilege Privilege (nolock) On Privilege.MenuId=M.menu_id and Privilege.RoleId=" + ddlUserRole.SelectedValue.ToString() + " ";
            sql = sql + " where  ";
            sql = sql + " COALESCE(MENU_PARENT_ID,'')= " + module_id.ToString() + " And M.Status=0 Order by M.menu_id  ";

            cmd.CommandText = sql;
            dtMenuList = objDLGeneric.SpDataTableByCommandText(cmd, user.ConnectionString);

            //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sql, conn);
            //System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
            //da.Fill(table);

            DataView view = new DataView(dtMenuList);
            view.RowFilter = "menu_id is not NULL";
            foreach (DataRowView row in view)
            {
                TreeNode tr = new TreeNode(row["menu_name"].ToString(), row["menu_id"].ToString());
                if (row["Accessible"].ToString() == "1") tr.Checked = true;

                int.TryParse(row["menu_id"].ToString(), out moduleID);
                InsertChildNodes(tr, moduleID );
                if (n == null)
                    trvMenu.Nodes.Add(tr);
                else
                    n.ChildNodes.Add(tr);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDLGeneric = null;
            dtMenuList = null;
            cmd = null;
        }
    }

    protected void trvMenu_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    {
        if (e != null && e.Node != null)
        {
            if (e.Node.ChildNodes.Count > 0)
            {
                CheckTreeNodeRecursive(e.Node, e.Node.Checked);
            }
        }
    }
    private void CheckTreeNodeRecursive(TreeNode parent, bool fCheck)
    {
        foreach (TreeNode child in parent.ChildNodes)
        {
            if (child.Checked != fCheck)
            {
                child.Checked = fCheck;
            }
            if (child.ChildNodes.Count > 0)
            {
                CheckTreeNodeRecursive(child, fCheck);
            }
        }
    }
    #endregion
    protected void ddlUserRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        string _strAlertMessage = string.Empty;
        DataTable dtInfo;
        clsUserMenuPrevilege objclsUserMenuPrevilege = new clsUserMenuPrevilege();
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objclsCommon = new clsCommon();
        int RoleID;
        try
        {
            trvMenu.Nodes.Clear();
            InsertParentNodes();

            trvMenu.ExpandAll();
            lblMessage.Text = "";

            int.TryParse(ddlUserRole.SelectedValue, out RoleID);
            dtInfo = objclsUserMenuPrevilege.ShowRole(user.ConnectionString, RoleID);

            if (dtInfo != null)
            {
                ddlLandingPage.SelectedValue = dtInfo.Rows[0]["LANDING_PG_NAME"].ToString();
                chkAccessDashboard.Checked = dtInfo.Rows[0]["ACCESS_DASHBAORD"].ToString() == "Y" ? true : false;
                chkChangePassword.Checked = dtInfo.Rows[0]["CHANGE_PASSWORD"].ToString() == "Y" ? true : false;
                chkSelectUW.Checked = dtInfo.Rows[0]["SELECT_UW"].ToString() == "Y" ? true : false;
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
                objclsCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);

                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }
            dtInfo = null;
            objclsUserMenuPrevilege = null;
            objclsErrorLogger = null;
            objclsCommon = null;
        }
    }
}