using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLCMR;
using System.Data.SqlClient;

namespace BLCMR
{
    public class clsCommonUtility
    {

        #region "Common Master"
        public DataTable getCommonMaster(ObjMaster master)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataTable dtUtility;
            try
            {
                cmd.Parameters.AddWithValue("@P_MODE", master.Mode);
                dtUtility = objDLGeneric.SpDataTable("USP_GET_COMMON_MISC_MASTER", cmd, master.ConnectionString);
                return dtUtility;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDLGeneric = null;
                cmd = null;
                dtUtility = null;
            }

        }
        #endregion

        #region "Segment Master"

        public DataTable ShowSegment(string strCon)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataTable dtUtility;
            try
            {
                dtUtility = objDLGeneric.SpDataTable("usp_SelectCourseSegmentMaster", cmd, strCon);
                return dtUtility;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDLGeneric = null;
                cmd = null;
                dtUtility = null;
            }

        }

        public DataTable ShowSegment(string strCon, int SegmentID)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataTable dtUtility;
            try
            {
                cmd.Parameters.AddWithValue("@P_SEGMENT_ID", SegmentID);
                dtUtility = objDLGeneric.SpDataTable("usp_SelectCourseSegmentMaster", cmd, strCon);
                return dtUtility;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtUtility = null;
                cmd = null;
                objDLGeneric = null;
            }
        }

        #endregion 

        #region "Course Master"

        public DataTable SaveCourse(ObjMaster master)
        {
            DataTable dtUtility;
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (master.Mode == "C")
                {
                    cmd.Parameters.AddWithValue("@P_Course_Code", master.CourseCode);
                    cmd.Parameters.AddWithValue("@P_Course_Name", master.CourseName);
                    cmd.Parameters.AddWithValue("@P_Segment", master.SegmentId);
                    cmd.Parameters.AddWithValue("@P_Semester_Count", master.SemesterCount);
                    cmd.Parameters.AddWithValue("@P_Status", master.Status);
                    cmd.Parameters.AddWithValue("@P_Created_By", master.CreatedBy);
                    dtUtility = objDLGeneric.SpDataTable("USP_INSERT_COURSE", cmd, master.ConnectionString);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@P_Course_ID", master.CourseId);
                    cmd.Parameters.AddWithValue("@P_Course_Code", master.CourseCode);
                    cmd.Parameters.AddWithValue("@P_Course_Name", master.CourseName);
                    cmd.Parameters.AddWithValue("@P_Segment", master.SegmentId);
                    cmd.Parameters.AddWithValue("@P_Semester_Count", master.SemesterCount);
                    cmd.Parameters.AddWithValue("@P_Status", master.Status);
                    cmd.Parameters.AddWithValue("@P_MODIFIED_BY", master.ModifiedBy);
                    dtUtility = objDLGeneric.SpDataTable("USP_UPDATE_COURSE", cmd, master.ConnectionString);
                }
                return dtUtility;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtUtility = null;
                cmd = null;
                objDLGeneric = null;
            }
        }

        public DataTable ShowCourse(ObjMaster master)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataTable dtUtility;
            try
            {
                if (master.SearchText.Trim() != "") cmd.Parameters.AddWithValue("@P_SEARCH_TEXT", master.SearchText.Trim());
                dtUtility = objDLGeneric.SpDataTable("USP_SELECT_COURSE_DETAILS", cmd, master.ConnectionString);
                return dtUtility;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtUtility = null;
                cmd = null;
                objDLGeneric = null;
            }

        }

        public ObjMaster GetCourseDetails(ObjMaster master)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            ObjMaster lstObjMaster = null;
 
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dtUtility;
                cmd.Parameters.AddWithValue("@P_Course_ID", master.CourseId);
                dtUtility = objDLGeneric.SpDataTable("USP_SELECT_COURSE_DETAILS", cmd, master.ConnectionString);
               
                if (dtUtility.Rows.Count > 0)
                {
                    DataRow dr;
                    dr = dtUtility.Rows[0];
                    lstObjMaster = new ObjMaster();

                    lstObjMaster.CourseId = master.CourseId;
                    lstObjMaster.CourseCode = Convert.ToString(dr["COURSE_CODE"]);
                    lstObjMaster.CourseName = Convert.ToString(dr["COURSE_NAME"]);
                    lstObjMaster.SemesterCount =  Convert.ToInt32(dr["TOTAL_SEMESTER"]);;
                    lstObjMaster.SegmentId = Convert.ToInt32(dr["SEGMENT_ID"]);
                    lstObjMaster.SegmentName = Convert.ToString(dr["SEGMENT_NAME"]);
                    lstObjMaster.Status = Convert.ToInt32(dr["STS"]);
                    lstObjMaster.CreatedBy = Convert.ToString(dr["CREATED_BY"]);
                    lstObjMaster.ModifiedBy = Convert.ToString(dr["Modified_By"]);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstObjMaster;
        }
        
        public DataTable DeleteCourse(ObjMaster  master)
        {
            DataTable dtUtility;
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            try
            {

                cmd.Parameters.AddWithValue("@P_Course_ID", master.CourseId);
                cmd.Parameters.AddWithValue("@P_MODIFIED_BY", master.ModifiedBy);
                dtUtility = objDLGeneric.SpDataTable("usp_Delete_Course", cmd, master.ConnectionString);

                return dtUtility;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtUtility = null;
                cmd = null;
                objDLGeneric = null;
            }
        }

        #endregion

        #region "Fee Header Master"
     
        public DataTable SaveFeeHeader(ObjMaster master)
        {
            DataTable dtUtility;
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (master.Mode == "C")
                {
                    cmd.Parameters.AddWithValue("@P_Header_Code", master.HeaderCode);
                    cmd.Parameters.AddWithValue("@P_Header_Name", master.HeaderName);
                    cmd.Parameters.AddWithValue("@P_Segment", master.SegmentName);
                    cmd.Parameters.AddWithValue("@P_GST_Applicable", master.GSTApplicable);
                    cmd.Parameters.AddWithValue("@P_Status", master.Status);
                    cmd.Parameters.AddWithValue("@P_Created_By", master.CreatedBy);
                    dtUtility = objDLGeneric.SpDataTable("USP_INSERT_FEE_HEADER", cmd, master.ConnectionString);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@P_HEADER_ID", master.HeaderID);
                    cmd.Parameters.AddWithValue("@P_Header_Code", master.HeaderCode);
                    cmd.Parameters.AddWithValue("@P_Header_Name", master.HeaderName);
                    cmd.Parameters.AddWithValue("@P_Segment", master.SegmentName);
                    cmd.Parameters.AddWithValue("@P_GST_Applicable", master.GSTApplicable);
                    cmd.Parameters.AddWithValue("@P_Status", master.Status);
                    cmd.Parameters.AddWithValue("@P_MODIFIED_BY", master.ModifiedBy);
                    dtUtility = objDLGeneric.SpDataTable("USP_UPDATE_FEE_HEADER", cmd, master.ConnectionString);
                }
                return dtUtility;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtUtility = null;
                cmd = null;
                objDLGeneric = null;
            }
        }

        public DataTable ShowHeader(ObjMaster master)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataTable dtUtility;
            try
            {
                if (master.SearchText.Trim() != "") cmd.Parameters.AddWithValue("@P_SEARCH_TEXT", master.SearchText.Trim());
                dtUtility = objDLGeneric.SpDataTable("USP_SELECT_FEE_HEADER_DETAILS", cmd, master.ConnectionString);
                return dtUtility;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtUtility = null;
                cmd = null;
                objDLGeneric = null;
            }

        }

        public ObjMaster GetFeeHeaderDetails(ObjMaster master)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            ObjMaster lstObjMaster = null;

            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dtUtility;
                cmd.Parameters.AddWithValue("@P_Header_ID", master.HeaderID);
                dtUtility = objDLGeneric.SpDataTable("USP_SELECT_FEE_HEADER_DETAILS", cmd, master.ConnectionString);

                if (dtUtility.Rows.Count > 0)
                {
                    DataRow dr;
                    dr = dtUtility.Rows[0];
                    lstObjMaster = new ObjMaster();
                    
                    lstObjMaster.HeaderID = master.HeaderID;
                    lstObjMaster.HeaderCode = Convert.ToString(dr["HEADER_CODE"]);
                    lstObjMaster.HeaderName = Convert.ToString(dr["HEADER_NAME"]);
                    lstObjMaster.SemesterCount = Convert.ToInt32(dr["SEGMENT"]);
                    lstObjMaster.GSTApplicable = Convert.ToString(dr["GST_Applicable"]) == "No" ? 0 : 1;
                    lstObjMaster.Status = Convert.ToInt32(dr["STS"]);
                    lstObjMaster.CreatedBy = Convert.ToString(dr["CREATED_BY"]);
                    lstObjMaster.ModifiedBy = Convert.ToString(dr["Modified_By"]);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstObjMaster;
        }

        public DataTable DeleteHeader(ObjMaster master)
        {
            DataTable dtUtility;
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            try
            {

                cmd.Parameters.AddWithValue("@P_Header_ID", master.HeaderID);
                cmd.Parameters.AddWithValue("@P_MODIFIED_BY", master.ModifiedBy);
                dtUtility = objDLGeneric.SpDataTable("usp_Delete_FEE_Header", cmd, master.ConnectionString);

                return dtUtility;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtUtility = null;
                cmd = null;
                objDLGeneric = null;
            }
        }

        #endregion

        #region "Fee Master"
        public int saveFeeMaster(ObjMaster master)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                int intRes;
                intRes = objDLGeneric.ExecuteQuery(master.strQuery.ToString(), master.ConnectionString );
                return intRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable getFeeDetails(ObjMaster master)
        {
            DataTable dtUtility;
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (master.Mode == "D") //FOR THE PURPOSE OF DEFINING MASTER DATA
                {
                    cmd.Parameters.AddWithValue("@P_SESSION", master.Session.Trim());
                    cmd.Parameters.AddWithValue("@P_COURSE_ID", master.CourseId);
                    dtUtility = objDLGeneric.SpDataTable("usp_Get_Fee_Mastr_Detail", cmd, master.ConnectionString);
                }
                else
                {
                    if (master.SearchText.Trim() != "") cmd.Parameters.AddWithValue("@P_SEARCH_TEXT", master.SearchText.Trim());
                    dtUtility = objDLGeneric.SpDataTable("usp_Select_Fee_Mastr_Detail", cmd, master.ConnectionString);
                }

                return dtUtility;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtUtility = null;
                cmd = null;
                objDLGeneric = null;
            }

        }
 
        #endregion

    }
}
