using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLCMR;
using System.Data.SqlClient;

namespace BLCMR
{
    public class clsUserMaintainance
    {

        public DataTable SaveUser( ObjUser user )
        {
            DataTable dtUser;
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (user.Mode == "C")
                {
                    //cmd.Parameters.AddWithValue("@P_User_Id", user.UserId);
                    cmd.Parameters.AddWithValue("@P_User_Name", user.UserName);
                    cmd.Parameters.AddWithValue("@P_Role_ID", user.UserRoleId);
                    cmd.Parameters.AddWithValue("@P_EmailId", user.EmailId);
                    cmd.Parameters.AddWithValue("@P_Password", user.Password);
                    cmd.Parameters.AddWithValue("@P_SUB_BRANCH", user.SubmissionBranch);
                    cmd.Parameters.AddWithValue("@P_BYPASS_LOCK_CHECK", user.AccountLock );
                    cmd.Parameters.AddWithValue("@P_REGION", user.Region);
                    cmd.Parameters.AddWithValue("@P_Status", 0);
                    cmd.Parameters.AddWithValue("@P_Created_By", user.CreatedBy);
                    dtUser = objDLGeneric.SpDataTable("USP_INSERT_USER_MASTER", cmd, user.ConnectionString);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@P_User_Name", user.UserName);
                    cmd.Parameters.AddWithValue("@P_Role_ID", user.UserRoleId);
                    cmd.Parameters.AddWithValue("@P_EmailId", user.EmailId);
                    cmd.Parameters.AddWithValue("@P_PASSWORD", user.Password);
                    cmd.Parameters.AddWithValue("@P_Status",user.Status);
                    cmd.Parameters.AddWithValue("@P_User_Id", user.UId);
                    cmd.Parameters.AddWithValue("@P_SUB_BRANCH", user.SubmissionBranch);
                    cmd.Parameters.AddWithValue("@P_BYPASS_LOCK_CHECK", user.AccountLock);
                    cmd.Parameters.AddWithValue("@P_REGION", user.Region);
                    cmd.Parameters.AddWithValue("@P_MODIFIED_BY", user.ModifiedBy);
                    cmd.Parameters.AddWithValue("@P_LoginMachine", user.LogInMachine);
                    cmd.Parameters.AddWithValue("@P_MODE", user.Mode);
                    dtUser = objDLGeneric.SpDataTable("USP_UPDATE_USER_MASTER", cmd, user.ConnectionString);
                }
                return dtUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtUser = null;
                cmd = null;
                objDLGeneric = null;
            }
        }
        public DataTable ShowUsers(ObjUser user)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataTable dtUser;
            try
            {
                if (user.SearchText.Trim() != "") cmd.Parameters.AddWithValue("@P_SEARCH_TEXT", user.SearchText.Trim());
                dtUser = objDLGeneric.SpDataTable("usp_Select_User_Master", cmd, user.ConnectionString);
                return dtUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtUser = null;
                cmd = null;
                objDLGeneric = null;
            }

        }
        public ObjUser GetUserDetails(Int64 id, string strCon)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            ObjUser lstObjUser = null;
            int UID, UserRoleId;

            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dt;
                cmd.Parameters.AddWithValue("@P_Id", id);
                dt = objDLGeneric.SpDataTable("usp_SelectUserMaster", cmd, strCon);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr;
                    dr = dt.Rows[0];
                    lstObjUser = new ObjUser();

                    int.TryParse(dr["UId"].ToString(), out UID);
                    int.TryParse(dr["RoleID"].ToString(), out UserRoleId);  
                    lstObjUser.UId = UID;
                    lstObjUser.UserId = Convert.ToString(dr["UserId"]);
                    lstObjUser.UserName = Convert.ToString(dr["UserName"]);
                    lstObjUser.UserGroupId = UserRoleId;
                    lstObjUser.EmailId = Convert.ToString(dr["EmailId"]);
                    lstObjUser.Password = Convert.ToString(dr["Password"]);
                    lstObjUser.Status = Convert.ToString(dr["Status"]);
                    lstObjUser.MobileNo = Convert.ToString(dr["MobileNo"]);
                    lstObjUser.SubmissionBranch = Convert.ToString(dr["SubmissionBranch"]);
                    lstObjUser.Region = Convert.ToString(dr["Region"]);
                    lstObjUser.FirstTimeLoggedIn = Convert.ToString(dr["FirstTimeLoggedIn"]);
                    lstObjUser.CreatedBy = Convert.ToString(dr["CreatedBy"]);
                    lstObjUser.CreatedDate = Convert.ToString(dr["CreatedDate"]);
                    lstObjUser.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                    lstObjUser.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
                    lstObjUser.UserRoleId = UserRoleId;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstObjUser;
        }
        public DataTable DeleteUser(ObjUser user )
        {
            DataTable dtUser;
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            try
            {

                cmd.Parameters.AddWithValue("@P_Id", user.UId);
                cmd.Parameters.AddWithValue("@P_MODIFIED_BY", user.ModifiedBy);
                dtUser = objDLGeneric.SpDataTable("usp_Delete_User_Master", cmd, user.ConnectionString);

                return dtUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtUser = null;
                cmd = null;
                objDLGeneric = null;
            }
        }
        public DataTable ShowRole(string strCon)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataTable dtRole;
            try
            {
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


    }
}
