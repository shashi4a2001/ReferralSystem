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
using System.Data;
using System.Globalization;
using System.Text;

public partial class Operation_GrdDispCnfg : System.Web.UI.Page
    {
 
        string M_strcon = string.Empty;
        string M_UserId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            clsErrorLogger objclsErrorLogger = new clsErrorLogger();
            string _strAlertMessage = string.Empty;
            try
            {
                //Check User Session and Connection String 
                if (Session["PortalUserDtl"] != null)
                {
                    ObjPortalUser user;
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
                    //Get Bind User Roles
                    bindRoles();
                    
                    //Generate All Menus
                    InsertParentNodes();

                    btnSubmit.Attributes.Add("OnClick", "javascript:Confirm('Do you really want to continue the process?');");
                }
            }
            catch (Exception ex)
            {
                _strAlertMessage = ex.Message;
                objclsErrorLogger.WriteError(System.IO.Path.GetFileName(Request.Path), "", M_strcon, ex);
                Response.Redirect("~/Operation/Err.aspx");
            }
            finally
            {
                objclsErrorLogger = null;
                
                //Alert Message
                if (_strAlertMessage != string.Empty && _strAlertMessage.Trim() != "")
                {
                    lblMessage.Text = _strAlertMessage;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
                }
            }
        }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        StringBuilder strQueryBuilder = new StringBuilder();
        clsUserMenuPrevilege objclsUserMenuPrevilege = new clsUserMenuPrevilege();
        ObjUserMenuPrevilege UserMenuPrevilege = new ObjUserMenuPrevilege();
        clsCommon objClsCommon = new clsCommon();
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();

        string _strAlertMessage = string.Empty;
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
            UserMenuPrevilege.CreatedBy = M_UserId;
            UserMenuPrevilege.DeleteQuery = "";

            foreach (TreeNode item in this.trvMenu.CheckedNodes)
            {
                if (item.ChildNodes.Count == 0 && item.Checked == true)
                {
                    if (UserMenuPrevilege.DeleteQuery.Trim() == "") UserMenuPrevilege.DeleteQuery = " DELETE FROM Role_Menu_Privilege WHERE RoleID=" + UserMenuPrevilege.RoleId;
                    if (strQueryBuilder.Length > 0) strQueryBuilder.Append(" UNION ALL ");

                    P_Accessible = "1";
                    int.TryParse(item.Value, out menuID);
                    UserMenuPrevilege.MenuId = menuID;
                    strQueryBuilder.Append("SELECT " + UserMenuPrevilege.RoleId + "," + UserMenuPrevilege.MenuId + ",");
                    strQueryBuilder.Append("  '" + P_Accessible + "', '" + UserMenuPrevilege.CreatedBy + "',  GETDATE() ");
                }
            }
            if (strQueryBuilder.Length > 0)
            {
                UserMenuPrevilege.InsertQuery = strQueryBuilder.ToString();
                res = objclsUserMenuPrevilege.SaveRoleMenuPrivelege(M_strcon, UserMenuPrevilege);

                if (res > 0)
                {
                    lblMessage.Text = "Menu(s) is successfully assigned with the Role [" + ddlUserRole.SelectedItem.Text + "]";
                }
            }
        }
        catch (Exception ex)
        {
            _strAlertMessage = ex.Message;
            objclsErrorLogger.WriteError(System.IO.Path.GetFileName(Request.Path), "", M_strcon, ex);
            Response.Redirect("~/Operation/Err.aspx");
        }
        finally
        {
            if (_strAlertMessage != string.Empty && _strAlertMessage.Trim() != "")
            {
                lblMessage.Text = _strAlertMessage;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }
            strQueryBuilder.Length = 0;
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
            try
            {
                dtInfo = objclsUserMenuPrevilege.ShowRole(M_strcon);

                if (dtInfo != null)
                {
                    ddlUserRole.DataSource = dtInfo;
                    ddlUserRole.DataValueField = "RoleID";
                    ddlUserRole.DataTextField = "RoleName";
                    ddlUserRole.DataBind();
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
        int menuID;
        DLCMR.DLClsGeneric objDLGeneric = new DLCMR.DLClsGeneric();
        DataTable dtMenuList = new DataTable();
        SqlCommand cmd = new SqlCommand();

        try
        {
            //DataTable table = new DataTable();
            //string strCon = M_strcon;
            //System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(strCon);

            string sql = "select menu_id, menu_name, menu_parent_id from DISPLAY_MASTER  (nolock) ";
            sql = sql + "where Status=0 Order by menu_id ";

            cmd.CommandText = sql;
            dtMenuList = objDLGeneric.SpDataTableByCommandText(cmd, M_strcon);

            DataView view = new DataView(dtMenuList);

            // create root node
            TreeNode root = new TreeNode("All");
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
        //DataTable table = new DataTable();
        //string strCon = M_strcon;
        //SqlCommand cmd;
        //System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(strCon);

        DLCMR.DLClsGeneric objDLGeneric = new DLCMR.DLClsGeneric();
        DataTable dtMenuList = new DataTable();
        SqlCommand cmd = new SqlCommand();
        int moduleID;
        try
        {
            string sql = " select distinct menu_id, menu_name, menu_parent_id, menu_url,coalesce(Privilege.Accessible,0) as 'Accessible' ";
            sql = sql + " from DISPLAY_MASTER  M (nolock) left Join   ";
            sql = sql + " Role_Menu_Privilege Privilege (nolock) On Privilege.MenuId=M.menu_id and Privilege.RoleId=" + ddlUserRole.SelectedValue.ToString() + " ";
            sql = sql + " where  ";
            // sql = sql + " --( coalesce(M.menu_parent_id,'') ='' or coalesce(M.menu_url,'') ='#' ) AND ";
            sql = sql + " COALESCE(MENU_PARENT_ID,'')= " + module_id.ToString() + " And M.Status=0 Order by M.menu_id  ";

            cmd.CommandText = sql;
            dtMenuList = objDLGeneric.SpDataTableByCommandText(cmd, M_strcon);

            //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sql, conn);
            //System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
            //da.Fill(table);

            DataView view = new DataView(dtMenuList);
            view.RowFilter = "menu_id is not NULL";
            foreach (DataRowView row in view)
            {
                TreeNode t = new TreeNode(row["menu_name"].ToString(), row["menu_id"].ToString());
                if (row["Accessible"].ToString() == "1") t.Checked = true;

                int.TryParse(row["menu_id"].ToString(), out moduleID);
                InsertChildNodes(t, moduleID);
                if (n == null)
                    trvMenu.Nodes.Add(t);
                else
                    n.ChildNodes.Add(t);
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
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();

        try
        {
            trvMenu.Nodes.Clear();
            InsertParentNodes();

            trvMenu.ExpandAll();
            lblMessage.Text = "";
        }
        catch (Exception ex)
        {
            _strAlertMessage = ex.Message;
            objclsErrorLogger.WriteError(System.IO.Path.GetFileName(Request.Path), "", M_strcon, ex);
            Response.Redirect("~/Operation/Err.aspx");
        }
        finally
        {
            objclsErrorLogger = null;
            //Alert Message
            if (_strAlertMessage != string.Empty && _strAlertMessage.Trim() != "")
            {
                lblMessage.Text = _strAlertMessage;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }
        }
    }
}
 