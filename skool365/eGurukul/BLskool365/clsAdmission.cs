using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLCMR;
using System.Data.SqlClient;
namespace BLCMR
{
    public class clsAdmission 
    {

        public int SaveAdmissionCriteria(string strCon, ObjAdmission admission,bool isInsert)
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
                cmd.Parameters.AddWithValue("@IsMendatoryForDiplomaStudent", admission.MendatoryForDiplomaStudent);
                i = objDLGeneric.InsertValueinDB("usp_InsertAdmissionCriteria", cmd, strCon);
            }
            else
            {
                //--Update statement
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@P_ID", Convert.ToInt64(admission.ID));
                cmd.Parameters.AddWithValue("@P_PassMarks", admission.PassMarks);
                cmd.Parameters.AddWithValue("@P_ModifiedBy", admission.ModifiedBy);
                cmd.Parameters.AddWithValue("@IsMendatoryForDiplomaStudent", admission.MendatoryForDiplomaStudent);
                i = objDLGeneric.InsertValueinDB("usp_UpdateAdmissionCriteria", cmd, strCon);
             
            }

            return i;
        }
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
                    lstObjAdmission.IsMendatoryForDiplomaStudent = Convert.ToInt32(dr["IsMendatoryForDiplomaStudent"]);
                }
            }
            catch (Exception ex)
            {
            }
            return lstObjAdmission;
        }
        public int DeleteAdmissionCriteria(ObjAdmission admission, string strCon)
        {
            int res=0;
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
                if (dt.Rows[0][0].ToString()=="True"  )
                {
                    return true;
                }
                else
                    return false;
            }
            return false;
        }
        
        //Student Admission Details
        public DataTable StudentAdmissionDetails(string strCon )
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dt;
                dt = objDLGeneric.SpDataTable("usp_StudentAdmissionDetails", cmd, strCon);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataTable StudentAdmissionDetails(string strCon, int CourseID, string strSession)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            DataTable dt = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@P_SESSIONID", strSession);
                cmd.Parameters.AddWithValue("@P_COURSEID", CourseID);
                dt = objDLGeneric.SpDataTable("USP_GET_STUDENT_ADMISSION_DETAILS", cmd, strCon);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            return dt;
        } 
        //Student Admission Details
        public DataTable StudentAdmissionDetails(string strCon,string strRegNo)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dt;
                cmd.Parameters.AddWithValue("@P_RegistrationNo", strRegNo);
                dt = objDLGeneric.SpDataTable("usp_StudentAdmissionDetails", cmd, strCon);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        //Old Student Admission Details
        public DataTable OldStudentAdmissionDetails(string strCon, string strRegNo)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dt;
                cmd.Parameters.AddWithValue("@P_RegistrationNo", strRegNo);
                dt = objDLGeneric.SpDataTable("usp_OldStudentAdmissionDetails", cmd, strCon);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        //Update Student Photos
        public int uploadPhotos(string strCon,ObjStudentRegistrationInfo registration)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                int Res;
                cmd.Parameters.AddWithValue("@P_RegNo", registration.RegistrationNumber);
                cmd.Parameters.AddWithValue("@P_StudentImage", registration.ImageName);
                cmd.Parameters.AddWithValue("@P_Signature", registration.SignatureName);
                Res = objDLGeneric.ExecuteQuery("usp_UpdateStudentPhoto", cmd, strCon);
                return Res;
            }
            catch (Exception ex)
            {
                return -1;
            }

        }
        //Academic Marks Details
        public DataTable StudentAcademicMarksDetails(string strCon,ObjAdmissionAcademicCriteria criteria)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dt;
                cmd.Parameters.AddWithValue("@P_RegistrationNo", criteria.RegNo);
                dt = objDLGeneric.SpDataTable("usp_StudentAcademicMarksDetails", cmd, strCon);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        //Fee detail
        public int SaveAnnulFeeDetails(string strCon, ObjFeeMaster FeeMaster)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                int intRes;
                intRes=objDLGeneric.ExecuteQuery( FeeMaster.strInsertQuery.ToString(),strCon);
                return intRes;
            }
            catch (Exception ex)
            {
                return -1;
            }

        }
        //Student Academic Marks Details
        public int SaveStudentAcademicMarksDetails(string strCon, ObjAdmissionAcademicCriteria criteria)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                int intRes;
                intRes = objDLGeneric.ExecuteQuery(criteria.strInsertQuery.ToString(), strCon);
                return intRes;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        //Receipt Generation
       
        ///Fee detail
        public int SaveReceiptDetails(string strCon, ObjFeeMaster FeeMaster)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dt;
                int intReceiptNo;

                cmd.Parameters.AddWithValue("@P_RegistrationNo", FeeMaster.RegistrationNo);
                cmd.Parameters.AddWithValue("@P_Amount", FeeMaster.Amount);
                cmd.Parameters.AddWithValue("@P_Concession", FeeMaster.Concession);
                cmd.Parameters.AddWithValue("@P_PaymentMode", FeeMaster.PaymentMode);
                cmd.Parameters.AddWithValue("@P_OnlineTranId", FeeMaster.OnlineTranId);

                if (FeeMaster.ActivePlanID  != null)
                {
                    cmd.Parameters.AddWithValue("@P_ActivePlanID", FeeMaster.ActivePlanID);   
                }
                
                if (FeeMaster.PaymentMode == "Cheque")
                {
   
                    cmd.Parameters.AddWithValue("@P_ChequeDDNo", FeeMaster.ChequeDDNo);
                    cmd.Parameters.AddWithValue("@P_ChequeDate", FeeMaster.ChequeDate);
                    cmd.Parameters.AddWithValue("@P_IssuedBank", FeeMaster.IssuedBank);
                }
                cmd.Parameters.AddWithValue("@P_UserId", FeeMaster.CreatedBy);
                cmd.Parameters.AddWithValue("@P_Remarks", FeeMaster.Remarks);
                cmd.Parameters.AddWithValue("@P_InsertQry", FeeMaster.strInsertQuery);
                

                SqlParameter NewgeneratedRegNo = new SqlParameter();
                NewgeneratedRegNo.ParameterName = "@P_ReceiptNo";
                NewgeneratedRegNo.DbType = DbType.Int32;
                NewgeneratedRegNo.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(NewgeneratedRegNo);


                dt = objDLGeneric.SpDataTable("Usp_InsertReceipt", cmd, strCon);

                intReceiptNo = Convert.ToInt32(cmd.Parameters["@P_ReceiptNo"].Value.ToString());

                return intReceiptNo;
            }
            catch (Exception ex)
            {
                return -1;
            }

        }
        public DataTable  generateReceipt(string strCon, ObjFeeMaster FeeMaster)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dtReceipt;

                cmd.Parameters.AddWithValue("@P_RegistrationNo", FeeMaster.RegistrationNo);
                cmd.Parameters.AddWithValue("@P_Amount", FeeMaster.Amount);
                cmd.Parameters.AddWithValue("@P_Concession", FeeMaster.Concession);
                cmd.Parameters.AddWithValue("@p_Fine", FeeMaster.Fine);
                cmd.Parameters.AddWithValue("@P_PaymentMode", FeeMaster.PaymentMode);
                cmd.Parameters.AddWithValue("@P_OnlineTranId", FeeMaster.OnlineTranId);

                if (FeeMaster.FineLogNo != null && FeeMaster.FineLogNo != "") cmd.Parameters.AddWithValue("@P_FINE_LOG_NO", FeeMaster.FineLogNo);

                if (FeeMaster.ActivePlanID != null)
                {
                    cmd.Parameters.AddWithValue("@P_ActivePlanID", FeeMaster.ActivePlanID);
                }

                if (FeeMaster.PaymentMode == "Cheque" || FeeMaster.PaymentMode == "NEFT")
                {
                    cmd.Parameters.AddWithValue("@P_ChequeDDNo", FeeMaster.ChequeDDNo);
                    cmd.Parameters.AddWithValue("@P_ChequeDate", FeeMaster.ChequeDate);
                    cmd.Parameters.AddWithValue("@P_IssuedBank", FeeMaster.IssuedBank);
                }

                cmd.Parameters.AddWithValue("@P_UserId", FeeMaster.CreatedBy);
                cmd.Parameters.AddWithValue("@P_Remarks", FeeMaster.Remarks);
                cmd.Parameters.AddWithValue("@P_InsertQry", FeeMaster.strInsertQuery);


                SqlParameter NewgeneratedRegNo = new SqlParameter();
                NewgeneratedRegNo.ParameterName = "@P_ReceiptNo";
                NewgeneratedRegNo.DbType = DbType.Int32;
                NewgeneratedRegNo.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(NewgeneratedRegNo);
             
                dtReceipt = objDLGeneric.SpDataTable("Usp_InsertReceipt", cmd, strCon);
                return dtReceipt;

            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public int SaveOnLineProcedingTransDetails(string strCon, ObjFeeMaster FeeMaster)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dt;
                int intReceiptNo;

                cmd.Parameters.AddWithValue("@P_RegistrationNo", FeeMaster.RegistrationNo);
                cmd.Parameters.AddWithValue("@P_Amount", FeeMaster.Amount);
                cmd.Parameters.AddWithValue("@p_Fine", FeeMaster.Fine);
                cmd.Parameters.AddWithValue("@P_Concession", FeeMaster.Concession);
                cmd.Parameters.AddWithValue("@P_PaymentMode", FeeMaster.PaymentMode);
                cmd.Parameters.AddWithValue("@P_OnlineTranId", FeeMaster.OnlineTranId);
                cmd.Parameters.AddWithValue("@P_Remarks", FeeMaster.Remarks);
                cmd.Parameters.AddWithValue("@P_UserId", FeeMaster.CreatedBy);
                cmd.Parameters.AddWithValue("@P_InsertQry", FeeMaster.strInsertQuery);

                cmd.Parameters.AddWithValue("@P_VerifiedBy", FeeMaster.ModifiedBy);
                cmd.Parameters.AddWithValue("@P_VerifiedOn", FeeMaster.ModifiedDate);
                cmd.Parameters.AddWithValue("@P_Status", FeeMaster.TransStatus);
                cmd.Parameters.AddWithValue("@P_Mode", FeeMaster.Mode);

                if (FeeMaster.Mode == "UT")
                cmd.Parameters.AddWithValue("@P_MACHINE_IP", FeeMaster.MachineIP);

                if (FeeMaster.Mode == "UM")
                    cmd.Parameters.AddWithValue("@P_Tranaction_Type", FeeMaster.Transaction_Type);

                if (FeeMaster.ActivePlanID != null)
                {
                    cmd.Parameters.AddWithValue("@P_ActivePlanID", FeeMaster.ActivePlanID);
                }

                if (FeeMaster.FineLogNo != null && FeeMaster.FineLogNo != "") cmd.Parameters.AddWithValue("@P_FINE_LOG_NO", FeeMaster.FineLogNo);

                SqlParameter NewgeneratedRegNo = new SqlParameter();
                NewgeneratedRegNo.ParameterName = "@P_ReceiptNo";
                NewgeneratedRegNo.DbType = DbType.Int32;
                NewgeneratedRegNo.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(NewgeneratedRegNo);

                dt = objDLGeneric.SpDataTable("Usp_InsUpdOnLineProcedingTransDetails", cmd, strCon);

                intReceiptNo = Convert.ToInt32(cmd.Parameters["@P_ReceiptNo"].Value.ToString());

                return intReceiptNo;
            }
            catch (Exception ex)
            {
                return -1;
            }

        }
        public DataTable getNoticationOnlinetranDetails(string strCon, ObjFeeMaster FeeMaster)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dt;

                cmd.Parameters.AddWithValue("@P_OnlineTranId", FeeMaster.OnlineTranId);
                cmd.Parameters.AddWithValue("@P_Mode", FeeMaster.Mode);
                SqlParameter NewgeneratedRegNo = new SqlParameter();
                NewgeneratedRegNo.ParameterName = "@P_ReceiptNo";
                NewgeneratedRegNo.DbType = DbType.Int32;
                NewgeneratedRegNo.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(NewgeneratedRegNo);

                dt = objDLGeneric.SpDataTable("Usp_InsUpdOnLineProcedingTransDetails", cmd, strCon);

                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public DataTable GenerateOnlineTransactionReceipt(string strCon, string strOnlineTranID, string strFomPortal)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dt;
                cmd.Parameters.AddWithValue("@P_OnlineTranId", strOnlineTranID);
                cmd.Parameters.AddWithValue("@P_FomPortal", strFomPortal);
                dt = objDLGeneric.SpDataTable("Usp_GenerateOnlinePaymentReceipt", cmd, strCon);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public DataTable GetOnLineProcedingTransDetails(string strCon, ObjFeeMaster FeeMaster)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dt;
             
                if (FeeMaster.RegistrationNo == 0)
                {
                }
                else
                {
                    cmd.Parameters.AddWithValue("@P_RegistrationNo", FeeMaster.RegistrationNo);
                }
                cmd.Parameters.AddWithValue("@P_Mode", FeeMaster.Mode);

                SqlParameter NewgeneratedRegNo = new SqlParameter();
                NewgeneratedRegNo.ParameterName = "@P_ReceiptNo";
                NewgeneratedRegNo.DbType = DbType.Int32;
                NewgeneratedRegNo.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(NewgeneratedRegNo);

                dt = objDLGeneric.SpDataTable("Usp_InsUpdOnLineProcedingTransDetails", cmd, strCon);

               return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable VerifyOnlinePaymnts(string strCon, ObjFeeMaster FeeMaster)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dt;
                cmd.Parameters.AddWithValue("@P_ONLINETRANID", FeeMaster.OnlineTranId);
                cmd.Parameters.AddWithValue("@P_Mode", FeeMaster.Mode);
                cmd.Parameters.AddWithValue("@P_REMARKS", FeeMaster.Remarks);
                cmd.Parameters.AddWithValue("@P_STATUS", FeeMaster.TransStatus);
                cmd.Parameters.AddWithValue("@P_VERIFIEDBY", FeeMaster.ModifiedBy);
                if(FeeMaster.DateFrom!="")cmd.Parameters.AddWithValue("@P_TRANDATE_FROM", FeeMaster.DateFrom);
                if (FeeMaster.DateTo != "") cmd.Parameters.AddWithValue("@P_TRANDATE_TO", FeeMaster.DateTo);
                dt = objDLGeneric.SpDataTable("USP_VERIFYONLINEPAYMENTS", cmd, strCon);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetCCAvenueTranDetails(string strCon, ObjFeeMaster FeeMaster)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dt;
                cmd.Parameters.AddWithValue("@P_Mode", FeeMaster.Mode);
                if (FeeMaster.DateFrom != "") cmd.Parameters.AddWithValue("@P_TRANDATE_FROM", FeeMaster.DateFrom);
                if (FeeMaster.DateTo != "") cmd.Parameters.AddWithValue("@P_TRANDATE_TO", FeeMaster.DateTo);
                if (FeeMaster.RegistrationNo > 0) cmd.Parameters.AddWithValue("@P_REGISTRATIONNO", FeeMaster.RegistrationNo);

                dt = objDLGeneric.SpDataTable("USP_GetCCAvenueTranDetails", cmd, strCon);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable VerifyCETOnlinePaymnts(string strCon, ObjFeeMaster FeeMaster)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dt;
                cmd.Parameters.AddWithValue("@P_ONLINETRANID", FeeMaster.OnlineTranId);
                cmd.Parameters.AddWithValue("@P_Mode", FeeMaster.Mode);
                cmd.Parameters.AddWithValue("@P_REMARKS", FeeMaster.Remarks);
                cmd.Parameters.AddWithValue("@P_STATUS", FeeMaster.TransStatus);
                cmd.Parameters.AddWithValue("@P_VERIFIEDBY", FeeMaster.ModifiedBy);
                if (FeeMaster.DateFrom != "") cmd.Parameters.AddWithValue("@P_TRANDATE_FROM", FeeMaster.DateFrom);
                if (FeeMaster.DateTo != "") cmd.Parameters.AddWithValue("@P_TRANDATE_TO", FeeMaster.DateTo);
                dt = objDLGeneric.SpDataTable("USP_VERIFY_CET_ONLINEPAYMENTS", cmd, strCon);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable SearchAdmissionList(string searchstring, string strCon)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@SearchString", searchstring);
                DataTable dt;
                dt = objDLGeneric.SpDataTable("usp_SearchAdmissionList", cmd, strCon);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        
        //GET DAILY ACTIVITY TRAIL
        public DataSet getDailyOperationalDetails(string strCon )
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataSet dsResult;
            try
            {

                dsResult = objDLGeneric.SpDataSet("USP_DAILY_OPERATIONAL_DETAILS", cmd, strCon);
                return dsResult;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                objDLGeneric = null;
                cmd = null;
                dsResult = null;
            }

        }
    }
}
