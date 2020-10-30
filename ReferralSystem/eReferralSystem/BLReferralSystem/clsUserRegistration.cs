using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLCMR;
using System.Data.SqlClient;

namespace BLCMR
{
    public class clsUserRegistration
    {
        public DataTable RegisterUser(string strCon, ObjStudentRegistrationInfo objRegistrationUser, bool isInsert)
        {
            int i = 0;
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            DataTable dtInfo;
            if (isInsert)
            {
                #region
                //--Insert Statement
                SqlCommand cmd = new SqlCommand();
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
                cmd.Parameters.Add(new SqlParameter("@OptedStudyCenter", objRegistrationUser.StudyCenter));
                if(objRegistrationUser.CouncellorID!="")cmd.Parameters.Add(new SqlParameter("@CouncellorID", objRegistrationUser.CouncellorID));
                dtInfo = objDLGeneric.SpDataTable("sp_InsertStudentRegistration", cmd, strCon);
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
                cmd.Parameters.Add(new SqlParameter("@OptedStudyCenter", objRegistrationUser.StudyCenter));
                cmd.Parameters.Add(new SqlParameter("@CouncelorID", objRegistrationUser.CouncellorID)); 
                 dtInfo = objDLGeneric.SpDataTable("sp_UpdateStudentRegistration", cmd, strCon);
                #endregion
            }

            return dtInfo;
        }
        public DataTable  OlympiadRegistration(string strCon, ObjStudentRegistrationInfo objRegistrationUser, bool isInsert)
        {
            DataTable dtResult;
            DLClsGeneric objDLGeneric = new DLClsGeneric();

            if (isInsert)
            {
                #region
                //--Insert Statement
                SqlCommand cmd = new SqlCommand();
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
                cmd.Parameters.Add(new SqlParameter("@OptedStudyCenter", objRegistrationUser.StudyCenter));
                dtResult = objDLGeneric.SpDataTable("usp_UpdateOlympiadRegistration", cmd, strCon);
                #endregion
            }

            return dtResult;
        }
        public SqlDataReader SearchRegisterUser(string strCon, ObjStudentRegistrationSearch objStudentRegistrationSearch)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Add(new SqlParameter("@RegistrationNumber", objStudentRegistrationSearch.RegistrationNumber));
            cmd.Parameters.Add(new SqlParameter("@DOB", objStudentRegistrationSearch.DOB));
            cmd.Parameters.Add(new SqlParameter("@FatherName", objStudentRegistrationSearch.FatherName));
            cmd.Parameters.Add(new SqlParameter("@CheckedOtherSearch", objStudentRegistrationSearch.CheckedOtherSearch));
            cmd.Parameters.Add(new SqlParameter("@IsEditFlag", true));

            return objDLGeneric.GetDataReaderThroughSP("sp_SearchStudentRegistrationForm", cmd, strCon);
        }
        public SqlDataReader SearchTrackRegisterUser(string strCon, ObjStudentRegistrationSearch objStudentRegistrationSearch)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Add(new SqlParameter("@RegistrationNumber", objStudentRegistrationSearch.RegistrationNumber));

