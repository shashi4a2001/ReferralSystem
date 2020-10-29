using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLCMR;

namespace BLCMR
{
    public class clsOlympiad
    {

        /// <summary>
        /// GET STATE LIST
        /// </summary>
        /// <param name="strCon"></param>
        /// <returns></returns>
        public DataTable getStateList(string strCon)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataTable dtInfo;
            try
            {
                dtInfo = objDLGeneric.SpDataTable("USP_GET_STATE_LIST", cmd, strCon);
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
        /// Get Olympiad Registration details
        /// </summary>
        /// <param name="objOlympiad"></param>
        /// <returns></returns>
        public DataTable getOlympiadRegistrationDetails(objOlympiad Olympiad)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataTable dtInfo;
            try
            {
                cmd.Parameters.AddWithValue("@P_SCHOOL_ID", Olympiad.SchoolID);
                if (Olympiad.searchText.Trim() != "") cmd.Parameters.AddWithValue("@P_SEARCH_TEXT", Olympiad.searchText);
                dtInfo = objDLGeneric.SpDataTable("USP_GET_OLYMPIAD_REGISTRATION_DETAILS", cmd, Olympiad.ConnectionString);
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
        /// Create School Registration Requests
        /// </summary>
        /// <param name="objOlympiad"></param>
        /// <returns></returns>
        public DataTable CreateSchoolRegistrationRequests(objOlympiad Olympiad)
        {
            DataTable dtRequestInfo = new DataTable();
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.Parameters.AddWithValue("@P_SCHOOL_ID", Olympiad.SchoolID ); 
                cmd.Parameters.AddWithValue("@P_PRINCIPAL_NAME", Olympiad.PrincipalName);
                cmd.Parameters.AddWithValue("@P_SCHOOL_NAME", Olympiad.SchoolName);
                cmd.Parameters.AddWithValue("@P_BOARD", Olympiad.Board);
                cmd.Parameters.AddWithValue("@P_SCHOOL_ADDRESS", Olympiad.SchoolAdress);
                cmd.Parameters.AddWithValue("@P_DISTRICT", Olympiad.District);
                cmd.Parameters.AddWithValue("@P_ADDRESS_STATE", Olympiad.StateID);
                cmd.Parameters.AddWithValue("@P_CITY_VILLAGE_TOWN", Olympiad.CityVillageTown );
                cmd.Parameters.AddWithValue("@P_PIN", Olympiad.PIN );
                cmd.Parameters.AddWithValue("@P_CONTACT_NO", Olympiad.ContactNo);
                cmd.Parameters.AddWithValue("@P_MOBILE_NO", Olympiad.MobileNo);
                cmd.Parameters.AddWithValue("@P_EMAI_ID", Olympiad.EmailID );
                cmd.Parameters.AddWithValue("@P_WEBSITE_URL", Olympiad.SchoolURL);
                cmd.Parameters.AddWithValue("@P_CREATED_BY", Olympiad.USerID );
                cmd.Parameters.AddWithValue("@P_STS", Olympiad.Status);
                cmd.Parameters.AddWithValue("@P_Mode", Olympiad.Mode);

                dtRequestInfo = objDLGeneric.SpDataTable("dbo.USP_INS_DEL_UPD_SCHOOL_REGISTRATION", cmd, Olympiad.ConnectionString);
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
        /// Delete sCHOOL Registration Requests
        /// </summary>
        /// <param name="objOlympiad"></param>
        /// <returns></returns>
        public DataTable DeleteOlympiadSchoolRegistrationRequests(objOlympiad Olympiad)
        {
            DataTable dtRequestInfo = new DataTable();
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.Parameters.AddWithValue("@P_USER_ID", Olympiad.USerID);
                cmd.Parameters.AddWithValue("@P_SCHOOL_ID", Olympiad.SchoolID);

                dtRequestInfo = objDLGeneric.SpDataTable("dbo.USP_DELETE_OLYMPIAD_SCHOOL_REGISTRATION_REQUEST", cmd, Olympiad.ConnectionString);
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
        /// Delete Registration Requests
        /// </summary>
        /// <param name="objOlympiad"></param>
        /// <returns></returns>
        public DataTable DeleteOlympiadRegistration(objOlympiad Olympiad)
        {
            DataTable dtRequestInfo = new DataTable();
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.Parameters.AddWithValue("@P_USER_ID", Olympiad.USerID);
                cmd.Parameters.AddWithValue("@P_SCHOOL_ID", Olympiad.SchoolID);

                dtRequestInfo = objDLGeneric.SpDataTable("dbo.USP_DELETE_OLYMPIAD_REGISTRATION", cmd, Olympiad.ConnectionString);
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
        /// Confirm Registration Requests
        /// </summary>
        /// <param name="objOlympiad"></param>
        /// <returns></returns>
        public DataTable  ConfirmStudentSignupRequests(objOlympiad Olympiad)
        {
            DataTable dtRequestInfo = new DataTable();
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.Parameters.AddWithValue("@P_USER_ID", Olympiad.USerID);
                cmd.Parameters.AddWithValue("@P_SIGNUP_ID", Olympiad.SignupId);
                cmd.Parameters.AddWithValue("@P_PASSWORD", Olympiad.Password);

                dtRequestInfo = objDLGeneric.SpDataTable("dbo.USP_CONFIRM_STUDENT_SIGNUP_ACCOUNT", cmd, Olympiad.ConnectionString);
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
        /// Activate deactivated Registration Requests
        /// </summary>
        /// <param name="objOlympiad"></param>
        /// <returns></returns>
        public DataTable ActivateDeActivatedSignupRequests(objOlympiad Olympiad)
        {
            DataTable dtRequestInfo = new DataTable();
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.Parameters.AddWithValue("@P_USER_ID", Olympiad.USerID);
                cmd.Parameters.AddWithValue("@P_SIGNUP_ID", Olympiad.SignupId);
                dtRequestInfo = objDLGeneric.SpDataTable("dbo.USP_ACTIVATE_STUDENT_SIGNUP_ACCOUNT", cmd, Olympiad.ConnectionString);
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
        /// GET SCHOOL DETAILS
        /// </summary>
        /// <param name="objOlympiad"></param>
        /// <returns></returns>
        public DataTable getOlympiadRegisteredSchools(objOlympiad Olympiad)
        {
            DataTable dtRequestInfo = new DataTable();
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();

            try
            {
                if( Olympiad.SchoolID>0 ) cmd.Parameters.AddWithValue("@P_SCHOOL_ID", Olympiad.SchoolID);
                dtRequestInfo = objDLGeneric.SpDataTable("dbo.USP_GET_OLYMPIAD_INSTITUES", cmd, Olympiad.ConnectionString);
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
        public DataTable OlympiadRegistration(string strCon, ObjStudentRegistrationInfo objRegistrationUser, bool isInsert)
        {
            DataTable dtResult;
            DLClsGeneric objDLGeneric = new DLClsGeneric();

            if (isInsert)
            {
                #region
                //--Insert Statement
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@School_Id", objRegistrationUser.SchoolID));
                cmd.Parameters.Add(new SqlParameter("@Inserted_By", objRegistrationUser.EnrollmentStatusSaveBy));
                cmd.Parameters.Add(new SqlParameter("@FirstName", objRegistrationUser.FirstName));
                cmd.Parameters.Add(new SqlParameter("@MiddleName", objRegistrationUser.MiddleName));
                cmd.Parameters.Add(new SqlParameter("@LastName", objRegistrationUser.LastName));
                cmd.Parameters.Add(new SqlParameter("@Gender", objRegistrationUser.Gender));
                cmd.Parameters.Add(new SqlParameter("@DOB", objRegistrationUser.DOB));
                cmd.Parameters.Add(new SqlParameter("@Nationality", objRegistrationUser.Nationality));
                cmd.Parameters.Add(new SqlParameter("@Status", objRegistrationUser.EducationStatus));
                cmd.Parameters.Add(new SqlParameter("@Caste", objRegistrationUser.Caste));
                cmd.Parameters.Add(new SqlParameter("@Category", objRegistrationUser.Category));
                cmd.Parameters.Add(new SqlParameter("@Religion", objRegistrationUser.Religion));
                cmd.Parameters.Add(new SqlParameter("@MobileNumber", objRegistrationUser.MobileNumber));
                cmd.Parameters.Add(new SqlParameter("@AdmissionStatus", objRegistrationUser.AdmissionStatus));
                cmd.Parameters.Add(new SqlParameter("@ResidencePhone", objRegistrationUser.ResidencePhone));
                cmd.Parameters.Add(new SqlParameter("@Email", objRegistrationUser.Email));
                cmd.Parameters.Add(new SqlParameter("@CorrespondenceAddress", objRegistrationUser.CorrespondenceAddress));
                cmd.Parameters.Add(new SqlParameter("@PinCode", objRegistrationUser.PinCode));
                cmd.Parameters.Add(new SqlParameter("@PermanentAddress", objRegistrationUser.PermanentAddress));
                cmd.Parameters.Add(new SqlParameter("@ParAddressPinCode", objRegistrationUser.ParAddressPinCode));
                cmd.Parameters.Add(new SqlParameter("@PassportNumber", objRegistrationUser.PassportNumber));
                cmd.Parameters.Add(new SqlParameter("@PassportIssuedAt", objRegistrationUser.PassportIssuedAt));
                cmd.Parameters.Add(new SqlParameter("@CountriesTraveled", objRegistrationUser.CountriesTraveled));
                cmd.Parameters.Add(new SqlParameter("@FatherName", objRegistrationUser.FatherName));
                cmd.Parameters.Add(new SqlParameter("@MotherName", objRegistrationUser.MotherName));
                cmd.Parameters.Add(new SqlParameter("@FatherMobile", objRegistrationUser.FatherMobile));
                cmd.Parameters.Add(new SqlParameter("@MotherMobile", objRegistrationUser.MotherMobile));
                cmd.Parameters.Add(new SqlParameter("@FatherEmail", objRegistrationUser.FatherEmail));
                cmd.Parameters.Add(new SqlParameter("@MotherEmail", objRegistrationUser.MotherEmail));
                cmd.Parameters.Add(new SqlParameter("@Address", objRegistrationUser.Address));
                cmd.Parameters.Add(new SqlParameter("@NameOfCourseApplyingFor", objRegistrationUser.NameOfCourseApplyingFor));
                cmd.Parameters.Add(new SqlParameter("@CourseCode", objRegistrationUser.CourseCode));
                cmd.Parameters.Add(new SqlParameter("@AcedemicYear", objRegistrationUser.AcedemicYear));
                cmd.Parameters.Add(new SqlParameter("@AreApplyAsNRIStudent", objRegistrationUser.AreApplyAsNRIStudent));
                cmd.Parameters.Add(new SqlParameter("@EntTestName1", objRegistrationUser.EntTestName1));
                cmd.Parameters.Add(new SqlParameter("@EntTestDate1", objRegistrationUser.EntTestDate1));
                cmd.Parameters.Add(new SqlParameter("@EntTestPlace1", objRegistrationUser.EntTestPlace1));
                cmd.Parameters.Add(new SqlParameter("@EntTestRank1", objRegistrationUser.EntTestRank1));
                cmd.Parameters.Add(new SqlParameter("@EntTestName2", objRegistrationUser.EntTestName2));
                cmd.Parameters.Add(new SqlParameter("@EntTestDate2", objRegistrationUser.EntTestDate2));
                cmd.Parameters.Add(new SqlParameter("@EntTestPlace2", objRegistrationUser.EntTestPlace2));
                cmd.Parameters.Add(new SqlParameter("@EntTestRank2", objRegistrationUser.EntTestRank2));
                cmd.Parameters.Add(new SqlParameter("@EntTestName3", objRegistrationUser.EntTestName3));
                cmd.Parameters.Add(new SqlParameter("@EntTestDate3", objRegistrationUser.EntTestDate3));
                cmd.Parameters.Add(new SqlParameter("@EntTestPlace3", objRegistrationUser.EntTestPlace3));
                cmd.Parameters.Add(new SqlParameter("@EntTestRank3", objRegistrationUser.EntTestRank3));
                cmd.Parameters.Add(new SqlParameter("@EntTestName4", objRegistrationUser.EntTestName4));
                cmd.Parameters.Add(new SqlParameter("@EntTestDate4", objRegistrationUser.EntTestDate4));
                cmd.Parameters.Add(new SqlParameter("@EntTestPlace4", objRegistrationUser.EntTestPlace4));
                cmd.Parameters.Add(new SqlParameter("@EntTestRank4", objRegistrationUser.EntTestRank4));
                cmd.Parameters.Add(new SqlParameter("@ReasonForApply", objRegistrationUser.ReasonForApply));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassX_RollNo", objRegistrationUser.EduQualificationClassX_RollNo));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassX_Year", objRegistrationUser.EduQualificationClassX_Year));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassX_Stream", objRegistrationUser.EduQualificationClassX_Stream));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassX_Board", objRegistrationUser.EduQualificationClassX_Board));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassX_Marks", objRegistrationUser.EduQualificationClassX_Marks));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassX_MaxMarks", objRegistrationUser.EduQualificationClassX_MaxMarks));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassX_Percent", objRegistrationUser.EduQualificationClassX_Percent));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassX_Result", objRegistrationUser.EduQualificationClassX_Result));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassXII_RollNo", objRegistrationUser.EduQualificationClassXII_RollNo));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassXII_Year", objRegistrationUser.EduQualificationClassXII_Year));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassXII_Stream", objRegistrationUser.EduQualificationClassXII_Stream));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassXII_Board", objRegistrationUser.EduQualificationClassXII_Board));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassXII_Marks", objRegistrationUser.EduQualificationClassXII_Marks));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassXII_MaxMarks", objRegistrationUser.EduQualificationClassXII_MaxMarks));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassXII_Percent", objRegistrationUser.EduQualificationClassXII_Percent));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassXII_Result", objRegistrationUser.EduQualificationClassXII_Result));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassGraduation_RollNo", objRegistrationUser.EduQualificationClassGraduation_RollNo));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassGraduation_Year", objRegistrationUser.EduQualificationClassGraduation_Year));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassGraduation_Stream", objRegistrationUser.EduQualificationClassGraduation_Stream));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassGraduation_Board", objRegistrationUser.EduQualificationClassGraduation_Board));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassGraduation_Marks", objRegistrationUser.EduQualificationClassGraduation_Marks));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassGraduation_MaxMarks", objRegistrationUser.EduQualificationClassGraduation_MaxMarks));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassGraduation_Percent", objRegistrationUser.EduQualificationClassGraduation_Percent));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassGraduation_Result", objRegistrationUser.EduQualificationClassGraduation_Result));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassOther_RollNo", objRegistrationUser.EduQualificationClassOther_RollNo));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassOther_Year", objRegistrationUser.EduQualificationClassOther_Year));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassOther_Stream", objRegistrationUser.EduQualificationClassOther_Stream));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassOther_Board", objRegistrationUser.EduQualificationClassOther_Board));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassOther_Marks", objRegistrationUser.EduQualificationClassOther_Marks));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassOther_MaxMarks", objRegistrationUser.EduQualificationClassOther_MaxOtherMarks));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassOther_Percent", objRegistrationUser.EduQualificationClassOther_Percent));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassOther_Result", objRegistrationUser.EduQualificationClassOther_Result));
                cmd.Parameters.Add(new SqlParameter("@Profession_OtherQualificationAwards_Text", objRegistrationUser.Profession_OtherQualificationAwards_Text));
                cmd.Parameters.Add(new SqlParameter("@DetailsWorkExperience_Text", objRegistrationUser.DetailsWorkExperience_Text));
                cmd.Parameters.Add(new SqlParameter("@HobbiesAndInterest_Text", objRegistrationUser.HobbiesAndInterest_Text));
                cmd.Parameters.Add(new SqlParameter("@MaritalSatus", objRegistrationUser.MaritalSatus));
                cmd.Parameters.Add(new SqlParameter("@NameOfSpouse", objRegistrationUser.NameOfSpouse));
                cmd.Parameters.Add(new SqlParameter("@BloodGroup", objRegistrationUser.BloodGroup));
                cmd.Parameters.Add(new SqlParameter("@IsPhysicalDisable", objRegistrationUser.IsPhysicalDisable));
                cmd.Parameters.Add(new SqlParameter("@DetailsPhysicalDisable_Text", objRegistrationUser.DetailsPhysicalDisable_Text));
                cmd.Parameters.Add(new SqlParameter("@InforationGotFrom_Newspaper", objRegistrationUser.InforationGotFrom_Newspaper));
                cmd.Parameters.Add(new SqlParameter("@InforationGotFrom_Hoarding", objRegistrationUser.InforationGotFrom_Hoarding));
                cmd.Parameters.Add(new SqlParameter("@InforationGotFrom_Internet", objRegistrationUser.InforationGotFrom_Internet));
                cmd.Parameters.Add(new SqlParameter("@InforationGotFrom_Representative", objRegistrationUser.InforationGotFrom_Representative));
                cmd.Parameters.Add(new SqlParameter("@InforationGotFrom_Friends", objRegistrationUser.InforationGotFrom_Friends));
                cmd.Parameters.Add(new SqlParameter("@InforationGotFrom_OtherText", objRegistrationUser.InforationGotFrom_OtherText));

                cmd.Parameters.Add(new SqlParameter("@ProffOfIdentity_VoterCard", objRegistrationUser.ProffOfIdentity_VoterCard));
                cmd.Parameters.Add(new SqlParameter("@ProffOfIdentity_DL", objRegistrationUser.ProffOfIdentity_DL));
                cmd.Parameters.Add(new SqlParameter("@ProffOfIdentity_Passport", objRegistrationUser.ProffOfIdentity_Passport));
                cmd.Parameters.Add(new SqlParameter("@ProffOfIdentity_PANCard", objRegistrationUser.ProffOfIdentity_PANCard));
                cmd.Parameters.Add(new SqlParameter("@ProffOfIdentity_AdharCard", objRegistrationUser.ProffOfIdentity_AdharCard));
                cmd.Parameters.Add(new SqlParameter("@OtherDoc_TransriptsOfQualification", objRegistrationUser.OtherDoc_TransriptsOfQualification));
                cmd.Parameters.Add(new SqlParameter("@OtherDoc_CasteCertificate", objRegistrationUser.OtherDoc_CasteCertificate));
                cmd.Parameters.Add(new SqlParameter("@OtherDoc_Photographs", objRegistrationUser.OtherDoc_Photographs));
                cmd.Parameters.Add(new SqlParameter("@OtherDoc_ExpCertificate", objRegistrationUser.OtherDoc_ExpCertificate));
                cmd.Parameters.Add(new SqlParameter("@OtherDoc_AddProof", objRegistrationUser.OtherDoc_AddProof));
                cmd.Parameters.Add(new SqlParameter("@OtherDoc_TFWSIncomeCertificate", objRegistrationUser.OtherDoc_TFWSIncomeCertificate));
                cmd.Parameters.Add(new SqlParameter("@OtherDoc_DomicileCertificate", objRegistrationUser.OtherDoc_DomicileCertificate));
                cmd.Parameters.Add(new SqlParameter("@OtherDoc_MedicalCertificate", objRegistrationUser.OtherDoc_MedicalCertificate));
                cmd.Parameters.Add(new SqlParameter("@Declaration_InformationGivenIsTrue", objRegistrationUser.Declaration_InformationGivenIsTrue));
                cmd.Parameters.Add(new SqlParameter("@Declaration_FormDataWillNotProvided_ExternalOrganisation", objRegistrationUser.Declaration_FormDataWillNotProvided_ExternalOrganisation));
                cmd.Parameters.Add(new SqlParameter("@Declaration_ConfirmingMinimumEligCriteria", objRegistrationUser.Declaration_ConfirmingMinimumEligCriteria));
                cmd.Parameters.Add(new SqlParameter("@Declaration_ReadEnclosedDeclaration", objRegistrationUser.Declaration_ReadEnclosedDeclaration));
                cmd.Parameters.Add(new SqlParameter("@DraftNo", objRegistrationUser.DraftNo));
                cmd.Parameters.Add(new SqlParameter("@BankName", objRegistrationUser.BankName));
                cmd.Parameters.Add(new SqlParameter("@Amount", objRegistrationUser.Amount));
                cmd.Parameters.Add(new SqlParameter("@DraftDate", objRegistrationUser.DraftDate));
                cmd.Parameters.Add(new SqlParameter("@CurrentDate", objRegistrationUser.CurrentDate));
                cmd.Parameters.Add(new SqlParameter("@Place", objRegistrationUser.Place));

                cmd.Parameters.Add(new SqlParameter("@Image", objRegistrationUser.ImageName));
                cmd.Parameters.Add(new SqlParameter("@Signature", objRegistrationUser.SignatureName));
                cmd.Parameters.Add(new SqlParameter("@ChoosenCenters", objRegistrationUser.ChosenCenters));
                cmd.Parameters.Add(new SqlParameter("@IsOptedOnlineExamination", objRegistrationUser.IsOptedOnlineExamination));
                cmd.Parameters.Add(new SqlParameter("@OptedOnlineExaminatioDate", objRegistrationUser.OptedOnlineExaminatioDate));
                cmd.Parameters.Add(new SqlParameter("@OptedStudyCenter", objRegistrationUser.StudyCenter));
                dtResult = objDLGeneric.SpDataTable("Usp_InsertOlympiadRegistration", cmd, strCon);

                #endregion
            }
            else
            {
                #region
                //--Update statement
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@ID", objRegistrationUser.ID));
                cmd.Parameters.Add(new SqlParameter("@RegistrationNumber", objRegistrationUser.RegistrationNumber));
                cmd.Parameters.Add(new SqlParameter("@FirstName", objRegistrationUser.FirstName));
                cmd.Parameters.Add(new SqlParameter("@MiddleName", objRegistrationUser.MiddleName));
                cmd.Parameters.Add(new SqlParameter("@LastName", objRegistrationUser.LastName));
                cmd.Parameters.Add(new SqlParameter("@Gender", objRegistrationUser.Gender));
                cmd.Parameters.Add(new SqlParameter("@DOB", objRegistrationUser.DOB));
                cmd.Parameters.Add(new SqlParameter("@Nationality", objRegistrationUser.Nationality));
                cmd.Parameters.Add(new SqlParameter("@Status", objRegistrationUser.EducationStatus));
                cmd.Parameters.Add(new SqlParameter("@Caste", objRegistrationUser.Caste));
                cmd.Parameters.Add(new SqlParameter("@Category", objRegistrationUser.Category));
                cmd.Parameters.Add(new SqlParameter("@Religion", objRegistrationUser.Religion));
                cmd.Parameters.Add(new SqlParameter("@MobileNumber", objRegistrationUser.MobileNumber));
                cmd.Parameters.Add(new SqlParameter("@AdmissionStatus", objRegistrationUser.AdmissionStatus));
                cmd.Parameters.Add(new SqlParameter("@ResidencePhone", objRegistrationUser.ResidencePhone));
                cmd.Parameters.Add(new SqlParameter("@Email", objRegistrationUser.Email));
                cmd.Parameters.Add(new SqlParameter("@CorrespondenceAddress", objRegistrationUser.CorrespondenceAddress));
                cmd.Parameters.Add(new SqlParameter("@PinCode", objRegistrationUser.PinCode));
                cmd.Parameters.Add(new SqlParameter("@PermanentAddress", objRegistrationUser.PermanentAddress));
                cmd.Parameters.Add(new SqlParameter("@ParAddressPinCode", objRegistrationUser.ParAddressPinCode));
                cmd.Parameters.Add(new SqlParameter("@PassportNumber", objRegistrationUser.PassportNumber));
                cmd.Parameters.Add(new SqlParameter("@PassportIssuedAt", objRegistrationUser.PassportIssuedAt));
                cmd.Parameters.Add(new SqlParameter("@CountriesTraveled", objRegistrationUser.CountriesTraveled));
                cmd.Parameters.Add(new SqlParameter("@FatherName", objRegistrationUser.FatherName));
                cmd.Parameters.Add(new SqlParameter("@MotherName", objRegistrationUser.MotherName));
                cmd.Parameters.Add(new SqlParameter("@FatherMobile", objRegistrationUser.FatherMobile));
                cmd.Parameters.Add(new SqlParameter("@MotherMobile", objRegistrationUser.MotherMobile));
                cmd.Parameters.Add(new SqlParameter("@FatherEmail", objRegistrationUser.FatherEmail));
                cmd.Parameters.Add(new SqlParameter("@MotherEmail", objRegistrationUser.MotherEmail));
                cmd.Parameters.Add(new SqlParameter("@Address", objRegistrationUser.Address));
                cmd.Parameters.Add(new SqlParameter("@NameOfCourseApplyingFor", objRegistrationUser.NameOfCourseApplyingFor));
                cmd.Parameters.Add(new SqlParameter("@CourseCode", objRegistrationUser.CourseCode));
                cmd.Parameters.Add(new SqlParameter("@AcedemicYear", objRegistrationUser.AcedemicYear));
                cmd.Parameters.Add(new SqlParameter("@AreApplyAsNRIStudent", objRegistrationUser.AreApplyAsNRIStudent));
                cmd.Parameters.Add(new SqlParameter("@EntTestName1", objRegistrationUser.EntTestName1));
                cmd.Parameters.Add(new SqlParameter("@EntTestDate1", objRegistrationUser.EntTestDate1));
                cmd.Parameters.Add(new SqlParameter("@EntTestPlace1", objRegistrationUser.EntTestPlace1));
                cmd.Parameters.Add(new SqlParameter("@EntTestRank1", objRegistrationUser.EntTestRank1));
                cmd.Parameters.Add(new SqlParameter("@EntTestName2", objRegistrationUser.EntTestName2));
                cmd.Parameters.Add(new SqlParameter("@EntTestDate2", objRegistrationUser.EntTestDate2));
                cmd.Parameters.Add(new SqlParameter("@EntTestPlace2", objRegistrationUser.EntTestPlace2));
                cmd.Parameters.Add(new SqlParameter("@EntTestRank2", objRegistrationUser.EntTestRank2));
                cmd.Parameters.Add(new SqlParameter("@EntTestName3", objRegistrationUser.EntTestName3));
                cmd.Parameters.Add(new SqlParameter("@EntTestDate3", objRegistrationUser.EntTestDate3));
                cmd.Parameters.Add(new SqlParameter("@EntTestPlace3", objRegistrationUser.EntTestPlace3));
                cmd.Parameters.Add(new SqlParameter("@EntTestRank3", objRegistrationUser.EntTestRank3));
                cmd.Parameters.Add(new SqlParameter("@EntTestName4", objRegistrationUser.EntTestName4));
                cmd.Parameters.Add(new SqlParameter("@EntTestDate4", objRegistrationUser.EntTestDate4));
                cmd.Parameters.Add(new SqlParameter("@EntTestPlace4", objRegistrationUser.EntTestPlace4));
                cmd.Parameters.Add(new SqlParameter("@EntTestRank4", objRegistrationUser.EntTestRank4));
                cmd.Parameters.Add(new SqlParameter("@ReasonForApply", objRegistrationUser.ReasonForApply));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassX_RollNo", objRegistrationUser.EduQualificationClassX_RollNo));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassX_Year", objRegistrationUser.EduQualificationClassX_Year));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassX_Stream", objRegistrationUser.EduQualificationClassX_Stream));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassX_Board", objRegistrationUser.EduQualificationClassX_Board));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassX_Marks", objRegistrationUser.EduQualificationClassX_Marks));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassX_MaxMarks", objRegistrationUser.EduQualificationClassX_MaxMarks));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassX_Percent", objRegistrationUser.EduQualificationClassX_Percent));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassX_Result", objRegistrationUser.EduQualificationClassX_Result));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassXII_RollNo", objRegistrationUser.EduQualificationClassXII_RollNo));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassXII_Year", objRegistrationUser.EduQualificationClassXII_Year));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassXII_Stream", objRegistrationUser.EduQualificationClassXII_Stream));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassXII_Board", objRegistrationUser.EduQualificationClassXII_Board));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassXII_Marks", objRegistrationUser.EduQualificationClassXII_Marks));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassXII_MaxMarks", objRegistrationUser.EduQualificationClassXII_MaxMarks));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassXII_Percent", objRegistrationUser.EduQualificationClassXII_Percent));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassXII_Result", objRegistrationUser.EduQualificationClassXII_Result));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassGraduation_RollNo", objRegistrationUser.EduQualificationClassGraduation_RollNo));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassGraduation_Year", objRegistrationUser.EduQualificationClassGraduation_Year));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassGraduation_Stream", objRegistrationUser.EduQualificationClassGraduation_Stream));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassGraduation_Board", objRegistrationUser.EduQualificationClassGraduation_Board));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassGraduation_Marks", objRegistrationUser.EduQualificationClassGraduation_Marks));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassGraduation_MaxMarks", objRegistrationUser.EduQualificationClassGraduation_MaxMarks));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassGraduation_Percent", objRegistrationUser.EduQualificationClassGraduation_Percent));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassGraduation_Result", objRegistrationUser.EduQualificationClassGraduation_Result));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassOther_RollNo", objRegistrationUser.EduQualificationClassOther_RollNo));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassOther_Year", objRegistrationUser.EduQualificationClassOther_Year));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassOther_Stream", objRegistrationUser.EduQualificationClassOther_Stream));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassOther_Board", objRegistrationUser.EduQualificationClassOther_Board));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassOther_Marks", objRegistrationUser.EduQualificationClassOther_Marks));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassOther_MaxMarks", objRegistrationUser.EduQualificationClassOther_MaxOtherMarks));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassOther_Percent", objRegistrationUser.EduQualificationClassOther_Percent));
                cmd.Parameters.Add(new SqlParameter("@EduQualificationClassOther_Result", objRegistrationUser.EduQualificationClassOther_Result));
                cmd.Parameters.Add(new SqlParameter("@Profession_OtherQualificationAwards_Text", objRegistrationUser.Profession_OtherQualificationAwards_Text));
                cmd.Parameters.Add(new SqlParameter("@DetailsWorkExperience_Text", objRegistrationUser.DetailsWorkExperience_Text));
                cmd.Parameters.Add(new SqlParameter("@HobbiesAndInterest_Text", objRegistrationUser.HobbiesAndInterest_Text));
                cmd.Parameters.Add(new SqlParameter("@MaritalSatus", objRegistrationUser.MaritalSatus));
                cmd.Parameters.Add(new SqlParameter("@NameOfSpouse", objRegistrationUser.NameOfSpouse));
                cmd.Parameters.Add(new SqlParameter("@BloodGroup", objRegistrationUser.BloodGroup));
                cmd.Parameters.Add(new SqlParameter("@IsPhysicalDisable", objRegistrationUser.IsPhysicalDisable));
                cmd.Parameters.Add(new SqlParameter("@DetailsPhysicalDisable_Text", objRegistrationUser.DetailsPhysicalDisable_Text));
                cmd.Parameters.Add(new SqlParameter("@InforationGotFrom_Newspaper", objRegistrationUser.InforationGotFrom_Newspaper));
                cmd.Parameters.Add(new SqlParameter("@InforationGotFrom_Hoarding", objRegistrationUser.InforationGotFrom_Hoarding));
                cmd.Parameters.Add(new SqlParameter("@InforationGotFrom_Internet", objRegistrationUser.InforationGotFrom_Internet));
                cmd.Parameters.Add(new SqlParameter("@InforationGotFrom_Representative", objRegistrationUser.InforationGotFrom_Representative));
                cmd.Parameters.Add(new SqlParameter("@InforationGotFrom_Friends", objRegistrationUser.InforationGotFrom_Friends));
                cmd.Parameters.Add(new SqlParameter("@InforationGotFrom_OtherText", objRegistrationUser.InforationGotFrom_OtherText));

                cmd.Parameters.Add(new SqlParameter("@ProffOfIdentity_VoterCard", objRegistrationUser.ProffOfIdentity_VoterCard));
                cmd.Parameters.Add(new SqlParameter("@ProffOfIdentity_DL", objRegistrationUser.ProffOfIdentity_DL));
                cmd.Parameters.Add(new SqlParameter("@ProffOfIdentity_Passport", objRegistrationUser.ProffOfIdentity_Passport));
                cmd.Parameters.Add(new SqlParameter("@ProffOfIdentity_PANCard", objRegistrationUser.ProffOfIdentity_PANCard));
                cmd.Parameters.Add(new SqlParameter("@ProffOfIdentity_AdharCard", objRegistrationUser.ProffOfIdentity_AdharCard));
                cmd.Parameters.Add(new SqlParameter("@OtherDoc_TransriptsOfQualification", objRegistrationUser.OtherDoc_TransriptsOfQualification));
                cmd.Parameters.Add(new SqlParameter("@OtherDoc_CasteCertificate", objRegistrationUser.OtherDoc_CasteCertificate));
                cmd.Parameters.Add(new SqlParameter("@OtherDoc_Photographs", objRegistrationUser.OtherDoc_Photographs));
                cmd.Parameters.Add(new SqlParameter("@OtherDoc_ExpCertificate", objRegistrationUser.OtherDoc_ExpCertificate));
                cmd.Parameters.Add(new SqlParameter("@OtherDoc_AddProof", objRegistrationUser.OtherDoc_AddProof));
                cmd.Parameters.Add(new SqlParameter("@OtherDoc_TFWSIncomeCertificate", objRegistrationUser.OtherDoc_TFWSIncomeCertificate));
                cmd.Parameters.Add(new SqlParameter("@OtherDoc_DomicileCertificate", objRegistrationUser.OtherDoc_DomicileCertificate));
                cmd.Parameters.Add(new SqlParameter("@OtherDoc_MedicalCertificate", objRegistrationUser.OtherDoc_MedicalCertificate));
                cmd.Parameters.Add(new SqlParameter("@Declaration_InformationGivenIsTrue", objRegistrationUser.Declaration_InformationGivenIsTrue));
                cmd.Parameters.Add(new SqlParameter("@Declaration_FormDataWillNotProvided_ExternalOrganisation", objRegistrationUser.Declaration_FormDataWillNotProvided_ExternalOrganisation));
                cmd.Parameters.Add(new SqlParameter("@Declaration_ConfirmingMinimumEligCriteria", objRegistrationUser.Declaration_ConfirmingMinimumEligCriteria));
                cmd.Parameters.Add(new SqlParameter("@Declaration_ReadEnclosedDeclaration", objRegistrationUser.Declaration_ReadEnclosedDeclaration));
                cmd.Parameters.Add(new SqlParameter("@DraftNo", objRegistrationUser.DraftNo));
                cmd.Parameters.Add(new SqlParameter("@BankName", objRegistrationUser.BankName));
                cmd.Parameters.Add(new SqlParameter("@Amount", objRegistrationUser.Amount));
                cmd.Parameters.Add(new SqlParameter("@DraftDate", objRegistrationUser.DraftDate));
                cmd.Parameters.Add(new SqlParameter("@CurrentDate", objRegistrationUser.CurrentDate));
                cmd.Parameters.Add(new SqlParameter("@Place", objRegistrationUser.Place));
                cmd.Parameters.Add(new SqlParameter("@Image", objRegistrationUser.ImageName));
                cmd.Parameters.Add(new SqlParameter("@Signature", objRegistrationUser.SignatureName));
                cmd.Parameters.Add(new SqlParameter("@ChoosenCenters", objRegistrationUser.ChosenCenters));
                cmd.Parameters.Add(new SqlParameter("@IsOptedOnlineExamination", objRegistrationUser.IsOptedOnlineExamination));
                cmd.Parameters.Add(new SqlParameter("@OptedOnlineExaminatioDate", objRegistrationUser.OptedOnlineExaminatioDate));
                cmd.Parameters.Add(new SqlParameter("@Inserted_By", objRegistrationUser.EnrollmentStatusSaveBy));
                cmd.Parameters.Add(new SqlParameter("@OptedStudyCenter", objRegistrationUser.StudyCenter));
                dtResult = objDLGeneric.SpDataTable("usp_UpdateOlympiadRegistration", cmd, strCon);
                #endregion
            }

            return dtResult;
        }


        public DataTable ShowCenters(string strCon)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dt;
                dt = objDLGeneric.SpDataTable("usp_SelectCenters", cmd, strCon);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataTable ShowCETOnlineExaminationDates(string strCon)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dt;
                dt = objDLGeneric.SpDataTable("usp_GetCETOnlineExaminationDates", cmd, strCon);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataTable ShowOlympiadExaminationDates(string strCon)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dt;
                dt = objDLGeneric.SpDataTable("usp_GetOlympiadExaminationDates", cmd, strCon);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        public DataTable GetSetCandidateEnrollmentStatus(string strCon, ObjStudentRegistrationInfo objRegistrationUser)
        {
            DataTable dtResult = null;
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Parameters.Add(new SqlParameter("@P_ENROLLMENT_NUMBER", objRegistrationUser.RegistrationNumber));
                cmd.Parameters.Add(new SqlParameter("@P_Enrollment_Status", objRegistrationUser.EnrollmentStatus));
                cmd.Parameters.Add(new SqlParameter("@P_CREATED_ON", objRegistrationUser.EnrollmentStatusDate));
                cmd.Parameters.Add(new SqlParameter("@P_CREATED_BY", objRegistrationUser.EnrollmentStatusSaveBy));
                cmd.Parameters.Add(new SqlParameter("@P_Mode", objRegistrationUser.EnrollmentStatusMode));
                dtResult = objDLGeneric.SpDataTable("USP_INS_SEL_Candidate_Enrollment_Status", cmd, strCon);
            }
            catch (Exception ex)
            {
                return null;
            }
            return dtResult;
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



        /// <summary>
        /// Create School Registration Requests
        /// </summary>
        /// <param name="objOlympiad"></param>
        /// <returns></returns>
        public DataTable SignUpStudent(objOlympiad Olympiad)
        {
            DataTable dtRequestInfo = new DataTable();
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
 
            try
            {
                cmd.Parameters.AddWithValue("@P_SIGNUP_ID", Olympiad.SignupId);
                cmd.Parameters.AddWithValue("@P_STUDENT_NAME", Olympiad.ApplicantName);
                cmd.Parameters.AddWithValue("@P_ADDRESS", Olympiad.Address);
                cmd.Parameters.AddWithValue("@P_PINCODE", Olympiad.PinCode);
                cmd.Parameters.AddWithValue("@P_STATE", Olympiad.StateID);
                cmd.Parameters.AddWithValue("@P_OCCUPATION", Olympiad.Occupation);
                cmd.Parameters.AddWithValue("@P_MOBILE_NO", Olympiad.MobileNo);
                cmd.Parameters.AddWithValue("@P_EMAIL_ID", Olympiad.EmailID);
                cmd.Parameters.AddWithValue("@P_FATHER_NAME", Olympiad.FatherName);
                cmd.Parameters.AddWithValue("@P_MOTHER_NAME", Olympiad.MotherName);
                cmd.Parameters.AddWithValue("@P_DOB", Olympiad.DOB);
                cmd.Parameters.AddWithValue("@P_NATIONALITY", Olympiad.Nationality);
                cmd.Parameters.AddWithValue("@P_MARTIAL_STATUS", Olympiad.MartialStatus);
                cmd.Parameters.AddWithValue("@P_COURSE_APPLIED", Olympiad.CourseID);
                cmd.Parameters.AddWithValue("@P_CREATED_BY", Olympiad.USerID);
                cmd.Parameters.AddWithValue("@P_MACHINE_IP", Olympiad.MachineIP);
                cmd.Parameters.AddWithValue("@P_STS", Olympiad.Status);
                cmd.Parameters.AddWithValue("@P_Mode", Olympiad.Mode);

                dtRequestInfo = objDLGeneric.SpDataTable("dbo.USP_INS_DEL_UPD_SIGNUP_STUDENT", cmd, Olympiad.ConnectionString);
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
        /// Get USer Signup request details
        /// </summary>
        /// <param name="objOlympiad"></param>
        /// <returns></returns>
        public DataTable getStudentSignupRequest(objOlympiad Olympiad)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataTable dtInfo;
            try
            {
                cmd.Parameters.AddWithValue("@P_STS", Olympiad.Status);
                if (Olympiad.searchText.Trim() != "") cmd.Parameters.AddWithValue("@P_SEARCH_TEXT", Olympiad.searchText);
                dtInfo = objDLGeneric.SpDataTable("USP_GET_STUDENT_SIGNUP_REQUESTS", cmd, Olympiad.ConnectionString);
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
        /// Get Registration Status Count
        /// </summary>
        /// <param name="objOlympiad"></param>
        /// <returns></returns>
        public DataTable getStudentSignupStatusCount(objOlympiad Olympiad)
        {
            DataTable dtRequestInfo = new DataTable();
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();

            try
            {
                dtRequestInfo = objDLGeneric.SpDataTable("dbo.USP_GET_STUDENT_SIGNUP_REQUESTS_COUNT", cmd, Olympiad.ConnectionString);
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

    }
}
