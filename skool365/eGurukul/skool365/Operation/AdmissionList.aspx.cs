using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using BLCMR;
using System.Text.RegularExpressions;
using System.Security;
using System.Security.Authentication;
using System.Configuration;
using System.Web.Security;
using System.Data;

public partial class Operation_AdmissionList : System.Web.UI.Page
{

 
    string M_UserId = string.Empty;
    string M_strAlertMessage = string.Empty;
    ObjPortalUser user;

    protected void Page_Load(object sender, EventArgs e)
    {

        clsCommon objCommon = new clsCommon();
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        string _strAlertMessage = string.Empty;

        try
        {
            //Validate User Authetication Starts
            if (Context.User.Identity.IsAuthenticated)
            {
                //Check User Session and Connection String 
                if (Session["PortalUserDtl"] != null)
                {
                    user = (ObjPortalUser)Session["PortalUserDtl"];
                    //Check Browser Address
                    if (objCommon.GetIPAddress() != user.IPAddress)
                    {
                        Session.Clear();
                        Session.RemoveAll();
                        Session.Abandon();
                        FormsAuthentication.SignOut();
                        Response.Redirect("login.aspx?Exp=Y", false);
                    }
                }
                else
                {
                    Session.Clear();
                    Session.RemoveAll();
                    Session.Abandon();
                    FormsAuthentication.SignOut();
                    Response.Redirect("login.aspx?Exp=Y", false);
                    return;
                }
            }
            else
            {
                Session.Clear();
                Session.RemoveAll();
                Session.Abandon();
                Response.Redirect("login.aspx?Exp=Y", false);
                return;
            }

            //Check Page Accessibility
            if (user != null)
            {
                if (objCommon.checkPageAccessiblity(user.EmailId, System.IO.Path.GetFileName(Request.Path), user.ConnectionString) != "Y")
                    Response.Redirect("~/Operation/AccessDenied.aspx", false);
            }

            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }
        catch (Exception ex)
        {
            M_strAlertMessage = ex.Message;
        }
        finally
        {
            //Alert Message
            if (M_strAlertMessage != string.Empty && M_strAlertMessage.Trim() != "")
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + M_strAlertMessage + "');</script>", false);
            }
        }
    }

    protected void grdRegisteredCandidates_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string RegNumber = e.CommandArgument.ToString();

        if (e.CommandName == "EditRecord")
        {
            EditRegistrationForm(RegNumber);
        }

        if (e.CommandName == "Stage2")
        {
            Response.Redirect("UploadChecklist.aspx?RegNo=" + RegNumber);
        }

        if (e.CommandName == "Stage3")
        {
            Response.Redirect("AdmissionSubjectCriteria.aspx?RegNo=" + RegNumber);
        }

        if (e.CommandName == "Stage4")
        {
            Response.Redirect("AdmissionFeeDetails.aspx?RegNo=" + RegNumber);
        }

        if (e.CommandName == "Stage5")
        {
            Response.Redirect("InstallmentDtl.aspx?RegNo=" + RegNumber);
        }
    }
    protected void grdRegisteredCandidates_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Label lblConfirmStaus = (Label)e.Row.FindControl("lblConfirm");
            //LinkButton lnkAllotExamCenter = (LinkButton)e.Row.FindControl("lnkAllotExamCenter");
            //if (lblConfirmStaus.Text.ToUpper() == "PENDING")
            //{
            //    lnkAllotExamCenter.Enabled = false;
            //    lnkAllotExamCenter.Font.Bold = true;
            //    lnkAllotExamCenter.Text = "Not Confirmed!";
            //}
            //else
            //{
            //    ImageButton ConfirmButton = (ImageButton)e.Row.FindControl("ConfirmButton");
            //    ConfirmButton.Enabled = false;
            //    //--Gunjesh need to find a new disable link button image here, Name should be DisableConfirm.jpg
            //    //ConfirmButton.ImageUrl = "~/Common/Images/DisableConfirm.jpg";
            //}
        }

    }

    private void EditRegistrationForm(string strRegistrationNumber)
    {
        try
        {
            ObjStudentRegistrationSearch objStudentRegistrationSearch = new ObjStudentRegistrationSearch();

            objStudentRegistrationSearch.RegistrationNumber = strRegistrationNumber.Trim();
            objStudentRegistrationSearch.CheckedOtherSearch = false;

            clsUserRegistration objclsUserRegistration = new clsUserRegistration();
            SqlDataReader dr = null;


            dr = objclsUserRegistration.SearchEnrolledStudent(user.ConnectionString, objStudentRegistrationSearch);


            if (dr != null && dr.HasRows)
            {
                ObjStudentRegistrationInfo objRegistrationUser = new ObjStudentRegistrationInfo();

                while (dr.Read())
                {
                    //objRegistrationUser.ID = Convert.ToInt32(dr["ID"]);
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
                    if (dr["NameOfCourseApplyingFor_Accounts"] != DBNull.Value) objRegistrationUser.NameOfCourseApplyingFor_fin = Convert.ToString(dr["NameOfCourseApplyingFor_Accounts"]);
                    if (dr["CourseCode_Accounts"] != DBNull.Value) objRegistrationUser.CourseCode_fin = Convert.ToString(dr["CourseCode_Accounts"]);
                    if (dr["AcedemicYear_Accounts"] != DBNull.Value) objRegistrationUser.AcedemicYear_fin = Convert.ToString(dr["AcedemicYear_Accounts"]);
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
                    if (dr["InforationGotFrom_Direct"] != DBNull.Value) objRegistrationUser.InforationGotFrom_Direct = Convert.ToBoolean(dr["InforationGotFrom_Direct"]);
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
                    if (dr["OPTED_ADDITIONAL_SUB"] != DBNull.Value) objRegistrationUser.OptedAdditionalSubject = Convert.ToString(dr["OPTED_ADDITIONAL_SUB"]);
                    if (dr["INSERTED_ON"] != DBNull.Value) objRegistrationUser.RegistrationDate = Convert.ToString(dr["INSERTED_ON"]);
                    if (dr["INSERTED_BY"] != DBNull.Value) objRegistrationUser.EntryMadeBy = Convert.ToString(dr["INSERTED_BY"]);

                    objRegistrationUser.CouncellorID = "0";
                    if (dr["CouncellorID"] != DBNull.Value) objRegistrationUser.CouncellorID = Convert.ToString(dr["CouncellorID"]);

                    objRegistrationUser.ConsultantName = "";
                    if (dr["ConsultantName"] != DBNull.Value) objRegistrationUser.ConsultantName = Convert.ToString(dr["ConsultantName"]);

                    objRegistrationUser.IsOnlineStudent = 0;
                    objRegistrationUser.OnLineRegistrationNumber = "";
                    if (dr["IsOnlineStudent"] != DBNull.Value) objRegistrationUser.IsOnlineStudent = Convert.ToInt32(dr["IsOnlineStudent"]);
                    if (dr["OnlineRegistrationNo"] != DBNull.Value) objRegistrationUser.OnLineRegistrationNumber = Convert.ToString(dr["OnlineRegistrationNo"]);

                    objRegistrationUser.IsDiplomaStudent = 0;
                    if (dr["IsDiplomaStudent"] != DBNull.Value)
                    {
                        if (Convert.ToBoolean(dr["IsDiplomaStudent"]) == true)
                        {
                            objRegistrationUser.IsDiplomaStudent = 1;
                        }
                    }

                    objRegistrationUser.StudyCenter = "";
                    if (dr["STUDY_CENTER"] != DBNull.Value)
                    {
                        objRegistrationUser.StudyCenter = Convert.ToString(dr["STUDY_CENTER"]);
                    }

                    Session["SearchResult_UserRegistration"] = objRegistrationUser;
                    Response.Redirect("Admission.aspx?Mode=Admission");
                }
            }
        }
        catch (Exception ex)
        {
            M_strAlertMessage = ex.Message;
        }
        finally
        {
            //Alert Message
            if (M_strAlertMessage != string.Empty && M_strAlertMessage.Trim() != "")
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + M_strAlertMessage + "');</script>", false);
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string str = txtSearchString.Text;
        DataTable dt;
        clsAdmission objclsAdmission = new clsAdmission();
        try
        {
            dt = objclsAdmission.SearchAdmissionList(str, user.ConnectionString);
            grdRegisteredCandidates.DataSource = dt;
            grdRegisteredCandidates.DataBind();

            ltrlMessage.Text = " Total Record Searched :" + dt.Rows.Count.ToString();
        }
        catch (Exception ex)
        {
            M_strAlertMessage = ex.Message;
        }
        finally
        {
            objclsAdmission = null;
            dt = null;
            //Alert Message
            if (M_strAlertMessage != string.Empty && M_strAlertMessage.Trim() != "")
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + M_strAlertMessage + "');</script>", false);
            }
        }

    }

}