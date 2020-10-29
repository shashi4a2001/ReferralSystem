using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLCMR;
 
    public partial class Operation_DashBoard : System.Web.UI.Page
    {
        string M_strcon = string.Empty;
        string M_UserId = string.Empty;
        int M_UId ;
        ObjPortalUser user;

        protected void Page_Load(object sender, EventArgs e)
        {
            clsErrorLogger objclsErrorLogger = new clsErrorLogger();
            clsCommon objCommon = new clsCommon();
            string _strAlertMessage = string.Empty;
        try
        {
            //Validate User Authetication Starts
            if (Context.User.Identity.IsAuthenticated)
            {
                //Check User Session and Connection String 
                if (Session["PortalUserDtl"] != null)
                {
                    user = (ObjPortalUser)Session["PortalUserDtl"];
                    ltrlDashBoardUserName.Text = user.UserName;
                    ltrlUserName.Text = user.UserName;
                    lblUserRole.Text = user.UserRole;

                    //User Log In Information
                    lblLastLogIn.Text = " " + user.LastLogInTime;
                    lblLastPasswordChange.Text = " " + user.LastPasswordChangeTime;
                    lblWorkLocation.Text = user.Orig_Code;
                    lblOrganization.Text = user.Orig_Name;

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
                //Generate All Menus
                InsertParentNodes();
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
            _strAlertMessage = ex.Message;
            objclsErrorLogger.WriteError(System.IO.Path.GetFileName(Request.Path), "", user == null ? "" : user.ConnectionString, ex);
            Response.Redirect("~/Operation/Err.aspx");
        }
        finally
        {
            objCommon = null;
            objclsErrorLogger = null;

            //Alert Message
            if (_strAlertMessage != string.Empty && _strAlertMessage.Trim() != "")
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }
        }
        }

        //For Dashboard Accessibility
        private void InsertParentNodes()
        {
            int moduleID;
            DLCMR.DLClsGeneric objDLGeneric = new DLCMR.DLClsGeneric();
            DataTable dtMenuList = new DataTable();
            DataTable dtRolePrivilege = new DataTable();
            SqlCommand cmd = new SqlCommand();

        try
        {
            //DataTable table = new DataTable();
            //string strCon = M_strcon;
            //System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(strCon);


            //GET PRIVILEGE AT ROLE LEVEL 
            string sql = " SELECT * FROM ROLE_Menu_Privilege RM (NOLOCK)  INNER JOIN USER_MASTER UM(NOLOCK)   ON UM.ROLE_ID=RM.ROLEID   WHERE UM.UID=@UID ";
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@UID", user.UId.ToString());
            dtRolePrivilege = objDLGeneric.SpDataTableByCommandText(cmd, user.ConnectionString);
            sql = string.Empty;


            sql = "select menu_id, menu_name, menu_parent_id, menu_url from Menu_MASTER  ";
            sql = sql + "where( coalesce( menu_parent_id,'') ='' or coalesce( menu_url,'') ='#' )  And Status=0 Order by menu_id ";

            cmd.CommandText = sql;
            dtMenuList = objDLGeneric.SpDataTableByCommandText(cmd, user.ConnectionString);

            DataView view = new DataView(dtMenuList);
            view.RowFilter = "menu_parent_id is NULL";

            foreach (DataRowView row in view)
            {
                int.TryParse(row["menu_id"].ToString(), out moduleID);
                InsertChildNodes(moduleID, dtRolePrivilege);
            }

            //trvMenu.Nodes.Add(root);
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
        private void InsertChildNodes(int module_id, DataTable dtRolePrivilege)
        {
            DLCMR.DLClsGeneric objDLGeneric = new DLCMR.DLClsGeneric();
            DataTable dtMenuList = new DataTable();
            SqlCommand cmd = new SqlCommand();
        int menuID;
            string str = "";

        try
        {
            string sql = " SELECT DISTINCT MENU_ID, MENU_NAME, MENU_PARENT_ID, MENU_URL,COALESCE(PRIVILEGE.ACCESSIBLE,0) AS 'ACCESSIBLE' ";
            sql = sql + " , [ICON_IMAGE], [ICON_HEIGHT], [ICON_WIDTH], [ICON_TITLE] FROM MENU_MASTER  M  (NOLOCK)  LEFT JOIN   ";
            sql = sql + " USER_MENU_PRIVILEGE PRIVILEGE (NOLOCK) ON PRIVILEGE.MENUID=M.MENU_ID AND PRIVILEGE.UID=@UID ";
            sql = sql + " WHERE  ";
            sql = sql + " COALESCE(MENU_PARENT_ID,'')= @MODULE_ID  AND M.STATUS=0 ORDER BY M.MENU_ID  ";

            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@UID", user.UId.ToString());
            cmd.Parameters.AddWithValue("@MODULE_ID", module_id.ToString());

            dtMenuList = objDLGeneric.SpDataTableByCommandText(cmd, user.ConnectionString);

            DataView view = new DataView(dtMenuList);
            view.RowFilter = "menu_id is not NULL";
            foreach (DataRowView row in view)
            {

                //DO NOT ALLOW TO REMOVE ROLE LEVEL RIGHTS
                if (row["Accessible"].ToString() == "1" || dtRolePrivilege.Select("MenuId=" + row["menu_id"].ToString()).Length > 0)
                {
                    str = "<li><a href='" + row["MENU_URL"].ToString() + "'  title='" + row["ICON_TITLE"].ToString() + "'> ";
                    str = str + "<img src='" + row["ICON_IMAGE"].ToString() + "' alt='Availables Menus in dashboard' height='" + row["ICON_HEIGHT"].ToString() + "' width='" + row["ICON_WIDTH"].ToString() + "' /><br />";
                    str = str + " <strong style='font-size: 12px;'>" + row["MENU_NAME"].ToString() + "</strong> </a></li>";
                    DashBoardShortcut.Controls.Add(new LiteralControl(str));
                }

                int.TryParse(row["menu_id"].ToString(), out menuID);
                InsertChildNodes(menuID, dtRolePrivilege);
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
    }
 