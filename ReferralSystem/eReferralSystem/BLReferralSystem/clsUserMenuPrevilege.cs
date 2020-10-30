using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLCMR;
using System.Data.SqlClient;

namespace BLCMR
{
    public class clsUserMenuPrevilege
    {
        public DataTable ShowRole(string strCon)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataTable dtInfo;
            try
            {
                dtInfo = objDLGeneric.SpDataTable("usp_SelectRoleMaster", cmd, strCon);
                return dtInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDLGeneric = null;
                cmd = null;
                dtInfo = null;
            }

        }


        public DataTable ShowRole(string strCon, int RoleID)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataTable dtRole;
            try
            {
                cmd.Parameters.AddWithValue("@P_ROLE_ID", RoleID);
                dtRole = objDLGeneric.SpDataTable("usp_SelectRoleMaster", cmd, strCon);
                return dtRole;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtRole = null;
                cmd = null;
                objDLGeneric = null;
            }
        }

        public int SaveRoleMenuPrivelege(string strCon, ObjUserMenuPrevilege UserMenuPrevilege)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                int res;
                UserMenuPrevilege.InsertQuery = "INSERT INTO Role_Menu_Privilege( RoleId ,MenuId , Accessible ,Created_By ,Created_Date    )" + UserMenuPrevilege.InsertQuery;
                res = objDLGeneric.ExecuteQuery(UserMenuPrevilege.DeleteQuery + "; " + UserMenuPrevilege.InsertQuery, strCon);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable GetActiveUsers(string strCon)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataTable dtInfo;
            try
            {
                dtInfo = objDLGeneric.SpDataTable("USP_GET_ACTIVE_USERS", cmd, strCon);
                return dtInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDLGeneric = null;
                cmd = null;
                dtInfo = null;
            }

        }
        public int SaveUserMenuPrivelege(string strCon, ObjUserMenuPrevilege UserMenuPrevilege)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                int res;
                UserMenuPrevilege.InsertQuery = "INSERT INTO User_Menu_Privilege( UId ,MenuId , Accessible ,Created_By ,Created_Date    )" + UserMenuPrevilege.InsertQuery;
                res = objDLGeneric.ExecuteQuery(UserMenuPrevilege.DeleteQuery + "; " + UserMenuPrevilege.InsertQuery, strCon);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDLGeneric = null;
            }

        }
    }
}