            return objDLGeneric.GetDataReaderThroughSP("sp_TrackApplicationStatus", cmd, strCon);
        }
        public SqlDataReader SearchConfirmedUser(string strCon, ObjStudentRegistrationSearch objStudentRegistrationSearch)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Add(new SqlParameter("@P_RegistrationNumber", objStudentRegistrationSearch.RegistrationNumber));
            return objDLGeneric.GetDataReaderThroughSP("sp_SearchConfirmedRegisteredUser", cmd, strCon);
        }
        public DataTable getStudentDetails(string strCon, int RegistrationNo)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Add(new SqlParameter("@P_RegistrationNumber", RegistrationNo));
            return objDLGeneric.SpDataTable("sp_SearchConfirmedRegisteredUser", cmd, strCon);
        }
        public int UpdatePaymentDetails (string strCon, ObjStudentRegistrationInfo objRegistrationUser )
        {
            int i = 0;
            DLClsGeneric objDLGeneric = new DLClsGeneric();

                //--Update Payment Details
                SqlCommand cmd = new SqlCommand();
                
                cmd.Parameters.Add(new SqlParameter("@DraftNo", objRegistrationUser.DraftNo));
                cmd.Parameters.Add(new SqlParameter("@BankName", objRegistrationUser.BankName));
                cmd.Parameters.Add(new SqlParameter("@Amount", objRegistrationUser.Amount));
                cmd.Parameters.Add(new SqlParameter("@DraftDate", objRegistrationUser.DraftDate));
                cmd.Parameters.Add(new SqlParameter("@CurrentDate", objRegistrationUser.CurrentDate));
                cmd.Parameters.Add(new SqlParameter("@Place", objRegistrationUser.Place));
                cmd.Parameters.Add(new SqlParameter("@Submitted", objRegistrationUser.Submitted));
                cmd.Parameters.Add(new SqlParameter("@RegistrationNumber", objRegistrationUser.RegistrationNumber));
       
                i = objDLGeneric.InsertValueinDB("sp_UpdateOnlineRegistrationPaymentDetails", cmd, strCon);
            
            return i;
        }
        public DataTable GetListOfRegisteredCandidates(string strCon)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                return objDLGeneric.SpDataTable("sp_GetListRegisteredCandidate", cmd, strCon);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetListOfRegisteredCandidates(string strCon, string strFromDate, string strToDate, int courseID, string strSession, string strCandidateName, string strEnrollmentNo, string strMode, int OnlineOpted, string strAddress, int ObtCETDateVal, int CETPassedOnly, string strHistory, int CouncellorID)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
               if(strFromDate.Trim()!="") cmd.Parameters.Add("@P_FROMDATE", strFromDate);
               if (strToDate.Trim() != "") cmd.Parameters.Add("@P_TODATE", strToDate);
               if (courseID > 0) cmd.Parameters.Add("@P_COURSEID", courseID);
               if (strSession.Trim() != "--Please Select--") cmd.Parameters.Add("@P_SESSION", strSession);
               if (strMode.Trim() != "") cmd.Parameters.Add("@P_MODE", strMode);
               cmd.Parameters.Add("@P_REGISTRATION_NUMBER", strEnrollmentNo.Trim ());
               cmd.Parameters.Add("@P_CANDIDATE_NAME", strCandidateName.Trim());
               cmd.Parameters.Add("@P_ONLINEOPTED", OnlineOpted );
               cmd.Parameters.Add("@P_ADDRESS", strAddress.Trim());
               if (OnlineOpted > 0) cmd.Parameters.Add("@P_ObtCETDateVal", ObtCETDateVal);
               if (CETPassedOnly > 0) cmd.Parameters.Add("@P_CETPassedOnly", CETPassedOnly);
               cmd.Parameters.Add("@P_All", strHistory);
               if (CouncellorID > 0) cmd.Parameters.Add("@P_UID", CouncellorID); 
               return objDLGeneric.SpDataTable("sp_GetListRegisteredCandidate", cmd, strCon);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void sendConfirmationMail(ObjStudentRegistrationInfo objRegistrationUser, string strCon,string RegistratioStatus)
        {
            //Send mail
            string strCenters = "";
            string strPaymentMode = "";
            string CET_Session = string.Empty;
            try
            {
                clsCommon objClsCommon = new clsCommon();
                DataTable dtInformation;
                dtInformation = objClsCommon.getMailBody("COMMON", "MailFormat", "Mail On CET Registration Acknowledgement", strCon);

                CET_Session = "2018-19";
                if(objRegistrationUser.AcedemicYear !=null)
                {
                    CET_Session = objRegistrationUser.AcedemicYear;
                }

                if (dtInformation != null & dtInformation.Rows.Count > 0)
                {
                    ObjSendEmail SendMail = new ObjSendEmail();
                    //SEND MAIL STARTS
                    try
                    {
                        //Opted Centers details
                        strCenters = objRegistrationUser.ChosenCenters;
                        if (strCenters != null)
                        {
                            if (strCenters.EndsWith(","))
                            {
                                strCenters = strCenters.Substring(0, strCenters.Length - 1);
                            }
                        }

                        //Payment Mode 
                        if (objRegistrationUser.BankName != "")
                        {
                            strPaymentMode = "By Cheque/DD: Amount -" + objRegistrationUser.Amount.ToString() + ", Bank  Name-" + objRegistrationUser.BankName;
                            strPaymentMode = strPaymentMode + ", Issue Date-" + objRegistrationUser.DraftDate + ", DD/Draft No-" + objRegistrationUser.DraftNo;
                        }
                        else
                        {
                            strPaymentMode = "Online";
                        }

                        SendMail.From = dtInformation.Rows[0]["FROM"].ToString();
                        SendMail.To = objRegistrationUser.Email;
                       
                        if (objRegistrationUser.FatherEmail != null && objRegistrationUser.FatherEmail != "")
                        {
                            SendMail.To = SendMail.To + "," + objRegistrationUser.FatherEmail;
                        }

                        SendMail.SMTPHost = dtInformation.Rows[0]["HOST"].ToString();
                        SendMail.SMTPPort = Convert.ToInt32(dtInformation.Rows[0]["PORT"].ToString());
                        SendMail.UserId = dtInformation.Rows[0]["USER_ID"].ToString();
                        SendMail.Subject = "School of Aeronautics : CET(" + objRegistrationUser.AcedemicYear + ") Registration Acknowledgement";
                        SendMail.Password = dtInformation.Rows[0]["PASSWORD"].ToString();
                        SendMail.MailBody = dtInformation.Rows[0]["MAIL_BODY"].ToString();
                        SendMail.ConnectionString = strCon;

                        if (SendMail.To.Trim() != "")
                        {

                            SendMail.MailBody = SendMail.MailBody.Replace("#Session#", objRegistrationUser.AcedemicYear);

                            if (RegistratioStatus=="Y")
                            {
                                string strMailContent = "Once the application is received/proceed, the admission office will contact you via email with further information. You will receive E.Mail from our office regarding the status of your application.";
                                strMailContent = strMailContent + "</br></br> Please send the Demad Draft along with acknowledgement mail at below address, if paymnet made by offline </br></br>";
                                strMailContent = strMailContent + "<strong>NATA Aviation Academy</strong></br>";
                                strMailContent = strMailContent + "I-04, RIICO Industrail, Neemrana, Dist. Alwar, Rajasthan.</br>";
                                strMailContent = strMailContent + "Ph. 01494-297000,297779</br></br>";
                                SendMail.MailBody = SendMail.MailBody.Replace("#MailContent#", strMailContent);
                            }
                            else if(RegistratioStatus=="N")
                            {
                                SendMail.MailBody = SendMail.MailBody.Replace("#MailContent#", "Payment has not be confirmed. Please make the payment again. Please search the application form and re-submit the application form and make the payment successful. You may contact to admin department also.");
                            }
                            else if (RegistratioStatus == "C")
                            {
                                SendMail.MailBody = SendMail.MailBody.Replace("Acknowledgement", "Confirmation");
                                SendMail.MailBody = SendMail.MailBody.Replace("#MailContent#", "This is confirmed that the CET Application has been Confirmed and Verified by Admin department. ");
                            }


                            SendMail.MailBody = SendMail.MailBody.Replace("#RegistrationNo#", objRegistrationUser.RegistrationNumber.ToString());
                            SendMail.MailBody = SendMail.MailBody.Replace("#Name#", objRegistrationUser.FirstName + " " + objRegistrationUser.MiddleName + " " + objRegistrationUser.LastName);
                            SendMail.MailBody = SendMail.MailBody.Replace("#Sex#", objRegistrationUser.Gender);
                            SendMail.MailBody = SendMail.MailBody.Replace("#DOB#", objClsCommon.ConvertDateForInsert(objRegistrationUser.DOB));
                            SendMail.MailBody = SendMail.MailBody.Replace("#EmailId#", objRegistrationUser.Email);
                            SendMail.MailBody = SendMail.MailBody.Replace("#FatherName#", objRegistrationUser.FatherName);
                            SendMail.MailBody = SendMail.MailBody.Replace("#MotherName#", objRegistrationUser.MotherName);
                            SendMail.MailBody = SendMail.MailBody.Replace("#Address#", objRegistrationUser.Address);
                            SendMail.MailBody = SendMail.MailBody.Replace("#CourseApplied#", objRegistrationUser.CourseName);
                            SendMail.MailBody = SendMail.MailBody.Replace("#CentersOpted#", strCenters);
                            SendMail.MailBody = SendMail.MailBody.Replace("#PaymentMode#", strPaymentMode);
                            SendMail.MailBody = SendMail.MailBody.Replace("#RegistrationMode#", "Online");
                            objClsCommon.SendEmail(SendMail );
                        }
                    }
                    catch (Exception ex)
                    {
                        //lblMessage.Text = ex.Message;
                    }
                    //SEND MAIL ENDS

                    //Sending SMS -Start  
                    string strKey=string.Empty ;
                    strKey =RegistratioStatus =="c" ?"SMS On OnLine Registration Confirmation":"SMS On OnLine Registration";
                    dtInformation = objClsCommon.getSMSBody("SMS", strKey, strCon);

                    if (dtInformation != null & dtInformation.Rows.Count > 0)
                    {

                        EC_SMS_SendMessage objEC_SMS_SendMessage = new EC_SMS_SendMessage();
                        clsEC_SMS_SendMessage objclsEC_SMS_SendMessage = new clsEC_SMS_SendMessage();
                        try
                        {
                            objEC_SMS_SendMessage.SMS_KEY = strKey;// "SMS On OnLine Payment(Success)";
                            objEC_SMS_SendMessage.SMS_Desc = "SMS";
                            objEC_SMS_SendMessage.SMS_MobileNo = objRegistrationUser.MobileNumber;
                            objEC_SMS_SendMessage.SMS_Msg = dtInformation.Rows[0]["SMS_MSG"].ToString();
                            objEC_SMS_SendMessage.SMS_Msg = objEC_SMS_SendMessage.SMS_Msg.Replace("#Session#", CET_Session);
                            objEC_SMS_SendMessage.SMS_Msg = objEC_SMS_SendMessage.SMS_Msg.Replace("#RegistrationNo#", objRegistrationUser.RegistrationNumber);
                            objEC_SMS_SendMessage.SMS_URLLink = dtInformation.Rows[0]["SMS_Link"].ToString();
                            objclsEC_SMS_SendMessage.SendSMS(objEC_SMS_SendMessage);
                        }
                        catch (Exception ex)
                        {
                            //lblMessage.Text = lblMessage.Text + " " + ex.Message;
                        }
                        finally
                        {
                            objEC_SMS_SendMessage = null;
                            objclsEC_SMS_SendMessage = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }

        }
        public int DeleteRegisteredUser(string strSql, string strCon)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                return objDLGeneric.ExecuteQuery(strSql, strCon);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int ConfirmUserRegistration(string strCon, ObjStudentRegistrationInfo objRegistrationUser)
        {
            int i = 0;
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            //--Update Payment Details
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Add(new SqlParameter("@ID", objRegistrationUser.ID));
            cmd.Parameters.Add(new SqlParameter("@RegistrationNumber", objRegistrationUser.RegistrationNumber));
            cmd.Parameters.Add(new SqlParameter("@OptExamCenter", objRegistrationUser.ChosenCenters));
            i = objDLGeneric.InsertValueinDB("sp_ConfirmUserRegistration", cmd, strCon);
            return i;
        }
        public DataTable  StudentAdmission(  ObjStudentRegistrationInfo objRegistrationUser, bool isInsert)
        {
            //int i = 0;
            DataTable dtResult = new DataTable();
            DLClsGeneric objDLGeneric = new DLClsGeneric();

            if (isInsert)
            {
                #region
                //--Insert Statement
                SqlCommand cmd = new SqlCommand();
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

                cmd.Parameters.Add(new SqlParameter("@NameOfCourseApplyingFor_Fin", objRegistrationUser.NameOfCourseApplyingFor_fin));
                cmd.Parameters.Add(new SqlParameter("@CourseCode_Fin", objRegistrationUser.CourseCode_fin));
                cmd.Parameters.Add(new SqlParameter("@AcedemicYear_Fin", objRegistrationUser.AcedemicYear_fin));

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
                cmd.Parameters.Add(new SqlParameter("@InforationGotFrom_Direct", objRegistrationUser.InforationGotFrom_Direct));
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
               
                //cmd.Parameters.Add(new SqlParameter("@DraftNo", objRegistrationUser.DraftNo));
                //cmd.Parameters.Add(new SqlParameter("@BankName", objRegistrationUser.BankName));
                //cmd.Parameters.Add(new SqlParameter("@Amount", objRegistrationUser.Amount));
                //cmd.Parameters.Add(new SqlParameter("@DraftDate", objRegistrationUser.DraftDate));
                //cmd.Parameters.Add(new SqlParameter("@CurrentDate", objRegistrationUser.CurrentDate));
                //cmd.Parameters.Add(new SqlParameter("@Place", objRegistrationUser.Place));
               
                cmd.Parameters.Add(new SqlParameter("@Image", objRegistrationUser.ImageName));
                cmd.Parameters.Add(new SqlParameter("@Signature", objRegistrationUser.SignatureName));
                cmd.Parameters.Add(new SqlParameter("@ChoosenCenters", objRegistrationUser.ChosenCenters));
                cmd.Parameters.Add(new SqlParameter("@IsOnlineStudent", objRegistrationUser.IsOnlineStudent));
                cmd.Parameters.Add(new SqlParameter("@OnlineRegistrationNo", objRegistrationUser.OnLineRegistrationNumber));
                cmd.Parameters.Add(new SqlParameter("@AdmissionStage", objRegistrationUser.AdmissionStage));
                cmd.Parameters.Add(new SqlParameter("@IsDiplomaStudent", objRegistrationUser.IsDiplomaStudent));
                cmd.Parameters.Add(new SqlParameter("@OPTED_ADDITIONAL_SUB", objRegistrationUser.OptedAdditionalSubject));

                if (objRegistrationUser.InsertedBy != null || objRegistrationUser.InsertedBy == string.Empty)
                {
                    cmd.Parameters.Add(new SqlParameter("@Entry_By", objRegistrationUser.InsertedBy));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@Entry_By", ""));
                }

                cmd.Parameters.Add(new SqlParameter("@CouncellorID", objRegistrationUser.CouncellorID));
                cmd.Parameters.Add(new SqlParameter("@ConsultantName", objRegistrationUser.ConsultantName));
                cmd.Parameters.Add(new SqlParameter("@StudyCenter", objRegistrationUser.StudyCenter));

                SqlParameter NewgeneratedRegNo = new SqlParameter();
                NewgeneratedRegNo.ParameterName = "@RegNo";
                NewgeneratedRegNo.DbType = DbType.Int32;
                NewgeneratedRegNo.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(NewgeneratedRegNo);


                //i = objDLGeneric.InsertValueinDB("sp_InsertStudentAdmission", cmd, strCon);

                //i = Convert.ToInt32(cmd.Parameters["@RegNo"].Value.ToString());

                dtResult = objDLGeneric.SpDataTable("USP_INSERT_STUDENT_ADMISSION", cmd, objRegistrationUser.ConnectionString);

                #endregion
            }
            else
            {
                #region
                //--Update statement
                SqlCommand cmd = new SqlCommand();
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

                cmd.Parameters.Add(new SqlParameter("@NameOfCourseApplyingFor_Fin", objRegistrationUser.NameOfCourseApplyingFor_fin));
                cmd.Parameters.Add(new SqlParameter("@CourseCode_Fin", objRegistrationUser.CourseCode_fin));
                cmd.Parameters.Add(new SqlParameter("@AcedemicYear_Fin", objRegistrationUser.AcedemicYear_fin));

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
                cmd.Parameters.Add(new SqlParameter("@InforationGotFrom_Direct", objRegistrationUser.InforationGotFrom_Direct));
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
                cmd.Parameters.Add(new SqlParameter("@Image", objRegistrationUser.ImageName));
                cmd.Parameters.Add(new SqlParameter("@Signature", objRegistrationUser.SignatureName));
                cmd.Parameters.Add(new SqlParameter("@ChoosenCenters", objRegistrationUser.ChosenCenters));
                cmd.Parameters.Add(new SqlParameter("@IsDiplomaStudent", objRegistrationUser.IsDiplomaStudent));
                cmd.Parameters.Add(new SqlParameter("@OPTED_ADDITIONAL_SUB", objRegistrationUser.OptedAdditionalSubject));

                if (objRegistrationUser.InsertedBy != null || objRegistrationUser.InsertedBy == string.Empty)
                {
                    cmd.Parameters.Add(new SqlParameter("@Entry_By", objRegistrationUser.InsertedBy));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@Entry_By", ""));
                }

                cmd.Parameters.Add(new SqlParameter("@CouncellorID", objRegistrationUser.CouncellorID));
                cmd.Parameters.Add(new SqlParameter("@ConsultantName", objRegistrationUser.ConsultantName));
                cmd.Parameters.Add(new SqlParameter("@IsOnlineStudent", objRegistrationUser.IsOnlineStudent));
                cmd.Parameters.Add(new SqlParameter("@OnlineRegistrationNo", objRegistrationUser.OnLineRegistrationNumber));
                cmd.Parameters.Add(new SqlParameter("@StudyCenter", objRegistrationUser.StudyCenter));
                dtResult = objDLGeneric.SpDataTable("USP_UPDATE_STUDENT_ADMISSION",cmd, objRegistrationUser.ConnectionString);
                #endregion
            }

            return dtResult;// i;
        }
        public SqlDataReader SearchEnrolledStudent(string strCon, ObjStudentRegistrationSearch objStudentRegistrationSearch)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Add(new SqlParameter("@RegistrationNumber", objStudentRegistrationSearch.RegistrationNumber));
            cmd.Parameters.Add(new SqlParameter("@DOB", objStudentRegistrationSearch.DOB));
            cmd.Parameters.Add(new SqlParameter("@FatherName", objStudentRegistrationSearch.FatherName));
            cmd.Parameters.Add(new SqlParameter("@CheckedOtherSearch", objStudentRegistrationSearch.CheckedOtherSearch));
            return objDLGeneric.GetDataReaderThroughSP("sp_SearchStudentAdmissionInfo", cmd, strCon);
        }
        public ObjStudentRegistrationInfo GetEnrolledCandidateDetails(string strCon, string strRegistrationNo)
        {
            ObjStudentRegistrationSearch objStudentRegistrationSearch = new ObjStudentRegistrationSearch();
            ObjStudentRegistrationInfo objRegistrationUser = new ObjStudentRegistrationInfo();
            clsUserRegistration objclsUserRegistration = new clsUserRegistration();
            SqlDataReader dr = null;

            objStudentRegistrationSearch.RegistrationNumber = strRegistrationNo.Trim();
            dr = objclsUserRegistration.SearchTrackRegisterUser(strCon, objStudentRegistrationSearch);

            if (dr != null && dr.HasRows)
            {
                while (dr.Read())
                {
                    objRegistrationUser.ID = Convert.ToInt32(dr["ID"]);
                    if (dr["RegistrationNumber"] != DBNull.Value) objRegistrationUser.RegistrationNumber = Convert.ToString(dr["RegistrationNumber"]);
                    if (dr["FirstName"] != DBNull.Value) objRegistrationUser.FirstName = Convert.ToString(dr["FirstName"]);
                    if (dr["MiddleName"] != DBNull.Value) objRegistrationUser.MiddleName = Convert.ToString(dr["MiddleName"]);
                    if (dr["LastName"] != DBNull.Value) objRegistrationUser.LastName = Convert.ToString(dr["LastName"]);
                    if (dr["Gender"] != DBNull.Value) objRegistrationUser.Gender = Convert.ToString(dr["Gender"]);
                    if (dr["DOB"] != DBNull.Value) objRegistrationUser.DOB = Convert.ToString(dr["DOB"]);
                    if (dr["Nationality"] != DBNull.Value) objRegistrationUser.Nationality = Convert.ToString(dr["Nationality"]);
                    if (dr["Status"] != DBNull.Value) objRegistrationUser.EducationStatus = Convert.ToString(dr["Status"]);
                    if (dr["Caste"] != DBNull.Value) objRegistrationUser.Caste = Convert.ToString(dr["Caste"]);
                    if (dr["Category"] != DBNull.Value) objRegistrationUser.Category = Convert.ToString(dr["Category"]);
                    if (dr["Religion"] != DBNull.Value) objRegistrationUser.Religion = Convert.ToString(dr["Religion"]);
                    if (dr["MobileNumber"] != DBNull.Value) objRegistrationUser.MobileNumber = Convert.ToString(dr["MobileNumber"]);
                    if (dr["AdmissionStatus"] != DBNull.Value) objRegistrationUser.AdmissionStatus = Convert.ToString(dr["AdmissionStatus"]);
                    if (dr["ResidencePhone"] != DBNull.Value) objRegistrationUser.ResidencePhone = Convert.ToString(dr["ResidencePhone"]);
                    if (dr["Email"] != DBNull.Value) objRegistrationUser.Email = Convert.ToString(dr["Email"]);
                    if (dr["CorrespondenceAddress"] != DBNull.Value) objRegistrationUser.CorrespondenceAddress = Convert.ToString(dr["CorrespondenceAddress"]);
                    if (dr["PinCode"] != DBNull.Value) objRegistrationUser.PinCode = Convert.ToString(dr["PinCode"]);
                    if (dr["PermanentAddress"] != DBNull.Value) objRegistrationUser.PermanentAddress = Convert.ToString(dr["PermanentAddress"]);
                    if (dr["ParAddressPinCode"] != DBNull.Value) objRegistrationUser.ParAddressPinCode = Convert.ToString(dr["ParAddressPinCode"]);
                    if (dr["PassportNumber"] != DBNull.Value) objRegistrationUser.PassportNumber = Convert.ToString(dr["PassportNumber"]);
                    if (dr["PassportIssuedAt"] != DBNull.Value) objRegistrationUser.PassportIssuedAt = Convert.ToString(dr["PassportIssuedAt"]);
                    if (dr["CountriesTraveled"] != DBNull.Value) objRegistrationUser.CountriesTraveled = Convert.ToString(dr["CountriesTraveled"]);
                    if (dr["FatherName"] != DBNull.Value) objRegistrationUser.FatherName = Convert.ToString(dr["FatherName"]);
                    if (dr["MotherName"] != DBNull.Value) objRegistrationUser.MotherName = Convert.ToString(dr["MotherName"]);
                    if (dr["FatherMobile"] != DBNull.Value) objRegistrationUser.FatherMobile = Convert.ToString(dr["FatherMobile"]);
                    if (dr["MotherMobile"] != DBNull.Value) objRegistrationUser.MotherMobile = Convert.ToString(dr["MotherMobile"]);
                    if (dr["FatherEmail"] != DBNull.Value) objRegistrationUser.FatherEmail = Convert.ToString(dr["FatherEmail"]);
                    if (dr["MotherEmail"] != DBNull.Value) objRegistrationUser.MotherEmail = Convert.ToString(dr["MotherEmail"]);
                    if (dr["Address"] != DBNull.Value) objRegistrationUser.Address = Convert.ToString(dr["Address"]);
                    if (dr["NameOfCourseApplyingFor"] != DBNull.Value) objRegistrationUser.NameOfCourseApplyingFor = Convert.ToString(dr["NameOfCourseApplyingFor"]);
                    if (dr["CourseCode"] != DBNull.Value) objRegistrationUser.CourseCode = Convert.ToString(dr["CourseCode"]);
                    if (dr["AcedemicYear"] != DBNull.Value) objRegistrationUser.AcedemicYear = Convert.ToString(dr["AcedemicYear"]);
                    if (dr["AreApplyAsNRIStudent"] != DBNull.Value) objRegistrationUser.AreApplyAsNRIStudent = Convert.ToBoolean(dr["AreApplyAsNRIStudent"]);
                    if (dr["EntTestName1"] != DBNull.Value) objRegistrationUser.EntTestName1 = Convert.ToString(dr["EntTestName1"]);
                    if (dr["EntTestDate1"] != DBNull.Value) objRegistrationUser.EntTestDate1 = Convert.ToString(dr["EntTestDate1"]);
                    if (dr["EntTestPlace1"] != DBNull.Value) objRegistrationUser.EntTestPlace1 = Convert.ToString(dr["EntTestPlace1"]);
                    if (dr["EntTestRank1"] != DBNull.Value) objRegistrationUser.EntTestRank1 = Convert.ToString(dr["EntTestRank1"]);
                    if (dr["EntTestName2"] != DBNull.Value) objRegistrationUser.EntTestName2 = Convert.ToString(dr["EntTestName2"]);
                    if (dr["EntTestDate2"] != DBNull.Value) objRegistrationUser.EntTestDate2 = Convert.ToString(dr["EntTestDate2"]);
                    if (dr["EntTestPlace2"] != DBNull.Value) objRegistrationUser.EntTestPlace2 = Convert.ToString(dr["EntTestPlace2"]);
                    if (dr["EntTestRank2"] != DBNull.Value) objRegistrationUser.EntTestRank2 = Convert.ToString(dr["EntTestRank2"]);
                    if (dr["EntTestName3"] != DBNull.Value) objRegistrationUser.EntTestName3 = Convert.ToString(dr["EntTestName3"]);
                    if (dr["EntTestDate3"] != DBNull.Value) objRegistrationUser.EntTestDate3 = Convert.ToString(dr["EntTestDate3"]);
                    if (dr["EntTestPlace3"] != DBNull.Value) objRegistrationUser.EntTestPlace3 = Convert.ToString(dr["EntTestPlace3"]);
                    if (dr["EntTestRank3"] != DBNull.Value) objRegistrationUser.EntTestRank3 = Convert.ToString(dr["EntTestRank3"]);
                    if (dr["EntTestName4"] != DBNull.Value) objRegistrationUser.EntTestName4 = Convert.ToString(dr["EntTestName4"]);
                    if (dr["EntTestDate4"] != DBNull.Value) objRegistrationUser.EntTestDate4 = Convert.ToString(dr["EntTestDate4"]);
                    if (dr["EntTestPlace4"] != DBNull.Value) objRegistrationUser.EntTestPlace4 = Convert.ToString(dr["EntTestPlace4"]);
                    if (dr["EntTestRank4"] != DBNull.Value) objRegistrationUser.EntTestRank4 = Convert.ToString(dr["EntTestRank4"]);
                    if (dr["ReasonForApply"] != DBNull.Value) objRegistrationUser.ReasonForApply = Convert.ToString(dr["ReasonForApply"]);
                    if (dr["EduQualificationClassX_RollNo"] != DBNull.Value) objRegistrationUser.EduQualificationClassX_RollNo = Convert.ToString(dr["EduQualificationClassX_RollNo"]);
                    if (dr["EduQualificationClassX_Year"] != DBNull.Value) objRegistrationUser.EduQualificationClassX_Year = Convert.ToString(dr["EduQualificationClassX_Year"]);
                    if (dr["EduQualificationClassX_Stream"] != DBNull.Value) objRegistrationUser.EduQualificationClassX_Stream = Convert.ToString(dr["EduQualificationClassX_Stream"]);
                    if (dr["EduQualificationClassX_Board"] != DBNull.Value) objRegistrationUser.EduQualificationClassX_Board = Convert.ToString(dr["EduQualificationClassX_Board"]);
                    if (dr["EduQualificationClassX_Marks"] != DBNull.Value) objRegistrationUser.EduQualificationClassX_Marks = Convert.ToString(dr["EduQualificationClassX_Marks"]);
                    if (dr["EduQualificationClassX_MaxMarks"] != DBNull.Value) objRegistrationUser.EduQualificationClassX_MaxMarks = Convert.ToString(dr["EduQualificationClassX_MaxMarks"]);
                    if (dr["EduQualificationClassX_Percent"] != DBNull.Value) objRegistrationUser.EduQualificationClassX_Percent = Convert.ToString(dr["EduQualificationClassX_Percent"]);
                    if (dr["EduQualificationClassX_Result"] != DBNull.Value) objRegistrationUser.EduQualificationClassX_Result = Convert.ToString(dr["EduQualificationClassX_Result"]);
                    if (dr["EduQualificationClassXII_RollNo"] != DBNull.Value) objRegistrationUser.EduQualificationClassXII_RollNo = Convert.ToString(dr["EduQualificationClassXII_RollNo"]);
                    if (dr["EduQualificationClassXII_Year"] != DBNull.Value) objRegistrationUser.EduQualificationClassXII_Year = Convert.ToString(dr["EduQualificationClassXII_Year"]);
                    if (dr["EduQualificationClassXII_Stream"] != DBNull.Value) objRegistrationUser.EduQualificationClassXII_Stream = Convert.ToString(dr["EduQualificationClassXII_Stream"]);
                    if (dr["EduQualificationClassXII_Board"] != DBNull.Value) objRegistrationUser.EduQualificationClassXII_Board = Convert.ToString(dr["EduQualificationClassXII_Board"]);
                    if (dr["EduQualificationClassXII_Marks"] != DBNull.Value) objRegistrationUser.EduQualificationClassXII_Marks = Convert.ToString(dr["EduQualificationClassXII_Marks"]);
                    if (dr["EduQualificationClassXII_MaxMarks"] != DBNull.Value) objRegistrationUser.EduQualificationClassXII_MaxMarks = Convert.ToString(dr["EduQualificationClassXII_MaxMarks"]);
                    if (dr["EduQualificationClassXII_Percent"] != DBNull.Value) objRegistrationUser.EduQualificationClassXII_Percent = Convert.ToString(dr["EduQualificationClassXII_Percent"]);
                    if (dr["EduQualificationClassXII_Result"] != DBNull.Value) objRegistrationUser.EduQualificationClassXII_Result = Convert.ToString(dr["EduQualificationClassXII_Result"]);
                    if (dr["EduQualificationClassGraduation_RollNo"] != DBNull.Value) objRegistrationUser.EduQualificationClassGraduation_RollNo = Convert.ToString(dr["EduQualificationClassGraduation_RollNo"]);
                    if (dr["EduQualificationClassGraduation_Year"] != DBNull.Value) objRegistrationUser.EduQualificationClassGraduation_Year = Convert.ToString(dr["EduQualificationClassGraduation_Year"]);
                    if (dr["EduQualificationClassGraduation_Stream"] != DBNull.Value) objRegistrationUser.EduQualificationClassGraduation_Stream = Convert.ToString(dr["EduQualificationClassGraduation_Stream"]);
                    if (dr["EduQualificationClassGraduation_Board"] != DBNull.Value) objRegistrationUser.EduQualificationClassGraduation_Board = Convert.ToString(dr["EduQualificationClassGraduation_Board"]);
                    if (dr["EduQualificationClassGraduation_Marks"] != DBNull.Value) objRegistrationUser.EduQualificationClassGraduation_Marks = Convert.ToString(dr["EduQualificationClassGraduation_Marks"]);
                    if (dr["EduQualificationClassGraduation_MaxMarks"] != DBNull.Value) objRegistrationUser.EduQualificationClassGraduation_MaxMarks = Convert.ToString(dr["EduQualificationClassGraduation_MaxMarks"]);
                    if (dr["EduQualificationClassGraduation_Percent"] != DBNull.Value) objRegistrationUser.EduQualificationClassGraduation_Percent = Convert.ToString(dr["EduQualificationClassGraduation_Percent"]);
                    if (dr["EduQualificationClassGraduation_Result"] != DBNull.Value) objRegistrationUser.EduQualificationClassGraduation_Result = Convert.ToString(dr["EduQualificationClassGraduation_Result"]);
                    if (dr["EduQualificationClassOther_RollNo"] != DBNull.Value) objRegistrationUser.EduQualificationClassOther_RollNo = Convert.ToString(dr["EduQualificationClassOther_RollNo"]);
                    if (dr["EduQualificationClassOther_Year"] != DBNull.Value) objRegistrationUser.EduQualificationClassOther_Year = Convert.ToString(dr["EduQualificationClassOther_Year"]);
                    if (dr["EduQualificationClassOther_Stream"] != DBNull.Value) objRegistrationUser.EduQualificationClassOther_Stream = Convert.ToString(dr["EduQualificationClassOther_Stream"]);
                    if (dr["EduQualificationClassOther_Board"] != DBNull.Value) objRegistrationUser.EduQualificationClassOther_Board = Convert.ToString(dr["EduQualificationClassOther_Board"]);
                    if (dr["EduQualificationClassOther_Marks"] != DBNull.Value) objRegistrationUser.EduQualificationClassOther_Marks = Convert.ToString(dr["EduQualificationClassOther_Marks"]);
                    if (dr["EduQualificationClassOther_MaxMarks"] != DBNull.Value) objRegistrationUser.EduQualificationClassOther_MaxOtherMarks = Convert.ToString(dr["EduQualificationClassOther_MaxMarks"]);
                    if (dr["EduQualificationClassOther_Percent"] != DBNull.Value) objRegistrationUser.EduQualificationClassOther_Percent = Convert.ToString(dr["EduQualificationClassOther_Percent"]);
                    if (dr["EduQualificationClassOther_Result"] != DBNull.Value) objRegistrationUser.EduQualificationClassOther_Result = Convert.ToString(dr["EduQualificationClassOther_Result"]);
                    if (dr["Profession_OtherQualificationAwards_Text"] != DBNull.Value) objRegistrationUser.Profession_OtherQualificationAwards_Text = Convert.ToString(dr["Profession_OtherQualificationAwards_Text"]);
                    if (dr["DetailsWorkExperience_Text"] != DBNull.Value) objRegistrationUser.DetailsWorkExperience_Text = Convert.ToString(dr["DetailsWorkExperience_Text"]);
                    if (dr["HobbiesAndInterest_Text"] != DBNull.Value) objRegistrationUser.HobbiesAndInterest_Text = Convert.ToString(dr["HobbiesAndInterest_Text"]);
                    if (dr["MaritalSatus"] != DBNull.Value) objRegistrationUser.MaritalSatus = Convert.ToString(dr["MaritalSatus"]);
                    if (dr["NameOfSpouse"] != DBNull.Value) objRegistrationUser.NameOfSpouse = Convert.ToString(dr["NameOfSpouse"]);
                    if (dr["BloodGroup"] != DBNull.Value) objRegistrationUser.BloodGroup = Convert.ToString(dr["BloodGroup"]);
                    if (dr["IsPhysicalDisable"] != DBNull.Value) objRegistrationUser.IsPhysicalDisable = Convert.ToBoolean(dr["IsPhysicalDisable"]);
                    if (dr["DetailsPhysicalDisable_Text"] != DBNull.Value) objRegistrationUser.DetailsPhysicalDisable_Text = Convert.ToString(dr["DetailsPhysicalDisable_Text"]);
                    if (dr["InforationGotFrom_Newspaper"] != DBNull.Value) objRegistrationUser.InforationGotFrom_Newspaper = Convert.ToBoolean(dr["InforationGotFrom_Newspaper"]);
                    if (dr["InforationGotFrom_Hoarding"] != DBNull.Value) objRegistrationUser.InforationGotFrom_Hoarding = Convert.ToBoolean(dr["InforationGotFrom_Hoarding"]);
                    if (dr["InforationGotFrom_Internet"] != DBNull.Value) objRegistrationUser.InforationGotFrom_Internet = Convert.ToBoolean(dr["InforationGotFrom_Internet"]);
                    if (dr["InforationGotFrom_Representative"] != DBNull.Value) objRegistrationUser.InforationGotFrom_Representative = Convert.ToBoolean(dr["InforationGotFrom_Representative"]);
                    if (dr["InforationGotFrom_Friends"] != DBNull.Value) objRegistrationUser.InforationGotFrom_Friends = Convert.ToBoolean(dr["InforationGotFrom_Friends"]);
                    if (dr["InforationGotFrom_OtherText"] != DBNull.Value) objRegistrationUser.InforationGotFrom_OtherText = Convert.ToString(dr["InforationGotFrom_OtherText"]);
                    if (dr["ProffOfIdentity_VoterCard"] != DBNull.Value) objRegistrationUser.ProffOfIdentity_VoterCard = Convert.ToBoolean(dr["ProffOfIdentity_VoterCard"]);
                    if (dr["ProffOfIdentity_DL"] != DBNull.Value) objRegistrationUser.ProffOfIdentity_DL = Convert.ToBoolean(dr["ProffOfIdentity_DL"]);
                    if (dr["ProffOfIdentity_Passport"] != DBNull.Value) objRegistrationUser.ProffOfIdentity_Passport = Convert.ToBoolean(dr["ProffOfIdentity_Passport"]);
                    if (dr["ProffOfIdentity_PANCard"] != DBNull.Value) objRegistrationUser.ProffOfIdentity_PANCard = Convert.ToBoolean(dr["ProffOfIdentity_PANCard"]);
                    if (dr["ProffOfIdentity_AdharCard"] != DBNull.Value) objRegistrationUser.ProffOfIdentity_AdharCard = Convert.ToBoolean(dr["ProffOfIdentity_AdharCard"]);
                    if (dr["OtherDoc_TransriptsOfQualification"] != DBNull.Value) objRegistrationUser.OtherDoc_TransriptsOfQualification = Convert.ToBoolean(dr["OtherDoc_TransriptsOfQualification"]);
                    if (dr["OtherDoc_CasteCertificate"] != DBNull.Value) objRegistrationUser.OtherDoc_CasteCertificate = Convert.ToBoolean(dr["OtherDoc_CasteCertificate"]);
                    if (dr["OtherDoc_Photographs"] != DBNull.Value) objRegistrationUser.OtherDoc_Photographs = Convert.ToBoolean(dr["OtherDoc_Photographs"]);
                    if (dr["OtherDoc_ExpCertificate"] != DBNull.Value) objRegistrationUser.OtherDoc_ExpCertificate = Convert.ToBoolean(dr["OtherDoc_ExpCertificate"]);
                    if (dr["OtherDoc_AddProof"] != DBNull.Value) objRegistrationUser.OtherDoc_AddProof = Convert.ToBoolean(dr["OtherDoc_AddProof"]);
                    if (dr["OtherDoc_TFWSIncomeCertificate"] != DBNull.Value) objRegistrationUser.OtherDoc_TFWSIncomeCertificate = Convert.ToBoolean(dr["OtherDoc_TFWSIncomeCertificate"]);
                    if (dr["OtherDoc_DomicileCertificate"] != DBNull.Value) objRegistrationUser.OtherDoc_DomicileCertificate = Convert.ToBoolean(dr["OtherDoc_DomicileCertificate"]);
                    if (dr["OtherDoc_MedicalCertificate"] != DBNull.Value) objRegistrationUser.OtherDoc_MedicalCertificate = Convert.ToBoolean(dr["OtherDoc_MedicalCertificate"]);
                    if (dr["Declaration_InformationGivenIsTrue"] != DBNull.Value) objRegistrationUser.Declaration_InformationGivenIsTrue = Convert.ToBoolean(dr["Declaration_InformationGivenIsTrue"]);
                    if (dr["Declaration_FormDataWillNotProvided_ExternalOrganisation"] != DBNull.Value) objRegistrationUser.Declaration_FormDataWillNotProvided_ExternalOrganisation = Convert.ToBoolean(dr["Declaration_FormDataWillNotProvided_ExternalOrganisation"]);
                    if (dr["Declaration_ConfirmingMinimumEligCriteria"] != DBNull.Value) objRegistrationUser.Declaration_ConfirmingMinimumEligCriteria = Convert.ToBoolean(dr["Declaration_ConfirmingMinimumEligCriteria"]);
                    if (dr["Declaration_ReadEnclosedDeclaration"] != DBNull.Value) objRegistrationUser.Declaration_ReadEnclosedDeclaration = Convert.ToBoolean(dr["Declaration_ReadEnclosedDeclaration"]);
                    if (dr["DraftNo"] != DBNull.Value) objRegistrationUser.DraftNo = Convert.ToString(dr["DraftNo"]);
                    if (dr["BankName"] != DBNull.Value) objRegistrationUser.BankName = Convert.ToString(dr["BankName"]);
                    if (dr["Amount"] != DBNull.Value) objRegistrationUser.Amount = Convert.ToString(dr["Amount"]);
                    if (dr["DraftDate"] != DBNull.Value) objRegistrationUser.DraftDate = Convert.ToString(dr["DraftDate"]);
                    if (dr["CurrentDate"] != DBNull.Value) objRegistrationUser.CurrentDate = Convert.ToString(dr["CurrentDate"]);
                    if (dr["Place"] != DBNull.Value) objRegistrationUser.Place = Convert.ToString(dr["Place"]);
                    if (dr["Image"] != DBNull.Value) objRegistrationUser.ImageName = Convert.ToString(dr["Image"]);
                    if (dr["Signature"] != DBNull.Value) objRegistrationUser.SignatureName = Convert.ToString(dr["Signature"]);
                    if (dr["RegistrationAccepted"] == DBNull.Value) objRegistrationUser.RegistrationAccepted = false; else objRegistrationUser.RegistrationAccepted = Convert.ToBoolean(dr["RegistrationAccepted"]);
                    if (dr["ChoosenCenters"] != DBNull.Value) objRegistrationUser.ChosenCenters = Convert.ToString(dr["ChoosenCenters"]);
                    if (dr["CourseName"] != DBNull.Value) objRegistrationUser.CourseName = Convert.ToString(dr["CourseName"]);
                    
                }
            }

            if (!dr.IsClosed) dr.Close();

            return objRegistrationUser;
        }
        public DataTable  GetSetCandidateEnrollmentStatus(string strCon, ObjStudentRegistrationInfo objRegistrationUser)
        {
            DataTable dtResult =null;
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
        public DataTable ResetCETRegistrationConfirmation(string strCon, int Mode)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add("@P_MODE", Mode);
                   return objDLGeneric.SpDataTable("usp_Reset_Void_Registration", cmd, strCon);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    
    //FOR SIGNUP STUDENTS

        public DataTable getConfirmedSignupStudents( ObjStudentRegistrationSearch StudentRegistrationSearch)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            DataTable dtResult;
            
            try
            {
                cmd.Parameters.Add(new SqlParameter("@P_Session", StudentRegistrationSearch.Session));
                cmd.Parameters.Add(new SqlParameter("@P_CourseID", StudentRegistrationSearch.CourseID));

                dtResult = objDLGeneric.SpDataTable("USP_GetActiveSignupStudents", cmd, StudentRegistrationSearch.ConnectionString);
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
}
