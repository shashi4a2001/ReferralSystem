using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLCMR;
using BLCMR;
using System.Data.SqlClient;

namespace BLCMR
{
   public class clsCheckListProperties
    {

        private int _CheckListID;
        public int CheckListID
        {
            get { return _CheckListID; }
            set { _CheckListID = value; }
        }

        private int _FileSize;
        public int FileSize
        {
            get { return _FileSize; }
            set { _FileSize = value; }
        }

        private int _RegNo;
        public int  RegNo
        {
            get { return _RegNo; }
            set { _RegNo = value; }
        }

        private string _sqlString;
        public string sqlString
        {
            get { return _sqlString; }
            set { _sqlString = value; }
        }

        private string _SubmittedBy;
        public string SubmittedBy
        {
            get { return _SubmittedBy; }
            set { _SubmittedBy = value; }
        }

        private string _ReqForAdmission;
        public string ReqForAdmission
        {
            get { return _ReqForAdmission; }
            set { _ReqForAdmission = value; }
        }

        private string _ModifiedBy;
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }

        private string _CheckListName;
        public string CheckListName
        {
            get { return _CheckListName; }
            set { _CheckListName = value; }
        }

        private string _Extensions;
        public string Extensions
        {
            get { return _Extensions; }
            set { _Extensions = value; }
        }
 
        private string _ConnectionString;
        public string ConnectionString
        {
            get { return _ConnectionString; }
            set { _ConnectionString = value; }
        }
 
        private string _UpdateImage;
        public string  UpdateImage
        {
            get { return _UpdateImage; }
            set { _UpdateImage = value; }
        }

