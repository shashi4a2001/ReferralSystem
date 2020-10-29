using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLCMR;

namespace BLCMR
{
    public class clsUnderWriter
    {
        /// <summary>
        /// Get All the assignment detail mapped to student
        /// </summary>
        /// <param name="UnderWriter"></param>
        /// <returns></returns>
        public DataTable getAssignedAssignment(objUnderWriter UnderWriter)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataTable dtInfo;
            try
            {
                cmd.Parameters.AddWithValue("@P_REG_NO", UnderWriter.UnderWriterID);
                dtInfo = objDLGeneric.SpDataTable("USP_GET_ASSIGNMENT_MAPPING_DETAILS", cmd, UnderWriter.ConnectionString);
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
        /// <summary>
        /// GET Approver List who are allowed to Approve the assignment
        /// </summary>
        /// <param name="strCon"></param>
        /// <returns></returns>
        public DataTable getUWList(string strCon)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataTable dtInfo;
            try
            {
                dtInfo = objDLGeneric.SpDataTable("USP_GET_UW_LIST", cmd, strCon);
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
        /// <summary>
        /// Get Underwriter Requests
        /// </summary>
        /// <param name="UnderWriter"></param>
        /// <returns></returns>
        public DataTable getUnderwriterRequests(objUnderWriter UnderWriter)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataTable dtInfo;
            try
            {
                cmd.Parameters.AddWithValue("@P_Underwriter_ID", UnderWriter.UnderWriterID);
                cmd.Parameters.AddWithValue("@P_YEAR", UnderWriter.Year);
                if (UnderWriter.RequetID != null && UnderWriter.RequetID > 0) cmd.Parameters.AddWithValue("@P_RequestID", UnderWriter.RequetID); 
                if(UnderWriter.searchText.Trim()!="") cmd.Parameters.AddWithValue("@P_SEARCH_TEXT", UnderWriter.searchText); 
                dtInfo = objDLGeneric.SpDataTable("USP_SELECT_UNDERWRITER_REQUESTS", cmd, UnderWriter.ConnectionString);
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

        /// <summary>
        /// Get Underwriter Requests
        /// </summary>
        /// <param name="UnderWriter"></param>
        /// <returns></returns>
        public DataTable getPendingAccountStatus(objUnderWriter UnderWriter)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataTable dtInfo;
            try
            {
                cmd.Parameters.AddWithValue("@P_Underwriter_ID", UnderWriter.UnderWriterID);
                cmd.Parameters.AddWithValue("@P_YEAR", UnderWriter.Year); 
                dtInfo = objDLGeneric.SpDataTable("USP_GET_PENDING_REQUESTS", cmd, UnderWriter.ConnectionString);
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

        /// <summary>
        /// Get Remodel reason
        /// </summary>
        /// <param name="strCon"></param>
        /// <returns></returns>
        public DataTable getRemodelReason(string strCon)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataTable dtInfo;
            try
            {
                dtInfo = objDLGeneric.SpDataTable("USP_GET_REMODEL_REASON", cmd, strCon);
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

        /// <summary>
        /// Get ACCOUNT SUBMITTED IN A DUE DATE STATUS
        /// </summary>
        /// <param name="UnderWriter"></param>
        /// <returns></returns>
        public DataTable  getAccountLimitInADueDate(objUnderWriter UnderWriter)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataTable dtInfo;
            try
            {
                cmd.Parameters.AddWithValue("@P_ASSIGNMENT_ID", UnderWriter.AssignmentID);
                dtInfo = objDLGeneric.SpDataTable("USP_GET_ACCOUNT_LIMIT_ON_DUE_DATE", cmd, UnderWriter.ConnectionString);
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

        /// <summary>
        /// Get MIS Data
        /// </summary>
        /// <param name="UnderWriter"></param>
        /// <returns></returns>
        public DataSet getMISReportDetails(objUnderWriter UnderWriter)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataSet  dsInfo;
            try
            {	
                if(UnderWriter.UnderWriterID!="0")cmd.Parameters.AddWithValue("@P_Underwriter_ID", UnderWriter.UnderWriterID);
                //cmd.Parameters.AddWithValue("@P_DATE_FROM", UnderWriter.MISDateFrom);
                //cmd.Parameters.AddWithValue("@P_DATE_TO", UnderWriter.MISDateTo);
                //cmd.Parameters.AddWithValue("@P_MODE", UnderWriter.MISReporttype);
                dsInfo = objDLGeneric.SpDataSet("USP_GET_MIS_REPORT_DETAILS", cmd, UnderWriter.ConnectionString);
                return dsInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDLGeneric = null;
                cmd = null;
                dsInfo = null;
            }

        }
        /// <summary>
        /// Create UnderWriter Requests
        /// </summary>
        /// <param name="UnderWriter"></param>
        /// <returns></returns>
        public DataTable CreateUnderWriterRequest(objUnderWriter UnderWriter)
        {
            DataTable dtRequestInfo = new DataTable();
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.Parameters.AddWithValue("@P_ASSIGNMENT_ID", UnderWriter.AssignmentID);
                cmd.Parameters.AddWithValue("@P_Type", UnderWriter.Type);
                cmd.Parameters.AddWithValue("@P_Request_Type", UnderWriter.RequestType);
                cmd.Parameters.AddWithValue("@P_Account_Name", UnderWriter.AccountName);
                cmd.Parameters.AddWithValue("@P_Due_Date", UnderWriter.DueDate);
                cmd.Parameters.AddWithValue("@P_SOV_NAME", UnderWriter.SOVName);
                cmd.Parameters.AddWithValue("@P_MACHINE_IP", UnderWriter.MachineIP);
                cmd.Parameters.AddWithValue("@P_COMMENT", UnderWriter.Comment);          
                cmd.Parameters.AddWithValue("@P_UNDERWRITER_ID", UnderWriter.UnderWriterID);
                cmd.Parameters.AddWithValue("@P_REQUEST_ID", UnderWriter.RequetID);
                cmd.Parameters.AddWithValue("@P_REG_NO", UnderWriter.ReGnO);
                dtRequestInfo = objDLGeneric.SpDataTable("dbo.USP_CREATE_UNDERWRITER_REQUEST", cmd, UnderWriter.ConnectionString);
                return dtRequestInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtRequestInfo = null;
                objDLGeneric = null;
                cmd = null;
            }

        }


        /// <summary>
        /// Update UnderWriter Requests
        /// </summary>
        /// <param name="UnderWriter"></param>
        /// <returns></returns>
        public DataTable UpdateUnderWriterRequest(objUnderWriter UnderWriter)
        {
            DataTable dtRequestInfo = new DataTable();
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.Parameters.AddWithValue("@P_ASSIGNMENT_ID", UnderWriter.AssignmentID);
                cmd.Parameters.AddWithValue("@P_Type", UnderWriter.Type);
                cmd.Parameters.AddWithValue("@P_Request_Type", UnderWriter.RequestType);
                cmd.Parameters.AddWithValue("@P_Account_Name", UnderWriter.AccountName);
                cmd.Parameters.AddWithValue("@P_Due_Date", UnderWriter.DueDate);
                cmd.Parameters.AddWithValue("@P_SOV_NAME", UnderWriter.SOVName);
                cmd.Parameters.AddWithValue("@P_MACHINE_IP", UnderWriter.MachineIP);
                cmd.Parameters.AddWithValue("@P_COMMENT", UnderWriter.Comment);
                cmd.Parameters.AddWithValue("@P_UNDERWRITER_ID", UnderWriter.UnderWriterID);
                cmd.Parameters.AddWithValue("@P_REF_REQUEST_ID", UnderWriter.RequetID);
                cmd.Parameters.AddWithValue("@P_RequestID", UnderWriter.RequetID);

                dtRequestInfo = objDLGeneric.SpDataTable("dbo.USP_UPDATE_UNDERWRITER_REQUEST", cmd, UnderWriter.ConnectionString);
                return dtRequestInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtRequestInfo = null;
                objDLGeneric = null;
                cmd = null;
            }

        }


        /// <summary>
        /// Delete UnderWriter Requests
        /// </summary>
        /// <param name="UnderWriter"></param>
        /// <returns></returns>
        public DataTable DeleteUnderWriterRequest(objUnderWriter UnderWriter)
        {
            DataTable dtRequestInfo = new DataTable();
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.Parameters.AddWithValue("@P_Underwriter", UnderWriter.UnderWriter);
                cmd.Parameters.Add("@P_RequestID", UnderWriter.RequetID);

                dtRequestInfo = objDLGeneric.SpDataTable("dbo.USP_DELETE_UNDERWRITER_REQUEST", cmd, UnderWriter.ConnectionString);
                return dtRequestInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtRequestInfo = null;
                objDLGeneric = null;
                cmd = null;
            }

        }



        /// <summary>
        /// GET UW mail log
        /// </summary>
        /// <param name="UnderWriter"></param>
        /// <returns></returns>
        public DataTable getUWMailLog(objUnderWriter UnderWriter)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataTable dtInfo;
            try
            {
                cmd.Parameters.AddWithValue("@P_REQUEST_ID", UnderWriter.RequetID);
                dtInfo = objDLGeneric.SpDataTable("USP_UW_MAIL_LOG", cmd, UnderWriter.ConnectionString);
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
 
    }
}
