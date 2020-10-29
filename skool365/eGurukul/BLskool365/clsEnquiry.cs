using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLCMR;

namespace BLCMR
{
    class clsEnquiry
    {
        public int SaveEnquiry(string strCon, ObjEnquiry enquiry)
        {
            DataTable dtInfo;
            int i = 0;
            DLClsGeneric objDLGeneric = new DLClsGeneric();

            SqlCommand cmd = new SqlCommand();
            try
            {

                cmd.Parameters.AddWithValue("@P_CandidateName", enquiry.CandidateName);
                cmd.Parameters.AddWithValue("@P_DOB", enquiry.DOB);
                cmd.Parameters.AddWithValue("@P_EmailId", enquiry.Email);
                cmd.Parameters.AddWithValue("@P_Telephone", enquiry.Telephone);
                cmd.Parameters.AddWithValue("@P_MobileNo", enquiry.MobileNo);
                cmd.Parameters.AddWithValue("@P_Citizenship", enquiry.Citizenship);
                cmd.Parameters.AddWithValue("@P_CourseAppliedFor", enquiry.CourseAppliedFor);
                cmd.Parameters.AddWithValue("@P_ContactMeBy", enquiry.ContactMeBy);
                cmd.Parameters.AddWithValue("@P_Subscription", enquiry.Subscription);
                cmd.Parameters.AddWithValue("@P_Query", enquiry.EnquiryId);
                i = objDLGeneric.InsertValueinDB("usp_InsertEnquiryDetails", cmd, strCon);
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
            return i;
        }

        public DataTable ShowCourseMaster(string strCon)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dt;
                dt = objDLGeneric.SpDataTable("usp_SelectCourseMaster", cmd, strCon);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataTable ShowState(string strCon)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dt;
                dt = objDLGeneric.SpDataTable("usp_SelectStateMaster", cmd, strCon);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataTable ShowEnquiry(string strCon, ObjEnquiry enquiry)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dt;
                cmd.Parameters.AddWithValue("@P_FromDate", enquiry.FromDt);
                cmd.Parameters.AddWithValue("@P_ToDate", enquiry.ToDt);
                dt = objDLGeneric.SpDataTable("usp_SelectEnquiryDetails", cmd, strCon);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