        private string _Mode;
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
 
    }

    public class clsCheckList : clsCheckListProperties
    {

        #region "Student Checklist"
        /// <summary>
        /// Define CheckList
        /// </summary>
        /// <param name="objclsTestimonialProperties"></param>
        /// <returns></returns>
        public DataTable   DefineCheckList(clsCheckListProperties objclsCheckListProperties)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            DataTable dtCheckList;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Parameters.AddWithValue("@P_CHECKLIST_ID", objclsCheckListProperties.CheckListID);
                cmd.Parameters.AddWithValue("@P_CHECKLIST_NAME", objclsCheckListProperties.CheckListName);
                cmd.Parameters.AddWithValue("@P_FILE_SIZE", objclsCheckListProperties.FileSize);
                cmd.Parameters.AddWithValue("@P_REQ_FOR_ADMN", objclsCheckListProperties.ReqForAdmission);
                cmd.Parameters.AddWithValue("@P_CHECKLIST_EXT", objclsCheckListProperties.Extensions);
                cmd.Parameters.AddWithValue("@P_ADDED_BY", objclsCheckListProperties.SubmittedBy);
                cmd.Parameters.AddWithValue("@P_MODIFIED_BY", objclsCheckListProperties.ModifiedBy);
                cmd.Parameters.AddWithValue("@P_MODE", objclsCheckListProperties.Mode);

                dtCheckList = objDLGeneric.SpDataTable("dbo.USP_INS_UPD_DEL_CHECKLIST_MASTER", cmd, objclsCheckListProperties.ConnectionString);
                return dtCheckList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                objDLGeneric = null;
                dtCheckList = null;
            }
        }

        /// <summary>
        /// Upload Checklist Document
        /// </summary>
        /// <param name="objclsCheckListProperties"></param>
        /// <returns></returns>
         public DataTable UploadCheckListDocument(clsCheckListProperties objclsCheckListProperties)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            DataTable dtCheckList;
            SqlCommand cmd = new SqlCommand();
            try
            {
		
                cmd.Parameters.AddWithValue("@P_CHECKLIST_ID", objclsCheckListProperties.CheckListID);
                cmd.Parameters.AddWithValue("@P_REGISTRATION_NO", objclsCheckListProperties.RegNo);
                cmd.Parameters.AddWithValue("@P_DOCUMENT_NAME", objclsCheckListProperties.UpdateImage);
                cmd.Parameters.AddWithValue("@P_UPLOADED_BY", objclsCheckListProperties.SubmittedBy);
                cmd.Parameters.AddWithValue("@P_MODE", objclsCheckListProperties.Mode);

                dtCheckList = objDLGeneric.SpDataTable("dbo.USP_INS_DEL_STUDENT_CHECKLIST_UPLOAD_DETAILS", cmd, objclsCheckListProperties.ConnectionString);
                return dtCheckList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                objDLGeneric = null;
                dtCheckList = null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objclsCheckListProperties"></param>
        /// <returns></returns>
         public DataTable getCheckListName(clsCheckListProperties objclsCheckListProperties)
         {
             DLClsGeneric objDLGeneric = new DLClsGeneric();
             DataTable dtCheckList;
             SqlCommand cmd = new SqlCommand();
             try
             {
                 cmd.Parameters.AddWithValue("@P_REGISTRATION_NO", objclsCheckListProperties.RegNo);
                 dtCheckList = objDLGeneric.SpDataTable("dbo.USP_GET_CHECKLIST_HEADER_NAME", cmd, objclsCheckListProperties.ConnectionString);
                 return dtCheckList;
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 cmd.Dispose();
                 objDLGeneric = null;
                 dtCheckList = null;
             }
         }
              
        /// <summary>
        /// Get Student Details
        /// </summary>
        /// <param name="objclsMyWalletProperties"></param>
        /// <returns></returns>
        public DataTable   getStudentDetails(clsCheckListProperties objclsCheckListProperties)
        {
            clsCommon objclsCommon = new clsCommon();
            DataTable dtDetails;
            try
            {
                dtDetails =objclsCommon.GetStudentDetails(objclsCheckListProperties.ConnectionString,objclsCheckListProperties.RegNo )  ;
                return dtDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objclsCommon = null;
                dtDetails = null;
            }
        }

        /// <summary>
        /// Get CheckList Master Details
        /// </summary>
        /// <param name="objclsMyWalletProperties"></param>
        /// <returns></returns>
        public DataTable getCheckListMasterDetails(clsCheckListProperties objclsCheckListProperties)
        {
            clsCommon objclsCommon = new clsCommon();
            DataTable dtDetails;
            try
            {
                dtDetails = objclsCommon.GetStudentDetails(objclsCheckListProperties.ConnectionString, objclsCheckListProperties.RegNo);
                return dtDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objclsCommon = null;
                dtDetails = null;
            }
        }

        /// <summary>
        /// Check whether the checklist document has uploaded or not.
        /// </summary>
        /// <param name="objclsCheckListProperties"></param>
        /// <returns></returns>
        public DataTable  checkStudentCheckListUploadStatus(clsCheckListProperties objclsCheckListProperties)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            DataTable dtCheckList;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Parameters.AddWithValue("@P_REGISTRATION_NO", objclsCheckListProperties.RegNo);
                dtCheckList = objDLGeneric.SpDataTable("dbo.USP_CHECK_STUDENT_CHECKLIST_UPLOAD_STATUS", cmd, objclsCheckListProperties.ConnectionString);

                return dtCheckList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                objDLGeneric = null;
                dtCheckList = null;
            }
        }

        /// <summary>
        /// Get List of Document opted at the time of admission
        /// </summary>
        /// <param name="RegistrationNumber"></param>
        /// <param name="strCon"></param>
        /// <returns></returns>
        public DataTable GetAdmissionDocumentProofFlag(clsCheckListProperties objclsCheckListProperties)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            DataTable dt;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Parameters.AddWithValue("@RegistrationNumber", objclsCheckListProperties.RegNo);
                dt = objDLGeneric.SpDataTable("usp_SelectAdmissionDocumentProofFlag", cmd, objclsCheckListProperties.ConnectionString);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                objDLGeneric = null;
                dt = null;
            }

        }
         
        
        #endregion
    }
}