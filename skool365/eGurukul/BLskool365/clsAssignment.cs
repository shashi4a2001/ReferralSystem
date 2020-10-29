using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLCMR;
using System.Data.SqlClient;


namespace BLCMR
{

    public class clsAssignments
    {
        public DataTable SaveAssignments(EC_Assignments objEC_Assignments, bool isInsert)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataTable dtResult=new DataTable();
            try
            {
                if (isInsert)
                {
                    if (objEC_Assignments.AssignmentId > 0) cmd.Parameters.AddWithValue("@P_AssignmentId", objEC_Assignments.AssignmentId);
                    cmd.Parameters.AddWithValue("@P_AssignmentTitle", objEC_Assignments.AssignmentTitle);
                    cmd.Parameters.AddWithValue("@P_AssignmentSummary", objEC_Assignments.AssignmentSummary);
                    cmd.Parameters.AddWithValue("@P_AssignmentType", objEC_Assignments.AssignmentType);
                    cmd.Parameters.AddWithValue("@P_AttachmentPath", objEC_Assignments.AttachmentPath);
                    cmd.Parameters.AddWithValue("@P_PostedDate", objEC_Assignments.PostedDate);
                    cmd.Parameters.AddWithValue("@P_CreatedBy", objEC_Assignments.CreatedBy);
                    cmd.Parameters.AddWithValue("@P_Is_ForLifeTime", objEC_Assignments.Is_ForLifeTime);
                    cmd.Parameters.AddWithValue("@P_Is_Submission_Required", objEC_Assignments.Is_Submission_Required);
                    if(objEC_Assignments.AvailableTillDate!="")
                        cmd.Parameters.AddWithValue("@P_Active_Till_Date", objEC_Assignments.AvailableTillDate);
                    cmd.Parameters.AddWithValue("@P_Grid_Qry", objEC_Assignments.GrdiSrchInput);
                    dtResult = objDLGeneric.SpDataTable("usp_InsertAssignments", cmd, objEC_Assignments.ConnectionString);
                }

                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDLGeneric = null;
                cmd = null;
                dtResult = null;
            }
        }


