using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Services;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using BLCMR;
using System.Text.RegularExpressions;
using System.Security;
using System.Security.Authentication;

public partial class Operation_Registration : System.Web.UI.Page
{
    string M_strAlertMessage = string.Empty;
    string _strAlertMessage = string.Empty;
    string _strMsgType = string.Empty;
    ObjPortalUser user;
    private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
 

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

            if (IsPostBack == false)
            {
                lblAllowedImageText.Text = "Size of image should not be more than " + Convert.ToString(ConfigurationManager.AppSettings["MaxImageSizeForOnlineApplication"]) + "KB";
                lblAllowedSignatureText.Text = "Size of signature should not be more than " + Convert.ToString(ConfigurationManager.AppSettings["MaxSignatureSizeForOnlineApplication"]) + "KB";
                getOlympiadRegisteredSchools();
                getOlympiadExamDates();
                getCousreMaster();
                getStateList();
                getOlympiadRegisteredDetails();
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "setFocusToEntryTab('EntryTab')", true);
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
            _strMsgType = "Error";
            _strAlertMessage = ex.Message;
            objclsErrorLogger.WriteError(System.IO.Path.GetFileName(Request.Path), "", user == null ? "" : user.ConnectionString, ex);                
            Response.Redirect("~/Operation/Err.aspx");
        }
        finally
        {
            //Alert Message
            if (_strAlertMessage != string.Empty && _strAlertMessage.Trim() != "")
            {
                lblMessage.Text = _strAlertMessage;
                objCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);

                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }

            objclsErrorLogger = null;
            objCommon = null;

            UpdatePanel1.Update();
            UpdatePanel2.Update();
            UpdatePanel3.Update();
        }
    }

    private void getOlympiadRegisteredSchools()
    {
        clsOlympiad objclsOlympiad= new clsOlympiad();
        objOlympiad Olympiad = new objOlympiad();
        DataTable dtSchools = new DataTable();
        try
        {
            Olympiad.SchoolID = user.SchoolID ;
            Olympiad.ConnectionString = user.ConnectionString;
            dtSchools = objclsOlympiad.getOlympiadRegisteredSchools(Olympiad);

            if (dtSchools != null)
            {
                if (dtSchools.Rows.Count > 0)
                {
                    ddlSchool.DataSource = dtSchools;
                    ddlSchool.DataTextField = dtSchools.Columns["SCHOOL_NAME"].ToString();
                    ddlSchool.DataValueField = dtSchools.Columns["Address"].ToString();
                    ddlSchool.DataBind();

                    //if (ddlSchool.Items.Count > 0)
                    //{
                    //    ddlSchool.SelectedIndex = 0;
                    //    lblSchoolAddress.Text =  (ddlSchool.SelectedValue.ToString().Replace("\n","<br/>"));
                    //}
                    setSelectedSchoolAdress();
               }
            }
        }
        catch { }
        finally
        {
            objclsOlympiad = null;
            Olympiad = null;
            dtSchools = null;
        }
    }
    private void getOlympiadRegisteredDetails()
    {
        clsOlympiad objclsOlympiad= new clsOlympiad();
        objOlympiad Olympiad = new objOlympiad();
        DataTable dtSchools = new DataTable();
        int schoolID;
        try
        {
            int.TryParse(ddlSchool.SelectedValue , out schoolID);
            Olympiad.SchoolID = schoolID;
            Olympiad.searchText = txtSearch.Text.Trim();
            Olympiad.ConnectionString =  user.ConnectionString;
            dtSchools = objclsOlympiad.getOlympiadRegistrationDetails(Olympiad);

            grdStyled.DataSource = dtSchools;
            grdStyled.DataBind();

        }
        catch (Exception EX)
        {
            throw EX;
        }
        finally
        {
            objclsOlympiad = null;
            Olympiad = null;
            dtSchools = null;
        }
    }
    private void getAcademicYear()
    {
        SqlConnection con = null;
        try
        {
            txtAcademicYr.Text = Convert.ToString(ConfigurationManager.AppSettings["AcademicYear"]);
            con = new SqlConnection(user.ConnectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT [VALUE] FROM SEETINGS WHERE [KEY]='ONLINE ACADEMIC SESSION'", con);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    txtAcademicYr.Text = Convert.ToString(dr["VALUE"]);
                }
                dr.Close();
            }
        }
        catch (Exception ex)
        {
            txtAcademicYr.Text = Convert.ToString(ConfigurationManager.AppSettings["AcademicYear"]);
        }
        finally
        {
            con.Close();
            txtAcademicYr.Text = "2018";
            lblHeader.Text = "Aero Olympiad :" + txtAcademicYr.Text + " APPLICATION FORM";
        }
    }
    private void getCousreMaster()
    {
        SqlConnection con = null;
        try
        {
            con = new SqlConnection(user.ConnectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * from OlympiadCourseMaster(NOLOCK) order by CourseId asc", con);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                ListItem lst;
                while (dr.Read())
                {
                    lst = new ListItem(Convert.ToString(dr["CourseName"]), Convert.ToString(dr["CourseCode"]));
                    ddlCourseApplyingFor.Items.Add(lst);
                }
                dr.Close();
            }
        }
        catch (Exception ex)
        { }
        finally
        {
            con.Close();
        }
    }
    private void getOlympiadExamDates()
    {
        clsOlympiad objclsUserRegistration = new clsOlympiad();
        DataTable dtExamdates = new DataTable();
        try
        {
            dtExamdates = objclsUserRegistration.ShowOlympiadExaminationDates(user.ConnectionString);

            if (dtExamdates != null)
            {
                if (dtExamdates.Rows.Count > 0)
                {
                    rdoOLExamDates.DataSource = dtExamdates;
                    rdoOLExamDates.DataTextField = dtExamdates.Columns["EXAM_DATE"].ToString();
                    rdoOLExamDates.DataValueField = dtExamdates.Columns["ID"].ToString();
                    rdoOLExamDates.DataBind();

                    if (rdoOLExamDates.Items.Count > 0)
                    {
                        rdoOLExamDates.SelectedValue = dtExamdates.Rows[0]["SELECTEDVAL"].ToString();
                    }
                }
            }
        }
        catch { }
        finally
        {
            objclsUserRegistration = null;
            dtExamdates = null;
        }
    }
    private void getStateList()
    {
        clsOlympiad objclsOlympiad = new clsOlympiad();
        DataTable dtCenters;
        try
        {
            dtCenters = objclsOlympiad.getStateList(user.ConnectionString);
            dtCenters.DefaultView.RowFilter = "STATEID<>0";

            lstCenters.DataSource = dtCenters.DefaultView.ToTable();
            lstCenters.DataValueField = dtCenters.Columns["StateID"].ToString();
            lstCenters.DataTextField = dtCenters.Columns["StateName"].ToString();
            lstCenters.DataBind();

            lstCenters.Enabled = true;
         }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dtCenters = null;
            objclsOlympiad = null;
        }
    }
    private ObjStudentRegistrationInfo getUserObject()
    {
        ObjStudentRegistrationInfo objRegistrationUser = new ObjStudentRegistrationInfo();
        clsCommon objclsCommon = new clsCommon();
        try
        {
            int schoolID;
            int ID;

            if (txtRegistrationNo.Text.Trim() != "Pending")
            {
                int.TryParse(hdID.Value, out ID);
                objRegistrationUser.ID = ID;
                objRegistrationUser.RegistrationNumber = hdRegistrationNo.Value;
            }
            else
            {
                //if (txtRegistrationNo.Text == "Pending")
                //{
                //    objRegistrationUser.RegistrationNumber = GetUniqueRegistrationNumber();
                //    hdRegistrationNo.Value = objRegistrationUser.RegistrationNumber;
                //}
                hdRegistrationNo.Value = "";
            }

            int.TryParse(ddlSchool.SelectedValue, out schoolID);
            objRegistrationUser.SchoolID = schoolID;
            objRegistrationUser.EnrollmentStatusSaveBy = user.UserId;
            objRegistrationUser.FirstName = txtFirstName.Text.ToUpper();
            objRegistrationUser.MiddleName = txtMiddleName.Text.ToUpper();
            objRegistrationUser.LastName = txtLastName.Text.ToUpper();
            objRegistrationUser.Gender = ddlGender.SelectedItem.Text;
            objRegistrationUser.DOB = objclsCommon.ConvertDateForInsert(dtDOB.Text);
            objRegistrationUser.Nationality = txtNationality.Text.ToUpper();
            objRegistrationUser.EducationStatus = ddlEducationStatus.SelectedItem.Text;
            objRegistrationUser.Caste = txtCaste.Text.ToUpper();
            objRegistrationUser.Category = ddlCategory.SelectedItem.Text;
            objRegistrationUser.Religion = txtReligion.Text.ToUpper();
            objRegistrationUser.MobileNumber = txtMobileNumber.Text.ToUpper();
            objRegistrationUser.AdmissionStatus = ddlAdmissionStatus.SelectedItem.Text;
            objRegistrationUser.ResidencePhone = txtResidencePh.Text;
            objRegistrationUser.Email = txtEmailAddress.Text.ToUpper();
            objRegistrationUser.CorrespondenceAddress = txtCorrAddress.Text.ToUpper();
            objRegistrationUser.PinCode = txtPinCode.Text;
            objRegistrationUser.PermanentAddress = txtParmAddress.Text.ToUpper();
            objRegistrationUser.ParAddressPinCode = txtParmAddressPinCode.Text.ToUpper();
            objRegistrationUser.PassportNumber = txtPassportNumber.Text.ToUpper();
            objRegistrationUser.PassportIssuedAt = txtPassportIssuedAt.Text.ToUpper();
            objRegistrationUser.CountriesTraveled = txtCountriesTraveled.Text.ToUpper();
            objRegistrationUser.FatherName = txtFatherName.Text.ToUpper();
            objRegistrationUser.MotherName = txtMotherName.Text.ToUpper();
            objRegistrationUser.FatherMobile = txtFatherMobile.Text;
            objRegistrationUser.MotherMobile = txtMotherMobile.Text;
            objRegistrationUser.FatherEmail = txtFatherEmail.Text.ToUpper();
            objRegistrationUser.MotherEmail = txtMotherEmail.Text.ToUpper();
            objRegistrationUser.Address = txtAddress.Text.ToUpper();
            objRegistrationUser.NameOfCourseApplyingFor = ddlCourseApplyingFor.SelectedValue;
            objRegistrationUser.CourseCode = ddlCourseApplyingFor.SelectedValue;
            objRegistrationUser.AcedemicYear = txtAcademicYr.Text;
            objRegistrationUser.AreApplyAsNRIStudent = iif(rdoIsApplyingAsAForeignStudent.SelectedValue) ? true : false;
            objRegistrationUser.EntTestName1 = EntTestName1.Text;
            objRegistrationUser.EntTestDate1 = EntTestDate1.Text;
            objRegistrationUser.EntTestPlace1 = EntTestPlace1.Text;
            objRegistrationUser.EntTestRank1 = EntTestRank1.Text;
            objRegistrationUser.EntTestName2 = EntTestName2.Text;
            objRegistrationUser.EntTestDate2 = EntTestDate2.Text;
            objRegistrationUser.EntTestPlace2 = EntTestPlace2.Text;
            objRegistrationUser.EntTestRank2 = EntTestRank2.Text;
            objRegistrationUser.EntTestName3 = EntTestName3.Text;
            objRegistrationUser.EntTestDate3 = EntTestDate3.Text;
            objRegistrationUser.EntTestPlace3 = EntTestPlace3.Text;
            objRegistrationUser.EntTestRank3 = EntTestRank3.Text;
            objRegistrationUser.EntTestName4 = EntTestName4.Text;
            objRegistrationUser.EntTestDate4 = EntTestDate4.Text;
            objRegistrationUser.EntTestPlace4 = EntTestPlace4.Text;
            objRegistrationUser.EntTestRank4 = EntTestRank4.Text;

            objRegistrationUser.ReasonForApply = txtReasonForApply.Text.ToUpper();
            objRegistrationUser.EduQualificationClassX_RollNo = EduQualificationClassX_RollNo.Text.ToUpper(); ;
            objRegistrationUser.EduQualificationClassX_Year = EduQualificationClassX_Year.Text;
            objRegistrationUser.EduQualificationClassX_Stream = EduQualificationClassX_Stream.Text;
            objRegistrationUser.EduQualificationClassX_Board = EduQualificationClassX_Board.Text.ToUpper(); ;
            objRegistrationUser.EduQualificationClassX_Marks = EduQualificationClassX_Marks.Text;
            objRegistrationUser.EduQualificationClassX_MaxMarks = EduQualificationClassX_MaxMarks.Text;
            objRegistrationUser.EduQualificationClassX_Percent = EduQualificationClassX_Percent.Text;
            objRegistrationUser.EduQualificationClassX_Result = ddlEduQualificationClassX_Result.SelectedItem.Text;

            objRegistrationUser.EduQualificationClassXII_RollNo = EduQualificationClassXII_RollNo.Text.ToUpper(); ;
            objRegistrationUser.EduQualificationClassXII_Year = EduQualificationClassXII_Year.Text;
            objRegistrationUser.EduQualificationClassXII_Stream = EduQualificationClassXII_Stream.Text;
            objRegistrationUser.EduQualificationClassXII_Board = EduQualificationClassXII_Board.Text.ToUpper(); ;
            objRegistrationUser.EduQualificationClassXII_Marks = EduQualificationClassXII_Marks.Text;
            objRegistrationUser.EduQualificationClassXII_MaxMarks = EduQualificationClassXII_MaxMarks.Text;
            objRegistrationUser.EduQualificationClassXII_Percent = EduQualificationClassXII_Percent.Text;
            objRegistrationUser.EduQualificationClassXII_Result = ddlEduQualificationClassXII_Result.SelectedItem.Text;

            objRegistrationUser.EduQualificationClassGraduation_RollNo = EduQualificationClassGraduation_RollNo.Text.ToUpper(); ;
            objRegistrationUser.EduQualificationClassGraduation_Year = EduQualificationClassGraduation_Year.Text;
            objRegistrationUser.EduQualificationClassGraduation_Stream = EduQualificationClassGraduation_Stream.Text;
            objRegistrationUser.EduQualificationClassGraduation_Board = EduQualificationClassGraduation_Board.Text.ToUpper(); ;
            objRegistrationUser.EduQualificationClassGraduation_Marks = EduQualificationClassGraduation_Marks.Text;
            objRegistrationUser.EduQualificationClassGraduation_MaxMarks = EduQualificationClassGraduation_MaxMarks.Text;
            objRegistrationUser.EduQualificationClassGraduation_Percent = EduQualificationClassGraduation_Percent.Text;
            objRegistrationUser.EduQualificationClassGraduation_Result = ddlEduQualificationClassGraduation_Result.SelectedItem.Text;

            objRegistrationUser.EduQualificationClassOther_RollNo = EduQualificationClassOther_RollNo.Text.ToUpper(); ;
            objRegistrationUser.EduQualificationClassOther_Year = EduQualificationClassOther_Year.Text;
            objRegistrationUser.EduQualificationClassOther_Stream = EduQualificationClassOther_Stream.Text;
            objRegistrationUser.EduQualificationClassOther_Board = EduQualificationClassOther_Board.Text.ToUpper(); ;
            objRegistrationUser.EduQualificationClassOther_Marks = EduQualificationClassOther_Marks.Text;
            objRegistrationUser.EduQualificationClassOther_MaxOtherMarks = EduQualificationClassOther_MaxMarks.Text;
            objRegistrationUser.EduQualificationClassOther_Percent = EduQualificationClassOther_Percent.Text;
            objRegistrationUser.EduQualificationClassOther_Result = ddlEduQualificationClassOther_Result.SelectedItem.Text;

            objRegistrationUser.Profession_OtherQualificationAwards_Text = txtProfession_OtherQualificationAwards_Text.Text;
            objRegistrationUser.DetailsWorkExperience_Text = lblSchoolAddress.Text.ToUpper();
            objRegistrationUser.HobbiesAndInterest_Text = txtHobbies.Text.ToUpper();
            objRegistrationUser.MaritalSatus = rdoMaritalStatus.SelectedItem.Text;
            objRegistrationUser.NameOfSpouse = txtNameOfSpouse.Text.ToUpper();
            objRegistrationUser.BloodGroup = txtBloodGroup.Text;

            objRegistrationUser.IsPhysicalDisable = iif(rdoIsPhysicalDisable.SelectedValue) ? true : false;
            objRegistrationUser.DetailsPhysicalDisable_Text = txtDetailsPhysicalDisable_Text.Text;
            objRegistrationUser.InforationGotFrom_Newspaper = chkInforationGotFrom_Newspaper.Checked ? true : false;
            objRegistrationUser.InforationGotFrom_Hoarding = chkInforationGotFrom_Hoarding.Checked ? true : false;
            objRegistrationUser.InforationGotFrom_Internet = chkInforationGotFrom_Internet.Checked ? true : false;
            objRegistrationUser.InforationGotFrom_Representative = chkInforationGotFrom_Representative.Checked ? true : false;
            objRegistrationUser.InforationGotFrom_Friends = chkInforationGotFrom_Friends.Checked ? true : false;
            objRegistrationUser.InforationGotFrom_OtherText = txtInforationGotFrom_OtherText.Text;
            objRegistrationUser.ProffOfIdentity_VoterCard = chkProffIdentityVoterCard.Checked ? true : false;
            objRegistrationUser.ProffOfIdentity_DL = chkProffIdentityDrivingLicense.Checked ? true : false;
            objRegistrationUser.ProffOfIdentity_Passport = chkProffIdentityPasspport.Checked ? true : false;
            objRegistrationUser.ProffOfIdentity_PANCard = chkProffIdentityPanCard.Checked ? true : false;
            objRegistrationUser.ProffOfIdentity_AdharCard = chkProffIdentityAdharCard.Checked ? true : false;
            objRegistrationUser.OtherDoc_TransriptsOfQualification = chkTranscriptsOfQualifications.Checked ? true : false;
            objRegistrationUser.OtherDoc_CasteCertificate = chkCasteCertificateForSCSTOBC.Checked ? true : false;
            objRegistrationUser.OtherDoc_Photographs = chkPassportSizePhotograph.Checked ? true : false;
            objRegistrationUser.OtherDoc_ExpCertificate = chkExperienceCertificateSupportDocuments.Checked ? true : false;
            objRegistrationUser.OtherDoc_AddProof = chkAddressProof.Checked ? true : false;
            objRegistrationUser.OtherDoc_TFWSIncomeCertificate = chkForTFWSIncomeCertificate.Checked ? true : false;
            objRegistrationUser.OtherDoc_DomicileCertificate = chkDomicileCertificate.Checked ? true : false;
            objRegistrationUser.OtherDoc_MedicalCertificate = chkMedicalCertificate.Checked ? true : false;
            objRegistrationUser.Declaration_InformationGivenIsTrue = chkIconfirmThatInformationIHaveGivenIsTrue.Checked ? true : false;
            objRegistrationUser.Declaration_FormDataWillNotProvided_ExternalOrganisation = chkIUnderstandThatTheDataInThisFormWillNot.Checked ? true : false;
            objRegistrationUser.Declaration_ConfirmingMinimumEligCriteria = chkIConfirmThatIFulfillTheMinimumEligibilityCriteria.Checked ? true : false;
            objRegistrationUser.Declaration_ReadEnclosedDeclaration = chkIConfirmThatIHaveReadTheEnclosedDeclaration.Checked ? true : false;

            objRegistrationUser.DraftNo = Convert.ToString(txtDraftNumber.Text).Trim();
            objRegistrationUser.BankName = Convert.ToString(txtBankName.Text).Trim().ToUpper(); ;
            objRegistrationUser.Amount = Convert.ToString(txtAmount.Text).Trim();
            objRegistrationUser.DraftDate = Convert.ToString(txtDraftDate.Text).Trim();
            objRegistrationUser.CurrentDate = Convert.ToString(txtCurrentDate.Text).Trim();
            objRegistrationUser.Place = Convert.ToString(txtPlace.Text).Trim().ToUpper(); ;
            objRegistrationUser.IsOptedOnlineExamination = chkOnlineExamOpted.Checked ? 1 : 0;
            objRegistrationUser.StudyCenter = ddlSchool.SelectedItem.Text;//rdoSOA.Checked ? rdoSOA.Text : rdoSOA_N.Checked ? rdoSOA_N.Text : rdoSOA_F.Text;

            if (rdoOLExamDates.Items.Count > 0)
            {
                objRegistrationUser.OptedOnlineExaminatioDate = rdoOLExamDates.SelectedValue.ToString();
                objRegistrationUser.OptedOnlineExaminatioDateVal = rdoOLExamDates.SelectedItem.Text.ToString();
            }
            else
            {
                objRegistrationUser.OptedOnlineExaminatioDate = "";
                objRegistrationUser.OptedOnlineExaminatioDateVal = "";
            }

            //--for Choosen centers
            for (int intLoop = 0; intLoop <= lstCenters.Items.Count - 1; intLoop++)
            {
                if (lstCenters.Items[intLoop].Selected)
                {
                    objRegistrationUser.ChosenCenters += lstCenters.Items[intLoop].Text + ",";
                }
            }

            try
            {
                if (hdPhoto.Value == null) hdPhoto.Value = "";
                if (hdSignature.Value == null) hdSignature.Value = "";
               
                objRegistrationUser.ImageName = hdPhoto.Value;
                objRegistrationUser.SignatureName = hdSignature.Value;

                if (hdRegistrationNo.Value != "")
                {
                    if (objRegistrationUser.ImageName != "")
                    {
                        objRegistrationUser.ImageName = "P" + hdRegistrationNo.Value + objRegistrationUser.ImageName.Substring(objRegistrationUser.ImageName.LastIndexOf("."));
                    }
                    if (objRegistrationUser.SignatureName != "")
                    {
                        objRegistrationUser.SignatureName = "S" + hdRegistrationNo.Value + objRegistrationUser.SignatureName.Substring(objRegistrationUser.SignatureName.LastIndexOf("."));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objRegistrationUser;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objclsCommon = null;
        }

    }
   
    private void PopulateControl(ObjStudentRegistrationInfo objRegistrationUser)
    {
        try
        {
            hdRegistrationNo.Value = objRegistrationUser.RegistrationNumber;
            txtRegistrationNo.Text = objRegistrationUser.RegistrationNumber;
            txtFirstName.Text = objRegistrationUser.FirstName;
            txtMiddleName.Text = objRegistrationUser.MiddleName;
            txtLastName.Text = objRegistrationUser.LastName;
            ddlGender.SelectedValue = SelectedValueofDropDown(ddlGender, objRegistrationUser.Gender);
            dtDOB.Text = objRegistrationUser.DOB;
            txtNationality.Text = objRegistrationUser.Nationality;
            ddlEducationStatus.SelectedValue = SelectedValueofDropDown(ddlEducationStatus, objRegistrationUser.EducationStatus);
            txtCaste.Text = objRegistrationUser.Caste;
            ddlCategory.SelectedValue = SelectedValueofDropDown(ddlCategory, objRegistrationUser.Category);
            txtReligion.Text = objRegistrationUser.Religion;
            txtMobileNumber.Text = objRegistrationUser.MobileNumber;
            ddlAdmissionStatus.SelectedValue = SelectedValueofDropDown(ddlAdmissionStatus, objRegistrationUser.AdmissionStatus);
            txtResidencePh.Text = objRegistrationUser.ResidencePhone;
            txtEmailAddress.Text = objRegistrationUser.Email;
            txtCorrAddress.Text = objRegistrationUser.CorrespondenceAddress;
            txtPinCode.Text = objRegistrationUser.PinCode;
            txtParmAddress.Text = objRegistrationUser.PermanentAddress;
            txtParmAddressPinCode.Text = objRegistrationUser.ParAddressPinCode;
            txtPassportNumber.Text = objRegistrationUser.PassportNumber;
            txtPassportIssuedAt.Text = objRegistrationUser.PassportIssuedAt;
            txtCountriesTraveled.Text = objRegistrationUser.CountriesTraveled;
            txtFatherName.Text = objRegistrationUser.FatherName;
            txtMotherName.Text = objRegistrationUser.MotherName;
            txtFatherMobile.Text = objRegistrationUser.FatherMobile;
            txtMotherMobile.Text = objRegistrationUser.MotherMobile;
            txtFatherEmail.Text = objRegistrationUser.FatherEmail;
            txtMotherEmail.Text = objRegistrationUser.MotherEmail;
            txtAddress.Text = objRegistrationUser.Address;
            ddlCourseApplyingFor.SelectedValue = objRegistrationUser.NameOfCourseApplyingFor;
            txtCourseCode.Text = objRegistrationUser.CourseCode;
            txtAcademicYr.Text = objRegistrationUser.AcedemicYear;
            if (objRegistrationUser.AreApplyAsNRIStudent) rdoIsApplyingAsAForeignStudent.SelectedValue = "1"; else rdoIsApplyingAsAForeignStudent.SelectedValue = "0";
            EntTestName1.Text = objRegistrationUser.EntTestName1;
            EntTestDate1.Text = objRegistrationUser.EntTestDate1;
            EntTestPlace1.Text = objRegistrationUser.EntTestPlace1;
            EntTestRank1.Text = objRegistrationUser.EntTestRank1;
            EntTestName2.Text = objRegistrationUser.EntTestName2;
            EntTestDate2.Text = objRegistrationUser.EntTestDate2;
            EntTestPlace2.Text = objRegistrationUser.EntTestPlace2;
            EntTestRank2.Text = objRegistrationUser.EntTestRank2;
            EntTestName3.Text = objRegistrationUser.EntTestName3;
            EntTestDate3.Text = objRegistrationUser.EntTestDate3;
            EntTestPlace3.Text = objRegistrationUser.EntTestPlace3;
            EntTestRank3.Text = objRegistrationUser.EntTestRank3;
            EntTestName4.Text = objRegistrationUser.EntTestName4;
            EntTestDate4.Text = objRegistrationUser.EntTestDate4;
            EntTestPlace4.Text = objRegistrationUser.EntTestPlace4;
            EntTestRank4.Text = objRegistrationUser.EntTestRank4;
            txtReasonForApply.Text = objRegistrationUser.ReasonForApply;
            EduQualificationClassX_RollNo.Text = objRegistrationUser.EduQualificationClassX_RollNo;
            EduQualificationClassX_Year.Text = objRegistrationUser.EduQualificationClassX_Year;
            EduQualificationClassX_Stream.Text = objRegistrationUser.EduQualificationClassX_Stream;
            EduQualificationClassX_Board.Text = objRegistrationUser.EduQualificationClassX_Board;
            EduQualificationClassX_Marks.Text = objRegistrationUser.EduQualificationClassX_Marks;
            EduQualificationClassX_MaxMarks.Text = objRegistrationUser.EduQualificationClassX_MaxMarks;
            EduQualificationClassX_Percent.Text = objRegistrationUser.EduQualificationClassX_Percent;
            ddlEduQualificationClassX_Result.SelectedValue = SelectedValueofDropDown(ddlEduQualificationClassX_Result, objRegistrationUser.EduQualificationClassX_Result);
            EduQualificationClassXII_RollNo.Text = objRegistrationUser.EduQualificationClassXII_RollNo;
            EduQualificationClassXII_Year.Text = objRegistrationUser.EduQualificationClassXII_Year;
            EduQualificationClassXII_Stream.Text = objRegistrationUser.EduQualificationClassXII_Stream;
            EduQualificationClassXII_Board.Text = objRegistrationUser.EduQualificationClassXII_Board;
            EduQualificationClassXII_Marks.Text = objRegistrationUser.EduQualificationClassXII_Marks;
            EduQualificationClassXII_MaxMarks.Text = objRegistrationUser.EduQualificationClassXII_MaxMarks;
            EduQualificationClassXII_Percent.Text = objRegistrationUser.EduQualificationClassXII_Percent;
            ddlEduQualificationClassXII_Result.SelectedValue = SelectedValueofDropDown(ddlEduQualificationClassXII_Result, objRegistrationUser.EduQualificationClassXII_Result);
            EduQualificationClassGraduation_RollNo.Text = objRegistrationUser.EduQualificationClassGraduation_RollNo;
            EduQualificationClassGraduation_Year.Text = objRegistrationUser.EduQualificationClassGraduation_Year;
            EduQualificationClassGraduation_Stream.Text = objRegistrationUser.EduQualificationClassGraduation_Stream;
            EduQualificationClassGraduation_Board.Text = objRegistrationUser.EduQualificationClassGraduation_Board;
            EduQualificationClassGraduation_Marks.Text = objRegistrationUser.EduQualificationClassGraduation_Marks;
            EduQualificationClassGraduation_MaxMarks.Text = objRegistrationUser.EduQualificationClassGraduation_MaxMarks;
            EduQualificationClassGraduation_Percent.Text = objRegistrationUser.EduQualificationClassGraduation_Percent;
            ddlEduQualificationClassGraduation_Result.SelectedValue = SelectedValueofDropDown(ddlEduQualificationClassGraduation_Result, objRegistrationUser.EduQualificationClassGraduation_Result);
            EduQualificationClassOther_RollNo.Text = objRegistrationUser.EduQualificationClassOther_RollNo;
            EduQualificationClassOther_Year.Text = objRegistrationUser.EduQualificationClassOther_Year;
            EduQualificationClassOther_Stream.Text = objRegistrationUser.EduQualificationClassOther_Stream;
            EduQualificationClassOther_Board.Text = objRegistrationUser.EduQualificationClassOther_Board;
            EduQualificationClassOther_Marks.Text = objRegistrationUser.EduQualificationClassOther_Marks;
            EduQualificationClassOther_MaxMarks.Text = objRegistrationUser.EduQualificationClassOther_MaxOtherMarks;
            EduQualificationClassOther_Percent.Text = objRegistrationUser.EduQualificationClassOther_Percent;
            ddlEduQualificationClassOther_Result.SelectedValue = SelectedValueofDropDown(ddlEduQualificationClassOther_Result, objRegistrationUser.EduQualificationClassOther_Result);
            txtProfession_OtherQualificationAwards_Text.Text = objRegistrationUser.Profession_OtherQualificationAwards_Text;
            lblSchoolAddress.Text = objRegistrationUser.DetailsWorkExperience_Text;
            txtHobbies.Text = objRegistrationUser.HobbiesAndInterest_Text;
            if (objRegistrationUser.MaritalSatus == "Married") rdoMaritalStatus.SelectedValue = "1"; else rdoMaritalStatus.SelectedValue = "0";
            txtNameOfSpouse.Text = objRegistrationUser.NameOfSpouse;
            txtBloodGroup.Text = objRegistrationUser.BloodGroup;
            if (objRegistrationUser.IsPhysicalDisable) rdoIsPhysicalDisable.SelectedValue = "1"; else rdoIsPhysicalDisable.SelectedValue = "0";
            txtDetailsPhysicalDisable_Text.Text = objRegistrationUser.DetailsPhysicalDisable_Text;
            chkInforationGotFrom_Newspaper.Checked = objRegistrationUser.InforationGotFrom_Newspaper;
            chkInforationGotFrom_Hoarding.Checked = objRegistrationUser.InforationGotFrom_Hoarding;
            chkInforationGotFrom_Internet.Checked = objRegistrationUser.InforationGotFrom_Internet;
            chkInforationGotFrom_Representative.Checked = objRegistrationUser.InforationGotFrom_Representative;
            chkInforationGotFrom_Friends.Checked = objRegistrationUser.InforationGotFrom_Friends;
            txtInforationGotFrom_OtherText.Text = objRegistrationUser.InforationGotFrom_OtherText;
            chkProffIdentityVoterCard.Checked = objRegistrationUser.ProffOfIdentity_VoterCard;
            chkProffIdentityDrivingLicense.Checked = objRegistrationUser.ProffOfIdentity_DL;
            chkProffIdentityPasspport.Checked = objRegistrationUser.ProffOfIdentity_Passport;
            chkProffIdentityPanCard.Checked = objRegistrationUser.ProffOfIdentity_PANCard;
            chkProffIdentityAdharCard.Checked = objRegistrationUser.ProffOfIdentity_AdharCard;
            chkTranscriptsOfQualifications.Checked = objRegistrationUser.OtherDoc_TransriptsOfQualification;
            chkCasteCertificateForSCSTOBC.Checked = objRegistrationUser.OtherDoc_CasteCertificate;
            chkPassportSizePhotograph.Checked = objRegistrationUser.OtherDoc_Photographs;
            chkExperienceCertificateSupportDocuments.Checked = objRegistrationUser.OtherDoc_ExpCertificate;
            chkAddressProof.Checked = objRegistrationUser.OtherDoc_AddProof;
            chkForTFWSIncomeCertificate.Checked = objRegistrationUser.OtherDoc_TFWSIncomeCertificate;
            chkDomicileCertificate.Checked = objRegistrationUser.OtherDoc_DomicileCertificate;
            chkMedicalCertificate.Checked = objRegistrationUser.OtherDoc_MedicalCertificate;
            chkIconfirmThatInformationIHaveGivenIsTrue.Checked = objRegistrationUser.Declaration_InformationGivenIsTrue;
            chkIUnderstandThatTheDataInThisFormWillNot.Checked = objRegistrationUser.Declaration_FormDataWillNotProvided_ExternalOrganisation;
            chkIConfirmThatIFulfillTheMinimumEligibilityCriteria.Checked = objRegistrationUser.Declaration_ConfirmingMinimumEligCriteria;
            chkIConfirmThatIHaveReadTheEnclosedDeclaration.Checked = objRegistrationUser.Declaration_ReadEnclosedDeclaration;
            txtDraftNumber.Text = objRegistrationUser.DraftNo;
            txtBankName.Text = objRegistrationUser.BankName;
            txtAmount.Text = objRegistrationUser.Amount;
            txtDraftDate.Text = objRegistrationUser.DraftDate;
            txtCurrentDate.Text = objRegistrationUser.CurrentDate;
            txtPlace.Text = objRegistrationUser.Place;

            chkOnlineExamOpted.Checked = objRegistrationUser.IsOptedOnlineExamination == 1 ? true : false;
            if (objRegistrationUser.StudyCenter == rdoSOA.Text) rdoSOA.Checked = true;
            if (objRegistrationUser.StudyCenter == rdoSOA_N.Text) rdoSOA_N.Checked = true;
            if (objRegistrationUser.StudyCenter == rdoSOA_F.Text) rdoSOA_F.Checked = true;

            rdoOLExamDates.SelectedValue = objRegistrationUser.OptedOnlineExaminatioDate;
            if (objRegistrationUser.ChosenCenters != null && objRegistrationUser.ChosenCenters != "")
            {
                string[] strChoosencenters = objRegistrationUser.ChosenCenters.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                //--for Choosen centers
                for (int intLoop = 0; intLoop <= lstCenters.Items.Count - 1; intLoop++)
                    foreach (string s in strChoosencenters)
                        if (lstCenters.Items[intLoop].Text.Trim().ToUpper() == s.Trim().ToUpper())
                            lstCenters.Items[intLoop].Selected = true;
            }

            if (Request.QueryString.Keys.Count != 0 && Convert.ToString(Request.QueryString["Confirm"]) == "true")
            {
                lstCenters.Enabled = false;
                ddlCourseApplyingFor.Enabled = false;
            }

            UserImage.ImageUrl = UserImage.ResolveUrl("../UploadedDocument/Olympiad/" + objRegistrationUser.ImageName + "?" + DateTime.Now.Ticks.ToString());
            ImageSignature.ImageUrl = ImageSignature.ResolveUrl("../UploadedDocument/Olympiad/" + objRegistrationUser.SignatureName + "?" + DateTime.Now.Ticks.ToString());

            UserImage.ImageUrl = VirtualPathUtility.ToAbsolute("../UploadedDocument/Olympiad/" + objRegistrationUser.ImageName + "?" + DateTime.Now.Ticks.ToString());
            ImageSignature.ImageUrl = VirtualPathUtility.ToAbsolute("../UploadedDocument/Olympiad/" + objRegistrationUser.SignatureName + "?" + DateTime.Now.Ticks.ToString());
        }
        catch (Exception ex)
        {
            lblAllowedImageText.Visible = true;
            lblAllowedImageText.Text = ex.Message;
            throw ex;
        }
    }
   
    private string SelectedValueofDropDown(DropDownList ddl, string strValue)
    {
        string iSelectedValue = "";
        try
        {
            foreach (ListItem lst in ddl.Items)
            {
                if (lst.Text == strValue) iSelectedValue = lst.Value;
            }
        }
        catch (Exception ex)
        { }
        return iSelectedValue;
    }
    private string SelectedValueofRadioButton(RadioButtonList rdo, string strValue)
    {
        string iSelectedValue = "";
        try
        {
            foreach (ListItem lst in rdo.Items)
            {
                if (lst.Text == strValue) iSelectedValue = lst.Value;
            }
        }
        catch (Exception ex)
        { }
        return iSelectedValue;
    }
    private bool iif(string p)
    {
        if (p == "1") { return true; } else { return false; }
    }
    public string GetUniqueRegistrationNumber()
    {
        string strRegistrationNumber = string.Empty;
        SqlConnection con = null;
        System.Text.StringBuilder bldRunningNo = new System.Text.StringBuilder();
        try
        {
            con = new SqlConnection(user.ConnectionString);
            con.Open();

            bldRunningNo.Append("DECLARE @W_REG_NO INT BEGIN TRAN ");
            bldRunningNo.Append("SELECT @W_REG_NO=[VALUE] FROM SEETINGS WHERE [KEY]='OLYMPIAD_REGISTRAION_RUNNING_NO '");
            bldRunningNo.Append("UPDATE SEETINGS SET  [VALUE]=@W_REG_NO+1  WHERE [KEY]='OLYMPIAD_REGISTRAION_RUNNING_NO '");
            bldRunningNo.Append("SELECT @W_REG_NO+1 COMMIT TRAN ");
            SqlCommand cmd = new SqlCommand(bldRunningNo.ToString(), con);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strRegistrationNumber = dr[0].ToString();
                    if (strRegistrationNumber.Length == 1) strRegistrationNumber = "00000" + strRegistrationNumber;
                    if (strRegistrationNumber.Length == 2) strRegistrationNumber = "0000" + strRegistrationNumber;
                    if (strRegistrationNumber.Length == 3) strRegistrationNumber = "000" + strRegistrationNumber;
                    if (strRegistrationNumber.Length == 4) strRegistrationNumber = "00" + strRegistrationNumber;
                    if (strRegistrationNumber.Length == 5) strRegistrationNumber = "0" + strRegistrationNumber;
                    strRegistrationNumber = "O" + strRegistrationNumber;
                }
                dr.Close();
            }
            else
            {
                strRegistrationNumber = "O000001";
            }
        }
        catch (Exception ex)
        { }
        finally
        {
            con.Close();
        }
        return strRegistrationNumber;
    }

    private void SaveImageFile(string RegistrationNo)
    {
        try
        {
            string strImageName = string.Empty;
            string strSignatureName = string.Empty;
            string strFilepath = string.Empty;
            string strNewFilepath = string.Empty;

            if (hdPhoto.Value.Trim() != "")
            {
                strImageName = "P" + RegistrationNo + hdPhoto.Value.Trim().Substring(hdPhoto.Value.Trim().LastIndexOf("."));
                strFilepath = System.IO.Path.Combine(Server.MapPath("."), "Uploads/ManageRequests/Temp/", hdPhoto.Value.Trim());

                if (System.IO.File.Exists(strFilepath))
                {
                    strNewFilepath = System.IO.Path.Combine(Server.MapPath("."), "Uploads/ManageRequests/", strImageName);
                    System.IO.File.Copy(strFilepath, strNewFilepath, true);

                    //Delete Temp File
                    try
                    {
                        System.IO.File.Delete(strFilepath);
                    }
                    catch { }
                }
            }

            if (hdSignature.Value.Trim() != "")
            {
                strImageName = "S" + RegistrationNo + hdSignature.Value.Trim().Substring(hdSignature.Value.Trim().LastIndexOf("."));

                strFilepath = System.IO.Path.Combine(Server.MapPath("."), "Uploads/ManageRequests/Temp/" + hdSignature.Value.Trim());
                if (System.IO.File.Exists(strFilepath))
                {
                    strNewFilepath = System.IO.Path.Combine(Server.MapPath("."), "Uploads/ManageRequests/", strImageName);
                    System.IO.File.Copy(strFilepath, strNewFilepath, true);

                    //Delete Temp File
                    try
                    {
                        System.IO.File.Delete(strFilepath);
                    }
                    catch { }
                }
            }
        }
        catch (Exception ex)
        { }
    }

    protected void clearControls()
    {
        try
        {
            hdPhoto.Value = "";
            hdRegistrationNo.Value  = "";
            hdSignature.Value = "";
            txtRegistrationNo.Text = "Pending";
            txtAcademicYr.Text = "";
            txtAddress.Text = "";
            txtAmount.Text = "";
            txtBankName.Text = "";
            txtBloodGroup.Text = "";
            txtCaste.Text = "";
            txtCorrAddress.Text = "";
            txtCountriesTraveled.Text = "";
            txtCurrentDate.Text = "";
            txtDetailsPhysicalDisable_Text.Text = "";
            txtDraftDate.Text = "";
            txtDraftNumber.Text = "";
            txtEmailAddress.Text = "";
            txtFatherEmail.Text = "";
            txtFatherMobile.Text = "";
            txtFatherName.Text = "";
            txtFirstName.Text = "";
            txtHobbies.Text = "";
            txtInforationGotFrom_OtherText.Text = "";
            txtLastName.Text = "";
            txtMiddleName.Text = "";
            txtMobileNumber.Text = "";
            txtMotherEmail.Text = "";
            txtMotherMobile.Text = "";
            txtMotherName.Text = "";
            txtNameOfSpouse.Text = "";
            txtNationality.Text = "";
            txtParmAddress.Text = "";
            txtParmAddressPinCode.Text = "";
            txtPassportIssuedAt.Text = "";
            txtPassportNumber.Text = "";
            txtPinCode.Text = "";
            txtPlace.Text = "";
            txtProfession_OtherQualificationAwards_Text.Text = "";
            txtReasonForApply.Text = "";
            txtReligion.Text = "";
            txtResidencePh.Text = "";
            dtDOB.Text = "";

            EduQualificationClassX_RollNo.Text="";
            EduQualificationClassX_Board.Text="";
            EduQualificationClassX_Marks.Text="";
            EduQualificationClassX_MaxMarks.Text="";
            EduQualificationClassX_Percent.Text="";
            EduQualificationClassX_Stream.Text="";
            EduQualificationClassX_Year.Text="";

            EduQualificationClassXII_RollNo.Text = "";
            EduQualificationClassXII_Board.Text = "";
            EduQualificationClassXII_Marks.Text = "";
            EduQualificationClassXII_MaxMarks.Text = "";
            EduQualificationClassXII_Percent.Text = "";
            EduQualificationClassXII_Stream.Text = "";
            EduQualificationClassXII_Year.Text = "";

            lstCenters.SelectedIndex = -1;
 
            chkAddressProof.Checked = false;
            chkCasteCertificateForSCSTOBC.Checked = false;
            chkDomicileCertificate.Checked = false;
            chkExperienceCertificateSupportDocuments.Checked = false;
            chkFillParmanentAddress.Checked = false;
            chkForTFWSIncomeCertificate.Checked = false;
            chkIConfirmThatIFulfillTheMinimumEligibilityCriteria.Checked = false;
            chkIConfirmThatIHaveReadTheEnclosedDeclaration.Checked = false;
            chkIconfirmThatInformationIHaveGivenIsTrue.Checked = false;
            chkInforationGotFrom_Friends.Checked = false;
            chkInforationGotFrom_Hoarding.Checked = false;
            chkInforationGotFrom_Internet.Checked = false;
            chkInforationGotFrom_Internet.Checked = false;
            chkInforationGotFrom_Newspaper.Checked = false;
            chkInforationGotFrom_Representative.Checked = false;
            chkIUnderstandThatTheDataInThisFormWillNot.Checked = false;
            chkMedicalCertificate.Checked = false;
            chkOnlineExamOpted.Checked = false;
            chkPassportSizePhotograph.Checked = false;
            chkProffIdentityAdharCard.Checked = false;
            chkProffIdentityDrivingLicense.Checked = false;
            chkProffIdentityPanCard.Checked = false;
            chkProffIdentityPasspport.Checked = false;
            chkProffIdentityVoterCard.Checked = false;
            chkTranscriptsOfQualifications.Checked = false;

            ImageSignature.ImageUrl = "~/Common/Images/SampleSignature.jpg";
            UserImage.ImageUrl = "~/Common/Images/SampleImage.jpg";

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void PopulateControl(DataTable dtDetails)
    {
        try
        {
            hdID.Value = dtDetails.Rows[0]["ID"].ToString();
            hdRegistrationNo.Value = dtDetails.Rows[0]["RegistrationNumber"].ToString();
            txtRegistrationNo.Text = dtDetails.Rows[0]["RegistrationNumber"].ToString();
            txtFirstName.Text = dtDetails.Rows[0]["FirstName"].ToString();
            txtMiddleName.Text = dtDetails.Rows[0]["MiddleName"].ToString();
            txtLastName.Text = dtDetails.Rows[0]["LastName"].ToString(); 
            ddlGender.SelectedItem.Text  = dtDetails.Rows[0]["Gender"].ToString();
            dtDOB.Text = dtDetails.Rows[0]["DOB"].ToString(); 	 
            txtNationality.Text = dtDetails.Rows[0]["Nationality"].ToString();
            ddlEducationStatus.SelectedItem.Text = dtDetails.Rows[0]["Status"].ToString();  
            txtCaste.Text =  dtDetails.Rows[0]["Caste"].ToString();
            ddlCategory.SelectedItem.Text = dtDetails.Rows[0]["Category"].ToString();  
            txtReligion.Text =  dtDetails.Rows[0]["Religion"].ToString(); 
            txtMobileNumber.Text = dtDetails.Rows[0]["MobileNumber"].ToString();
            ddlAdmissionStatus.SelectedItem.Text =dtDetails.Rows[0]["AdmissionStatus"].ToString();  
            txtResidencePh.Text =  dtDetails.Rows[0]["ResidencePhone"].ToString();
            txtEmailAddress.Text =  dtDetails.Rows[0]["Email"].ToString();
            txtCorrAddress.Text =  dtDetails.Rows[0]["CorrespondenceAddress"].ToString();
            txtPinCode.Text =  dtDetails.Rows[0]["PinCode"].ToString();
            txtParmAddress.Text =  dtDetails.Rows[0]["PermanentAddress"].ToString();
            txtParmAddressPinCode.Text =  dtDetails.Rows[0]["ParAddressPinCode"].ToString();
            txtPassportNumber.Text =  dtDetails.Rows[0]["PassportNumber"].ToString();
            txtPassportIssuedAt.Text =  dtDetails.Rows[0]["PassportIssuedAt"].ToString();
            txtCountriesTraveled.Text =  dtDetails.Rows[0]["CountriesTraveled"].ToString();
            txtFatherName.Text =  dtDetails.Rows[0]["FatherName"].ToString();
            txtMotherName.Text =  dtDetails.Rows[0]["MotherName"].ToString();
            txtFatherMobile.Text =  dtDetails.Rows[0]["FatherMobile"].ToString();
            txtMotherMobile.Text =  dtDetails.Rows[0]["MotherMobile"].ToString();
            txtFatherEmail.Text = dtDetails.Rows[0]["FatherEmail"].ToString();
            txtMotherEmail.Text =  dtDetails.Rows[0]["MotherEmail"].ToString();
            txtAddress.Text =  dtDetails.Rows[0]["Address"].ToString();
            ddlCourseApplyingFor.SelectedValue = dtDetails.Rows[0]["NameOfCourseApplyingFor"].ToString();
            txtCourseCode.Text = dtDetails.Rows[0]["CourseCode"].ToString();
            txtAcademicYr.Text = dtDetails.Rows[0]["AcedemicYear"].ToString();
            txtReasonForApply.Text =  dtDetails.Rows[0]["ReasonForApply"].ToString();
            EduQualificationClassX_RollNo.Text =dtDetails.Rows[0]["EduQualificationClassX_RollNo"].ToString();
            EduQualificationClassX_Year.Text = dtDetails.Rows[0]["EduQualificationClassX_Year"].ToString();
            EduQualificationClassX_Stream.Text = dtDetails.Rows[0]["EduQualificationClassX_Stream"].ToString();
            EduQualificationClassX_Board.Text = dtDetails.Rows[0]["EduQualificationClassX_Board"].ToString();
            EduQualificationClassX_Marks.Text = dtDetails.Rows[0]["EduQualificationClassX_Marks"].ToString();
            EduQualificationClassX_MaxMarks.Text = dtDetails.Rows[0]["EduQualificationClassX_MaxMarks"].ToString();
            EduQualificationClassX_Percent.Text = dtDetails.Rows[0]["EduQualificationClassX_Percent"].ToString();
            ddlEduQualificationClassX_Result.SelectedItem.Text = dtDetails.Rows[0]["EduQualificationClassX_Result"].ToString();

            EduQualificationClassXII_RollNo.Text = dtDetails.Rows[0]["EduQualificationClassXII_RollNo"].ToString();
            EduQualificationClassXII_Year.Text = dtDetails.Rows[0]["EduQualificationClassXII_Year"].ToString();
            EduQualificationClassXII_Stream.Text = dtDetails.Rows[0]["EduQualificationClassXII_Stream"].ToString();
            EduQualificationClassXII_Board.Text = dtDetails.Rows[0]["EduQualificationClassXII_Board"].ToString();
            EduQualificationClassXII_Marks.Text = dtDetails.Rows[0]["EduQualificationClassXII_Marks"].ToString();
            EduQualificationClassXII_MaxMarks.Text = dtDetails.Rows[0]["EduQualificationClassXII_MaxMarks"].ToString();
            EduQualificationClassXII_Percent.Text = dtDetails.Rows[0]["EduQualificationClassXII_Percent"].ToString();
            ddlEduQualificationClassXII_Result.SelectedItem.Text = dtDetails.Rows[0]["EduQualificationClassXII_Result"].ToString();

            ddlSchool.SelectedItem.Text = dtDetails.Rows[0]["OptedStudyCenter"].ToString();
            lblSchoolAddress.Text = dtDetails.Rows[0]["DetailsWorkExperience_Text"].ToString();
            chkInforationGotFrom_Newspaper.Checked = dtDetails.Rows[0]["InforationGotFrom_Newspaper"].ToString() == "False" ? false : true;
            chkInforationGotFrom_Hoarding.Checked = dtDetails.Rows[0]["InforationGotFrom_Hoarding"].ToString() == "False" ? false : true;
            chkInforationGotFrom_Internet.Checked = dtDetails.Rows[0]["InforationGotFrom_Internet"].ToString() == "False" ? false : true;
            chkInforationGotFrom_Representative.Checked = dtDetails.Rows[0]["InforationGotFrom_Representative"].ToString() == "False" ? false : true;
            chkInforationGotFrom_Friends.Checked = dtDetails.Rows[0]["InforationGotFrom_Friends"].ToString() == "False" ? false : true;
            txtInforationGotFrom_OtherText.Text=   dtDetails.Rows[0]["InforationGotFrom_OtherText"].ToString() ;
            chkProffIdentityVoterCard.Checked = dtDetails.Rows[0]["ProffOfIdentity_VoterCard"].ToString() == "False" ? false : true;
            chkProffIdentityDrivingLicense.Checked = dtDetails.Rows[0]["ProffOfIdentity_DL"].ToString() == "False" ? false : true;
            chkProffIdentityPasspport.Checked = dtDetails.Rows[0]["ProffOfIdentity_Passport"].ToString() == "False" ? false : true;
            chkProffIdentityPanCard.Checked = dtDetails.Rows[0]["ProffOfIdentity_PANCard"].ToString() == "False" ? false : true;
            chkProffIdentityAdharCard.Checked = dtDetails.Rows[0]["ProffOfIdentity_AdharCard"].ToString() == "False" ? false : true;
            chkTranscriptsOfQualifications.Checked = dtDetails.Rows[0]["OtherDoc_TransriptsOfQualification"].ToString() == "False" ? false : true;
            chkCasteCertificateForSCSTOBC.Checked = dtDetails.Rows[0]["OtherDoc_CasteCertificate"].ToString() == "False" ? false : true;
            chkPassportSizePhotograph.Checked = dtDetails.Rows[0]["OtherDoc_Photographs"].ToString() == "False" ? false : true;
            chkExperienceCertificateSupportDocuments.Checked = dtDetails.Rows[0]["OtherDoc_ExpCertificate"].ToString() == "False" ? false : true;
            chkAddressProof.Checked = dtDetails.Rows[0]["OtherDoc_AddProof"].ToString() == "False" ? false : true;
            chkForTFWSIncomeCertificate.Checked = dtDetails.Rows[0]["OtherDoc_TFWSIncomeCertificate"].ToString() == "False" ? false : true;
            chkDomicileCertificate.Checked = dtDetails.Rows[0]["OtherDoc_DomicileCertificate"].ToString() == "False" ? false : true;
            chkMedicalCertificate.Checked = dtDetails.Rows[0]["OtherDoc_MedicalCertificate"].ToString() == "False" ? false : true;
            chkIconfirmThatInformationIHaveGivenIsTrue.Checked = dtDetails.Rows[0]["Declaration_InformationGivenIsTrue"].ToString() == "False" ? false : true;
            chkIUnderstandThatTheDataInThisFormWillNot.Checked = dtDetails.Rows[0]["Declaration_FormDataWillNotProvided_ExternalOrganisation"].ToString() == "False" ? false : true;
            chkIConfirmThatIFulfillTheMinimumEligibilityCriteria.Checked = dtDetails.Rows[0]["Declaration_ConfirmingMinimumEligCriteria"].ToString() == "False" ? false : true;
            chkIConfirmThatIHaveReadTheEnclosedDeclaration.Checked = dtDetails.Rows[0]["Declaration_ReadEnclosedDeclaration"].ToString() == "False" ? false : true;
            rdoOLExamDates.SelectedValue = dtDetails.Rows[0]["OptedOnlineExaminatioDate"].ToString();

            //Photo & Signature
            if (dtDetails.Rows[0]["Image"].ToString() != "")
            {
                hdPhoto.Value = dtDetails.Rows[0]["Image"].ToString();
                UserImage.ImageUrl = "~/Operation/Uploads/ManageRequests/" + dtDetails.Rows[0]["Image"].ToString();
            }
            else
            {
                UserImage.ImageUrl="~/Common/Images/SampleImage.jpg";
            }

            if( dtDetails.Rows[0]["Signature"].ToString()!="")
            {
                hdSignature.Value = dtDetails.Rows[0]["Signature"].ToString();
                ImageSignature.ImageUrl = "~/Operation/Uploads/ManageRequests/" + dtDetails.Rows[0]["Signature"].ToString(); 
            }
            else
            {
                ImageSignature.ImageUrl = "~/Common/Images/SampleSignature.jpg";
            }        
        }
        catch (Exception ex)
        {
            lblAllowedImageText.Visible = true;
            lblAllowedImageText.Text = ex.Message;
            throw ex;
        }
    }

    #region "Grid Events"
    protected void grdStyled_PageIndexChanging(Object sender, GridViewPageEventArgs e)
    {
        grdStyled.PageIndex = e.NewPageIndex;
        getOlympiadRegisteredDetails();
    }
    protected void grdStyled_SelectedIndexChanged(object sender, EventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objclsCommon = new clsCommon();
        clsOlympiad objclsOlympiad = new clsOlympiad();
        objOlympiad Olympiad = new objOlympiad();
        DataTable dtDetails = new DataTable();
        int SchoolID;
        string ImgPath = string.Empty;

        try
        {
            lblMessage.Text = string.Empty;
            int.TryParse(grdStyled.DataKeys[grdStyled.SelectedIndex]["SCHOOL_ID"].ToString(), out SchoolID);
            Olympiad.SchoolID = SchoolID;
            Olympiad.searchText = string.Empty;
            Olympiad.ConnectionString = user.ConnectionString;
            dtDetails = objclsOlympiad.getOlympiadRegistrationDetails(Olympiad);

            if (SchoolID >= 0)
            {
                PopulateControl(dtDetails);
                btnSubmit.Text = "Update";
            }

            UpdatePanel1.Update();
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "setFocusToEntryTab('EntryTab')", true);
        }
        catch (Exception ex)
        {
            _strMsgType = "Error";
            _strAlertMessage = ex.Message;
            objclsErrorLogger.WriteError(System.IO.Path.GetFileName(Request.Path), "", user == null ? "" : user.ConnectionString, ex);
            Response.Redirect("~/Operation/Err.aspx");
        }
        finally
        {
            if (_strAlertMessage != string.Empty)
            {
                lblMessage.Text = _strAlertMessage;
                objclsCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);

                UpdatePanel1.Update();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "setFocusToEntryTab('EntryTab')", true);
            objclsErrorLogger = null;
            objclsCommon = null;
        }
    }
    protected void grdStyled_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        DataTable dtResult;
        clsCommon objclsCommon = new clsCommon();
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsOlympiad objclsOlympiad = new clsOlympiad();
        objOlympiad Olympiad = new objOlympiad();

        string _strAlertMessage = string.Empty;
        string confirmValue = string.Empty;
        string strSOVName = string.Empty;

        int schoolID;
        try
        {
            lblMessage.Text = "";

            Olympiad.ConnectionString = user.ConnectionString;
            Olympiad.USerID = user.UserId;

            int.TryParse(grdStyled.DataKeys[e.RowIndex]["SCHOOL_ID"].ToString(), out schoolID);

            Olympiad.SchoolID = schoolID;
            dtResult = objclsOlympiad.DeleteOlympiadRegistration(Olympiad);

            if (dtResult != null)
            {
                if (dtResult.Columns.Contains("ERROR"))
                {
                    if (dtResult.Rows[0]["ERROR"].ToString().Trim() != "")
                    {
                        _strMsgType = "Error_DB";
                        _strAlertMessage = dtResult.Rows[0]["ERROR"].ToString().Trim();
                        return;
                    }
                }

                _strMsgType = "Success";
                _strAlertMessage = dtResult.Rows[0]["SUCCESS"].ToString().Trim();

                //Refresh the Grid
                getOlympiadRegisteredDetails();
                UpdatePanel2.Update();

                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "setFocusToEntryTab('UWList')", true);
                lblMessage.Text = _strAlertMessage;
            }
        }
        catch (Exception ex)
        {
            _strMsgType = "Error";
            _strAlertMessage = ex.Message;
            objclsErrorLogger.WriteError(System.IO.Path.GetFileName(Request.Path), "", user == null ? "" : user.ConnectionString, ex);
            Response.Redirect("~/Operation/Err.aspx");
        }
        finally
        {

            //Alert Message
            if (_strAlertMessage != string.Empty && _strAlertMessage.Trim() != "")
            {
                lblMessage.Text = _strAlertMessage;
                objclsCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);
                UpdatePanel1.Update();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }

            objclsOlympiad = null;
            dtResult = null;
            objclsCommon = null;
            Olympiad = null;
            objclsErrorLogger = null;
        }
    }
    protected void grdStyled_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strStatus = string.Empty;
            strStatus = e.Row.Cells[2].Text.ToString().Trim();

            if (strStatus == "ACTIVE")
            {
                e.Row.Cells[2].BackColor = System.Drawing.Color.Green;
                e.Row.Cells[2].ForeColor = System.Drawing.Color.White;
            }
            else if (strStatus == "PENDING")
            {
                e.Row.Cells[2].BackColor = System.Drawing.Color.Yellow;
                e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
            }

            else if (strStatus == "DEACTIVATED")
            {
                e.Row.Cells[2].BackColor = System.Drawing.Color.Gray;
                e.Row.Cells[2].ForeColor = System.Drawing.Color.White;
                e.Row.Enabled = false;
            }

        }

    }
    #endregion
    #region "Page Methods"
    private void setSelectedSchoolAdress()
    {
        lblSchoolAddress.Text = ddlSchool.SelectedValue.ToString();
        UpdatePanel1.Update();
    }
    #endregion
    #region "Controls events"
    protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
    {
        setSelectedSchoolAdress();
    }

    protected void ddlSchool_TextChanged(object sender, EventArgs e)
    {
        setSelectedSchoolAdress();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objclsCommon = new clsCommon();
        string _strAlertMessage = string.Empty;

        try
        {
            getOlympiadRegisteredDetails();
        }
        catch (Exception ex)
        {
            _strMsgType = "Error";
            _strAlertMessage = ex.Message;
            objclsErrorLogger.WriteError(System.IO.Path.GetFileName(Request.Path), "", user == null ? "" : user.ConnectionString, ex);
            Response.Redirect("~/Operation/Err.aspx");
        }
        finally
        {
            if (_strAlertMessage != string.Empty && _strAlertMessage.Trim() != "")
            {
                lblMessage.Text = _strAlertMessage;
                objclsCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }
            objclsCommon = null;
            objclsErrorLogger = null;
        }
    }
    protected void btnUploadImage_Click(object sender, EventArgs e)
    {
        string _strAlertMessage = string.Empty;
        string strFolderName = string.Empty;
        string strUniqueFileName = string.Empty;
        clsCommon objclsCommon = new clsCommon();
        try
        {
            if (FileUploadImage.HasFile == false) return;

            lblImageError.Visible = false;
            int filesize = (FileUploadImage.PostedFile.ContentLength) / 1024;

            if (!FileUploadImage.PostedFile.ContentType.Trim().ToUpper().Contains("IMAGE"))
            {
                _strAlertMessage = "Please select the correct file format.";
                lblImageError.Visible = true;
                return;
            }

            if (filesize > Convert.ToInt32(ConfigurationManager.AppSettings["MaxImageSizeForOnlineApplication"]))
            {
                _strAlertMessage = "The file size exceeds the allowed limit.";
                lblImageError.Visible = true;
                return;
            }

            //FOR FILE SERVER ACTIVITY STARTS
            try
            {
                if (FileUploadImage.HasFile == true)
                {
                    strFolderName = System.IO.Path.Combine(Server.MapPath("."), "Uploads/ManageRequests", "Temp");

                    if (System.IO.Directory.Exists(strFolderName) == false)
                    {
                        System.IO.Directory.CreateDirectory(strFolderName);
                    }
                    if (System.IO.Directory.Exists(strFolderName))
                    {
                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            HttpPostedFile PostedFile = FileUploadImage.PostedFile;// Request.Files[i];
                            if (PostedFile.ContentLength > 0)
                            {
                                strUniqueFileName = "P" + Session.SessionID + PostedFile.FileName.Substring(PostedFile.FileName.LastIndexOf("."));
                                PostedFile.SaveAs(strFolderName + "\\" + strUniqueFileName);
                            }
                        }

                        hdPhoto.Value = strUniqueFileName;//"~/Operation/Uploads/ManageRequests/Temp/swami.jpg"
                        UserImage.ImageUrl = "~/Operation/Uploads/ManageRequests/Temp/" + strUniqueFileName;
                        //System.IO.Path.Combine(Server.MapPath("."),  "Uploads/ManageRequests/Temp" , strUniqueFileName);
                        UpdatePanel1.Update();
                    }
                }
            }
            catch { }
            //FOR FILE SERVER ACTIVITY ENDS
        }
        catch (Exception ex)
        {
            lblImageError.Text = ex.Message;
            objclsCommon.setLabelMessage(lblMessage, "Error");
            ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);
            UpdatePanel1.Update();
        }
        finally
        {
            if (_strAlertMessage != string.Empty)
            {
                lblImageError.Text = _strAlertMessage;
                objclsCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);
                UpdatePanel1.Update();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }
            objclsCommon = null;
        }
    }
    protected void btnUploadSignature_Click(object sender, EventArgs e)
    {
        string _strAlertMessage = string.Empty;
        string strFolderName = string.Empty;
        string strUniqueFileName = string.Empty;
        clsCommon objclsCommon = new clsCommon();
        try
        {
            if (FileUploadSignature.HasFile == false) return;
            lblSignatureError.Visible = false;
            int filesize = FileUploadSignature.PostedFile.ContentLength / 1024;

            if (!FileUploadSignature.PostedFile.ContentType.Trim().ToUpper().Contains("IMAGE"))
            {
                _strAlertMessage = "Please select the correct file format.";
                lblSignatureError.Visible = true;
                return;
            }

            if (filesize > Convert.ToInt32(ConfigurationManager.AppSettings["MaxSignatureSizeForOnlineApplication"]))
            {
                _strAlertMessage = "The file size exceeds the allowed limit.";
                lblSignatureError.Visible = true;
                return;
            }

            //FOR FILE SERVER ACTIVITY STARTS
            try
            {
                if (FileUploadSignature.HasFile == true)
                {
                    strFolderName = System.IO.Path.Combine(Server.MapPath("."), "Uploads/ManageRequests", "Temp");

                    if (System.IO.Directory.Exists(strFolderName) == false)
                    {
                        System.IO.Directory.CreateDirectory(strFolderName);
                    }

                    if (System.IO.Directory.Exists(strFolderName))
                    {
                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            HttpPostedFile PostedFile = FileUploadSignature.PostedFile;// Request.Files[i];
                            if (PostedFile.ContentLength > 0)
                            {
                                strUniqueFileName = "S" + Session.SessionID + PostedFile.FileName.Substring(PostedFile.FileName.LastIndexOf("."));
                                PostedFile.SaveAs(strFolderName + "\\" + strUniqueFileName);
                            }
                        }

                        hdSignature.Value = strUniqueFileName;
                        ImageSignature.ImageUrl = "~/Operation/Uploads/ManageRequests/Temp/" + strUniqueFileName;
                        UpdatePanel1.Update();
                        //System.IO.Path.Combine(Server.MapPath("."), "/Uploads/ManageRequests/Temp/", strUniqueFileName);  
                    }
                }
            }
            catch { }
            //FOR FILE SERVER ACTIVITY ENDS
        }
        catch (Exception ex)
        {
            lblImageError.Text = ex.Message;
            objclsCommon.setLabelMessage(lblMessage, "Error");
            ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);
            UpdatePanel1.Update();
        }
        finally
        {
            if (_strAlertMessage != string.Empty)
            {
                lblImageError.Text = _strAlertMessage;
                objclsCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);
                UpdatePanel1.Update();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }
            objclsCommon = null;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dtResult;
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        ObjStudentRegistrationInfo objRegistrationUser = getUserObject();
        clsOlympiad objclsUserRegistration = new clsOlympiad();
        clsCommon objclsCommon = new clsCommon();
        string strName = string.Empty;
        try
        {
            //Submit the Form after saving the information
            if (Session["Submit"] != null)
            {
                if ((string)Session["Submit"] == "Y")
                {
                   
                    objRegistrationUser.RegistrationNumber = txtRegistrationNo.Text;
                    Session["RegistrationDetails"] = objRegistrationUser;
                    Response.Redirect("Confirmation.aspx");
                }
            }

            if (btnSubmit.Text == "Update" && hdRegistrationNo.Value != "")
            {
                dtResult = objclsUserRegistration.OlympiadRegistration(user.ConnectionString, objRegistrationUser, false);
            }
            else
            {
                dtResult = objclsUserRegistration.OlympiadRegistration(user.ConnectionString, objRegistrationUser, true);
            }

            if (dtResult != null)
            {

                if (dtResult.Rows.Count > 0)
                {
                    if (dtResult.Columns.Contains("ERROR"))
                    {
                        if (dtResult.Rows[0]["ERROR"].ToString().Trim() != "")
                        {
                            _strMsgType = "Error_DB";
                            _strAlertMessage = dtResult.Rows[0]["ERROR"].ToString().Trim();
                            return;
                        }
                    }

                    _strMsgType = "Success";
                    _strAlertMessage = dtResult.Rows[0]["SUCCESS"].ToString().Trim();
                    objRegistrationUser.RegistrationNumber = dtResult.Rows[0]["RegistrationNumber"].ToString().Trim();
                    objRegistrationUser.SchoolEmailId = dtResult.Rows[0]["PRINCIPAL_EMAIL"].ToString().Trim();
                    objRegistrationUser.CourseName = ddlCourseApplyingFor.Text;

                }
            }

            //File Server Activity
            SaveImageFile(objRegistrationUser.RegistrationNumber);

            //Send Notification Starts
            if (btnSubmit.Text != "Update")
            {
                ObjSendEmail SendMail = new ObjSendEmail();
                DataTable dtInformation = new DataTable();
                clsCommon objClsCommon = new clsCommon();

                string strRequestID = string.Empty;
                int smtpPort;
                SendMail.CC = "";

                //Send Mail to Candidate/School/Organizing Committee
                dtInformation = objClsCommon.getMailBody("MailFormat", "MAIL ON NATIONAL OLYMPIAD REGISTRATION FORM SUBMISSION", user.ConnectionString);
                try
                {
                    if (dtInformation != null & dtInformation.Rows.Count > 0)
                    {
                        SendMail.From = user.SMTPUSerID;

                        //To Student/Father/Mother
                        SendMail.To = objRegistrationUser.Email.Trim();

                        if (objRegistrationUser.FatherEmail.Trim() != "")
                        {
                            SendMail.To = SendMail.To + "," + objRegistrationUser.FatherEmail.Trim();
                        }

                        if (objRegistrationUser.MotherEmail.Trim() != "")
                        {
                            SendMail.To = SendMail.To + "," + objRegistrationUser.MotherEmail.Trim();
                        }

                        //Principal
                        SendMail.CC = objRegistrationUser.SchoolEmailId;

                        //Organizating committee
                        SendMail.BCC = objClsCommon.getConfigKeyValue("EMAIL IDS FOR CC OF MAIL MESSAGE FOR EACH OPERATION", "EMAIL RECEPIENTS GROUP", user.ConnectionString); ;

                        if (SendMail.BCC != "")
                        {
                            SendMail.BCC = SendMail.BCC + "," + objClsCommon.getConfigKeyValue("EMAIL IDS FOR CC OF MAIL MESSAGE ON OLYMPIAD FORM SUBMISSION", "EMAIL RECEPIENTS GROUP", user.ConnectionString);
                        }
                        else
                        {
                            SendMail.BCC = objClsCommon.getConfigKeyValue("EMAIL IDS FOR CC OF MAIL MESSAGE ON OLYMPIAD FORM SUBMISSION", "EMAIL RECEPIENTS GROUP", user.ConnectionString);
                        }

                        SendMail.SMTPHost = user.SMTPServer;
                        int.TryParse(user.SMTPPort, out smtpPort);
                        SendMail.SMTPPort = smtpPort;
                        SendMail.UserId = user.SMTPUSerID;
                        SendMail.Password = user.SMTPPassword; ;
                        SendMail.SMTPSSL = user.SMTPSSL == "Y" ? true : false; ;
                        SendMail.MailType = "I";

                        SendMail.Subject = "National Aero Olympiad: New Registration Form Submission Confirmation";
                        strName = objRegistrationUser.FirstName.Trim()  ;
                        strName = strName + objRegistrationUser.MiddleName.Trim() == "" ? "" : " " + objRegistrationUser.MiddleName;
                        strName = strName + objRegistrationUser.LastName.Trim() == "" ? "" : " " + objRegistrationUser.LastName;
 
                        SendMail.MailBody = dtInformation.Rows[0]["MAIL_BODY"].ToString();
                        SendMail.MailBody = SendMail.MailBody.Replace("#SchoolName#", objRegistrationUser.StudyCenter);
                        SendMail.MailBody = SendMail.MailBody.Replace("#SchoolAddress#", lblSchoolAddress.Text);
                        SendMail.MailBody = SendMail.MailBody.Replace("#RegistrationNo#", objRegistrationUser.RegistrationNumber);
                        SendMail.MailBody = SendMail.MailBody.Replace("#StudentName#",strName);
                        SendMail.MailBody = SendMail.MailBody.Replace("#DOB#", objRegistrationUser.DOB);
                        SendMail.MailBody = SendMail.MailBody.Replace("#FatherName#", objRegistrationUser.FatherName);
                        SendMail.MailBody = SendMail.MailBody.Replace("#MotherName#", objRegistrationUser.MotherName);
                        SendMail.MailBody = SendMail.MailBody.Replace("#EmailID#", objRegistrationUser.Email);
                        SendMail.MailBody = SendMail.MailBody.Replace("#OlympiadLevel#", objRegistrationUser.CourseName);
                        SendMail.MailBody = SendMail.MailBody.Replace("#OlympiadDate#", objRegistrationUser.OptedOnlineExaminatioDateVal);
                        SendMail.MailBody = SendMail.MailBody.Replace("#OlympiadCenter#", objRegistrationUser.StudyCenter);
                          
                        //Photo & Signature
                        objRegistrationUser.ImageName=dtResult.Rows[0]["IMAGE"].ToString().Trim();
                        objRegistrationUser.SignatureName = dtResult.Rows[0]["SIGNATURE"].ToString().Trim();

                        if (objRegistrationUser.ImageName != "")
                        {
                            SendMail.MailBody = SendMail.MailBody.Replace("#Photo#", "http://www.soaneemrana.com/lnvm/Operation/Uploads/ManageRequests/" + objRegistrationUser.ImageName);
                        }
                        else
                        {
                            SendMail.MailBody = SendMail.MailBody.Replace("#Photo#", "http://www.soaneemrana.com/lnvm/Common/Images/SampleImage.jpg");
                        }

                        if (objRegistrationUser.SignatureName != "")
                        {
                            SendMail.MailBody = SendMail.MailBody.Replace("#Signature#", "http://www.soaneemrana.com/lnvm/Operation/Uploads/ManageRequests/" + objRegistrationUser.SignatureName);
                        }
                        else
                        {
                            SendMail.MailBody = SendMail.MailBody.Replace("#Signature#", "http://www.soaneemrana.com/lnvm/Common/Images/SampleSignature.jpg");
                        }

                        SendMail.RequestID = objRegistrationUser.RegistrationNumber;
                        SendMail.ConnectionString = user.ConnectionString;
                        objClsCommon.SendEmail(SendMail);
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    SendMail = null;
                    dtInformation = null;
                    objClsCommon = null;
                }
            }
            //Send Notification Ends

            //Refresh the grid
            getOlympiadRegisteredDetails();
            //Clear Controls
            clearControls();
            ////Save Opted Centers Details
        }
        catch (Exception ex)
        {
            _strMsgType = "Error";
            _strAlertMessage = ex.Message;
            objclsErrorLogger.WriteError(System.IO.Path.GetFileName(Request.Path), "", user == null ? "" : user.ConnectionString, ex);
            Response.Redirect("~/Operation/Err.aspx");
        }
        finally
        {
            //Alert Message
            if (_strAlertMessage != string.Empty && _strAlertMessage.Trim() != "")
            {
                lblMessage.Text = _strAlertMessage;
                objclsCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
                UpdatePanel1.Update();
                UpdatePanel2.Update();
            }
            dtResult = null;
            objclsCommon = null;
            objclsErrorLogger = null;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objclsCommon = new clsCommon();
        string _strAlertMessage = string.Empty;

        try
        {
            clearControls();
        }
        catch (Exception ex)
        {
            _strMsgType = "Error";
            _strAlertMessage = ex.Message;
            objclsErrorLogger.WriteError(System.IO.Path.GetFileName(Request.Path), "", user == null ? "" : user.ConnectionString, ex);
            Response.Redirect("~/Operation/Err.aspx");
        }
        finally
        {
            if (_strAlertMessage != string.Empty && _strAlertMessage.Trim() != "")
            {
                lblMessage.Text = _strAlertMessage;
                objclsCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }
            objclsCommon = null;
            objclsErrorLogger = null;
        }
    }
    #endregion

   
}
 