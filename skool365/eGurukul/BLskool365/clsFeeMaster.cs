using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLCMR;
using System.Data.SqlClient;
 
namespace BLCMR
{
 
    public class clsFeeMaster
    {
        ///Save Amission Pass Marks Entry
        public int SaveAdmissionCriteria(string strCon, ObjAdmission admission, bool isInsert)
        {
            int i = 0;
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            if (isInsert)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@P_Session", admission.Session);
                cmd.Parameters.AddWithValue("@P_CourseId", admission.CourseID);
                cmd.Parameters.AddWithValue("@P_SubjectId", admission.SubjetID);
                cmd.Parameters.AddWithValue("@P_PassMarks", admission.PassMarks);
                cmd.Parameters.AddWithValue("@P_Status", "Active");
                cmd.Parameters.AddWithValue("@P_CreatedBy", admission.CreatedBy);
                i = objDLGeneric.InsertValueinDB("usp_InsertAdmissionCriteria", cmd, strCon);
            }
            else
            {
                //--Update statement
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@P_ID", Convert.ToInt64(admission.ID));
                cmd.Parameters.AddWithValue("@P_PassMarks", admission.PassMarks);
                cmd.Parameters.AddWithValue("@P_ModifiedBy", admission.ModifiedBy);
                i = objDLGeneric.InsertValueinDB("usp_UpdateAdmissionCriteria", cmd, strCon);
            }
            return i;
        }
        ///Show Subject Minimum Pass Marks Entry
        public DataTable ShowAdmissionPassingMarksDetails(string strCon)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dt;
                dt = objDLGeneric.SpDataTable("usp_SelectAdmissionSubject", cmd, strCon);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        ///Get Subject Minimum Pass Marks Entry
        public ObjAdmission GetSubjectPassMarksDetails(Int64 id, string strCon)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            ObjAdmission lstObjAdmission = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dt;
                cmd.Parameters.AddWithValue("@P_ID", id);
                dt = objDLGeneric.SpDataTable("usp_SelectAdmissionSubject", cmd, strCon);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr;
                    dr = dt.Rows[0];
                    lstObjAdmission = new ObjAdmission();
                    lstObjAdmission.ID = Convert.ToInt32(dr["ID"]);
                    lstObjAdmission.CourseID = Convert.ToInt32(dr["CourseId"]);
                    lstObjAdmission.SubjetID = Convert.ToInt32(dr["SubjectID"]);
                    lstObjAdmission.Session = Convert.ToString(dr["Session"]);
                    lstObjAdmission.SubjectName = Convert.ToString(dr["SubjectName"]);
                    lstObjAdmission.PassMarks = Convert.ToInt32(dr["PassMarks"]);
                    lstObjAdmission.Status = Convert.ToString(dr["Status"]);
                    lstObjAdmission.CreatedBy = Convert.ToString(dr["CreatedBy"]);
                    lstObjAdmission.CreatedDate = Convert.ToString(dr["CreatedDate"]);
                    lstObjAdmission.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                    lstObjAdmission.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
                }
            }
            catch (Exception ex)
            {
            }
            return lstObjAdmission;
        }
        ///Delete Subject Marks Entry
        public int DeleteAdmissionCriteria(ObjAdmission admission, string strCon)
        {
            int res = 0;
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@P_ID", admission.ID);
                cmd.Parameters.AddWithValue("@P_ModifiedBy", admission.ModifiedBy);
                res = objDLGeneric.ExecuteQuery("usp_DeleteAdmissionCriteria", cmd, strCon);
            }
            catch (Exception ex)
            {
            }
            return res;
        }
        ///Check Duplicate Subject Entry
        public Boolean CheckDuplicateSubjectEntry(string strOperation, string strCon, ObjAdmission admission)
        {

            SqlCommand cmd = new SqlCommand();
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            DataTable dt;


            if (strOperation.ToString() == "Save")
            {
                cmd.Parameters.AddWithValue("@P_Session", admission.Session);
                cmd.Parameters.AddWithValue("@P_SubjectID", admission.SubjetID);
                cmd.Parameters.AddWithValue("@P_CourseID", admission.CourseID);
            }
            else if (strOperation == "Update")
            {
                cmd.Parameters.AddWithValue("@P_Id", Convert.ToInt64(admission.ID));
                cmd.Parameters.AddWithValue("@P_Session", admission.Session);
                cmd.Parameters.AddWithValue("@P_SubjectID", admission.SubjetID);
                cmd.Parameters.AddWithValue("@P_CourseID", admission.CourseID);
            }


            dt = objDLGeneric.SpDataTable("usp_ValidateDuplicateSubject", cmd, strCon);

            if (dt != null)
            {
                if (dt.Rows[0][0].ToString() == "True")
                {
                    return true;
                }
                else
                    return false;
            }
            return false;
        }
        ///Academic Fee Details
        public DataSet AnnualFeeDetails(string strCon, ObjFeeMaster feemaster)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataSet ds = new DataSet();
                cmd.Parameters.AddWithValue("@P_CourseID", feemaster.CourseID);
                cmd.Parameters.AddWithValue("@P_Session", feemaster.Session);
                ds = objDLGeneric.SpDataSet("usp_SelectFeeDetailSectionSegmentWise", cmd, strCon);
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        ///Student Fee Details
        public DataSet StudentAdmissionFeeDetails(string strCon, ObjFeeMaster feemaster)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataSet ds = new DataSet();
                cmd.Parameters.AddWithValue("@P_RegistrationNo", feemaster.RegistrationNo);
                //For Receipt
                if (feemaster.Mode == "Receipt")
                {
                    cmd.Parameters.AddWithValue("@P_Mode", feemaster.Mode);
                }
                ds = objDLGeneric.SpDataSet("usp_Student_Fee_Details", cmd, strCon);
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        ///Student Semester Installemnt Details
        public DataSet StudentSemesterInstallmentDetails(string strCon, ObjFeeMaster feemaster)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataSet ds = new DataSet();
                cmd.Parameters.AddWithValue("@P_RegistrationNo", feemaster.RegistrationNo);
                ds = objDLGeneric.SpDataSet("usp_Student_Installment_Detail", cmd, strCon);
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        ///Student Semester Fee Payment Details
        public DataSet StudentSemesterFeePaymentDetails(string strCon, ObjFeeMaster feemaster)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataSet ds = new DataSet();
                cmd.Parameters.AddWithValue("@P_RegistrationNo", feemaster.RegistrationNo);
                cmd.Parameters.AddWithValue("@P_User_ID", feemaster.CreatedBy);
                ds = objDLGeneric.SpDataSet("usp_Student_Installment_Fee_Details", cmd, strCon);
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        ///Student Miscellaneous Fee Payment Details
        public DataSet StudentMiscellaneousFeePaymentDetails(string strCon, ObjFeeMaster feemaster)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataSet ds = new DataSet();
                cmd.Parameters.AddWithValue("@P_RegistrationNo", feemaster.RegistrationNo);
                cmd.Parameters.AddWithValue("@P_User_ID", feemaster.CreatedBy );
                ds = objDLGeneric.SpDataSet("usp_Student_Miscellaneous_Fee_Details", cmd, strCon);
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        ///Show Receipt
        public DataTable GetReceiptDetails(string strCon, int From, int To)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dt;
                cmd.Parameters.AddWithValue("@P_From", From);
                cmd.Parameters.AddWithValue("@P_To", To);
                dt = objDLGeneric.SpDataTable("Usp_ReceiptDetails", cmd, strCon);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //Cancel Receipt
        public DataSet GetReceiptDetails(string strCon,ObjFeeMaster feemaster)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataSet dsResult;
                cmd.Parameters.AddWithValue("@P_ReceiptNo", feemaster.ReceiptNo );
                cmd.Parameters.AddWithValue("@P_Mode", feemaster.Mode );
                dsResult = objDLGeneric.SpDataSet("USP_INS_DEL_UPD_CANCEL_RECEIPT", cmd, strCon);
                return dsResult;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable  CancelReceipt(string strCon, ObjFeeMaster feemaster)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dtResult;
                cmd.Parameters.AddWithValue("@P_ReceiptNo", feemaster.ReceiptNo);
                cmd.Parameters.AddWithValue("@P_RequestedBy", feemaster.RequestedBy);
                cmd.Parameters.AddWithValue("@P_ReasonToCancel", feemaster.ReasonToCancel);
                cmd.Parameters.AddWithValue("@P_Mode", feemaster.Mode);
                dtResult = objDLGeneric.SpDataTable("USP_INS_DEL_UPD_CANCEL_RECEIPT", cmd, strCon);
                return dtResult;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable CancelReceiptDetails(string strCon, ObjFeeMaster feemaster)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dtResult;
                cmd.Parameters.AddWithValue("@P_ReceiptNo", feemaster.ReceiptNo);
                cmd.Parameters.AddWithValue("@P_Mode", feemaster.Mode);
                dtResult = objDLGeneric.SpDataTable("USP_INS_DEL_UPD_CANCEL_RECEIPT", cmd, strCon);
                return dtResult;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable ApproveCancelrequest(string strCon, ObjFeeMaster feemaster)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dtResult;
                cmd.Parameters.AddWithValue("@P_CancelID", feemaster.CancelID);
                cmd.Parameters.AddWithValue("@P_ReceiptNo", feemaster.ReceiptNo);
                cmd.Parameters.AddWithValue("@P_Approved", feemaster.Approved);
                cmd.Parameters.AddWithValue("@P_CancelledBy", feemaster.CancelBy); 
                cmd.Parameters.AddWithValue("@P_Mode", feemaster.Mode);
                dtResult = objDLGeneric.SpDataTable("USP_INS_DEL_UPD_CANCEL_RECEIPT", cmd, strCon);
                return dtResult;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        ///Student Miscellaneous Fee(fINE) Payment Details
        public DataTable  GetInstallment_Change_Details(string strCon, ObjFeeMaster feemaster)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dtDetails = new DataTable();
                cmd.Parameters.AddWithValue("@P_RegistrationNo", feemaster.RegistrationNo);
                cmd.Parameters.AddWithValue("@P_SEGMENT", feemaster.SegmentName);
                dtDetails = objDLGeneric.SpDataTable("USP_CHANGE_INSTALLMENT_PLAN", cmd, strCon);
                return dtDetails;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        ///Student Due Details
        public DataTable getAccountDueDetails( ObjFeeMaster feemaster)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dtDetails = new DataTable();

                if (feemaster.CourseID > 0) cmd.Parameters.AddWithValue("@P_CourseID", feemaster.CourseID);
                if (feemaster.SemesterId > 0) cmd.Parameters.AddWithValue("@P_SemesterID", feemaster.SemesterId);
                if (feemaster.FromRegNO > 0) cmd.Parameters.AddWithValue("@P_FROM_REGNO", feemaster.FromRegNO);
                if (feemaster.ToRegNO > 0) cmd.Parameters.AddWithValue("@P_TO_REGNO", feemaster.ToRegNO);
                if (feemaster.Session.Trim() != "") cmd.Parameters.AddWithValue("@P_Session", feemaster.Session.Trim());
                if (feemaster.BatchId > 0) cmd.Parameters.AddWithValue("@P_BatchID", feemaster.BatchId);
                if (feemaster.SegmentName.Trim() != "") cmd.Parameters.AddWithValue("@P_SEGMENT", feemaster.SegmentName.Trim());
                if (feemaster.DateFrom.Trim() != "") cmd.Parameters.AddWithValue("@P_ASONDATE", feemaster.DateFrom.Trim());

                dtDetails = objDLGeneric.SpDataTable("USP_REPORT_STUDENT_DUES_DETAILS", cmd, feemaster.ConnectionString.Trim());
                return dtDetails;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
      
        ///Accounts Reconciliation Details
        public DataTable   getAccountReconciliationDetails(ObjFeeMaster feemaster)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataTable dtDetails = new DataTable ();
           
            try
            {
                cmd.Parameters.AddWithValue("@P_DATE_FROM", feemaster.DateFrom);
                cmd.Parameters.AddWithValue("@P_DATE_TO", feemaster.DateTo);
                cmd.Parameters.AddWithValue("@P_USER", feemaster.CreatedBy);
                cmd.Parameters.AddWithValue("@P_MODE", feemaster.Mode);
                dtDetails = objDLGeneric.SpDataTable("USP_GET_ACCOUNT_RECONCILIATION_DETAILS", cmd, feemaster.ConnectionString.Trim());
                return dtDetails;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                dtDetails = null;
                cmd = null;
                objDLGeneric  = null;
            }
        }
    
        ///Accounts Reconciliation Details
        public DataTable   submitAccountReconciliationDetails(ObjFeeMaster feemaster)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataTable dtDetails = new DataTable ();

            try
            {
                cmd.Parameters.AddWithValue("@P_DATE_FROM", feemaster.DateFrom);
                cmd.Parameters.AddWithValue("@P_DATE_TO", feemaster.DateTo);
                cmd.Parameters.AddWithValue("@P_USER", feemaster.CreatedBy);
                cmd.Parameters.AddWithValue("@P_MODE", feemaster.Mode);
                cmd.Parameters.AddWithValue("@P_CHQ_DOC_NAME", feemaster.Reconcile_Doc_Name_Chq);
                cmd.Parameters.AddWithValue("@P_NEFT_DOC_NAME", feemaster.Reconcile_Doc_Name_NEFT);
                cmd.Parameters.AddWithValue("@P_ONL_DOC_NAME", feemaster.Reconcile_Doc_Name_Online);
                cmd.Parameters.AddWithValue("@P_CASH_DOC_NAME", feemaster.Reconcile_Doc_Name_Cash);
                dtDetails = objDLGeneric.SpDataTable("USP_GET_ACCOUNT_RECONCILIATION_DETAILS", cmd, feemaster.ConnectionString.Trim());
                return dtDetails;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                dtDetails = null;
                cmd = null;
                objDLGeneric  = null;
            }
        }
    }
}