        public DataTable ShowAssignments(EC_Assignments objEC_Assignments)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataTable dt;
            try
            {
                cmd.Parameters.AddWithValue("@P_SearchText", objEC_Assignments.GrdiSrchInput);
                dt = objDLGeneric.SpDataTable("usp_SelectAssignments", cmd, objEC_Assignments.ConnectionString);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDLGeneric = null;
                cmd = null;
                dt = null;
            }

        }

        public DataSet  Assignment_Mapping_Details(EC_Assignments objEC_Assignments)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataSet ds;
            try
            {
                cmd.Parameters.AddWithValue("@P_RegNO", objEC_Assignments.RegNO);
                cmd.Parameters.AddWithValue("@P_Mode", objEC_Assignments.Mode);
                cmd.Parameters.AddWithValue("@P_SearchText", objEC_Assignments.GrdiSrchInput);
                ds = objDLGeneric.SpDataSet("usp_Assignment_Mapping_Details", cmd, objEC_Assignments.ConnectionString);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDLGeneric = null;
                cmd = null;
                ds = null;
            }

        }

        public DataTable Map_UnMap_Assignments(EC_Assignments objEC_Assignments)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataTable dt;
            try
            {
                cmd.Parameters.AddWithValue("@P_RegNO", objEC_Assignments.RegNO);
                cmd.Parameters.AddWithValue("@P_Assgnment_Ids", objEC_Assignments.AssgnmentIds);
                cmd.Parameters.AddWithValue("@P_Mode", objEC_Assignments.Mode);
                cmd.Parameters.AddWithValue("@P_USER", objEC_Assignments.CreatedBy); 
                dt = objDLGeneric.SpDataTable("usp_Add_Remove_Assignments", cmd, objEC_Assignments.ConnectionString);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDLGeneric = null;
                cmd = null;
                dt = null;
            }

        }

        public DataTable Activate_Assignments(EC_Assignments objEC_Assignments)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataTable dt;
            try
            {
                cmd.Parameters.AddWithValue("@P_RegNO", objEC_Assignments.RegNO);
                cmd.Parameters.AddWithValue("@P_Assgnment_Id", objEC_Assignments.AssignmentId);
                cmd.Parameters.AddWithValue("@P_Mode", objEC_Assignments.Mode);
                cmd.Parameters.AddWithValue("@P_USER", objEC_Assignments.CreatedBy);
                dt = objDLGeneric.SpDataTable("usp_Add_Remove_Assignments", cmd, objEC_Assignments.ConnectionString);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDLGeneric = null;
                cmd = null;
                dt = null;
            }

        }


        public DataTable ShowAssignmentsByType(string Type, EC_Assignments objEC_Assignments)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataTable dt;

            try
            {
                cmd.Parameters.AddWithValue("@AssignmentType", Type);
                cmd.Parameters.AddWithValue("@RegNo", objEC_Assignments.RegNO);
                dt = objDLGeneric.SpDataTable("usp_SelectAssignmentsByType", cmd, objEC_Assignments.ConnectionString);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDLGeneric = null;
                cmd = null;
                dt = null;
            }
        }

        public DataTable DeleteAssignments(EC_Assignments objEC_Assignments)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataTable dtResult=new DataTable() ;
            try
            {
                cmd.Parameters.AddWithValue("@P_AssignmentId", objEC_Assignments.AssignmentId);
                cmd.Parameters.AddWithValue("@P_UserID", objEC_Assignments.CreatedBy);
                dtResult = objDLGeneric.SpDataTable("usp_DeleteAssignments", cmd, objEC_Assignments.ConnectionString);
                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDLGeneric = null;
                cmd = null;
                dtResult = null;
            }
        }

        public DataTable Add_Remove_Assignments_Marks(EC_Assignments objEC_Assignments)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataTable dtResult = new DataTable();
            try
            {		
                cmd.Parameters.AddWithValue("@P_AssignmentId", objEC_Assignments.AssignmentId);
                cmd.Parameters.AddWithValue("@P_UserID", objEC_Assignments.CreatedBy);
                cmd.Parameters.AddWithValue("@P_REG_NO", objEC_Assignments.RegNO);
                cmd.Parameters.AddWithValue("@P_Mode", objEC_Assignments.Mode);
               if(objEC_Assignments.Reading!="") cmd.Parameters.AddWithValue("@P_READING", objEC_Assignments.Reading);
               if (objEC_Assignments.Listening != "") cmd.Parameters.AddWithValue("@P_LISTENING", objEC_Assignments.Listening);
               if (objEC_Assignments.Writing1 != "") cmd.Parameters.AddWithValue("@P_WRITING1", objEC_Assignments.Writing1);
               if (objEC_Assignments.Writing2 != "") cmd.Parameters.AddWithValue("@P_WRITING2", objEC_Assignments.Writing2);
               if (objEC_Assignments.Speaking != "") cmd.Parameters.AddWithValue("@P_SPEAKING", objEC_Assignments.Speaking);
               if (objEC_Assignments.NoteRemarks != "") cmd.Parameters.AddWithValue("@P_Remarks", objEC_Assignments.NoteRemarks);
               
                dtResult = objDLGeneric.SpDataTable("USP_Add_Remove_Assignments_Marks", cmd, objEC_Assignments.ConnectionString);
                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDLGeneric = null;
                cmd = null;
                dtResult = null;
            }
        }
        
    }

    public class EC_Assignments
    {

        private int _Assignmentid;
        public int AssignmentId
        {
            get { return _Assignmentid; }
            set { _Assignmentid = value; }
        }

        private string _AssignmentTitle;
        public string AssignmentTitle
        {
            get { return _AssignmentTitle; }
            set { _AssignmentTitle = value; }
        }


        private string _AssignmentType;
        public string AssignmentType
        {
            get { return _AssignmentType; }
            set { _AssignmentType = value; }
        }


        private string _AssignmentSummary;
        public string AssignmentSummary
        {
            get { return _AssignmentSummary; }
            set { _AssignmentSummary = value; }
        }

        private string _AttachmentPath;
        public string AttachmentPath
        {
            get { return _AttachmentPath; }
            set { _AttachmentPath = value; }
        }

        private string _PostedDate;
        public string PostedDate
        {
            get { return _PostedDate; }
            set { _PostedDate = value; }
        }
        private string _GrdiSrchInput;
        public string GrdiSrchInput
        {
            get { return _GrdiSrchInput; }
            set { _GrdiSrchInput = value; }
        }

        private string _CreatedBy;
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        private string _CreatedDate;
        public string CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }

        private int _regNO;
        public int RegNO
        {
            get { return _regNO; }
            set { _regNO = value; }
        }

        private string _connectionString;
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        private string _Is_ForLifeTime;
        public string Is_ForLifeTime
        {
            get { return _Is_ForLifeTime; }
            set { _Is_ForLifeTime = value; }
        }

        private string _Is_Submission_Required;
        public string Is_Submission_Required
        {
            get { return _Is_Submission_Required; }
            set { _Is_Submission_Required = value; }
        }

        private string _AvailableTillDate;
        public string AvailableTillDate
        {
            get { return _AvailableTillDate; }
            set { _AvailableTillDate = value; }
        }

        private string _Mode;
        public string  Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }

        private string _AssgnmentIds;
        public string AssgnmentIds
        {
            get { return _AssgnmentIds; }
            set { _AssgnmentIds = value; }
        }


        private string _Listening;
        public string Listening
        {
            get { return _Listening; }
            set { _Listening = value; }
        }

        private string _Speaking;
        public string Speaking
        {
            get { return _Speaking; }
            set { _Speaking = value; }
        }

        private string _Writing1;
        public string Writing1
        {
            get { return _Writing1; }
            set { _Writing1 = value; }
        }

        private string _Writing2;
        public string Writing2
        {
            get { return _Writing2; }
            set { _Writing2 = value; }
        }

        private string _Reading;
        public string Reading
        {
            get { return _Reading; }
            set { _Reading = value; }
        }

        private string _NoteRemarks;
        public string NoteRemarks
        {
            get { return _NoteRemarks; }
            set { _NoteRemarks = value; }
        }
    }
    
}
