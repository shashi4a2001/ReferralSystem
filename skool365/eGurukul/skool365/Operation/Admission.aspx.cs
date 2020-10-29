using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using  BLCMR;

  public partial class Operation_Admission : System.Web.UI.Page 
    {
 
        string M_UserId = string.Empty;
        string M_strConn= string.Empty;
        string M_strAlertMessage = string.Empty;
       
        ObjPortalUser user  ;
        clsCommon objCommon = new clsCommon();

        protected void Page_Load(object sender, EventArgs e)
        {
          
            try
            {
                //Check User Session and Connection String 
                if (Session["PortalUserDtl"] != null)
                {
                    user = (ObjPortalUser)Session["PortalUserDtl"];
                    M_UserId = user.UserId;
                    M_strConn = user.ConnectionString;
                }
                else
                {
                    Session.Abandon();
                    Response.Redirect("login.aspx?Exp=Y");
                }

                if (!Page.IsPostBack)
                {

                    hdPhoto.Value = "";
                    hdSignature.Value = "";
                    txtAcademicYr.Text = objCommon.DecryptData(ConfigurationManager.AppSettings["AcademicYear"].ToString());
                    txtFinAcademicYr.Text = objCommon.DecryptData(ConfigurationManager.AppSettings["AcademicYear"].ToString());

                    if (user.WorkingLocation == rdoCenter1.Text )
                    {
                        rdoCenter1.Checked = true;
                    }

                    chkIConfirmThatIHaveReadTheEnclosedDeclaration.Text = "I confirm that I have read the <a target='_blank' href='EnclosedDocumentUserRegistration.aspx'>enclosed declaration</a> and will abide by it.";

                    bindCourse();
                    FillCouncellor();
                    getAcademicYear();

                    if (Request.QueryString.Keys.Count != 0 && (Convert.ToString(Request.QueryString["Mode"]).ToUpper() == "ADMISSION" || Convert.ToString(Request.QueryString["Mode"]).ToUpper() == "ONLINE"))
                    {
                        ObjStudentRegistrationInfo objRegistrationUser = new ObjStudentRegistrationInfo();
                        objRegistrationUser = (ObjStudentRegistrationInfo)Session["SearchResult_UserRegistration"];
                        PopulateControl(objRegistrationUser);

                        //--Once Registration accepted from back office, No update would allowed...
                        if (objRegistrationUser.RegistrationAccepted) btnSubmit.Visible = false; else btnSubmit.Text = "Update";
                        if (objRegistrationUser.IsDiplomaStudent == 1) chkDiplomaCheck.Checked = true;
                        if (Convert.ToString(Request.QueryString["Mode"]).ToUpper() == "ONLINE")
                        {
                            btnSubmit.Text = "Save";
                            txtRegistrationNo.Text = "Auto generate value";
                            Session["SearchResult_UserRegistration"] = "";
                        }
                    }
                    else
                    {
                        btnSubmit.Text = "Save";
                        txtRegistrationNo.Text = "Auto generate value";
                        Session["SearchResult_UserRegistration"] = "";
                    }

                    Page.Title = user.Orig_Name + ":Admission Form";
                }

            }
            catch(Exception ex)
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

        #region "Control Events"
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DataTable dtResult = new DataTable();
            ObjStudentRegistrationInfo objRegistrationUser = getUserObject();
            clsUserRegistration objclsUserRegistration = new clsUserRegistration();
            clsCommon objclsCommon = new clsCommon();
            try
            {
                if (btnSubmit.Text == "Update" && ((ObjStudentRegistrationInfo)Session["SearchResult_UserRegistration"]).RegistrationNumber != "0")
                {
                    objRegistrationUser.InsertedBy = M_UserId;
                    dtResult = objclsUserRegistration.StudentAdmission( objRegistrationUser, false);
                }
                else
                {
                    objRegistrationUser.AdmissionStage = "Stage1";
                    objRegistrationUser.InsertedBy = M_UserId;
                    dtResult = objclsUserRegistration.StudentAdmission( objRegistrationUser, true);
                }

                string strSuccess = string.Empty;

                if (dtResult != null)
                {
                    if (dtResult.Rows.Count > 0)
                    {
                        if (dtResult.Columns.Contains("ERROR"))
                        {
                            if (dtResult.Rows[0]["ERROR"].ToString().Trim() != "")
                            {
                                //Saved successfully
                                string scriptAlert = string.Format("alert('{0}');", dtResult.Rows[0]["ERROR"].ToString().Trim());
                                Page pageAlert = HttpContext.Current.CurrentHandler as Page;

                                if (pageAlert != null && !pageAlert.ClientScript.IsClientScriptBlockRegistered("alert"))
                                {
                                    pageAlert.ClientScript.RegisterClientScriptBlock(pageAlert.GetType(), "Admission Alert", scriptAlert, true);
                                }
                                return;
                            }
                        }

                        if (dtResult.Columns.Contains("Success"))
                        {
                            if (dtResult.Rows[0]["Success"].ToString().Trim() != "")
                            {
                                //Saved successfully
                                strSuccess = dtResult.Rows[0]["Success"].ToString().Trim();
                                if (objRegistrationUser.AdmissionStage == "Stage1")
                                {
                                    txtRegistrationNo.Text = dtResult.Rows[0]["RegNo"].ToString().Trim();
                                }

                                //Saved successfully
                                string scriptAlert = string.Format("alert('{0}');", strSuccess);
                                Page pageAlert = HttpContext.Current.CurrentHandler as Page;

                                if (pageAlert != null && !pageAlert.ClientScript.IsClientScriptBlockRegistered("alert"))
                                {
                                    pageAlert.ClientScript.RegisterClientScriptBlock(pageAlert.GetType(), "Admission Alert", scriptAlert, true);
                                }

                                //Redirect to other page for checklist preparation
                                Response.Redirect("UploadChecklist.aspx?RegNo=" + txtRegistrationNo.Text);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                M_strAlertMessage = ex.Message;
            }
            finally
            {
                dtResult = null;
                objRegistrationUser = null;
                objclsUserRegistration = null;
                objclsCommon = null;

                //Alert Message
                if (M_strAlertMessage != string.Empty && M_strAlertMessage.Trim() != "")
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + M_strAlertMessage + "');</script>", false);
                }
            }
        }
        #endregion

        #region "Methods"
        private void FillCouncellor()
        {
            SqlConnection con = null;
            SqlCommand cmd;
            SqlDataReader dr  ;
          
            try
            {
                ListItem lst;

                //Default Select
                lst = new ListItem("Select", "0");
                ddlCouncellor.Items.Add(lst);

                lst = new ListItem("College Dekho", "-1");
                ddlCouncellor.Items.Add(lst);

                con = new SqlConnection(user.ConnectionString );
                con.Open();

                cmd = new SqlCommand("SELECT UID,USER_NAME AS [USER] FROM USER_MASTER UM(NOLOCK) INNER JOIN ROLE_MASTER RM(NOLOCK) ON UM.ROLE_ID=RM.ROLEID WHERE RM.ROLENAME='COUNSELOR' AND RM.STATUS='Active' AND UM.STATUS=0 ", con);
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        lst = new ListItem(Convert.ToString(dr["USER"]), Convert.ToString(dr["UID"]));
                        ddlCouncellor.Items.Add(lst);
                    }

                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                con = null;
                cmd = null;
                dr = null;
            }
        }
        private void getAcademicYear()
        {
            SqlConnection con = null;
            SqlCommand cmd ;
            SqlDataReader dr;
            try
            {

                con = new SqlConnection(user.ConnectionString );
                con.Open();

                cmd = new SqlCommand("SELECT [KEY],[VALUE]  FROM CONFIGURATION (NOLOCK) WHERE [KEY] IN( 'ONLINE ACADEMIC SESSION' ,'ALLOW MODIFY COUNCELOR') ", con);
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (Convert.ToString(dr["KEY"].ToString()) == "ONLINE ACADEMIC SESSION")
                        {
                            txtAcademicYr.Text = Convert.ToString(dr["VALUE"]);
                            txtFinAcademicYr.Text = Convert.ToString(dr["VALUE"]);
                        }

                        if (Convert.ToString(dr["KEY"].ToString()) == "ALLOW MODIFY COUNCELOR")
                        {
                            hdAlloModCouncellor.Value = "Y";

                            if (Convert.ToString(dr["VALUE"]) == "N")
                            {
                                hdAlloModCouncellor.Value = "N";
                            }
                        }
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                con = null;
                cmd = null;
                dr = null;
            }
        }
        private void bindCourse()
        {
            clsCommonUtility objclsCommonUtility = new clsCommonUtility();
            ObjMaster master = new ObjMaster();
            DataTable dtInfo;
 
            try
            {
                master.Mode = "COURSE";
                master.SearchText = string.Empty ;
                master.ConnectionString = user.ConnectionString;
                dtInfo = objclsCommonUtility.ShowCourse(master);

                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        ddlCourseApplyingFor.DataSource = dtInfo;
                        ddlCourseApplyingFor.DataTextField = dtInfo.Columns["Course_Name"].ToString();
                        ddlCourseApplyingFor.DataValueField = dtInfo.Columns["COURSE_CODE"].ToString();
                        ddlCourseApplyingFor.DataBind();

                        ddlFinCourseApplyingFor.DataSource = dtInfo;
                        ddlFinCourseApplyingFor.DataTextField = dtInfo.Columns["Course_Name"].ToString();
                        ddlFinCourseApplyingFor.DataValueField = dtInfo.Columns["COURSE_CODE"].ToString();
                        ddlFinCourseApplyingFor.DataBind();

                        //Set All
                        ddlFinCourseApplyingFor.Items.Insert(0, new ListItem("Select", "Auto Generate Course code"));
                        ddlCourseApplyingFor.Items.Insert(0, new ListItem("Select", "Auto Generate Course code"));
                    }
                    else
                    {
                        //Set All
                        ddlFinCourseApplyingFor.Items.Insert(0, new ListItem("Select", "Auto Generate Course code"));
                        ddlCourseApplyingFor.Items.Insert(0, new ListItem("Select", "Auto Generate Course code"));
                    }

                    if (ddlCourseApplyingFor.Items.Count > 0)
                    {
                        ddlCourseApplyingFor.SelectedValue = "0";
                        ddlFinCourseApplyingFor.SelectedValue = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objclsCommonUtility = null;
                dtInfo = null;
                master = null;
            }
        }

        private void PopulateControl(ObjStudentRegistrationInfo objRegistrationUser)
        {
            try
            {
                //if (objRegistrationUser.RegistrationDate != "")
                //    lblRegDate.Text = "Registration Date :" + objRegistrationUser.RegistrationDate;

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

                ddlFinCourseApplyingFor.SelectedValue = objRegistrationUser.NameOfCourseApplyingFor_fin;
                txtFinCourseCode.Text = objRegistrationUser.CourseCode_fin;
                txtFinAcademicYr.Text = objRegistrationUser.AcedemicYear_fin;

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
                txtWorkExperience.Text = objRegistrationUser.DetailsWorkExperience_Text;
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
                chkInforationGotFrom_Direct.Checked = objRegistrationUser.InforationGotFrom_Direct;
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
                ddlAdditinalSubject.SelectedValue = objRegistrationUser.OptedAdditionalSubject;

                if (Request.QueryString.Keys.Count != 0 && Convert.ToString(Request.QueryString["Confirm"]) == "true")
                {
                    if (objRegistrationUser.ConfirmedExamCenter != null && objRegistrationUser.ConfirmedExamCenter != "")
                    {
                        //--for Choosen centers
                        for (int intLoop = 0; intLoop <= chkListCenters.Items.Count - 1; intLoop++)
                            if (chkListCenters.Items[intLoop].Text.Trim().ToUpper() == objRegistrationUser.ConfirmedExamCenter.Trim().ToUpper())
                            {
                                chkListCenters.Items[intLoop].Selected = true;
                                chkListCenters.Enabled = false;
                                break;
                            }
                    }
                }
                else
                {
                    if (objRegistrationUser.ChosenCenters != null && objRegistrationUser.ChosenCenters != "")
                    {
                        string[] strChoosencenters = objRegistrationUser.ChosenCenters.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                        //--for Choosen centers
                        for (int intLoop = 0; intLoop <= chkListCenters.Items.Count - 1; intLoop++)
                            foreach (string s in strChoosencenters)
                                if (chkListCenters.Items[intLoop].Text.Trim().ToUpper() == s.Trim().ToUpper())
                                    chkListCenters.Items[intLoop].Selected = true;
                    }
                }

                hdPhoto.Value = objRegistrationUser.ImageName;
                hdSignature.Value = objRegistrationUser.SignatureName;

                //For Councellor & Consultant
                if (objRegistrationUser.CouncellorID != "0")
                {
                    ddlCouncellor.SelectedValue = objRegistrationUser.CouncellorID;
                    chkCouncellor.Checked = true;
                }

                if (objRegistrationUser.ConsultantName.Trim() != "")
                {
                    txtConsultantName.Text = objRegistrationUser.ConsultantName.Trim();
                    chkConsultant.Checked = true;
                }

                if (objRegistrationUser.OnLineRegistrationNumber.Trim() != "")
                {
                    txtOnlineRegNo.Text = objRegistrationUser.OnLineRegistrationNumber.Trim();
                }

                //Check permission to change the Councellot/Consultant
                if (hdAlloModCouncellor.Value == "N" && (chkConsultant.Checked == true || chkCouncellor.Checked == true))
                {
                    ddlCouncellor.Enabled = false;
                    txtConsultantName.Enabled = false;
                    chkConsultant.Enabled = false;
                    chkCouncellor.Enabled = false;

                    if (chkCouncellor.Checked == true)
                        txtOnlineRegNo.Enabled = false;
                }

                //for Study Center

                if (objRegistrationUser.StudyCenter != null)
                {
                    if (rdoCenter1.Text == objRegistrationUser.StudyCenter)
                        rdoCenter1.Checked = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ObjStudentRegistrationInfo getUserObject()
        {

            ObjStudentRegistrationInfo objRegistrationUser = new ObjStudentRegistrationInfo();
            try
            {
                if (txtRegistrationNo.Text.Trim() != "" && btnSubmit.Text == "Update")
                {
                    objRegistrationUser.ID = ((ObjStudentRegistrationInfo)Session["SearchResult_UserRegistration"]).ID;
                    objRegistrationUser.RegistrationNumber = ((ObjStudentRegistrationInfo)Session["SearchResult_UserRegistration"]).RegistrationNumber;
                }

                objRegistrationUser.FirstName = txtFirstName.Text.Trim().ToUpper(); ;
                objRegistrationUser.MiddleName = txtMiddleName.Text.Trim().ToUpper(); ;
                objRegistrationUser.LastName = txtLastName.Text.Trim().ToUpper();
                objRegistrationUser.Gender = ddlGender.SelectedItem.Text;
                objRegistrationUser.DOB = dtDOB.Text;
                objRegistrationUser.Nationality = txtNationality.Text.Trim().ToUpper();
                objRegistrationUser.EducationStatus = ddlEducationStatus.SelectedItem.Text;
                objRegistrationUser.Caste = txtCaste.Text.Trim().ToUpper();
                objRegistrationUser.Category = ddlCategory.SelectedItem.Text;
                objRegistrationUser.Religion = txtReligion.Text.Trim().ToUpper();
                objRegistrationUser.MobileNumber = txtMobileNumber.Text.ToUpper();
                objRegistrationUser.AdmissionStatus = ddlAdmissionStatus.SelectedItem.Text;
                objRegistrationUser.ResidencePhone = txtResidencePh.Text;
                objRegistrationUser.Email = txtEmailAddress.Text.Trim().ToUpper();
                objRegistrationUser.CorrespondenceAddress = txtCorrAddress.Text.Trim().ToUpper();
                objRegistrationUser.PinCode = txtPinCode.Text;
                objRegistrationUser.PermanentAddress = txtParmAddress.Text.Trim().ToUpper();
                objRegistrationUser.ParAddressPinCode = txtParmAddressPinCode.Text;
                objRegistrationUser.PassportNumber = txtPassportNumber.Text.Trim().ToUpper();
                objRegistrationUser.PassportIssuedAt = txtPassportIssuedAt.Text.Trim().ToUpper();
                objRegistrationUser.CountriesTraveled = txtCountriesTraveled.Text.Trim().ToUpper();
                objRegistrationUser.FatherName = txtFatherName.Text.Trim().ToUpper();
                objRegistrationUser.MotherName = txtMotherName.Text.Trim().ToUpper();
                objRegistrationUser.FatherMobile = txtFatherMobile.Text.Trim().ToUpper();
                objRegistrationUser.MotherMobile = txtMotherMobile.Text.Trim().ToUpper();
                objRegistrationUser.FatherEmail = txtFatherEmail.Text.Trim().ToUpper();
                objRegistrationUser.MotherEmail = txtMotherEmail.Text.Trim().ToUpper();
                objRegistrationUser.Address = txtAddress.Text.Trim().ToUpper();
                objRegistrationUser.NameOfCourseApplyingFor = ddlCourseApplyingFor.Text.ToString();
                objRegistrationUser.CourseCode = ddlCourseApplyingFor.SelectedValue;
                objRegistrationUser.AcedemicYear = txtAcademicYr.Text;

                objRegistrationUser.NameOfCourseApplyingFor_fin = ddlCourseApplyingFor.Text.ToString();
                objRegistrationUser.CourseCode_fin = ddlFinCourseApplyingFor.SelectedValue;
                objRegistrationUser.AcedemicYear_fin = txtFinAcademicYr.Text;

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
                objRegistrationUser.ReasonForApply = txtReasonForApply.Text.Trim().ToUpper();
                objRegistrationUser.EduQualificationClassX_RollNo = EduQualificationClassX_RollNo.Text.Trim().ToUpper();
                objRegistrationUser.EduQualificationClassX_Year = EduQualificationClassX_Year.Text;
                objRegistrationUser.EduQualificationClassX_Stream = EduQualificationClassX_Stream.Text;
                objRegistrationUser.EduQualificationClassX_Board = EduQualificationClassX_Board.Text.Trim().ToUpper();
                objRegistrationUser.EduQualificationClassX_Marks = EduQualificationClassX_Marks.Text;
                objRegistrationUser.EduQualificationClassX_MaxMarks = EduQualificationClassX_MaxMarks.Text;
                objRegistrationUser.EduQualificationClassX_Percent = EduQualificationClassX_Percent.Text;
                objRegistrationUser.EduQualificationClassX_Result = ddlEduQualificationClassX_Result.SelectedItem.Text;

                objRegistrationUser.EduQualificationClassXII_RollNo = EduQualificationClassXII_RollNo.Text.Trim().ToUpper();
                objRegistrationUser.EduQualificationClassXII_Year = EduQualificationClassXII_Year.Text;
                objRegistrationUser.EduQualificationClassXII_Stream = EduQualificationClassXII_Stream.Text;
                objRegistrationUser.EduQualificationClassXII_Board = EduQualificationClassXII_Board.Text.Trim().ToUpper();
                objRegistrationUser.EduQualificationClassXII_Marks = EduQualificationClassXII_Marks.Text;
                objRegistrationUser.EduQualificationClassXII_MaxMarks = EduQualificationClassXII_MaxMarks.Text;
                objRegistrationUser.EduQualificationClassXII_Percent = EduQualificationClassXII_Percent.Text;
                objRegistrationUser.EduQualificationClassXII_Result = ddlEduQualificationClassXII_Result.SelectedItem.Text;
                objRegistrationUser.EduQualificationClassGraduation_RollNo = EduQualificationClassGraduation_RollNo.Text;
                objRegistrationUser.EduQualificationClassGraduation_Year = EduQualificationClassGraduation_Year.Text;
                objRegistrationUser.EduQualificationClassGraduation_Stream = EduQualificationClassGraduation_Stream.Text;
                objRegistrationUser.EduQualificationClassGraduation_Board = EduQualificationClassGraduation_Board.Text;
                objRegistrationUser.EduQualificationClassGraduation_Marks = EduQualificationClassGraduation_Marks.Text;
                objRegistrationUser.EduQualificationClassGraduation_MaxMarks = EduQualificationClassGraduation_MaxMarks.Text;
                objRegistrationUser.EduQualificationClassGraduation_Percent = EduQualificationClassGraduation_Percent.Text;
                objRegistrationUser.EduQualificationClassGraduation_Result = ddlEduQualificationClassGraduation_Result.SelectedItem.Text;

                objRegistrationUser.EduQualificationClassOther_RollNo = EduQualificationClassOther_RollNo.Text.Trim().ToUpper();
                objRegistrationUser.EduQualificationClassOther_Year = EduQualificationClassOther_Year.Text;
                objRegistrationUser.EduQualificationClassOther_Stream = EduQualificationClassOther_Stream.Text;
                objRegistrationUser.EduQualificationClassOther_Board = EduQualificationClassOther_Board.Text.Trim().ToUpper();
                objRegistrationUser.EduQualificationClassOther_Marks = EduQualificationClassOther_Marks.Text;
                objRegistrationUser.EduQualificationClassOther_MaxOtherMarks = EduQualificationClassOther_MaxMarks.Text;
                objRegistrationUser.EduQualificationClassOther_Percent = EduQualificationClassOther_Percent.Text;
                objRegistrationUser.EduQualificationClassOther_Result = ddlEduQualificationClassOther_Result.SelectedItem.Text;
                objRegistrationUser.Profession_OtherQualificationAwards_Text = txtProfession_OtherQualificationAwards_Text.Text;

                objRegistrationUser.DetailsWorkExperience_Text = txtWorkExperience.Text;
                objRegistrationUser.HobbiesAndInterest_Text = txtHobbies.Text.Trim().ToUpper();
                objRegistrationUser.MaritalSatus = rdoMaritalStatus.SelectedItem.Text;
                objRegistrationUser.NameOfSpouse = txtNameOfSpouse.Text.Trim().ToUpper();
                objRegistrationUser.BloodGroup = txtBloodGroup.Text;
                objRegistrationUser.IsPhysicalDisable = iif(rdoIsPhysicalDisable.SelectedValue) ? true : false;
                objRegistrationUser.DetailsPhysicalDisable_Text = txtDetailsPhysicalDisable_Text.Text;
                objRegistrationUser.InforationGotFrom_Newspaper = chkInforationGotFrom_Newspaper.Checked ? true : false;
                objRegistrationUser.InforationGotFrom_Hoarding = chkInforationGotFrom_Hoarding.Checked ? true : false;
                objRegistrationUser.InforationGotFrom_Internet = chkInforationGotFrom_Internet.Checked ? true : false;
                objRegistrationUser.InforationGotFrom_Representative = chkInforationGotFrom_Representative.Checked ? true : false;
                objRegistrationUser.InforationGotFrom_Direct = chkInforationGotFrom_Direct.Checked ? true : false;
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
                objRegistrationUser.IsDiplomaStudent = 0;
                objRegistrationUser.OptedAdditionalSubject = ddlAdditinalSubject.SelectedValue;
                objRegistrationUser.OnLineRegistrationNumber = txtOnlineRegNo.Text.Trim();

                objRegistrationUser.ConsultantName = "";
                objRegistrationUser.CouncellorID = "0";

                if (chkConsultant.Checked == true)
                {
                    objRegistrationUser.ConsultantName = txtConsultantName.Text.Trim();
                }

                if (chkCouncellor.Checked == true)
                {
                    objRegistrationUser.CouncellorID = ddlCouncellor.SelectedValue;
                }

                if (chkDiplomaCheck.Checked) objRegistrationUser.IsDiplomaStudent = 1;

                //--for Choosen centers
                for (int intLoop = 0; intLoop <= chkListCenters.Items.Count - 1; intLoop++)
                {
                    if (chkListCenters.Items[intLoop].Selected)
                    {
                        objRegistrationUser.ChosenCenters += chkListCenters.Items[intLoop].Text + ",";
                    }
                }

                //for Studey Center
                if (rdoCenter1.Checked == true)
                {
                    objRegistrationUser.StudyCenter = rdoCenter1.Text;
                }

                objRegistrationUser.ImageName = hdPhoto.Value;
                objRegistrationUser.SignatureName = hdSignature.Value;
                objRegistrationUser.ConnectionString = user.ConnectionString ;


                return objRegistrationUser;
            }
            catch (Exception ex)
            {
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
            {
                throw ex;
            }
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
            {
                throw ex;
            }
            return iSelectedValue;
        }
        private bool iif(string p)
        {
            if (p == "1") { return true; } else { return false; }
        }
        #endregion

         
}
 