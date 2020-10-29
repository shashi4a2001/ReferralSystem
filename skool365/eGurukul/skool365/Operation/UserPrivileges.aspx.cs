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

public partial class Operation_UserPrivileges : System.Web.UI.Page
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
                //Bind Active Users
                bindUser();
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

        StringBuilder strQueryBuilder = new StringBuilder();
        clsUserMenuPrevilege objclsUserMenuPrevilege = new clsUserMenuPrevilege();
        ObjUserMenuPrevilege UserMenuPrevilege = new ObjUserMenuPrevilege();
        clsCommon objClsCommon = new clsCommon();
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        string confirmValue = "";
        int res, UID, menuID;
        string P_Accessible = "";

        try
        {
            //Confirmation Message
            confirmValue = objClsCommon.getConfirmVal(Request.Form["confirm_value"]);
            if (confirmValue != "Yes") return;

            int.TryParse(ddlUsers.SelectedValue, out UID);
            UserMenuPrevilege.UserId = UID;
            UserMenuPrevilege.CreatedBy = user.UserId;
            UserMenuPrevilege.DeleteQuery = "";

            foreach (TreeNode item in this.trvMenu.CheckedNodes)
            {
                if (item.ChildNodes.Count == 0 && item.Checked == true)
                {
                    if (UserMenuPrevilege.DeleteQuery.Trim() == "") UserMenuPrevilege.DeleteQuery = " DELETE FROM User_Menu_Privilege WHERE UId=" + UserMenuPrevilege.UserId;
                    if (strQueryBuilder.Length > 0) strQueryBuilder.Append(" UNION ALL ");

                    P_Accessible = "1";
                    int.TryParse(item.Value, out menuID);
                    UserMenuPrevilege.MenuId = menuID ;
                    strQueryBuilder.Append("SELECT " + UserMenuPrevilege.UserId + "," + UserMenuPrevilege.MenuId + ",");
                    strQueryBuilder.Append("  '" + P_Accessible + "', '" + UserMenuPrevilege.CreatedBy + "', getdate() ");

                }
            }
            if (strQueryBuilder.Length > 0)
            {
                UserMenuPrevilege.InsertQuery = strQueryBuilder.ToString();
                res = objclsUserMenuPrevilege.SaveUserMenuPrivelege(user.ConnectionString, UserMenuPrevilege);
                if (res > 0)
                {
                    _strMsgType = "Success";
                    _strAlertMessage = "Menu(s) is successfully assigned to the User [" + ddlUsers.SelectedItem.Text + "]";
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
            objclsUserMenuPrevilege = null;
            UserMenuPrevilege = null;
            objClsCommon = null;
            objclsErrorLogger = null;
        }
    }
    #region"Methods & Function"
    private void bindUser()
    {
        clsUserMenuPrevilege objclsUserMenuPrevilege = new clsUserMenuPrevilege();
        DataTable dtInfo;
        try
        {
            dtInfo = objclsUserMenuPrevilege.GetActiveUsers(user.ConnectionString);
            if (dtInfo != null)
            {
                ddlUsers.DataSource = dtInfo;
                ddlUsers.DataValueField = "UID";
                ddlUsers.DataTextField = "USERDESC";
                ddlUsers.DataBind();
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
        DataTable dtRolePrivilege = new DataTable();
        SqlCommand cmd = new SqlCommand();
        int moduleID;
        try
        {
            //DataTable table = new DataTable();
            //string strCon =  user.ConnectionString;
            //System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(strCon);


            //GET PRIVILEGE AT ROLE LEVEL 
            string sql = " SELECT * FROM ROLE_Menu_Privilege RM (NOLOCK)  INNER JOIN USER_MASTER UM(NOLOCK)   ON UM.ROLE_ID=RM.ROLEID   WHERE UM.UID=" + ddlUsers.SelectedValue.ToString() + " ";
            cmd.CommandText = sql;
            dtRolePrivilege = objDLGeneric.SpDataTableByCommandText(cmd, user.ConnectionString);
            sql = string.Empty;


            sql = "select menu_id, menu_name, menu_parent_id, menu_url from Menu_MASTER  ";
            sql = sql + "where( coalesce( menu_parent_id,'') ='' or coalesce( menu_url,'') ='#' )  And Status=0 Order by menu_id ";

            cmd.CommandText = sql;
            dtMenuList = objDLGeneric.SpDataTableByCommandText(cmd, user.ConnectionString);

            DataView view = new DataView(dtMenuList);
            view.RowFilter = "menu_parent_id is NULL";

            // create root node
            TreeNode root = new TreeNode("CMR");
            foreach (DataRowView row in view)
            {
                TreeNode tRoot = new TreeNode(row["menu_name"].ToString(), row["menu_id"].ToString());
                root.ChildNodes.Add(tRoot);

                int.TryParse(row["menu_id"].ToString(), out moduleID);
                InsertChildNodes(tRoot, moduleID  , dtRolePrivilege);
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
    private void InsertChildNodes(TreeNode trnode, int module_id, DataTable dtRolePrivilege)
    {
        DLCMR.DLClsGeneric objDLGeneric = new DLCMR.DLClsGeneric();
        DataTable dtMenuList = new DataTable();
        SqlCommand cmd = new SqlCommand();
        int moduleID;
        try
        {
            string sql = " select distinct menu_id, menu_name, menu_parent_id, menu_url,coalesce(Privilege.Accessible,0) as 'Accessible' ";
            sql = sql + " from Menu_MASTER  M  (NOLOCK)  left Join   ";
            sql = sql + " User_Menu_Privilege Privilege (NOLOCK) On Privilege.MenuId=M.menu_id and Privilege.UId=" + ddlUsers.SelectedValue.ToString() + " ";
            sql = sql + " where  ";
            sql = sql + " COALESCE(MENU_PARENT_ID,'')= " + module_id.ToString() + " And M.Status=0 Order by M.menu_id  ";

            cmd.CommandText = sql;
            dtMenuList = objDLGeneric.SpDataTableByCommandText(cmd, user.ConnectionString);

            DataView view = new DataView(dtMenuList);
            view.RowFilter = "menu_id is not NULL";
            foreach (DataRowView row in view)
            {
                TreeNode tr = new TreeNode(row["menu_name"].ToString(), row["menu_id"].ToString());
                if (row["Accessible"].ToString() == "1") tr.Checked = true;

                //DO NOT ALLOW TO REMOVE ROLE LEVEL RIGHTS
                if (dtRolePrivilege.Select("MenuId=" + row["menu_id"].ToString()).Length > 0)
                {

                    tr.ShowCheckBox = false;
                    tr.ToolTip = "Role Level Privilege";
                    tr.SelectAction = TreeNodeSelectAction.None;
                    tr.Text = "<div style='color:gray;'>" + tr.Text + "</div>";
                }

                int.TryParse(row["menu_id"].ToString(), out moduleID);
                InsertChildNodes(tr, moduleID , dtRolePrivilege);

                if (trnode == null)
                    trvMenu.Nodes.Add(tr);
                else
                    trnode.ChildNodes.Add(tr);
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
    protected void ddlUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
        string _strAlertMessage = string.Empty;
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objClsCommon = new clsCommon();
        try
        {
            trvMenu.Nodes.Clear();
            InsertParentNodes();

            trvMenu.ExpandAll();
            lblMessage.Text = "";
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
                objClsCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);

                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }
            objClsCommon = null;
            objclsErrorLogger = null;
        }
    }
}
 